using System;
using System.Collections.Specialized;
using ApplitrackUITests.DataAccess;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using ApplitrackUITests.PageObjects.AdminSide.Users;
using ApplitrackUITests.WorkFlows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Recruit.Persistence.Models;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.UserTests
{
    [TestClass]
    public class UserSmokeTests : ApplitrackUIBase
    {

        #region Setup and TearDown

        private IWebDriver _driver;
        private ExtentTest _test;

        /// <summary>
        /// Test Initialize Contains items to run before each [TestMethod]
        /// </summary>
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
        [TestProperty("TestArea", "User")]
        [TestProperty("TestCaseName", "User Creation")]
        [TestProperty("TestCaseDescription", "Validate User Creation")]
        [TestProperty("UsesHardcodedData", "false")]
        public void User_Create_New()
        {
            // data
            var userData = new UserGenerator();

            // page objects
            var mainMenu = new MainMenu(_driver);
            var userMenu = new SubMenuUsers(_driver);
            var userPages = new UserPages(_driver);

            // helpers
            var userDataAccessor = new UserDataAccessor();

            try
            {
                mainMenu.ClickUsers();
                userMenu.ClickCreateNewUser();
                _test.Log(LogStatus.Info, "Navigate to Users > Create a new user");

                _driver.SwitchToFrameById("MainContentsIFrame");
                Assert.IsTrue(userPages.CreateNewUserPage.IsDisplayed(),
                    "The 'Create a new user' page is not displayed");
                _test.Log(LogStatus.Pass, "The 'Create a new user' page is displayed");

                // enter user info
                _driver.SwitchToFrameById("tabs_Panel");

                userPages.CreateNewUserPage.EnterShortName(userData.UserName);
                _test.Log(LogStatus.Info, "Enter username: " + userData.UserName);

                userPages.CreateNewUserPage.EnterRealName(userData.RealName);
                _test.Log(LogStatus.Info, "Enter real name: " + userData.RealName);

                userPages.CreateNewUserPage.EnterEmail(userData.Email);
                _test.Log(LogStatus.Info, "Enter user email: " + userData.Email);

                //save
                _driver.SwitchToDefaultFrame();
                _driver.SwitchToFrameById("MainContentsIFrame");
                userPages.CreateNewUserPage.ClickSaveButton();
                userPages.CreateNewUserPage.ClickSaveAndCloseButton();
                _test.Log(LogStatus.Info, "Saving user");

                Assert.IsInstanceOfType(userDataAccessor.GetUser(userData.UserName), typeof(RecruitUser),
                    "The user was not created in the database");
                _test.Log(LogStatus.Pass, "User was created in the database");

                Assert.IsTrue(userPages.ListUsersPage.IsDisplayed(), "The 'List all users' page is not displayed");
                _test.Log(LogStatus.Pass, "The 'List all users' page should be displayed after clicking Save and Close");

                // set the ID in order find the user on the 'List all users' page and to delete the user
                userData.Id = userDataAccessor.GetUser(userData.UserName).Id;
                Assert.IsTrue(userPages.ListUsersPage.ItemFound(userData.Id.ToString()),
                    "The user is listed on the List all users page");
                _test.Log(LogStatus.Pass, "User is listed on the List all users page");

                userData.DeleteFromDatabase();
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "User")]
        [TestProperty("TestCaseName", "User Deletion")]
        [TestProperty("TestCaseDescription", "Validate User Deletion")]
        [TestProperty("UsesHardcodedData", "false")]
        public void User_Delete()
        {
            // data
            var userData = new UserGenerator();

            // page objects
            var mainMenu = new MainMenu(_driver);
            var userMenu = new SubMenuUsers(_driver);
            var userPages = new UserPages(_driver);

            try
            {
                _test.Log(LogStatus.Info, "Creating user");
                userData.CreateInDatabase();

                // delete the user
                mainMenu.ClickUsers();
                userMenu.ClickUsersList();
                _driver.SwitchToFrameById("MainContentsIFrame");
                userPages.ListUsersPage.MarkItem(userData.Id.ToString());
                _test.Log(LogStatus.Info, "Select user");

                userPages.ListUsersPage.ClickDeleteUser();
                _test.Log(LogStatus.Info, "Click delete");

                userPages.ListUsersPage.ConfirmDeletion();
                _test.Log(LogStatus.Info, "Click 'Yes' to delete the user");

                // Navigate back to the List all users page
                // This is to get around a bug in the system which causes the deleted user to be displayed until page is refreshed
                mainMenu.ClickMainMenuTab();
                mainMenu.ClickUsers();
                userMenu.ClickUsersList();
                _driver.SwitchToFrameById("MainContentsIFrame");
                Assert.IsFalse(userPages.ListUsersPage.ItemFound(userData.Id.ToString()), "User was not deleted");
                _test.Log(LogStatus.Pass, "The user was deleted successfully");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "User")]
        [TestProperty("TestCaseName", "EditUser Route")]
        [TestProperty("TestCaseDescription", "Validate that the &Destination=UsersPage&Id= route opens the correct page")]
        [TestProperty("UsesHardcodedData", "false")]
        public void User_Route_EditUser()
        {
            // data
            var userData = new UserGenerator();

            // page objects
            var userPages = new UserPages(_driver);

            try
            {
                _test.Log(LogStatus.Info, "Creating user");
                userData.CreateInDatabase();

                var userUrl = $"{_driver.Url}?Destination=UsersPage&Id={userData.Id}";
                _driver.Navigate().GoToUrl(userUrl);
                _test.Log(LogStatus.Info, $"Navigate to {userUrl}");

                _driver.SwitchToFrameById("MainContentsIFrame");
                Assert.IsTrue(userPages.CreateNewUserPage.IsDisplayed(), "The Edit User page is not displayed");
                _test.Log(LogStatus.Pass, "The Edit User page is dispalyed");

                _driver.SwitchToFrameById("tabs_Panel");
                Assert.IsTrue(userPages.CreateNewUserPage.ShortNameFieldContains(userData.UserName), "The Shortname field does not contain the correct username");
                _test.Log(LogStatus.Pass, "The username in the Shortname field is correct");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
            finally
            {
                userData.DeleteFromDatabase();
            }
        }

        #endregion
    }
}
