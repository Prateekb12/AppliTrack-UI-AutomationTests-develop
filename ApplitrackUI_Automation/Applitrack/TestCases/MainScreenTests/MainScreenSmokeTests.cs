using System;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using ApplitrackUITests.PageObjects.AdminSide.Users;
using ApplitrackUITests.PageObjects.EmployeeCenter;
using ApplitrackUITests.PageObjects.Toolbar;
using ApplitrackUITests.TestBaseCases;
using ApplitrackUITests.WorkFlows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.MainScreenTests
{
    [TestClass]
    public class MainScreenSmokeTests : MainScreenBaseTests
    {
        #region Setup and TearDown

        protected override IWebDriver Driver { get; set; }
        protected override ExtentTest test { get; set; }

        /// <summary>
        ///Test Initialize Contains items to
        ///run before each [TestMethod]
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            // extent reports setup
            test = ExtentTestManager.StartTest(TestContext.Properties["TestCaseName"].ToString(),
                TestContext.Properties["TestCaseDescription"].ToString())
                .AssignCategory("Smoke");

            // browser setup
            Driver = SetUp(_BT); //Stand up Driver and Log Test
            Driver.Manage().Window.Maximize();
            BrowseTo(BaseUrls["ApplitrackLoginPage"], Driver);

            test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);
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
        [TestProperty("TestArea", "Toolbar")]
        [TestProperty("TestCaseName", "Toolbar Help Opens")]
        [TestProperty("TestCaseDescription", "Make sure the help menu is displayed after clicking the Help button on the toolbar")]
        [TestProperty("UsesHardcodedData", "false")]
        [Ignore] // TODO fix for zendesk functionality
        public void Toolbar_Help_Opens()
        {
            // page objects
            var toolbar = ToolbarFactory.Get(Driver);

            try  //Contains Contents of Test
            {
                new LoginWorkflows(Driver).LoginAsSuperUser();
                toolbar.ClickHelp();
                test.Log(LogStatus.Pass, "Click the Help button");

                Assert.IsTrue(toolbar.HelpMenuIsDisplayed(), "The help menu did not open");
                test.Log(LogStatus.Pass, "The help menu opened");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Toolbar")]
        [TestProperty("TestCaseName", "Toolbar Inbox Opens")]
        [TestProperty("TestCaseDescription", "Make sure the inbox is displayed after clicking the Inbox button on the toolbar")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Toolbar_Inbox_Opens()
        {
            try  //Contains Contents of Test
            {
                // page objects
                new LoginWorkflows(Driver).LoginAsSuperUser();

                var toolbar = ToolbarFactory.Get(Driver);
                toolbar.ClickInbox();
                test.Log(LogStatus.Pass, "Click the Inbox button");

                Assert.IsTrue(toolbar.InboxIsDisplayed(), "The inbox did not open");
                test.Log(LogStatus.Pass, "The inbox opened");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Toolbar")]
        [TestProperty("TestCaseName", "Toolbar Form Inbox Notification Appears")]
        [TestProperty("TestCaseDescription",
            "Make sure a forms inbox notification displays after creating the requisite data")]
        [TestProperty("UsesHardcodedData", "true")]
        public void Toolbar_Inbox_FormNotification_Appears()
        {
            var dataGenerator = new FormInboxNotificationGenerator();

            try  //Contains Contents of Test
            {
                //Set up notification
                Console.WriteLine("Setting up data");
                dataGenerator.CreateNotificationData();
                Console.WriteLine("Data setup succeeded");

                // page objects
                new LoginWorkflows(Driver).LoginAsSuperUser();
                var toolbar = ToolbarFactory.Get(Driver);

                toolbar.WaitForLoad();
                VerifyUnreadItemPresent(toolbar, dataGenerator.ExpectedResult);
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
            finally
            {
                dataGenerator.DeleteNotificationData();
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Toolbar")]
        [TestProperty("TestCaseName", "Toolbar Search Opens")]
        [TestProperty("TestCaseDescription", "Make sure the search page is displayed after clicking the Search button on the toolbar")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Toolbar_Search_Opens()
        {
            // page objects
            var toolbar = ToolbarFactory.Get(Driver);

            try  //Contains Contents of Test
            {
                new LoginWorkflows(Driver).LoginAsSuperUser();
                toolbar.ClickSearch();
                test.Log(LogStatus.Pass, "Click the Search button");

                Assert.IsTrue(toolbar.SearchIsDisplayed());
                test.Log(LogStatus.Pass, "The news feed opened");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Toolbar")]
        [TestProperty("TestCaseName", "Toolbar User Menu Opens")]
        [TestProperty("TestCaseDescription", "Make sure the user menu is displayed after clicking on the User Name on the toolbar")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Toolbar_UserMenu_Opens()
        {
            // page objects
            var toolbar = ToolbarFactory.Get(Driver);

            try
            {
                new LoginWorkflows(Driver).LoginAsSuperUser();
                toolbar.ClickUserMenu();
                test.Log(LogStatus.Pass, "Click the Search button");

                Assert.IsTrue(toolbar.UserMenuIsDisplayed());
                test.Log(LogStatus.Pass, "The user menu opened");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Sidekick")]
        [TestProperty("TestCaseName", "Sidekick Account Settings Opens")]
        [TestProperty("TestCaseDescription", "Make sure the Account Settings page opens from R&H")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Sidekick_AccountSettings_from_Recruit()
        {
            // page objects
            var toolbar = new SidekickToolbar(Driver);
            var accountSettingsPage = new IdmAccountSettingsPage(Driver);
            var dashboard = new DashboardPage(Driver);

            try
            {
                new LoginWorkflows(Driver).LoginAsSuperUser();
                toolbar.ClickUserMenu();
                toolbar.UserMenuPage.ClickAccountSettings();
                test.Log(LogStatus.Info, "In the toolbar, click on User Name > Account Settings ");

                Assert.IsTrue(accountSettingsPage.IsDisplayed(), "The account settings page is not displayed");
                test.Log(LogStatus.Pass, "The account settings page is displayed");

                accountSettingsPage.ClickBackToRecruit();
                test.Log(LogStatus.Info, "Click the \"Back to Recruiting & Hiring\" button");
                Assert.IsTrue(dashboard.IsDisplayed(), "R&H is not displayed");
                test.Log(LogStatus.Pass, "R&H is displayed");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        // this test assumes that the "Integration.IDM.SAAP.Enabled" flag is on
        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Sidekick")]
        [TestProperty("TestCaseName", "Sidekick Account Settings Opens from SAAP")]
        [TestProperty("TestCaseDescription", "Make sure the Account Settings page opens from the Shared Application Access Page")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Sidekick_AccountSettings_from_SharedApplicationAccessPage()
        {
            // page objects
            var toolbar = new SidekickToolbar(Driver);
            var accountSettingsPage = new IdmAccountSettingsPage(Driver);
            var mainMenu = new MainMenu(Driver);
            var usersMenu = new SubMenuUsers(Driver);
            var sharedApplicationAccessPage = new ManageUserAccessPage(Driver);

            try
            {
                new LoginWorkflows(Driver).LoginAsSuperUser();
                mainMenu.ClickUsers();
                usersMenu.ClickManageUserAccess();
                test.Log(LogStatus.Info, "Navigate to Users > Manage User Access");

                sharedApplicationAccessPage.WaitForPageToLoad();

                toolbar.ClickUserMenu();
                toolbar.UserMenuPage.ClickAccountSettings();
                test.Log(LogStatus.Info, "In the toolbar, click on User Name > Account Settings ");

                Assert.IsTrue(accountSettingsPage.IsDisplayed(), "The account settings page is not displayed");
                test.Log(LogStatus.Pass, "The account settings page is displayed");

                accountSettingsPage.ClickBackToRecruit();
                test.Log(LogStatus.Info, "Click the \"Back to Recruiting & Hiring\" button");
                Assert.IsTrue(sharedApplicationAccessPage.IsDisplayed(), "The SAAP is not displayed");
                test.Log(LogStatus.Pass, "The SAAP is displayed");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Sidekick")]
        [TestProperty("TestCaseName", "Sidekick Org Switch Opens")]
        [TestProperty("TestCaseDescription", "Make sure the org switcher is displayed after clicking on the organization name in the toolbar")]
        [TestProperty("UsesHardcodedData", "false")]
        [Ignore] // org switcher may not always be available depending on user
        public void Sidekick_OrgSwitch_Opens()
        {
            // page objects
            var toolbar = new SidekickToolbar(Driver);

            try  //Contains Contents of Test
            {
                new LoginWorkflows(Driver).LoginAsSuperUser();
                toolbar.ClickOrgSwitcher();
                test.Log(LogStatus.Pass, "Click the org switcher");

                Assert.IsTrue(toolbar.OrgSwitcherIsDisplayed());
                test.Log(LogStatus.Pass, "The org switcher opened");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }



        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Sidekick")]
        [TestProperty("TestCaseName", "Sidekick Org Switch to EC")]
        [TestProperty("TestCaseDescription", "Make sure EC opens after switching organizations using the Sidekick Org Switcher")]
        [TestProperty("UsesHardcodedData", "false")]
        [Ignore] // TODO wait for environments to become more stable
        public void Sidekick_OrgSwitch_To_EC()
        {
            // page objects
            var toolbar = new SidekickToolbar(Driver);
            var employeeCenter = new EmployeeCenterMainPage(Driver);

            try  //Contains Contents of Test
            {
                new LoginWorkflows(Driver).LoginAsSuperUser();
                toolbar.SwitchOrgs("North Branford");

                Assert.IsTrue(employeeCenter.IsDisplayed());
                test.Log(LogStatus.Pass, "The EC page is displayed");
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
