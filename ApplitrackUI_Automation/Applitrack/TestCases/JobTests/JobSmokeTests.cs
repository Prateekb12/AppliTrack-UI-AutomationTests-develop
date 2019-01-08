using System;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using ApplitrackUITests.WorkFlows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.JobTests
{
    [TestClass]
    public class JobSmokeTests : ApplitrackUIBase
    {
        #region Setup and TearDown

        private IWebDriver _driver;
        private ExtentTest _test;

        [TestInitialize]
        public void TestSetup()
        {
            // extent reports setup
            _test = ExtentTestManager.StartTest(TestContext.Properties["TestCaseName"].ToString(),
                TestContext.Properties["TestCaseDescription"].ToString())
                .AssignCategory("Smoke");

            // browser setup
            _driver = SetUp(_BT);
            _driver.Manage().Window.Maximize();
            BrowseTo(BaseUrls["ApplitrackLoginPage"], _driver);

            _test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

            // login
            var loginWorkflow = new LoginWorkflows(_driver);
            loginWorkflow.LoginAsSuperUser();
        }

        /// <summary>
        /// Runs after each test
        /// </summary>
        [TestCleanup]
        public void TestTearDown()
        {
            BaseTearDown(_driver);
        }

        #endregion

        #region Test Cases

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "JobPostings")]
        [TestProperty("TestCaseName", "JobPostings Route")]
        [TestProperty("TestCaseDescription", "Validate that the &Destination=JobPostPage&Id= route opens the correct page")]
        [TestProperty("UsesHardcodedData", "false")]
        public void JobPostings_Route_EditJob()
        {
            // data
            var job = new JobPostingGenerator();

            // page objects
            var jobPages = new JobPostingsPages(_driver);

            try
            {
                job.CreateInDatabase();
                _test.Log(LogStatus.Info, $"Create job posting: {job.AdditionalTitle}");

                var jobUrl = $"{_driver.Url}?Destination=JobPostPage&Id={job.Id}";
                _driver.Navigate().GoToUrl(jobUrl);
                _test.Log(LogStatus.Info, $"Navigate to {jobUrl}");

                _driver.SwitchToFrameById("MainContentsIFrame");
                Assert.IsTrue(jobPages.EditAndCreateJobPostingPage.IsDisplayed(), "The Edit Job Posting page is not dispalyed");
                _test.Log(LogStatus.Pass, "The Edit Job Posting page is displayed");

                _driver.SwitchToFrameById("tabs_Panel");
                Assert.IsTrue(jobPages.EditAndCreateJobPostingPage.TitleFieldContains(job.AdditionalTitle), "The Title field does not contain the correct job title");
                _test.Log(LogStatus.Pass, "The job title in the Title field is correct");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
            finally
            {
                job.DeleteFromDatabase();
            }
        }

        #endregion
    }
}