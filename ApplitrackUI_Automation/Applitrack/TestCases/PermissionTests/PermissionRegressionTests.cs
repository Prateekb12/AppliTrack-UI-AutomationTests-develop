using System;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide.Applicants;
using ApplitrackUITests.PageObjects.Menu;
using ApplitrackUITests.WorkFlows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.PermissionTests
{
    [TestClass]
    public class PermissionRegressionTests : ApplitrackUIBase
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
        [TestProperty("TestArea", "Permissions")]
        [TestProperty("TestCaseName", "Permissions 'Fill Out Interview Questionnaire' button on 'Interviews' page displayed for superusers")]
        [TestProperty("TestCaseDescription", "The 'Fill Out Interview Questionnaire' button in the Interviews page of an applicant is displayed to superusers.")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Permissions_InterviewQuestionnaire_Button_Displayed()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            // pages
            var applicantProfilePage = new ApplicantProfilePages(Driver);
            var applicantMenu = new ApplicantAdminMenu(Driver);

            // workflows
            var searchWorkflows = new SearchWorkflows(Driver);
            var loginWorkflows = new LoginWorkflows(Driver);

            // applicant data
            const string appNo = "1";
            const string appName = "Sample Applicant";

            try  //Contains Contents of Test
            {
                //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

                loginWorkflows.LoginAsSuperUser();
                test.Log(LogStatus.Pass, "Logging in as a superuser");

                searchWorkflows.OpenApplicantUsingSearch(appNo, appName);
                Driver.SwitchToFrameById("App"+appNo);
                test.Log(LogStatus.Pass, "Search for and open applicant: " + appNo + appName);

                applicantMenu.ClickInterviews();
                test.Log(LogStatus.Pass, "Click Interviews");

                Assert.IsTrue(applicantProfilePage.Interviews.IsFillOutInterviewQuestionnaireVisible());
                test.Log(LogStatus.Pass, "The 'Fill Out Interview Questionnaire' button is visible");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Permissions")]
        [TestProperty("TestCaseName", "Permissions 'Fill Out Interview Questionnaire' button on 'Interviews' page not displayed for users without permission")]
        [TestProperty("TestCaseDescription", "The 'Fill Out Interview Questionnaire' button in the Interviews page of an applicant is not displayed to users without permission.")]
        [TestProperty("UsesHardcodedData", "true")]
        [Ignore] // TODO do not use Bill Nye
        public void Permissions_InterviewQuestionnaire_Button_Not_Displayed()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            // pages
            var applicantProfilePage = new ApplicantProfilePages(Driver);
            var applicantMenu = new ApplicantAdminMenu(Driver);

            // workflows
            var searchWorkflows = new SearchWorkflows(Driver);
            var loginWorkflows = new LoginWorkflows(Driver);

            // applicant data
            const string appNo = "1";
            const string appName = "Sample Applicant";

            try  //Contains Contents of Test
            {
                //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

                loginWorkflows.Login("BillNye", "BillNye");
                test.Log(LogStatus.Pass, "Logging in as BillNye");

                searchWorkflows.OpenApplicantUsingSearch(appNo, appName);
                Driver.SwitchToFrameById("App"+appNo);
                test.Log(LogStatus.Pass, "Search for and open applicant: " + appNo + appName);

                applicantMenu.ClickInterviews();
                test.Log(LogStatus.Pass, "Click Interviews");

                Assert.IsFalse(applicantProfilePage.Interviews.IsFillOutInterviewQuestionnaireVisible());
                test.Log(LogStatus.Pass, "The 'Fill Out Interview Questionnaire' button is visible");
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
