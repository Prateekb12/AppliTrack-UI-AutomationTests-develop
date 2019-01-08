using System;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide;
using ApplitrackUITests.WorkFlows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.SHPTests
{
    [TestClass]
    public class SHPSmokeTests : ApplitrackUIBase
    {
        #region Setup and TearDown
        private IWebDriver Driver;
        private ExtentTest test;

        [TestInitialize]
        public void TestSetup()
        {
            // extent reports setup
            test = ExtentTestManager.StartTest(TestContext.Properties["TestCaseName"].ToString(),
                TestContext.Properties["TestCaseDescription"].ToString())
                .AssignCategory("Regression");

            // browser setup
            Driver = SetUp(_BT);
            Driver.Manage().Window.Maximize();
            BrowseTo(BaseUrls["ApplitrackLoginPage"], Driver);

            test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

            // login
            var loginWorkflow = new LoginWorkflows(Driver);
            loginWorkflow.LoginAsSuperUser();
        }

        /// <summary>
        /// Runs after each test
        /// </summary>
        [TestCleanup]
        public void TestTearDown()
        {
            BaseTearDown(Driver);
        }

        #endregion

        #region Test Cases

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "")]
        [TestProperty("TestCaseName", "SHP Install")]
        [TestProperty("TestCaseDescription", "Stronge Hiring Protocol Install")]
        [TestProperty("UsesHardcodedData", "true")]
        public void SHP_Install()
        {
            // page objects
            var shpInstallPage = new StrongeInstallPage(Driver);

            // the URL for the SHP installer
            var installUrl = BaseUrls["ApplitrackLoginPage"] + "/onlineapp/admin/store/install-stronge.aspx";

            try //Contains Contents of Test
            {
                // Navigate to install page
                BrowseTo(installUrl, Driver);
                test.Log(LogStatus.Info, "Navigating to: " + installUrl);

                // Enter value into 'Your name' field
                shpInstallPage.EnterYourName("Automated Tester");

                // Click the checkbox
                shpInstallPage.MarkTosBox();

                // Click Install
                shpInstallPage.ClickInstallButton();

                // Assert that the screen indicates install was successfull
                Assert.IsTrue(shpInstallPage.InstallSucceeded(), "The SHP install failed");
                test.Log(LogStatus.Pass, "The SHP install succeeded");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        #endregion
    }
}
