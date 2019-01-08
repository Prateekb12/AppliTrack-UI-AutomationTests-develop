using System;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.ApplicantSide;
using ApplitrackUITests.PageObjects.ApplicantSide.EmploymentApplication;
using ApplitrackUITests.WorkFlows;
using Automation.Framework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.TeacherFitTests
{
    [TestClass]
    public class TeacherFitSmokeTests : ApplitrackUIBase
    {
        #region Setup and Teardown

        private IWebDriver Driver;
        private ExtentTest test;

        // the job code will change depending on the environment
        int vacancyDesired = TestEnvironment.CurrentEnvironment == EnvironmentType.AwsQA ? 8027 : 8183;

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
            BrowseTo(BaseUrls["ApplitrackLandingPage"], Driver); // BaseUrls["ApplitrackLandingPage"] is the landing page

            test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLandingPage"]);
        }

        [TestCleanup]
        public void TestTearDown()
        {
            BaseTearDown(Driver);
        }

        #endregion

        #region Test Cases

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "TeacherFit")]
        [TestProperty("TestCaseName", "TeacherFit Page Displayed as New External Applicant")]
        [TestProperty("TestCaseDescription", "Make sure that the TeacherFit page is displayed for new external applicants")]
        [TestProperty("UsesHardcodedData", "true")]
        public void TeacherFit_Displayed_for_New_Applicant()
        {
            // page objects
            var landingPage = new DefaultLandingPage(Driver);
            var applicantPages = new ApplicantPages(Driver);

            // workflows
            var applicationWorkflows = new ApplicationWorkflows(Driver, test);

            // applicant data
            var applicantData = new ApplicantGenerator();

            try
            {
                // create new applicant
                landingPage.ClickExternalLogin();
                test.Log(LogStatus.Pass, "Click 'External Login'");

                Driver.SwitchToPopup();
                applicantPages.ClickEmploymentApplicationTab();
                test.Log(LogStatus.Pass, "Click the 'Employment Application' tab");

                applicationWorkflows.FillOutPersonalInfo(applicantData);

                applicantPages.EmploymentApplicationPages.ClickNextPage();
                test.Log(LogStatus.Pass, "Click 'Next Page'");

                applicantPages.EmploymentApplicationPages.ClickVacancyDesired();
                test.Log(LogStatus.Pass, "Click 'Vacancy Desired'");

                applicantPages.EmploymentApplicationPages.VacancyDesiredPage.SelectVacancy(vacancyDesired);
                test.Log(LogStatus.Pass, "Select job: " + vacancyDesired);

                applicantPages.EmploymentApplicationPages.ClickSaveAsDraft();
                test.Log(LogStatus.Pass, "Click 'Save as Draft'");

                // navigate to Teacher Fit
                applicantPages.EmploymentApplicationPages.ClickTeacherFit();
                test.Log(LogStatus.Pass, "Click 'TeacherFit'");

                // Assert that the page is displayed
                Assert.IsTrue(applicantPages.EmploymentApplicationPages.FitPages.IsDisplayed(), "The Teacher Fit starting page is not displayed");
                test.Log(LogStatus.Pass, "The teacher fit starting page is displayed");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "TeacherFit")]
        [TestProperty("TestCaseName", "TeacherFit SE Page Displayed as New External Applicant")]
        [TestProperty("TestCaseDescription", "Make sure that the TeacherFit SE page is displayed for new external applicants")]
        [TestProperty("UsesHardcodedData", "true")]
        public void TeacherFitSE_Displayed_for_New_Applicant()
        {
            // page objects
            var landingPage = new DefaultLandingPage(Driver);
            var applicantPages = new ApplicantPages(Driver);

            // workflows
            var applicationWorkflows = new ApplicationWorkflows(Driver, test);

            // applicant data
            var applicantData = new ApplicantGenerator();

            try
            {
                // create new applicant
                landingPage.ClickExternalLogin();
                test.Log(LogStatus.Pass, "Click 'External Login'");

                Driver.SwitchToPopup();
                applicantPages.ClickEmploymentApplicationTab();
                test.Log(LogStatus.Pass, "Click the 'Employment Application' tab");

                applicationWorkflows.FillOutPersonalInfo(applicantData);

                applicantPages.EmploymentApplicationPages.ClickNextPage();
                test.Log(LogStatus.Pass, "Click 'Next Page'");

                applicantPages.EmploymentApplicationPages.ClickVacancyDesired();
                test.Log(LogStatus.Pass, "Click 'Vacancy Desired'");

                applicantPages.EmploymentApplicationPages.VacancyDesiredPage.SelectVacancy(vacancyDesired);
                test.Log(LogStatus.Pass, "Select job: " + vacancyDesired);

                applicantPages.EmploymentApplicationPages.ClickSaveAsDraft();
                test.Log(LogStatus.Pass, "Click 'Save as Draft'");

                // navigate to Teacher Fit
                applicantPages.EmploymentApplicationPages.ClickTeacherFitSe();
                test.Log(LogStatus.Pass, "Click 'TeacherFit SE'");

                // Assert that the page is displayed
                Assert.IsTrue(applicantPages.EmploymentApplicationPages.FitPages.IsDisplayed(), "The TeacherFit SE starting page is not displayed");
                test.Log(LogStatus.Pass, "The TeacherFit SE starting page is displayed");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "TeacherFit")]
        [TestProperty("TestCaseName", "AdminFit Page Displayed as New External Applicant")]
        [TestProperty("TestCaseDescription", "Make sure that the AdminFit page is displayed for new external applicants")]
        [TestProperty("UsesHardcodedData", "true")]
        public void AdminFit_Displayed_for_New_Applicant()
        {
            // page objects
            var landingPage = new DefaultLandingPage(Driver);
            var applicantPages = new ApplicantPages(Driver);

            // workflows
            var applicationWorkflows = new ApplicationWorkflows(Driver, test);

            // applicant data
            var applicantData = new ApplicantGenerator();

            try
            {
                // create new applicant
                landingPage.ClickExternalLogin();
                test.Log(LogStatus.Pass, "Click 'External Login'");

                Driver.SwitchToPopup();
                applicantPages.ClickEmploymentApplicationTab();
                test.Log(LogStatus.Pass, "Click the 'Employment Application' tab");

                applicationWorkflows.FillOutPersonalInfo(applicantData);

                applicantPages.EmploymentApplicationPages.ClickNextPage();
                test.Log(LogStatus.Pass, "Click 'Next Page'");

                applicantPages.EmploymentApplicationPages.ClickVacancyDesired();
                test.Log(LogStatus.Pass, "Click 'Vacancy Desired'");

                applicantPages.EmploymentApplicationPages.VacancyDesiredPage.SelectVacancy(vacancyDesired);
                test.Log(LogStatus.Pass, "Select job: " + vacancyDesired);

                applicantPages.EmploymentApplicationPages.ClickSaveAsDraft();
                test.Log(LogStatus.Pass, "Click 'Save as Draft'");

                // navigate to Teacher Fit
                applicantPages.EmploymentApplicationPages.ClickAdminFit();
                test.Log(LogStatus.Pass, "Click 'AdminFit'");

                // Assert that the page is displayed
                Assert.IsTrue(applicantPages.EmploymentApplicationPages.FitPages.IsDisplayed(), "The AdminFit starting page is not displayed");
                test.Log(LogStatus.Pass, "The AdminFit starting page is displayed");
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