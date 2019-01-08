using System;
using System.Collections.Specialized;
using System.Configuration;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using ApplitrackUITests.PageObjects.AdminSide.Users;
using ApplitrackUITests.WorkFlows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.AdminSide;
using ApplitrackUITests.PageObjects.AdminSide.Applicants;
using ApplitrackUITests.PageObjects.AdminSide.Employees;
using ApplitrackUITests.PageObjects.AdminSide.Interviews;
using ApplitrackUITests.PageObjects.Toolbar;


namespace ApplitrackUITests.TestCases.ApplicationAccessTests
{
    [TestClass]
    public class ApplicationAccessSmokeTests : ApplitrackUIBase
    {
        private readonly string _muapUrl =
                $"{BaseUrls["FrontlineUrl"]}/recruit-useraccess?currentOrg={ConfigurationManager.AppSettings["OrgId"]}&oaap={BaseUrls["ApplitrackLoginPage"]}/onlineapp/admin/_admin.aspx?Destination=InAppAccessPage&Id=0"
            ;

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
            BrowseTo(BaseUrls["ApplitrackLoginPage"], _driver);

            _test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

            // login
            var loginWorkflow = new LoginWorkflows(_driver);
            loginWorkflow.LoginAsSuperUser();
        }

        [TestCleanup]
        public void TestTearDown()
        {
            BaseTearDown(_driver);
        }

        #endregion

        #region Test Cases

