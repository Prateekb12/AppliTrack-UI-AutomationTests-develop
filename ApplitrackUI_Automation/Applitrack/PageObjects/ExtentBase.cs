using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.IO;
using System.Threading;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.PageObjects
{
    /// <summary>
    /// Manage Extent Reports for parallel execution
    /// </summary>
    public static class ExtentTestManager
    {
        // using ConcurrentDictionary as opposed to Dictionary to prevent concurrency issues
        private static readonly ConcurrentDictionary<string, ExtentTest> ExtentDictionary = new ConcurrentDictionary<string, ExtentTest>();
        private static readonly ExtentReports Extent = ExtentSingleton.Instance;

        /// <summary>
        /// Get the currently executing Extent test
        /// </summary>
        /// <returns>The extent test which is being executed</returns>
        public static ExtentTest GetTest()
        {
            lock(ExtentDictionary)
            {
                ExtentTest test;

                if (ExtentDictionary.TryGetValue(Thread.CurrentThread.ManagedThreadId.ToString(), out test))
                {
                    return test;
                }
                return null;
            }
        }

        /// <summary>
        /// End the currently executing Extent test
        /// </summary>
        public static void EndTest()
        {
            lock(ExtentDictionary)
            {
                try
                {
                    Extent.EndTest(ExtentDictionary[Thread.CurrentThread.ManagedThreadId.ToString()]);
                }
                catch (TimeoutException e)
                {
                    // make sure the test doesnt fail when it cant connect to the Extent X server
                    Console.Write(" !! WARNING !! Extent X failed to connect");
                    Console.WriteLine(e);
                }
            }
        }

        /// <summary>
        /// Start the extent test in a new thread
        /// </summary>
        /// <param name="testName">The name of the test</param>
        /// <returns>Starts the test</returns>
        public static ExtentTest StartTest(string testName)
        {
            lock(ExtentDictionary)
            {
                return StartTest(testName, "");
            }
        }

        /// <summary>
        /// Start the extent test in a new thread. Add the thread and the test in a key value pair,
        /// i.e. extentDictionary[threadId] = test
        /// </summary>
        /// <param name="testName">The name of the test</param>
        /// <param name="testDescription">The description of the test</param>
        /// <returns>Starts the test</returns>
        public static ExtentTest StartTest(string testName, string testDescription)
        {
            lock(ExtentDictionary)
            {
                var test = Extent.StartTest(testName, testDescription);
                ExtentDictionary.TryAdd(Thread.CurrentThread.ManagedThreadId.ToString(), test);
                return test;
            }
        }
    }

    public sealed class ExtentSingleton
    {
        // This class uses the Multithreaded Singleton pattern - https://msdn.microsoft.com/en-us/library/ff650316.aspx

        private static volatile ExtentReports _instance;
        private static object _syncRoot = new Object();

        public static ExtentReports Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new ExtentReports(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Files", ExtentSetup.ReportLocation), true);
                        }
                    }
                }
                return _instance;
            }
        }
    }

    internal static class ExtentSetup
    {
        // ExtentX variables
        private static readonly MongoDB.Driver.MongoUrl ExtentXMongoUrl = new MongoDB.Driver.MongoUrl(ConfigurationManager.AppSettings["ExtentXMongoUrl"]);
        private static readonly string ExtentXProject = ConfigurationManager.AppSettings["ExtentXProject"];

        // Extent Location Properties

        /// <summary>
        /// The folder where the Extent Report should be saved
        /// </summary>
        private static string ReportFolder { get; } = ConfigurationManager.AppSettings["ReportFolderLocation"];

        /// <summary>
        /// The name of the Extent Report file
        /// </summary>
        private static string ReportFileName { get; } = ConfigurationManager.AppSettings["ReportFileName"];

        /// <summary>
        /// The full path for the report, i.e. "C:\logs\TestResults.html"
        /// </summary>
        internal static string ReportLocation { get; } = Path.Combine(ReportFolder, ReportFileName);

        /// <summary>
        /// If the folder indicated in the "ReportFolderLocation" configuration setting doesn't exist, create it.
        /// </summary>
        internal static void CreateLogFolderIfNeeded()
        {
            if (!Directory.Exists(ReportFolder))
            {
                Directory.CreateDirectory(ReportFolder);
            }
        }

        /// <summary>
        /// Set up the Extent X server if "usingExtentX" is set to "true" in the config file
        /// </summary>
        internal static void SetupExtentX()
        {
            var usingExtentX = Convert.ToBoolean(ConfigurationManager.AppSettings["usingExtentX"]);
            if (!usingExtentX) return;
            ExtentSingleton.Instance.AssignProject(ExtentXProject);
            ExtentSingleton.Instance.X(ExtentXMongoUrl);
        }

        /// <summary>
        /// Use the extent-config.xml file if it exists
        /// </summary>
        internal static void SetupConfigFile()
        {
            var configDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            if (configDirectory != null)
            {
                ExtentSingleton.Instance.LoadConfig(configDirectory.FullName + @"\extent-config.xml");
            }
        }
    }
}