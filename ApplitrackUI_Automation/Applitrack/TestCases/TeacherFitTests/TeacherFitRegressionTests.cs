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
    public class TeacherFitRegressionTests : ApplitrackUIBase
    {
        #region Setup and Teardown

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
        [TestCategory("LongRegression")]
        [TestProperty("TestArea", "TeacherFit")]
        [TestProperty("TestCaseName", "Take TeacherFit as New External Applicant")]
        [TestProperty("TestCaseDescription", "Make sure that a new external applicant is able to complete the TeacherFit assessment")]
        [TestProperty("UsesHardcodedData", "true")]
        public void TeacherFit_New_Applicant()
        {
            // page objects
            var landingPage = new DefaultLandingPage(Driver);
            var applicantPages = new ApplicantPages(Driver);

            // workflows
            var applicationWorkflows = new ApplicationWorkflows(Driver, test);

            // data
            var applicantData = new ApplicantGenerator();
            var vacancyDesired = 1171;

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
                test.Log(LogStatus.Pass, "Select vacancy: " + vacancyDesired);

                applicantPages.EmploymentApplicationPages.ClickSaveAsDraft();
                test.Log(LogStatus.Pass, "Click 'Save As Draft'");

                // take teacherfit
                applicantPages.EmploymentApplicationPages.ClickTeacherFit();
                test.Log(LogStatus.Pass, "Click 'TeacherFit'");

                Assert.IsTrue(applicantPages.EmploymentApplicationPages.FitPages.IsDisplayed(), "The Teacher Fit starting page is not displayed");
                test.Log(LogStatus.Pass, "The teacher fit starting page is displayed");

                applicationWorkflows.CompleteFitAssessment();

                // assert that the assessment has been completed
                Assert.IsTrue(applicantPages.EmploymentApplicationPages.FitPages.AssessmentCompleted(), "The fit assessment has not been completed");
                test.Log(LogStatus.Pass, "The fit assessment has been completed successfully");

                // submit the application
                applicantPages.EmploymentApplicationPages.ClickFinishAndSubmit();
                test.Log(LogStatus.Pass, "Click 'Finish and Submit'");

                // assert that the screen does not indicate that the teacherfit assessment needs to be completed
                Assert.IsTrue(applicantPages.EmploymentApplicationPages.ConfirmationPage.StepIsCompleted("TeacherFit"), 
                    "The confirmation page indicates the TeacherFit assessment was not completed");
                test.Log(LogStatus.Pass, "The confirmation page indicates that the TeacherFit assessment was completed");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("LongRegression")]
        [TestProperty("TestArea", "TeacherFit")]
        [TestProperty("TestCaseName", "Take TeacherFit SE as New External Applicant")]
        [TestProperty("TestCaseDescription", "Make sure that a new external applicant is able to complete the TeacherFit SE assessment")]
        [TestProperty("UsesHardcodedData", "true")]
        public void TeacherFitSE_New_Application()
        {
            // page objects
            var landingPage = new DefaultLandingPage(Driver);
            var applicantPages = new ApplicantPages(Driver);

            // workflows
            var applicationWorkflows = new ApplicationWorkflows(Driver, test);

            // applicant data
            var applicantData = new ApplicantGenerator();
            var vacancyDesired = 1171;

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
                test.Log(LogStatus.Pass, "Select vacancy: " + vacancyDesired);

                applicantPages.EmploymentApplicationPages.ClickSaveAsDraft();
                test.Log(LogStatus.Pass, "Click 'Save as Draft'");

                // take teacherfit
                applicantPages.EmploymentApplicationPages.ClickTeacherFitSe();

                Assert.IsTrue(applicantPages.EmploymentApplicationPages.FitPages.IsDisplayed(), "The Teacher Fit starting page is not displayed");
                test.Log(LogStatus.Pass, "The admin fit starting page is displayed");

                applicationWorkflows.CompleteFitAssessment();

                // assert that the assessment has been completed
                Assert.IsTrue(applicantPages.EmploymentApplicationPages.FitPages.AssessmentCompleted(), "The fit assessment has not been completed");
                test.Log(LogStatus.Pass, "The fit assessment has been completed successfully");

                // submit the application
                applicantPages.EmploymentApplicationPages.ClickFinishAndSubmit();

                // assert that the screen does not indicate that the TeacherFit SE assessment needs to be completed
                Assert.IsTrue(applicantPages.EmploymentApplicationPages.ConfirmationPage.StepIsCompleted("TeacherFit SE"), 
                    "The confirmation page indicates the TeacherFit SE assessment was not completed");
                test.Log(LogStatus.Pass, "The confirmation page indicates that the TeacherFit SE assessment was completed");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("LongRegression")]
        [TestProperty("TestArea", "TeacherFit")]
        [TestProperty("TestCaseName", "Take AdminFit as New External Applicant")]
        [TestProperty("TestCaseDescription", "Make sure that a new external applicant is able to complete the AdminFit assessment")]
        [TestProperty("UsesHardcodedData", "true")]
        public void AdminFit_New_Application()
        {
            // page objects
            var landingPage = new DefaultLandingPage(Driver);
            var applicantPages = new ApplicantPages(Driver);

            // workflows
            var applicationWorkflows = new ApplicationWorkflows(Driver, test);

            // data
            var applicantData = new ApplicantGenerator();
            var vacancyDesired = 1171;

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
                test.Log(LogStatus.Pass, "Select vacancy: " + vacancyDesired);

                applicantPages.EmploymentApplicationPages.ClickSaveAsDraft();
                test.Log(LogStatus.Pass, "Click 'Save as Draft'");

                // take teacherfit
                applicantPages.EmploymentApplicationPages.ClickAdminFit();
                test.Log(LogStatus.Pass, "Click 'AdminFit'");

                Assert.IsTrue(applicantPages.EmploymentApplicationPages.FitPages.IsDisplayed(), "The Teacher Fit starting page is not displayed");
                test.Log(LogStatus.Pass, "The admin fit starting page is displayed");

                applicationWorkflows.CompleteFitAssessment();

                // assert that the assessment has been completed
                Assert.IsTrue(applicantPages.EmploymentApplicationPages.FitPages.AssessmentCompleted(), "The fit assessment has not been completed");
                test.Log(LogStatus.Pass, "The fit assessment has been completed successfully");

                // submit the application
                applicantPages.EmploymentApplicationPages.ClickFinishAndSubmit();

                // assert that the screen does not indicate that the teacherfit assessment needs to be completed
                Assert.IsTrue(applicantPages.EmploymentApplicationPages.ConfirmationPage.StepIsCompleted("AdminFit"), 
                    "The confirmation page indicates the AdminFit assessment was not completed");
                test.Log(LogStatus.Pass, "The confirmation page indicates that the AdminFit assessment was completed");
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
