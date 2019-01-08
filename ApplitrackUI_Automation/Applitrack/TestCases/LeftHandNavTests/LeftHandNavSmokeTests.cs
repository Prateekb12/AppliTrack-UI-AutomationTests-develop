using System;
using System.Collections.Specialized;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide;
using ApplitrackUITests.PageObjects.AdminSide.Applicants;
using ApplitrackUITests.PageObjects.AdminSide.Employees;
using ApplitrackUITests.PageObjects.AdminSide.Interviews;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using ApplitrackUITests.WorkFlows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.LeftHandNavTests
{
    [TestClass]
    public class LeftHandNavSmokeTests : ApplitrackUIBase
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

            // browser setup
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

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "LeftHandNav")]
        [TestProperty("TestCaseName", "Main Menu Items")]
        [TestProperty("TestCaseDescription", "Ensure that the Main menu contains the correct items")]
        [TestProperty("UsesHardcodedData", "false")]
        public void LHN_Main()
        {
            var mainMenu = new MainMenu(_driver);
            var expectedMenuItems = new StringCollection
            {
                "My Dashboard",
                "Applicants",
                "Employees",
                "Job Postings",
                "Forms",
                $"{FeatureFlags.Term.Interview}s",
                "Users",
                "My Account",
                "Tools",
                "Setup",
                "Marketplace",
                "Additional Resources"
            };

            try
            {
                CollectionAssert.AreEqual(expectedMenuItems, mainMenu.GetNavLinkText(),
                    "The main menu does not contain the correct items");

                _test.Log(LogStatus.Pass, "The main menu contains the correct items");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "LeftHandNav")]
        [TestProperty("TestCaseName", "Applicants Menu Items")]
        [TestProperty("TestCaseDescription", "Ensure that the Applicants menu contains the correct items")]
        [TestProperty("UsesHardcodedData", "true")]
        public void LHN_Applicants()
        {
            var mainMenu = new MainMenu(_driver);
            var applicantAdminDashboardPage = new ApplicantAdminDashboardPage(_driver);

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
                mainMenu.ClickApplicants();
                _test.Log(LogStatus.Pass, "Navigate to Applicants");

                CollectionAssert.AreEqual(expectedMenuItems, mainMenu.GetNavLinkText(),
                    "The Applicants menu does not contain the correct items");
                _test.Log(LogStatus.Pass, "The Applicants menu contains the correct items");

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
        [TestProperty("TestArea", "LeftHandNav")]
        [TestProperty("TestCaseName", "Applicants > Applicants Dashboard")]
        [TestProperty("TestCaseDescription", "Ensure that Applicants > Applicants Dashboard displays the correct page")]
        [TestProperty("UsesHardcodedData", "false")]
        public void LHN_Applicants_ApplicantDashboard()
        {
            var mainMenu = new MainMenu(_driver);
            var applicantsMenu = new SubMenuApplicants(_driver);
            var applicantAdminDashboardPage = new ApplicantAdminDashboardPage(_driver);

            try
            {
                mainMenu.ClickApplicants();
                applicantsMenu.ClickApplicantDashboard();
                _test.Log(LogStatus.Pass, "Navigate to Applicants > Applicant Dashboard");

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
        [TestProperty("TestArea", "LeftHandNav")]
        [TestProperty("TestCaseName", "Applicants > Search Form")]
        [TestProperty("TestCaseDescription", "Ensure that Applicants > Search Form displays the correct page")]
        [TestProperty("UsesHardcodedData", "false")]
        public void LHN_Applicants_SearchForm()
        {
            var mainMenu = new MainMenu(_driver);
            var applicantsMenu = new SubMenuApplicants(_driver);
            var applicantAdminPages= new ApplicantAdminPages(_driver);

            try
            {
                mainMenu.ClickApplicants();
                applicantsMenu.ClickSearchForm();
                _test.Log(LogStatus.Pass, "Navigate to Applicants > Search Form");

                _driver.SwitchToFrameById("MainContentsIFrame");

                Assert.IsTrue(applicantAdminPages.SearchFormPage.IsDisplayed(), "Search Form page did not load correctly.");
                _test.Log(LogStatus.Pass, "Search Form page loaded correctly.");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }



        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "LeftHandNav")]
        [TestProperty("TestCaseName", "Employees Menu Items")]
        [TestProperty("TestCaseDescription", "Ensure that the Employees menu contains the correct items")]
        [TestProperty("UsesHardcodedData", "false")]
        public void LHN_Employees()
        {
            var mainMenu = new MainMenu(_driver);
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
                mainMenu.ClickEmployees();
                _test.Log(LogStatus.Pass, "Navigate to Employees");

                CollectionAssert.AreEqual(expectedMenuItems, mainMenu.GetNavLinkText(),
                    "The Employees menu does not contain the correct items");

                _test.Log(LogStatus.Pass, "The Employees menu contains the correct items");

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
        [TestProperty("TestArea", "LeftHandNav")]
        [TestProperty("TestCaseName", "JobPostings Menu Items")]
        [TestProperty("TestCaseDescription", "Ensure that the Job Postings menu contains the correct items")]
        [TestProperty("UsesHardcodedData", "false")]
        public void LHN_JobPostings()
        {
            var mainMenu = new MainMenu(_driver);
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
                mainMenu.ClickJobPostings();
                _test.Log(LogStatus.Info, "Navigate to Job Postings");

                CollectionAssert.AreEqual(expectedMenuItems, mainMenu.GetNavLinkText(),
                    "The Job Postings menu does not contain the correct items");
                _test.Log(LogStatus.Pass, "The Job Postings menu contains the correct items");

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
        [TestProperty("TestArea", "LeftHandNav")]
        [TestProperty("TestCaseName", "Forms Menu Items")]
        [TestProperty("TestCaseDescription", "Ensure that the Forms menu contains the correct items")]
        [TestProperty("UsesHardcodedData", "false")]
        public void LHN_Forms()
        {
            var mainMenu = new MainMenu(_driver);

            var expectedMenuItems = new StringCollection
            {
                "My Forms Inbox",
                "My Sent Forms",
                "Send a Form",
                "Fill Out a New Form",
                "View Submitted Forms By Category",
                "Design Forms and Packets",
            };

            try
            {
                mainMenu.ClickForms();
                _test.Log(LogStatus.Pass, "Navigate to Forms");

                CollectionAssert.AreEqual(expectedMenuItems, mainMenu.GetNavLinkText(),
                    "The Forms menu does not contain the correct items");
                _test.Log(LogStatus.Pass, "The Forms menu contains the correct items");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "LeftHandNav")]
        [TestProperty("TestCaseName", "Interviews Menu Items")]
        [TestProperty("TestCaseDescription", "Ensure that the Interviews menu contains the correct items")]
        [TestProperty("UsesHardcodedData", "false")]
        public void LHN_Intervews()
        {
            var mainMenu = new MainMenu(_driver);
            var interviewPages = new InterviewPages(_driver);
            var expectedMenuItems = new StringCollection
            {
                "By Date",
                "By Title",
                "By Organizer",
                $"Create {FeatureFlags.Term.Interview}",
            };

            try
            {
                mainMenu.ClickInterviews();
                _test.Log(LogStatus.Pass, $"Navigate to {FeatureFlags.Term.Interview}s");

                CollectionAssert.AreEqual(expectedMenuItems, mainMenu.GetNavLinkText(),
                    $"The {FeatureFlags.Term.Interview}s menu does not contain the correct items");

                _test.Log(LogStatus.Pass, $"The {FeatureFlags.Term.Interview}s menu contains the correct items");

                _driver.SwitchToFrameById("MainContentsIFrame");
                Assert.IsTrue(interviewPages.DashboardPage.IsDisplayed(),
                    $"{FeatureFlags.Term.Interview}s dashboard did not load correctly.");
                _test.Log(LogStatus.Pass, $"{FeatureFlags.Term.Interview}s dashboard loaded correctly.");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "LeftHandNav")]
        [TestProperty("TestCaseName", "Users Menu Items")]
        [TestProperty("TestCaseDescription", "Ensure that the Users menu contains the correct items")]
        [TestProperty("UsesHardcodedData", "false")]
        public void LHN_Users()
        {
            var mainMenu = new MainMenu(_driver);
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
                mainMenu.ClickUsers();
                _test.Log(LogStatus.Info, "Navigate to Users");

                CollectionAssert.AreEqual(expectedMenuItems, mainMenu.GetNavLinkText(),
                    "The Users menu does not contain the correct items");

                _test.Log(LogStatus.Pass, "The Users menu contains the correct items");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "LeftHandNav")]
        [TestProperty("TestCaseName", "My Account Menu Items")]
        [TestProperty("TestCaseDescription", "Ensure that the My Account menu contains the correct items")]
        [TestProperty("UsesHardcodedData", "false")]
        public void LHN_MyAccount()
        {
            var mainMenu = new MainMenu(_driver);
            var expectedMenuItems = new StringCollection
            {
                "My Folders & Routings",
                "Create A New Folder",
                "Configure My Routings",
                $"My {FeatureFlags.Term.Interview}s",
                "My Calendars",
                "Edit User Information",
                "Edit Email Templates",
                "User Preferences",
                "Fill Out a New Form",
                "Send a Form"
            };

            try
            {
                mainMenu.ClickMyAccount();
                _test.Log(LogStatus.Info, "Navigate to My Account");

                CollectionAssert.AreEqual(expectedMenuItems, mainMenu.GetNavLinkText(),
                    "The My Account menu does not contain the correct items");

                _test.Log(LogStatus.Pass, "The My Account menu contains the correct items");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "LeftHandNav")]
        [TestProperty("TestCaseName", "Tools Menu Items")]
        [TestProperty("TestCaseDescription", "Ensure that the Tools menu contains the correct items")]
        [TestProperty("UsesHardcodedData", "false")]
        public void LHN_Tools()
        {
            var mainMenu = new MainMenu(_driver);

            var expectedMenuItems = new StringCollection
            {
                "Additional Materials",
                "Maintenance Tasks",
                "Reports",
                "Filer"
            };

            try
            {
                mainMenu.ClickTools();
                _test.Log(LogStatus.Info, "Navigate to Tools");

                CollectionAssert.AreEqual(expectedMenuItems, mainMenu.GetNavLinkText(),
                    "The Tools menu does not contain the correct items");
                _test.Log(LogStatus.Pass, "The Tools menu contains the correct items");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "LeftHandNav")]
        [TestProperty("TestCaseName", "Setup Menu Items")]
        [TestProperty("TestCaseDescription", "Ensure that the Setup menu contains the correct items")]
        [TestProperty("UsesHardcodedData", "false")]
        public void LHN_Setup()
        {
            var mainMenu = new MainMenu(_driver);

            var expectedMenuItems = new StringCollection
            {
                "Core",
                "Applicant Settings",
                "Employee Settings",
                "Forms"
            };

            try
            {
                mainMenu.ClickSetup();
                _test.Log(LogStatus.Info, "Navigate to Setup");

                CollectionAssert.AreEqual(expectedMenuItems, mainMenu.GetNavLinkText(),
                    "The Setup menu does not contain the correct items");
                _test.Log(LogStatus.Pass, "The Setup menu contains the correct items");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "LeftHandNav")]
        [TestProperty("TestCaseName", "Marketplace Menu Items")]
        [TestProperty("TestCaseDescription", "Ensure that the Marketplace menu contains the correct items")]
        [TestProperty("UsesHardcodedData", "false")]
        public void LHN_Marketplace()
        {
            var mainMenu = new MainMenu(_driver);
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
                mainMenu.ClickMarketplace();
                _test.Log(LogStatus.Info, "Navigate to Marketplace");

                CollectionAssert.AreEqual(expectedMenuItems, mainMenu.GetNavLinkText(),
                    "The Marketplace menu does not contain the correct items");
                _test.Log(LogStatus.Pass, "The Marketplace menu contains the correct items");

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
