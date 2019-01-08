using System;
using System.Configuration;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide;
using ApplitrackUITests.PageObjects.Toolbar;
using ApplitrackUITests.WorkFlows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.LoginTests
{
    [TestClass]
    public class LoginSmokeTests : ApplitrackUIBase
    {
        #region Setup and TearDown

        private IWebDriver Driver;
        private ExtentTest test;
        private readonly bool _idmEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["IDMEnabled"]);

        /// <summary>
        /// Test Initialize Contains items to run before each [TestMethod]
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            // extent reports setup
            test = ExtentTestManager.StartTest(TestContext.Properties["TestCaseName"].ToString(),
                TestContext.Properties["TestCaseDescription"].ToString())
                .AssignCategory("Smoke");

            // browser setup
            Driver = SetUp(_BT);
            Driver.Manage().Window.Maximize();

            // navigate to the login page
            BrowseTo(BaseUrls["ApplitrackLoginPage"], Driver);

            test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);
        }

        /// <summary>
        /// Test Teardown contains items to run after each [TestMethod]
        /// </summary>
        [TestCleanup]
        public void TestTearDown()
        {
            BaseTearDown(Driver);
        }

        #endregion

        #region Test Cases

        // TODO refactor this once logout workflow is finalized
        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Login")]
        [TestProperty("TestCaseName", "Verify Logout Functionality")]
        [TestProperty("TestCaseDescription", "Verify Logout Functionality")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Logout_Verify()
        {
            // page objects
            var loginPage = new LoginPage(Driver);
            var toolbar = ToolbarFactory.Get(Driver);

            // workflows
            var loginWorkflow = new LoginWorkflows(Driver);

            try  //Contains Contents of Test
            {
                //ScreenshotHelper.TakeScreenShot(TestContext.Properties["TestCaseName"].ToString(), Driver);
                loginWorkflow.LoginAsSuperUser();
                test.Log(LogStatus.Pass, "Login as a super user");

                // Logout
                toolbar.Logout();

                test.Log(LogStatus.Pass, "Log out");

                // Assert that the logout was successful
                if (_idmEnabled)
                {
                    Assert.IsTrue(loginPage.IsTextOnScreen(Driver, "You are signed out."));
                }
                else
                {
                    Assert.IsTrue(loginPage.IsTextOnScreen(Driver, "Enter Your UserID And Password To Begin"));
                }

                test.Log(LogStatus.Pass, "Log out was successful");

            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Login")]
        [TestProperty("TestCaseName", "Login with valid User ID and valid Password")]
        [TestProperty("TestCaseDescription", "Login with valid User ID and valid Password")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Login_with_Valid_UserID_and_Valid_PW()
        {
            // page objects
            var toolbar = ToolbarFactory.Get(Driver);

            // workflows
            var loginWorkflow = new LoginWorkflows(Driver);

            try  // Contents of the test
            {
                loginWorkflow.LoginAsSuperUser();

                // Assert that the login was successful
                Assert.IsTrue(toolbar.IsDisplayed(), "Login failed");
                test.Log(LogStatus.Pass, "Login was successful");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Login")]
        [TestProperty("TestCaseName", "Login with invalid User ID and invalid Password")]
        [TestProperty("TestCaseDescription", "Login with invalid User ID and invalid Password")]
        [TestProperty("UsesHardcodedData", "false")]
        [Ignore] // TODO figure out how to handle this test for both standard and IDM logins
        public void Login_with_Invalid_UserID_and_Invalid_PW()
        {
            // page objects
            var toolbar = ToolbarFactory.Get(Driver);

            // workflows
            var loginWorkflow = new LoginWorkflows(Driver);

            try  // Contents of the test
            {
                loginWorkflow.Login("INVALID", "INVALID");

                // Assert that the login failed
                Assert.IsFalse(toolbar.IsDisplayed(), "The invalid login was successful");
                test.Log(LogStatus.Pass, "Login was unsuccessful");
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
