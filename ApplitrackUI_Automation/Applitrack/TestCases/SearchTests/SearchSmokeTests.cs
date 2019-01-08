using System;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide;
using ApplitrackUITests.PageObjects.Toolbar;
using ApplitrackUITests.WorkFlows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.SearchTests
{
    [TestClass]
    public class SearchSmokeTests : ApplitrackUIBase
    {

        #region Setup and TearDown

        private IWebDriver Driver;
        private ExtentTest test;
        private CommonActions _commonActions;

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
            BrowseTo(BaseUrls["ApplitrackLoginPage"], Driver);

            _commonActions = new CommonActions(Driver);

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
        [TestProperty("TestArea", "Search")]
        [TestProperty("TestCaseName", "Search SuperUser Can Search for Applicants")]
        [TestProperty("TestCaseDescription", "The search popover displays for super users")]
        [TestProperty("UsesHardcodedData", "true")]
        public void Search_SuperUser_Can_Search_for_Applicants()
        {
            // page objects
            var toolbar = ToolbarFactory.Get(Driver);

            // workflows
            var loginWorkflow = new LoginWorkflows(Driver);

            try
            {
                loginWorkflow.LoginAsSuperUser();
                test.Log(LogStatus.Pass, "Log in as super user");

                toolbar.ClickSearch();
                test.Log(LogStatus.Pass, "Click inside the search box");

                Assert.IsTrue(toolbar.SearchPage.IsDisplayed(), "The search is not displayed");
                test.Log(LogStatus.Pass, "Search is displayed");

                // Enter in a search that should find something
                toolbar.SearchPage.EnterSearchText("1");

                Assert.IsTrue(toolbar.SearchPage.ApplicantsAreDisplayed(), "The applicant results are not displayed");
                test.Log(LogStatus.Pass, "The applicant results are displayed");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Search")]
        [TestProperty("TestCaseName", "Search SuperUser Can Search for Employees")]
        [TestProperty("TestCaseDescription", "The search popover displays for super users")]
        [TestProperty("UsesHardcodedData", "true")]

        public void Search_SuperUser_Can_Search_for_Employees()
        {
            // page objects
            var toolbar = ToolbarFactory.Get(Driver);

            // workflows
            var loginWorkflow = new LoginWorkflows(Driver);

            try
            {
                loginWorkflow.LoginAsSuperUser();
                test.Log(LogStatus.Pass, "Log in as super user");

                toolbar.ClickSearch();
                test.Log(LogStatus.Pass, "Click inside the search box");

                Assert.IsTrue(toolbar.SearchPage.IsDisplayed(), "The search is not displayed");
                test.Log(LogStatus.Pass, "The search is displayed");

                // Enter in a search that should find something
                toolbar.SearchPage.EnterSearchText("1");

                Assert.IsTrue(toolbar.SearchPage.EmployeesAreDisplayed(), "The employee results are not displayed");
                test.Log(LogStatus.Pass, "The employee results are displayed");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Search")]
        [TestProperty("TestCaseName", "Search SuperUser Can Search for Vacancies")]
        [TestProperty("TestCaseDescription", "The search popover displays for super users")]
        [TestProperty("UsesHardcodedData", "true")]
        public void Search_SuperUser_Can_Search_for_JobPostings()
        {
            // page objects
            var toolbar = ToolbarFactory.Get(Driver);

            // workflows
            var loginWorkflow = new LoginWorkflows(Driver);

            try
            {
                loginWorkflow.LoginAsSuperUser();
                test.Log(LogStatus.Pass, "Log in as super user");

                toolbar.ClickSearch();
                test.Log(LogStatus.Pass, "Click inside the search box");

                Assert.IsTrue(toolbar.SearchPage.IsDisplayed(), "The search is not displayed");
                test.Log(LogStatus.Pass, "The search is displayed");

                // Enter in a search that should find something
                toolbar.SearchPage.EnterSearchText("1");

                Assert.IsTrue(toolbar.SearchPage.JobPostingsAreDisplayed(), "The job posting results are not displayed");
                test.Log(LogStatus.Pass, "The job posting results are displayed");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Search")]
        [TestProperty("TestCaseName", "Search SuperUser Can Search for Users")]
        [TestProperty("TestCaseDescription", "The search popover displays for super users")]
        [TestProperty("UsesHardcodedData", "true")]
        public void Search_SuperUser_Can_Search_for_Users()
        {
            // page objects
            var toolbar = ToolbarFactory.Get(Driver);

            // workflows
            var loginWorkflow = new LoginWorkflows(Driver);

            try
            {
                loginWorkflow.LoginAsSuperUser();
                test.Log(LogStatus.Pass, "Log in as super user");

                toolbar.ClickSearch();
                test.Log(LogStatus.Pass, "Click inside the search box");

                Assert.IsTrue(toolbar.SearchPage.IsDisplayed(), "The search is not displayed");
                test.Log(LogStatus.Pass, "The search is displayed");

                // Enter in a search that should find something
                toolbar.SearchPage.EnterSearchText("1");

                Assert.IsTrue(toolbar.SearchPage.UsersAreDisplayed(), "The user results are not displayed");
                test.Log(LogStatus.Pass, "The user results are displayed");
            }
            catch (Exception e)
            {

                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Search")]
        [TestProperty("TestCaseName", "Search Standard User Can Search for Applicants")]
        [TestProperty("TestCaseDescription", "A standard user can search for applicants")]
        [TestProperty("UsesHardcodedData", "true")]
        public void Search_StandardUser_Can_Search_for_Applicants()
        {
            // page objects
            var toolbar = ToolbarFactory.Get(Driver);

            // workflows
            var loginWorkflow = new LoginWorkflows(Driver);

            try
            {
                loginWorkflow.LoginAsStandardUser();
                test.Log(LogStatus.Pass, "Log in as a standard user");

                toolbar.ClickSearch();
                test.Log(LogStatus.Pass, "Click inside the search box");

                Assert.IsTrue(toolbar.SearchPage.IsDisplayed(), "The search is not displayed");
                test.Log(LogStatus.Pass, "Search is displayed");

                // Enter in a search that should find something
                toolbar.SearchPage.EnterSearchText("1");

                Assert.IsTrue(toolbar.SearchPage.ApplicantsAreDisplayed(), "The applicant results are not displayed");
                test.Log(LogStatus.Pass, "The employee results are displayed");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Search")]
        [TestProperty("TestCaseName", "Search Standard User Can Search for Applicants")]
        [TestProperty("TestCaseDescription", "A routing user can search for applicants")]
        [TestProperty("UsesHardcodedData", "true")]
        public void Search_RoutingUser_Can_Search_for_Applicants()
        {
            // page objects
            var toolbar = ToolbarFactory.Get(Driver);

            // workflows
            var loginWorkflow = new LoginWorkflows(Driver);

            try
            {
                loginWorkflow.LoginAsRoutingUser();
                test.Log(LogStatus.Pass, "Log in as a routing user");

                toolbar.ClickSearch();
                test.Log(LogStatus.Pass, "Click inside the search box");

                Assert.IsTrue(toolbar.SearchPage.IsDisplayed(), "The search is not displayed");
                test.Log(LogStatus.Pass, "The search is displayed");

                // Enter in a search that should find something
                toolbar.SearchPage.EnterSearchText("1");

                Assert.IsTrue(toolbar.SearchPage.ApplicantsAreDisplayed(), "The applicant results are not displayed");
                test.Log(LogStatus.Pass, "The applicant results are displayed");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Search")]
        [TestProperty("TestCaseName", "Search Advanced Search for Applicants Opens")]
        [TestProperty("TestCaseDescription", "The Advanced Search link for Applicants opens the correct page")]
        [TestProperty("UsesHardcodedData", "true")]
        public void Search_AdvancedSearch_for_Applicants_Opens()
        {
            // page objects
            var toolbar = ToolbarFactory.Get(Driver);
            var applicantAdvancedSearchPage = new ApplicantAdvancedSearchPage(Driver);

            // workflows
            var loginWorkflow = new LoginWorkflows(Driver);

            try
            {
                loginWorkflow.LoginAsSuperUser();
                test.Log(LogStatus.Pass, "Log in as super user");

                toolbar.ClickSearch();
                test.Log(LogStatus.Info, "Click inside the search box");

                toolbar.SearchPage.ClickApplicantsAdvancedSearch();
                test.Log(LogStatus.Info, "Click Advanced Search for Applicants");

                _commonActions.SwitchToMainContentsIFrame();

                Assert.IsTrue(applicantAdvancedSearchPage.IsDisplayed(), "The Applicant Advanced Search page did not open");
                test.Log(LogStatus.Pass, "The Advanced Search page for Applicants opens");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Search")]
        [TestProperty("TestCaseName", "Search Advanced Search for Employees Opens")]
        [TestProperty("TestCaseDescription", "The Advanced Search link for Employees opens the correct page")]
        [TestProperty("UsesHardcodedData", "true")]
        public void Search_AdvancedSearch_for_Employees_Opens()
        {
            // page objects
            var toolbar = ToolbarFactory.Get(Driver);
            var employeeAdvancedSearchPage = new EmployeeAdvancedSearchPage(Driver);

            // workflows
            var loginWorkflow = new LoginWorkflows(Driver);

            try
            {
                loginWorkflow.LoginAsSuperUser();
                test.Log(LogStatus.Info, "Log in as super user");

                toolbar.ClickSearch();
                test.Log(LogStatus.Info, "Click inside the search box");

                toolbar.SearchPage.ClickEmployeesAdvancedSearch();
                test.Log(LogStatus.Info, "Click Advanced Search for Employees");

                _commonActions.SwitchToMainContentsIFrame();

                Assert.IsTrue(employeeAdvancedSearchPage.IsDisplayed(), "The Employee Advanced Search page did not open");
                test.Log(LogStatus.Pass, "The Advanced Search page for Employees opens");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Search")]
        [TestProperty("TestCaseName", "Applicant Advanced Search Route")]
        [TestProperty("TestCaseDescription", "Validate that the route for Advanced Applicant Search opens the correct page")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Search_AdvancedSearch_for_Applicants_Route()
        {
            // page objects
            var applicantAdvancedSearchPage = new ApplicantAdvancedSearchPage(Driver);

            // workflows
            var loginWorkflow = new LoginWorkflows(Driver);

            try
            {
                loginWorkflow.LoginAsSuperUser();
                test.Log(LogStatus.Info, "Log in as super user");

                var appSearchUrl = $"{Driver.Url}?Destination=AdvancedAppSearch";
                Driver.Navigate().GoToUrl(appSearchUrl);
                test.Log(LogStatus.Info, $"Navigate to {appSearchUrl}");

                _commonActions.SwitchToMainContentsIFrame();

                Assert.IsTrue(applicantAdvancedSearchPage.IsDisplayed(), "The Applicant Advanced Search page did not open");
                test.Log(LogStatus.Pass, "The Advanced Search page for Applicants opens");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Search")]
        [TestProperty("TestCaseName", "Employee Advanced Search Route")]
        [TestProperty("TestCaseDescription", "Validate that the route for Advanced Employee Search opens the correct page")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Search_AdvancedSearch_for_Employees_Route()
        {
            // page objects
            var employeeAdvancedSearchPage = new EmployeeAdvancedSearchPage(Driver);

            // workflows
            var loginWorkflow = new LoginWorkflows(Driver);

            try
            {
                loginWorkflow.LoginAsSuperUser();
                test.Log(LogStatus.Info, "Log in as super user");

                var empSearchUrl = $"{Driver.Url}?Destination=AdvancedEmpSearch";
                Driver.Navigate().GoToUrl(empSearchUrl);
                test.Log(LogStatus.Info, $"Navigate to {empSearchUrl}");

                _commonActions.SwitchToMainContentsIFrame();

                Assert.IsTrue(employeeAdvancedSearchPage.IsDisplayed(), "The Employee Advanced Search page did not open");
                test.Log(LogStatus.Pass, "The Advanced Search page for Employees opens");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Search")]
        [TestProperty("TestCaseName", "Employee Advanced Search Route as Routings Only User")]
        [TestProperty("TestCaseDescription", "Validate that the route for Advanced Employee Search opens the correct page as a Routings Only user")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Search_AdvancedSearch_for_Employees_Route_as_RoutingOnly()
        {
            // page objects
            var employeeAdvancedSearchPage = new EmployeeAdvancedSearchPage(Driver);

            // workflows
            var loginWorkflow = new LoginWorkflows(Driver);

            try
            {
                loginWorkflow.LoginAsRoutingUser();
                test.Log(LogStatus.Info, "Log in as routing user");

                var empSearchUrl = $"{Driver.Url}?Destination=AdvancedEmpSearch";
                Driver.Navigate().GoToUrl(empSearchUrl);
                test.Log(LogStatus.Info, $"Navigate to {empSearchUrl}");

                _commonActions.SwitchToMainContentsIFrame();

                Assert.IsTrue(employeeAdvancedSearchPage.IsDisplayed(), "The Employee Advanced Search page did not open");
                test.Log(LogStatus.Pass, "The Advanced Search page for Employees opens");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        #endregion
    }
}