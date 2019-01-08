using System;
using System.Configuration;
using ApplitrackUITests.Helpers;
using Automation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.PageObjects
{
    [TestClass]
    public class ApplitrackUIBase : BaseFrameWork 
    {
        private static ExtentReports _extent;

        /// <summary>
        /// Start the extent instance
        /// </summary>
        /// <param name="context"></param>
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            // extent setup
            _extent = ExtentSingleton.Instance;
            ExtentSetup.SetupExtentX();
        }

        /// <summary>
        /// Write the log after all tests have run
        /// </summary>
        [AssemblyCleanup]
        public static void AssemblyTearDown()
        {
            ExtentSetup.SetupConfigFile();
            ExtentSetup.CreateLogFolderIfNeeded();
            _extent.Flush(); // write the log
        }

        /// <summary>
        /// End the currently executing test
        /// </summary>
        [TestCleanup]
        public void LogTearDown()
        {
            ExtentTestManager.EndTest();
        }

        /// <summary>
        /// When an exception occurs during a test case, this logs the stacktrace.
        /// This should only be called in the catch statement of a test.
        /// </summary>
        /// <param name="ex">The exception that occured</param>
        protected static void ReportException(Exception ex)
        {
            var currentTest = ExtentTestManager.GetTest();
            var stacktrace =
                $"<br><br><b><i>Exception:</b></i><br>{ex.Message}<pre>{ex}</pre><b><i>Stacktrace:</b></i><pre>{Environment.StackTrace}</pre>";

            currentTest.Log(stacktrace.Contains("Assert") ? LogStatus.Fail : LogStatus.Error, stacktrace);
        }

        /// <summary>
        /// Handle and report any exceptions that occur during a test case.
        /// This should be called in the catch block of a test.
        /// </summary>
        /// <param name="e">The exception that occured</param>
        /// <param name="driver">The web browser</param>
        protected void HandleException(Exception e, IWebDriver driver)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["CaptureScreenshots"]))
            {
                driver.TakeAndSaveScreenshot(TestContext.Properties["TestCaseName"].ToString());
            }
            ReportException(e);
            HandleError(e, driver);
        }
    }
}