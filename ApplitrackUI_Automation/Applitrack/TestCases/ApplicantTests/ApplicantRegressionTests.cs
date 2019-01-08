using System;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide.Applicants;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using ApplitrackUITests.PageObjects.Toolbar;
using ApplitrackUITests.WorkFlows;
using Automation.Framework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.ApplicantTests
{
    [TestClass]
    public class ApplicantRegressionTests : ApplitrackUIBase
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
        [TestProperty("TestArea", "Applicant")]
        [TestProperty("TestCaseName", "Applicant Logins in Last 24 Hours")]
        [TestProperty("TestCaseDescription", "On the admin applicant dashboard, check that the 'Applicant Logins in Last 24 Hours' count is increased after an applicant logs in")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Applicant_Login_Count()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            // page objects
            var mainMenu = new MainMenu(Driver);
            var applicantMenu = new SubMenuApplicants(Driver);
            var applicantPages = new ApplicantAdminPages(Driver);
            var toolbar = ToolbarFactory.Get(Driver);

            var applicantProfilePage = new ApplicantProfilePages(Driver);

            // workflows
            var searchWorkflow = new SearchWorkflows(Driver);
            var loginWorkflow = new LoginWorkflows(Driver);

            // applicant data
            const string appNo = "1";
            const string appName = "Sample Applicant";

            try
            {
                //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

                // Navigate to Applicants > Applicant Dashboard
                mainMenu.ClickApplicants();
                applicantMenu.ClickApplicantDashboard();
                test.Log(LogStatus.Pass, "Navigate to Applicants > Applicant Dashboard");

                // Navigate to 'Overall Statistics' and get the value from 'Applicant Logins in Last 24 Hours'
                applicantPages.Dashboard.ClickOverallStatistics();
                test.Log(LogStatus.Pass, "Click 'Overall Statistics'");
                var actualCount = applicantPages.Dashboard.GetLoginCount();
                test.Log(LogStatus.Info, "Current login count: " + actualCount);

                // Login as an applicant
                searchWorkflow.OpenApplicantUsingSearch(appNo, appName);
                Driver.SwitchToFrameById("App"+appNo);
                applicantProfilePage.Toolbar.LoginAsApplicant();
                test.Log(LogStatus.Pass, "Log in as applicant");
                Driver.SwitchToPopup();
                Driver.ClosePopup();
                test.Log(LogStatus.Pass, "Login as " + appName);

                // The count should incease by 1 after logging in
                var expectedCount = actualCount + 1;

                // Go back to the applicant dashboard to get the new count
                // The count will NOT refresh unless the user logs out and logs back in again.
                toolbar.Logout();
                BrowseTo(BaseUrls["ApplitrackLoginPage"], Driver); // go back to the login page
                loginWorkflow.LoginAsSuperUser();
                mainMenu.ClickApplicants();
                applicantMenu.ClickApplicantDashboard();
                applicantPages.Dashboard.ClickOverallStatistics();
                actualCount = applicantPages.Dashboard.GetLoginCount(); // get the new count
                test.Log(LogStatus.Info, "New login count: " + actualCount);

                // Assert that the count was increased
                Assert.AreEqual(expectedCount, actualCount, "The expected count: " + expectedCount + " does not match the actual count: " + actualCount);
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Applicant")]
        [TestProperty("TestCaseName", "Applicant Admin Dashboard Displays 'Applicant Dashboard Activity and Statistics'")]
        [TestProperty("TestCaseDescription", "On the admin applicant dashboard, check that the 'Applicant Logins in Last 24 Hours' count is increased after an applicant logs in")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Applicant_Admin_Dashboard_Text()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            // page objects
            var mainMenu = new MainMenu(Driver);
            var applicantMenu = new SubMenuApplicants(Driver);
            var applicantPages = new ApplicantAdminPages(Driver);

            try
            {
                //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

                // Navigate to Applicants > Applicant Dashboard
                mainMenu.ClickApplicants();
                applicantMenu.ClickApplicantDashboard();
                test.Log(LogStatus.Pass, "Navigate to Applicants > Applicant Dashboard");

                var expectedHeader = "Applicant Dashboard";
                var expectedSubHeader = "Activity and Statistics";

                var actualText = applicantPages.Dashboard.GetDashboardHeaderText();

                Assert.IsTrue(actualText.Contains(expectedHeader) && actualText.Contains(expectedSubHeader),
                    "The applicant dashboard does not contain {0} {1}", expectedHeader, expectedSubHeader);
                test.Log(LogStatus.Pass, "The text: " + expectedHeader + expectedSubHeader + " appears on the page");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Applicant")]
        [TestProperty("TestCaseName", "Applicant Quick Look Displays Resume")]
        [TestProperty("TestCaseDescription", "Make sure a resume is displayed on the Quick Look page for an applicant on the admin side")]
        [TestProperty("UsesHardcodedData", "true")]
        [Ignore] // TODO figure out why this is flaky
        public void Applicant_QuickLook_Displays_Resume()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            // page objects
            var applicantProfilePage = new ApplicantProfilePages(Driver);

            // workflows
            var searchWorkflow = new SearchWorkflows(Driver);

            // applicant data
            const string appNo = "1347";
            const string appName = "Rebecca J Bretz";

            try
            {
                //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

                // Open the applicant profile page
                searchWorkflow.OpenApplicantUsingSearch(appNo, appName);
                Driver.SwitchToFrameById("App"+appNo);
                test.Log(LogStatus.Pass, "Open profile page for applicant: " + appNo + appName);

                // Assert that a valid resume image appears
                Assert.IsTrue(applicantProfilePage.QuickLook.IsResumeDisplayed(), "The resume is not displayed correctly on the Quick Look page");
                test.Log(LogStatus.Pass, "A resume image is displayed on the Quick Look page");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        // Ignored as the way chrome renders PDFs is not readable by selenium
        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Applicant")]
        [TestProperty("TestCaseName", "Applicant Online Application Text is Displayed")]
        [TestProperty("TestCaseDescription", "On the admin applicant page, make sure 'Online Application' appears on the Online Application page")]
        [TestProperty("UsesHardcodedData", "true")]
        [Ignore]
        public void Applicant_Online_Application_Text()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            // page objects
            var applicantMenu = new SubMenuApplicants(Driver);
            var applicantProfilePage = new ApplicantProfilePages(Driver);

            // workflows
            var searchWorkflow = new SearchWorkflows(Driver);

            // applicant data
            const string appNo = "1";
            const string appName = "Sample Applicant";

            try
            {
                //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

                // Open applicant profile page
                searchWorkflow.OpenApplicantUsingSearch(appNo, appName);
                test.Log(LogStatus.Pass, "Search for and open applicant: " + appName);

                // Click Online Application
                Driver.SwitchToFrameById("App"+appNo);
                applicantMenu.ClickOnlineApplication();
                test.Log(LogStatus.Pass, "Click 'Online Application'");

                // Assert that 'Online Application' appears
                Driver.SwitchToFrameById("MainContentsIFrame");
                var expectedText = "Online Application";
                var actualText = applicantProfilePage.OnlineApplication.GetHeaderText();
                Assert.IsTrue(actualText.Contains(expectedText), "The PDF does not contain: " + expectedText + " Actual text: " + actualText);
                test.Log(LogStatus.Pass, "The PDF contains: '" + expectedText + "'");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Applicant")]
        [TestProperty("TestCaseName", "Applicant PDF Rotate Controls")]
        [TestProperty("TestCaseDescription", "Make sure the rotate controls exist for PDFs for the References pages in applicant profiles")]
        [TestProperty("UsesHardcodedData", "true")]
        [Ignore] // TODO figure out why this is flaky
        public void Applicant_PDF_RotateControls()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            // page objects
            var applicantMenu = new SubMenuApplicants(Driver);
            var applicantProfilePage = new ApplicantProfilePages(Driver);

            // workflows
            var searchWorkflow = new SearchWorkflows(Driver);

            // applicant data
            const string appNo = "1029";
            const string appName = "Sample Applicant";

            try
            {
                //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

                // Open applicant profile page
                searchWorkflow.OpenApplicantUsingSearch(appNo, appName);
                test.Log(LogStatus.Pass, "Search for and open applicant: " + appName);

                // Click Online Application
                Driver.SwitchToFrameById("App"+appNo);
                applicantMenu.ClickReferences();
                test.Log(LogStatus.Pass, "Click 'References'");

                // Assert that rotate buttons are displayed 
                applicantProfilePage.ReferencesPage.ClickItem("Reference Letter");

                Driver.SwitchToFrameById("MainContentsIFrame");
                Assert.IsTrue(applicantProfilePage.ReferencesPage.PdfRotateButtonsDisplayed());
                test.Log(LogStatus.Pass, "The rotate buttons are displayed");

                // Click the 'Right' rotate button
                applicantProfilePage.ReferencesPage.ClickRotateRight();
                test.Log(LogStatus.Pass, "Click the 'Right' rotate button");

                // Click the 'Left' rotate button
                applicantProfilePage.ReferencesPage.ClickRotateLeft();
                test.Log(LogStatus.Pass, "Click the 'Left' rotate button");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Applicant")]
        [TestProperty("TestCaseName", "Applicant Vacancies by Category Count")]
        [TestProperty("TestCaseDescription", "Make sure that the Vacancies by Category pages displays the correct job and the correct number of applicants")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Applicant_VacanciesByCategory_Count()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            // page objects
            var mainMenu = new MainMenu(Driver);
            var applicantMenu = new SubMenuApplicants(Driver);
            var applicantPages = new ApplicantAdminPages(Driver);

            try
            {
                //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

                // Navigate to Applicants > Vacancies by Category
                mainMenu.ClickApplicants();
                applicantMenu.ClickVacanciesByCategory();
                test.Log(LogStatus.Pass, "Navigate to Applicants > Vacancies by Category");

                // Go through each category listed on the menu
                foreach (var category in applicantMenu.GetMenuItems())
                {
                    applicantMenu.ClickCategory(category);
                    test.Log(LogStatus.Info, "Click '" + category + "'");

                    // Click the first job
                    applicantMenu.ClickJob();
                    test.Log(LogStatus.Pass, "Click the first job");

                    // Get the Job ID and count from the menu
                    var menuJobId = applicantMenu.GetJobIdHeader();
                    var menuApplicantCount = applicantMenu.GetMenuCount();

                    test.Log(LogStatus.Info, "Job ID from the menu: " + menuJobId);
                    test.Log(LogStatus.Info, "Applicant count from the menu: " + menuApplicantCount);

                    // Click View All Applicants
                    applicantMenu.ClickViewAllApplicants();
                    test.Log(LogStatus.Pass, "Click 'View all applicants'");

                    // Assert that the header contains the correct Job ID / Job Title and applicant count
                    Driver.SwitchToFrameById("MainContentsIFrame");
                    var tableJobId = applicantPages.VacanciesByCategoryPages.GetTableJobId();
                    var tableApplicantCount = applicantPages.VacanciesByCategoryPages.GetTableApplicantCount();

                    test.Log(LogStatus.Info, "Job ID from the table header: " + tableJobId);
                    test.Log(LogStatus.Info, "Applicant count from the table header: " + tableApplicantCount);

                    Assert.AreEqual(menuApplicantCount, tableApplicantCount, "The applicant count from the menu differs from the count displayed in the header");
                    test.Log(LogStatus.Pass, "The applicant count from the menu matches the applicant count from the table header");

                    Assert.AreEqual(menuJobId, tableJobId, "The Job ID from the menu differs from the Job ID displayed in the header");
                    test.Log(LogStatus.Pass, "The Job ID from the menu matches the Job ID displayed in the header");

                    // Go back to the Vacancies by Category menu
                    Driver.SwitchToDefaultFrame();
                    mainMenu.ClickBackIcon();
                    mainMenu.ClickBackIcon();
                    mainMenu.ClickBackIcon();
                }
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Applicant")]
        [TestProperty("TestCaseName", "Applicant Vacancies by Location Count")]
        [TestProperty("TestCaseDescription", "Make sure that the Vacancies by Location pages displays the correct job and the correct number of applicants")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Applicant_VacanciesByLocation_Count()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            // page objects
            var mainMenu = new MainMenu(Driver);
            var applicantMenu = new SubMenuApplicants(Driver);
            var applicantPages = new ApplicantAdminPages(Driver);

            try
            {
                //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

                // Navigate to Applicants > Vacancies by Category
                mainMenu.ClickApplicants();
                applicantMenu.ClickVacanciesByLocation();
                test.Log(LogStatus.Pass, "Navigate to Applicants > Vacancies by Location");

                foreach (var location in applicantMenu.GetMenuItems())
                {
                    // Click No Location 
                    applicantMenu.ClickCategory(location);
                    test.Log(LogStatus.Pass, "Click '" + location + "'");

                    // Click the first job
                    applicantMenu.ClickJob();

                    // Get the Job ID and count from the menu
                    var menuJobId = applicantMenu.GetJobIdHeader();
                    var menuApplicantCount = applicantMenu.GetMenuCount();

                    test.Log(LogStatus.Info, "Job ID from the menu: " + menuJobId);
                    test.Log(LogStatus.Info, "Applicant count from the menu: " + menuApplicantCount);

                    // Click View All Applicants
                    applicantMenu.ClickViewAllApplicants();
                    test.Log(LogStatus.Pass, "Click 'View all applicants'");

                    // Assert that the header contains the correct Job ID / Job Title and applicant count
                    Driver.SwitchToFrameById("MainContentsIFrame");
                    var tableJobId = applicantPages.VacanciesByCategoryPages.GetTableJobId();
                    var tableApplicantCount = applicantPages.VacanciesByCategoryPages.GetTableApplicantCount();

                    test.Log(LogStatus.Info, "Job ID from the table header: " + tableJobId);
                    test.Log(LogStatus.Info, "Applicant count from the table header: " + tableApplicantCount);

                    Assert.AreEqual(menuApplicantCount, tableApplicantCount, "The applicant count from the menu differs from the count displayed in the header");
                    test.Log(LogStatus.Pass, "The applicant count from the menu matches the applicant count from the table header");

                    Assert.AreEqual(menuJobId, tableJobId, "The Job ID from the menu differs from the Job ID displayed in the header");
                    test.Log(LogStatus.Pass, "The Job ID from the menu matches the Job ID displayed in the header");

                    // Go back to the Vacancies by Location menu
                    Driver.SwitchToDefaultFrame();
                    mainMenu.ClickBackIcon();
                    mainMenu.ClickBackIcon();
                    mainMenu.ClickBackIcon();
                }
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        #endregion // test cases
    }
}
