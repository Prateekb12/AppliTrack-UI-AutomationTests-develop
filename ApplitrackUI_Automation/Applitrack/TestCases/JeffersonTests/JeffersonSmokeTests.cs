using System;
using System.Threading;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide;
using ApplitrackUITests.PageObjects.Toolbar;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;
using ApplitrackUITests.PageObjects.Jefferson;

namespace ApplitrackUITests.TestCases.JeffersonTests
{
    [TestClass]
    public class JeffersonSmokeTests : ApplitrackUIBase
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

        [TestMethod]
        [TestCategory("JeffersonSmoke")]
        [TestProperty("TestArea", "Login")]
        [TestProperty("TestCaseName", "Federated Login Page Displayed")]
        [TestProperty("TestCaseDescription", "Verify that the federated login page is displayed")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Federated_Login_Displayed()
        {
            var loginPage = new FederatedLoginPage(Driver);
            
            try  //Contains Contents of Test
            {
                Assert.IsTrue(loginPage.IsDisplayed(), "Login page did not load");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("JeffersonSmoke")]
        [TestProperty("TestArea", "Login")]
        [TestProperty("TestCaseName", "Verify Login Functionality")]
        [TestProperty("TestCaseDescription", "Verify Login Functionality")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Federated_Login_with_Valid_Credentials()
        {
            var loginPage = new FederatedLoginPage(Driver);
            var toolbar = ToolbarFactory.Get(Driver);
            var appSelectorPage = new AppSelectorPage(Driver);
            
            try  //Contains Contents of Test
            {
                loginPage.Login(LoginData.SuperUserName, LoginData.SuperUserPassword); 

                appSelectorPage.SelectApp(@"Recruiting & Hiring");

                // the BaseFramework waits are not enough to stop this from failing...
                Thread.Sleep(TimeSpan.FromSeconds(3));

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
        [TestCategory("JeffersonSmoke")]
        [TestProperty("TestArea", "Login")]
        [TestProperty("TestCaseName", "Verify Logout Functionality")]
        [TestProperty("TestCaseDescription", "Verify Logout Functionality")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Federated_Logout()
        {
            var loginPage = new FederatedLoginPage(Driver);
            var toolbar = ToolbarFactory.Get(Driver);
            var appSelectorPage = new AppSelectorPage(Driver);

            // In order to fully logout of a system using the federated login, this URL must be hit
            var federatedLogoutUrl = "https://federateoidcawsstage.flqa.net/core/logout";

            try  //Contains Contents of Test
            {
                loginPage.Login(LoginData.SuperUserName, LoginData.SuperUserPassword);

                appSelectorPage.SelectApp(@"Recruiting & Hiring");

                // the BaseFramework waits are not enough to stop this from failing...
                Thread.Sleep(TimeSpan.FromSeconds(3));

                // Logout
                toolbar.Logout();
                BrowseTo(federatedLogoutUrl, Driver);

                // Navigate back to the login page, check that the login page is displayed
                BrowseTo(BaseUrls["ApplitrackLoginPage"], Driver);
                Assert.IsTrue(loginPage.IsDisplayed(), "Logout failed");
                test.Log(LogStatus.Pass, "Logout was successful");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("JeffersonSmoke")]
        [TestProperty("TestArea", "App Switcher")]
        [TestProperty("TestCaseName", "Verify App Switcher to Absence")]
        [TestProperty("TestCaseDescription", "Verify that the App Switcher correctly switches to Absence Management")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Jefferson_Switch_to_Absence()
        {
            var loginPage = new FederatedLoginPage(Driver);
            var toolbar = new SidekickToolbar(Driver);
            var appSelectorPage = new AppSelectorPage(Driver);

            const string absenceMgmtName = "Absence Management";

            try  //Contains Contents of Test
            {
                // login as a user with access to multiple applications
                loginPage.Login(LoginData.SuperUserName, LoginData.SuperUserPassword);
                test.Log(LogStatus.Pass, $"Login using {LoginData.SuperUserName} / {LoginData.SuperUserPassword}");

                appSelectorPage.SelectApp(@"Recruiting & Hiring");
                test.Log(LogStatus.Pass, "Select Recruiting & Hiring from the selection screen");

                // the BaseFramework waits are not enough to stop this from failing...
                Thread.Sleep(TimeSpan.FromSeconds(3));

                toolbar.SwitchApps(absenceMgmtName);

                Assert.AreEqual(toolbar.ApplicationTitle, absenceMgmtName, "Sidekick does not contain the correct application name");
                test.Log(LogStatus.Pass, $"Successfully switch to {absenceMgmtName}");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("JeffersonSmoke")]
        [TestProperty("TestArea", "App Switcher")]
        [TestProperty("TestCaseName", "Verify App Switcher to PG")]
        [TestProperty("TestCaseDescription", "Verify that the App Switcher correctly switches to Professional Growth")]
        [TestProperty("UsesHardcodedData", "false")]
        [Ignore] // PG install does not have sidekick enabled
        public void Jefferson_Switch_to_PG()
        {
            var loginPage = new FederatedLoginPage(Driver);
            var toolbar = new SidekickToolbar(Driver);
            var appSelectorPage = new AppSelectorPage(Driver);

            const string pgName = "Professional Growth";

            // Creating a new user here because the user in JeffersonLoginData does not have access to other applications.
            var testUser = new UserGenerator
            {
                UserName = "testuser1",
                Password = "q1234567"
            };

            try  //Contains Contents of Test
            {
                // login as a user with access to multiple applications
                loginPage.Login(testUser.UserName, testUser.Password);
                test.Log(LogStatus.Pass, $"Login using {testUser.UserName} / {testUser.Password}");

                appSelectorPage.SelectApp(@"Recruiting & Hiring");
                test.Log(LogStatus.Pass, "Select Recruiting & Hiring from the selection screen");

                // the BaseFramework waits are not enough to stop this from failing...
                Thread.Sleep(TimeSpan.FromSeconds(3));

                toolbar.SwitchApps(pgName);

                Assert.AreEqual(toolbar.ApplicationTitle, pgName, "Sidekick does not contain the correct application name");
                test.Log(LogStatus.Pass, $"Successfully switch to {pgName}");
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
