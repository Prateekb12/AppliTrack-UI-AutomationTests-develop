using System;
using System.Collections.Specialized;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using ApplitrackUITests.PageObjects.AdminSide.MyAccount;
using ApplitrackUITests.WorkFlows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;


namespace ApplitrackUITests.TestCases.MyAccountTests
{
    [TestClass]
    public class UserPreferencesRegressionTests : ApplitrackUIBase
    {
        #region Setup and TearDown

        private IWebDriver Driver;
        private ExtentTest test;

        /// <summary>
        /// Test Initialize Contains items to run before each [TestMethod]
        /// </summary>
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

        [TestCleanup]
        public void TestTearDown()
        {
            BaseTearDown(Driver);
        }

        #endregion

        #region Test Cases

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "UserSettings")]
        [TestProperty("TestCaseName", "UserSettings Only Applicant Profile and Employee Profile Settings are Displayed")]
        [TestProperty("TestCaseDescription", "Make sure that the only preferences available in My Account > User Preferences are Applicant Profile and Employee Profile")]
        [TestProperty("UsesHardcodedData", "false")]
        public void UserSettings_Only_ApplicantProfile_and_EmployeeProfile_Settings_Displayed()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            // pages
            var mainMenu = new MainMenu(Driver);
            var myAccountMenu = new SubMenuMyAccount(Driver);
            var myAccountPages = new MyAccountPages(Driver);

            // workflows

            // data
            var expectedPreferences = new StringCollection
            {
                "Applicant Profile",
                "Employee Profile"
            };

            try  //Contains Contents of Test
            {
                //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

                // Navigate to My Account > User Preferences
                mainMenu.ClickMyAccount();
                myAccountMenu.ClickUserPreferences();
                test.Log(LogStatus.Pass, "Navigate to My Account > User Preferences");

                // Assert that the only two options available are 'Applicant Profile' and 'Employee Profile'
                Driver.SwitchToFrameById("MainContentsIFrame");
                var actualPreferences = myAccountPages.UserPreferencesPage.GetActualPreferences();
                CollectionAssert.AreEqual(expectedPreferences, actualPreferences,
                    "The User Preferences do not match: " + expectedPreferences[0] + " and " + expectedPreferences[1]);
                test.Log(LogStatus.Pass, "The User Preferences match: " + expectedPreferences[0] + " and " + expectedPreferences[1]);
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
