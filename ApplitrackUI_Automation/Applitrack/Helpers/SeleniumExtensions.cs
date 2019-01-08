using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Threading;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using Automation.Framework.helpers;
using Automation.Helpers;
using System.Reflection;

namespace ApplitrackUITests.Helpers
{
    public static class SeleniumExtensions
    {
        /// <summary>
        /// Take and save a screenshot to the path specified in the config file
        /// </summary>
        /// <param name="driver">The web browser</param>
        /// <param name="testCaseName">The name of the test case to be used when writing the file</param>
        /// <returns>The path of the screenshot</returns>
        public static string TakeAndSaveScreenshot(this IWebDriver driver, string testCaseName)
        {
            Logging.Information("Screen Capture Routine Started");
            try
            {
                var folderName = $"Run{DateTime.Now:MMddyy}";
                var timeStamp = DateTime.Now.ToString("hhmmss");
                var fileName = $"{testCaseName}-{timeStamp}.jpg";
                var rootFolder = FileHelpers.GetResultsDir(BaseFrameWork._grid, BaseFrameWork._GridResultsDir, BaseFrameWork._LocalResultsDir);
                var folderPath = Path.Combine(rootFolder, folderName);
                var fullPath = Path.Combine(folderPath, fileName);

                Logging.Information($"Attempting to save screenshot to: {fullPath}");

                if (Directory.Exists(folderPath))
                {
                    Logging.Information($"Screenshot folder exists, re-using: {folderPath}");
                }
                else
                {
                    Logging.Information($"Screenshot folder does not exist: creating new folder: {folderPath}");
                    Directory.CreateDirectory(folderPath);
                }

                driver.TakeScreenshot().SaveAsFile(fullPath, ScreenshotImageFormat.Jpeg);

                Logging.Information($"Screenshot Success. Saved to: {fullPath}");

                return fullPath;
            }
            catch (Exception e)
            {
                Logging.Information($"!!! ScreenShot Capture Failed -- {e.Message}:::{e.InnerException}");
            }
            return null;
        }

        /// <summary>
        /// Get the parent element of the indicated element
        /// </summary>
        /// <param name="element">The element to get the parent of</param>
        /// <returns>The parent element</returns>
        public static IWebElement GetParentElement(this ISearchContext element)
        {
            return element.FindElement(By.XPath("./.."));
        }

        /// <summary>
        /// Switch to an iframe using an ID
        /// </summary>
        /// <param name="driver">The selenium webdriver</param>
        /// <param name="iframeId">The ID of the iframe to switch to</param>
        public static void SwitchToFrameById(this IWebDriver driver, string iframeId)
        {
            driver.Wait().Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.Id(iframeId)));
        }

        /// <summary>
        /// Switch to an iframe using the class name
        /// </summary>
        /// <param name="driver">The selenium webdriver</param>
        /// <param name="iframeClass">The class of the IFrame to switch to</param>
        public static void SwitchToFrameByClass(this IWebDriver driver, string iframeClass)
        {
            driver.Wait().Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.ClassName(iframeClass)));
        }

        /// <summary>
        /// Switch to the default frame
        /// </summary>
        public static void SwitchToDefaultFrame(this IWebDriver driver)
        {
            driver.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="waitTime"></param>
        /// <returns></returns>
        public static WebDriverWait Wait(this IWebDriver driver, Int32 waitTime = 0)
        {
            waitTime = (waitTime == 0) ?
                Convert.ToInt32(ConfigurationManager.AppSettings["Def_WaitTimeOutSecs"]) : waitTime;
            return new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime));
        }

        /// <summary>
        /// Wait for a web element to be available to interact with
        /// </summary>
        /// <param name="element">The element to interact with</param>
        /// <param name="driver">The selenium webdriver</param>
        /// <param name="maxRetries">Maximum amount of times to try to find the element</param>
        /// <param name="sleepSeconds">Time (in seconds) to sleep on failure to find the element</param>
        /// <returns>The web element to interact with</returns>
        public static IWebElement WaitRetry(this IWebElement element, IWebDriver driver, int maxRetries = 10, double sleepSeconds = 1)
        {
            var elementFound = false;
            var currentRetry = 0;

            while (!elementFound && currentRetry < maxRetries)
            {
                try
                {
                    element.WaitForElement(driver);
                    elementFound = true;
                }
                // catch the exceptions that Selenium throws when interacting or getting the state of an element
                catch (Exception e) when (e is NoSuchElementException || e is StaleElementReferenceException || e is TargetInvocationException)
                {
                    if (currentRetry++ > maxRetries)
                    {
                        throw;
                    }
                    // sleep because interacting with things too quickly can cause failures
                    Thread.Sleep(TimeSpan.FromSeconds(sleepSeconds));
                }
            }

            return element;
        }

        /// <summary>
        /// Return a collection of the text in a list of web elements
        /// </summary>
        /// <param name="elements">A list of web elements</param>
        /// <returns>A string collection of the text of the elements</returns>
        public static StringCollection GetElementsText(this IList<IWebElement> elements)
        {
            var itemText = new StringCollection();
            foreach (var item in elements)
            {
                itemText.Add(item.Text);
            }
            return itemText;
        }
    }
}