        // We assume that the "Integration.IDM.SAAP.Enabled" feature flag is set to "True" for these tests 

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Shared Application Access Page")]
        [TestProperty("TestCaseName", "Shared Application Access Page is Displayed")]
        [TestProperty("TestCaseDescription", "Verify that the Shared Application Access Page is displayed")]
        [TestProperty("UsesHardcodedData", "false")]
        public void ManageUserAccessPage_IsDisplayed()
        {
            // page objects
            var mainMenu = new MainMenu(_driver);
            var usersMenu = new SubMenuUsers(_driver);
            var manageUserAccessPage = new ManageUserAccessPage(_driver);

            try
            {
                mainMenu.ClickUsers();
                usersMenu.ClickManageUserAccess();
                _test.Log(LogStatus.Info, "Navigate to Users > Manage User Access");

                Assert.IsTrue(manageUserAccessPage.IsDisplayed(), "The shared application access page is not displayed");
                _test.Log(LogStatus.Pass, "The shared application access page is displayed");

                Assert.IsTrue(_driver.Url.Contains("/recruit-useraccess/"), "The shared application access page does not contain '/recruit-useraccess/'");
                _test.Log(LogStatus.Pass, "The shared application access page URL contains '/recruit-useraccess'");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Shared Application Access Page")]
        [TestProperty("TestCaseName", "Shared Application Access Page Navigate to Classic Application Access using Link")]
        [TestProperty("TestCaseDescription", "Verify that the Shared Application Access Page 'Classic Application Access' link works")]
        [TestProperty("UsesHardcodedData", "false")]
        [Ignore] // TODO This link has been removed, unignore if it reappears
        public void ManageUserAccessPage_Navigate_to_Classic_using_Link()
        {
            // page objects
            var mainMenu = new MainMenu(_driver);
            var usersMenu = new SubMenuUsers(_driver);
            var manageUserAccessPage = new ManageUserAccessPage(_driver);
            var applicationAccessPage = new ApplicationAccessPage(_driver);

            try
            {
                mainMenu.ClickUsers();
                usersMenu.ClickManageUserAccess();
                _test.Log(LogStatus.Info, "Navigate to Users > Manage User Access");

                manageUserAccessPage.ClickViewClassicLink();

                _driver.SwitchToFrameById("MainContentsIFrame");

                Assert.IsTrue(applicationAccessPage.IsDisplayed(), "The application access page is not displayed");
                _test.Log(LogStatus.Pass, "The application access page is displayed");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Shared Application Access Page")]
        [TestProperty("TestCaseName", "Shared Application Access Page Navigate to Classic Application Access using Button")]
        [TestProperty("TestCaseDescription", "Verify that the Shared Application Access Page 'View Classic' button works")]
        [TestProperty("UsesHardcodedData", "false")]
        [Ignore] // TODO This link has been removed, unignore if they reappear
        public void ManageUserAccessPage_Navigate_to_Classic_using_Button()
        {
            // page objects
            var mainMenu = new MainMenu(_driver);
            var usersMenu = new SubMenuUsers(_driver);
            var manageUserAccessPage = new ManageUserAccessPage(_driver);
            var applicationAccessPage = new ApplicationAccessPage(_driver);

            try
            {
                mainMenu.ClickUsers();
                usersMenu.ClickManageUserAccess();
                _test.Log(LogStatus.Info, "Navigate to Users > Manage User Access");

                manageUserAccessPage.ClickViewClassicButton();

                _driver.SwitchToFrameById("MainContentsIFrame");

                Assert.IsTrue(applicationAccessPage.IsDisplayed(), "The application access page is not displayed");
                _test.Log(LogStatus.Pass, "The application access page is displayed");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Shared Application Access Page")]
        [TestProperty("TestCaseName", "Shared Application Access Page Search")]
        [TestProperty("TestCaseDescription", "Verify that the Shared Application Access Page search functionality works")]
        [TestProperty("UsesHardcodedData", "false")]
        public void ManageUserAccessPage_Search()
        {
            // page objects
            var manageUserAccessPage = new ManageUserAccessPage(_driver);
            var toolbar = ToolbarFactory.Get(_driver);

            try
            {
                _driver.Navigate().GoToUrl(_muapUrl);
                _test.Log(LogStatus.Info, $"Navigate to the MUAP: {_muapUrl}");

                manageUserAccessPage.WaitForPageToLoad();

                toolbar.ClickSearch();
                _test.Log(LogStatus.Pass, "Click inside the search box");
                
                Assert.IsTrue(toolbar.SearchPage.IsDisplayed(), "The search is not displayed");
                _test.Log(LogStatus.Pass, "Search is displayed");

                // Enter in a search that should find something
                toolbar.SearchPage.EnterSearchText("1");

                Assert.IsTrue(toolbar.SearchPage.ApplicantsAreDisplayed(), "The applicant results are not displayed");
                _test.Log(LogStatus.Pass, "The applicant results are displayed");

                Assert.IsTrue(toolbar.SearchPage.EmployeesAreDisplayed(), "The employee results are not displayed");
                _test.Log(LogStatus.Pass, "The employee results are displayed");

                Assert.IsTrue(toolbar.SearchPage.JobPostingsAreDisplayed(), "The job posting results are not displayed");
                _test.Log(LogStatus.Pass, "The job posting results are displayed");

                Assert.IsTrue(toolbar.SearchPage.UsersAreDisplayed(), "The user results are not displayed");
                _test.Log(LogStatus.Pass, "The user results are displayed");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Shared Application Access Page")]
        [TestProperty("TestCaseName", "Shared Application Access Page Sign Out")]
        [TestProperty("TestCaseDescription", "Verify that the Shared Application Access Page sign out functionality works")]
        [TestProperty("UsesHardcodedData", "false")]
        public void ManageUserAccessPage_SignOut()
        {
            // page objects
            var manageUserAccessPage = new ManageUserAccessPage(_driver);
            var toolbar = ToolbarFactory.Get(_driver);
            var logoutPage = new IdmLogoutPage(_driver);

            try
            {
                _driver.Navigate().GoToUrl(_muapUrl);
                _test.Log(LogStatus.Info, $"Navigate to the MUAP: {_muapUrl}");

                manageUserAccessPage.WaitForPageToLoad();

                toolbar.Logout();
                _test.Log(LogStatus.Info, "Log out using Sidekick");

                Assert.IsTrue(logoutPage.IsDisplayed());
                _test.Log(LogStatus.Pass, "Log out was successful");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "InApp Application Access Page")]
        [TestProperty("TestCaseName", "InApp Application Access Page Route")]
        [TestProperty("TestCaseDescription", "Validate that the &Destination=InAppAccessPage route opens the correct page")]
        [TestProperty("UsesHardcodedData", "false")]
        public void InAppApplicationAccessPage_Route()
        {
            // page objects
            var applicationAccessPage = new ApplicationAccessPage(_driver);

            try
            {
                var inAppAccessPageUrl = $"{_driver.Url}?Destination=InAppAccessPage";
                _driver.Navigate().GoToUrl(inAppAccessPageUrl);
                _test.Log(LogStatus.Info, $"Navigate to {inAppAccessPageUrl}");

                _driver.SwitchToFrameById("MainContentsIFrame");
                Assert.IsTrue(applicationAccessPage.IsDisplayed(), "The application access page is not displayed");
                _test.Log(LogStatus.Pass, "The application access page is displayed");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Shared Application Access Page")]
        [TestProperty("TestCaseName", "Shared Application Access Page LHN")]
        [TestProperty("TestCaseDescription", "Verify that the menu items load correctly when navigating to MUAP")]
        [TestProperty("UsesHardcodedData", "false")]
        public void ManageUserAccessPage_LHN()
        {
            // page objects
            var superSuitNav = new SuperSuitNavigation(_driver);
            var manageUserAccessPage = new ManageUserAccessPage(_driver);

            var expectedMenuItems = new StringCollection
            {
                "My Dashboard",
                "Applicants",
                "Employees",
                "Job Postings",
                "Forms",
                "Interviews",
                "Users",
                "My Account",
                "Tools",
                "Setup",
                "Marketplace"
            };

            try
            {
                _driver.Navigate().GoToUrl(_muapUrl);
                manageUserAccessPage.WaitForPageToLoad();
                _test.Log(LogStatus.Info, $"Navigate to the MUAP: {_muapUrl}");

                var navLinkText = superSuitNav.GetNavLinkText();

                foreach (var menuItem in expectedMenuItems)
                {
                    Assert.IsTrue(navLinkText.Contains(menuItem), $"The MUAP nav does not contain {menuItem}");
                }

                _test.Log(LogStatus.Pass, "MUAP Navigation menu items loaded correctly");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Shared Application Access Page")]
        [TestProperty("TestCaseName", "Shared Application Access Page LHN My Dashboard")]
        [TestProperty("TestCaseDescription", "Verify that the dashboard loads correctly when navigating from MUAP")]
        [TestProperty("UsesHardcodedData", "false")]
        public void ManageUserAccessPage_LHN_MyDashboard()
        {
            // page objects
            var mainMenu = new MainMenu(_driver);
            var superSuitNav = new SuperSuitNavigation(_driver);
            var dashboardPage = new DashboardPage(_driver);
            var manageUserAccessPage = new ManageUserAccessPage(_driver);
            var expectedMenuItems = new StringCollection
            {
                "My Dashboard",
                "Applicants",
                "Employees",
                "Job Postings",
                "Forms",
                "Interviews",
                "Users",
                "My Account",
                "Tools",
                "Setup",
                "Marketplace",
                "Additional Resources"
            };

            try
            {
                _driver.Navigate().GoToUrl(_muapUrl);
                manageUserAccessPage.WaitForPageToLoad();
                _test.Log(LogStatus.Info, $"Navigate to the MUAP: {_muapUrl}");

                superSuitNav.ClickMyDashboard();
                CollectionAssert.AreEqual(mainMenu.GetNavLinkText(), expectedMenuItems, "Main Menu did not load correctly.");
                _test.Log(LogStatus.Pass, "Main Menu items loaded correctly.");
                
                Assert.IsTrue(dashboardPage.IsDisplayed(),"Dashboard did not load correctly.");
                _test.Log(LogStatus.Pass,"Dashboard loaded correctly.");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Shared Application Access Page")]
        [TestProperty("TestCaseName", "Shared Application Access Page LHN Applicants")]
        [TestProperty("TestCaseDescription", "Verify that applicant dashboard loads successfully when navigating from MUAP")]
        [TestProperty("UsesHardcodedData", "false")]
        public void ManageUserAccessPage_LHN_Applicants()
        {
            // page objects
            var mainMenu = new MainMenu(_driver);
            var superSuitNav = new SuperSuitNavigation(_driver);
            var applicantAdminDashboardPage = new ApplicantAdminDashboardPage(_driver);
            var manageUserAccessPage = new ManageUserAccessPage(_driver);
            var expectedMenuItems = new StringCollection
            {
                "Applicant Dashboard",
                "Vacancies by Category",
                "Vacancies by Location",
                "Category Pipelines",
                "Position Pools",
                "Actions, Notes and Status",
                "Certification",
                "Highly Qualified Subject",
                "Extracurricular Interest",
                "Folders & Routings",
                "Recruitment Effort",
                "Search Form",
                "Recent Submissions"
            };

            try
            {
                _driver.Navigate().GoToUrl(_muapUrl);
                manageUserAccessPage.WaitForPageToLoad();
                _test.Log(LogStatus.Info, $"Navigate to the MUAP: {_muapUrl}");

                superSuitNav.ClickApplicants();
                CollectionAssert.AreEqual(mainMenu.GetNavLinkText(), expectedMenuItems, "Applicants menu did not load correctly.");
                _test.Log(LogStatus.Pass, "Applicant menu items loaded correctly.");

                Assert.IsTrue(applicantAdminDashboardPage.IsDisplayed(), "Applicant dashboard did not load correctly.");
                _test.Log(LogStatus.Pass, "Applicant dashboard loaded correctly.");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Shared Application Access Page")]
        [TestProperty("TestCaseName", "Shared Application Access Page LHN Employees")]
        [TestProperty("TestCaseDescription", "Verify that employee dashboard loads successfully when navigating from MUAP")]
        [TestProperty("UsesHardcodedData", "false")]
        public void ManageUserAccessPage_LHN_Employees()
        {
            // page objects
            var mainMenu = new MainMenu(_driver);
            var superSuitNav = new SuperSuitNavigation(_driver);
            var manageUserAccessPage = new ManageUserAccessPage(_driver);
            var employeeAdminDashboardPage = new EmployeeAdminDashboardPage(_driver);
            var expectedMenuItems = new StringCollection
            {
                "Employee Dashboard",
                "Create New Employee",
                "Employee List",
                "Alpha Groups",
                "Location",
                "Position",
                "Folder",
                "Search Form",
                "Employee Evaluations Dashboard",
                "Employee Timelines Dashboard",
                "View Help Requests",
                "Help Request Categories",
                "Manage Timelines"
            };

            try
            {
                _driver.Navigate().GoToUrl(_muapUrl);
                manageUserAccessPage.WaitForPageToLoad();
                _test.Log(LogStatus.Info, $"Navigate to the MUAP: {_muapUrl}");

                superSuitNav.ClickEmployees();
                CollectionAssert.AreEqual(mainMenu.GetNavLinkText(), expectedMenuItems, "Employee menu did not load correctly.");
                _test.Log(LogStatus.Pass, "Employee Menu items loaded correctly.");

                Assert.IsTrue(employeeAdminDashboardPage.IsDisplayed(), "Employee dashboard did not load correctly.");
                _test.Log(LogStatus.Pass, "Employee dashboard loaded correctly.");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Shared Application Access Page")]
        [TestProperty("TestCaseName", "Shared Application Access Page LHN Job Postings")]
        [TestProperty("TestCaseDescription", "Verify that job postings dashboard loads successfully when navigating from MUAP")]
        [TestProperty("UsesHardcodedData", "false")]
        public void ManageUserAccessPage_LHN_JobPostings()
        {
            // page objects
            var mainMenu = new MainMenu(_driver);
            var superSuitNav = new SuperSuitNavigation(_driver);
            var manageUserAccessPage = new ManageUserAccessPage(_driver);
            var jobPostingPages = new JobPostingsPages(_driver);

            var expectedMenuItems = new StringCollection
            {
                "Job Postings Dashboard",
                "List All Active Postings",
                "Create New Posting",
                "Open Postings By Category",
                "Active Postings By Category",
                "All Postings By Status",
                "Requisition Inbox",
                "Create New Requisition",
                "My Draft Requisitions",
                "My Requisitions In Process",
                "My Open Requisitions",
                "My Closed Requisitions",
                "My Denied Requisitions",
                "Approved By Me -Active",
                "Approved By Me -InActive",
                "Close An Approved Posting",
                "All Active Requisitions",
                "All Inactive Requisitions",
                "Create New Template",
                "List Existing"
            };

            try
            {
                _driver.Navigate().GoToUrl(_muapUrl);
                manageUserAccessPage.WaitForPageToLoad();
                _test.Log(LogStatus.Info, $"Navigate to the MUAP: {_muapUrl}");

                superSuitNav.ClickJobPostings();
                CollectionAssert.AreEqual(mainMenu.GetNavLinkText(), expectedMenuItems, "Job Postings menu did not load correctly.");
                _test.Log(LogStatus.Pass, "Job Postings Menu items loaded correctly.");

                Assert.IsTrue(jobPostingPages.DashboardPage.IsDisplayed(), "Employee dashboard did not load correctly.");
                _test.Log(LogStatus.Pass, "Job Postings dashboard loaded correctly.");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Shared Application Access Page")]
        [TestProperty("TestCaseName", "Shared Application Access Page LHN Forms")]
        [TestProperty("TestCaseDescription", "Verify that forms dashboard loads successfully when navigating from MUAP")]
        [TestProperty("UsesHardcodedData", "false")]
        public void ManageUserAccessPage_LHN_Forms()
        {
            // page objects
            var mainMenu = new MainMenu(_driver);
            var superSuitNav = new SuperSuitNavigation(_driver);
            var manageUserAccessPage = new ManageUserAccessPage(_driver);
            var dashboardPage = new DashboardPage(_driver);

            var expectedMenuItems = new StringCollection
            {
                "My Forms Inbox",
                "My Sent Forms",
                "Send a Form",
                "Fill Out a New Form",
                "View Submitted Forms By Category",
                "Design Forms and Packets"
            };

            try
            {
                _driver.Navigate().GoToUrl(_muapUrl);
                manageUserAccessPage.WaitForPageToLoad();
                _test.Log(LogStatus.Info, $"Navigate to the MUAP: {_muapUrl}");

                superSuitNav.ClickForms();
                CollectionAssert.AreEqual(mainMenu.GetNavLinkText(), expectedMenuItems, "Forms menu did not load correctly.");
                _test.Log(LogStatus.Pass, "Forms Menu items loaded correctly.");

                Assert.IsTrue(dashboardPage.IsDisplayed(),"Dashboard did not load correctly.");
                _test.Log(LogStatus.Pass,"Dashboard loaded correctly.");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Shared Application Access Page")]
        [TestProperty("TestCaseName", "Shared Application Access Page LHN Interviews")]
        [TestProperty("TestCaseDescription", "Verify that interviews dashboard loads successfully when navigating from MUAP")]
        [TestProperty("UsesHardcodedData", "false")]
        public void ManageUserAccessPage_LHN_Interviews()
        {
            var mainMenu = new MainMenu(_driver);
            var superSuitNav = new SuperSuitNavigation(_driver);
            var manageUserAccessPage = new ManageUserAccessPage(_driver);
            var interviewsPages = new InterviewPages(_driver);

            var expectedMenuItems = new StringCollection
            {
                "By Date",
                "By Title",
                "By Organizer",
                "Create Interview"
            };

            try
            {
                _driver.Navigate().GoToUrl(_muapUrl);
                manageUserAccessPage.WaitForPageToLoad();
                _test.Log(LogStatus.Info, $"Navigate to the MUAP: {_muapUrl}");

                superSuitNav.ClickInterviews();
                CollectionAssert.AreEqual(mainMenu.GetNavLinkText(), expectedMenuItems, "Interviews menu did not load correctly.");
                _test.Log(LogStatus.Pass, "Interviews Menu items loaded correctly.");

                _driver.SwitchToFrameById("MainContentsIFrame");
                Assert.IsTrue(interviewsPages.DashboardPage.IsDisplayed(), "Interviews dashboard did not load correctly.");
                _test.Log(LogStatus.Pass,"Interviews dashboard loaded correctly.");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Shared Application Access Page")]
        [TestProperty("TestCaseName", "Shared Application Access Page LHN Users")]
        [TestProperty("TestCaseDescription", "Verify that users dashboard loads successfully when navigating from MUAP")]
        [TestProperty("UsesHardcodedData", "false")]
        public void ManageUserAccessPage_LHN_Users()
        {
            var mainMenu = new MainMenu(_driver);
            var superSuitNav = new SuperSuitNavigation(_driver);
            var manageUserAccessPage = new ManageUserAccessPage(_driver);
            var dashboardPage = new DashboardPage(_driver);

            var expectedMenuItems = new StringCollection
            {
                "List all users",
                "Create a new user",
                "Upload New Users",
                "Manage User Access",
                "List all groups",
                "Create a new group"
            };

            try
            {
                _driver.Navigate().GoToUrl(_muapUrl);
                manageUserAccessPage.WaitForPageToLoad();
                _test.Log(LogStatus.Info, $"Navigate to the MUAP: {_muapUrl}");

                superSuitNav.ClickUsers();
                CollectionAssert.AreEqual(mainMenu.GetNavLinkText(), expectedMenuItems, "Users menu did not load correctly.");
                _test.Log(LogStatus.Pass, "Users Menu items loaded correctly.");

                Assert.IsTrue(dashboardPage.IsDisplayed(),"Dashboard did not load correctly.");
                _test.Log(LogStatus.Pass,"Dashboard loaded correctly.");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Shared Application Access Page")]
        [TestProperty("TestCaseName", "Shared Application Access Page LHN My Account")]
        [TestProperty("TestCaseDescription", "Verify that my account dashboard loads successfully when navigating from MUAP")]
        [TestProperty("UsesHardcodedData", "false")]
        public void ManageUserAccessPage_LHN_MyAccount()
        {
            var mainMenu = new MainMenu(_driver);
            var superSuitNav = new SuperSuitNavigation(_driver);
            var manageUserAccessPage = new ManageUserAccessPage(_driver);
            var dashboardPage = new DashboardPage(_driver);

            var expectedMenuItems = new StringCollection
            {
                "My Folders & Routings",
                "Create A New Folder",
                "Configure My Routings",
                "My Interviews",
                "My Calendars",
                "Edit User Information",
                "Edit Email Templates",
                "User Preferences",
                "Fill Out a New Form",
                "Send a Form"
            };

            try
            {
                _driver.Navigate().GoToUrl(_muapUrl);
                manageUserAccessPage.WaitForPageToLoad();
                _test.Log(LogStatus.Info, $"Navigate to the MUAP: {_muapUrl}");

                superSuitNav.ClickMyAccount();
                CollectionAssert.AreEqual(mainMenu.GetNavLinkText(), expectedMenuItems, "My Account menu did not load correctly.");
                _test.Log(LogStatus.Pass, "My Account Menu items loaded correctly.");

                Assert.IsTrue(dashboardPage.IsDisplayed(),"Dashboard did not load correctly.");
                _test.Log(LogStatus.Pass,"Dashboard loaded correctly.");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Shared Application Access Page")]
        [TestProperty("TestCaseName", "Shared Application Access Page LHN Tools")]
        [TestProperty("TestCaseDescription", "Verify that tools dashboard loads successfully when navigating from MUAP")]
        [TestProperty("UsesHardcodedData", "false")]
        public void ManageUserAccessPage_LHN_Tools()
        {
            var mainMenu = new MainMenu(_driver);
            var superSuitNav = new SuperSuitNavigation(_driver);
            var manageUserAccessPage = new ManageUserAccessPage(_driver);
            var dashboardPage = new DashboardPage(_driver);

            var expectedMenuItems = new StringCollection
            {
                "Additional Materials",
                "Maintenance Tasks",
                "Reports",
                "Filer"
            };

            try
            {
                _driver.Navigate().GoToUrl(_muapUrl);
                manageUserAccessPage.WaitForPageToLoad();
                _test.Log(LogStatus.Info, $"Navigate to the MUAP: {_muapUrl}");

                superSuitNav.ClickTools();
                CollectionAssert.AreEqual(mainMenu.GetNavLinkText(), expectedMenuItems, "Tools menu did not load correctly.");
                _test.Log(LogStatus.Pass, "Tools menu items loaded correctly.");

                Assert.IsTrue(dashboardPage.IsDisplayed(),"Dashboard did not load correctly.");
                _test.Log(LogStatus.Pass,"Dashboard loaded correctly.");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Shared Application Access Page")]
        [TestProperty("TestCaseName", "Shared Application Access Page LHN Setup")]
        [TestProperty("TestCaseDescription", "Verify that setup dashboard loads successfully when navigating from MUAP")]
        [TestProperty("UsesHardcodedData", "false")]
        public void ManageUserAccessPage_LHN_Setup()
        {
            var mainMenu = new MainMenu(_driver);
            var superSuitNav = new SuperSuitNavigation(_driver);
            var manageUserAccessPage = new ManageUserAccessPage(_driver);
            var dashboardPage = new DashboardPage(_driver);

            var expectedMenuItems = new StringCollection
            {
                "Core",
                "Applicant Settings",
                "Employee Settings",
                "Forms"
            };

            try
            {
                _driver.Navigate().GoToUrl(_muapUrl);
                manageUserAccessPage.WaitForPageToLoad();
                _test.Log(LogStatus.Info, $"Navigate to the MUAP: {_muapUrl}");

                superSuitNav.ClickSetup();
                CollectionAssert.AreEqual(mainMenu.GetNavLinkText(), expectedMenuItems, "Setup menu did not load correctly.");
                _test.Log(LogStatus.Pass, "Setup Menu items loaded correctly.");

                Assert.IsTrue(dashboardPage.IsDisplayed(),"Dashboard did not load correctly.");
                _test.Log(LogStatus.Pass,"Dashboard loaded correctly.");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Shared Application Access Page")]
        [TestProperty("TestCaseName", "Shared Application Access Page LHN Marketplace")]
        [TestProperty("TestCaseDescription", "Verify that marketplace dashboard loads successfully when navigating from MUAP")]
        [TestProperty("UsesHardcodedData", "false")]
        public void ManageUserAccessPage_LHN_Marketplace()
        {
            var mainMenu = new MainMenu(_driver);
            var superSuitNav = new SuperSuitNavigation(_driver);
            var manageUserAccessPage = new ManageUserAccessPage(_driver);
            var marketPlacePage = new MarketPlacePage(_driver);

            var expectedMenuItems = new StringCollection
            {
                "Modules",
                "Training and Consulting Services",
                "Integrations",
                "Frontline"
            };

            try
            {
                _driver.Navigate().GoToUrl(_muapUrl);
                manageUserAccessPage.WaitForPageToLoad();
                _test.Log(LogStatus.Info, $"Navigate to the MUAP: {_muapUrl}");

                superSuitNav.ClickMarketplace();
                CollectionAssert.AreEqual(mainMenu.GetNavLinkText(), expectedMenuItems, "Marketplace menu did not load correctly.");
                _test.Log(LogStatus.Pass, "Marketplace Menu items loaded correctly.");

                Assert.IsTrue(marketPlacePage.IsDisplayed(),"Marketplace dashboard did not load correctly.");
                _test.Log(LogStatus.Pass,"Marketplace dashboard loaded correctly.");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        #endregion
    }
}