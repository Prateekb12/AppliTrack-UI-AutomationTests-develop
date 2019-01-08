using System;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide.Applicants;
using ApplitrackUITests.PageObjects.AdminSide.CommonPages;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using ApplitrackUITests.PageObjects.ApplicantSide.EmploymentApplication;
using ApplitrackUITests.PageObjects.Menu;
using ApplitrackUITests.WorkFlows;
using Automation.Framework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.ApplicantTests
{
    [TestClass]
    public class ApplicantSmokeTests : ApplitrackUIBase
    {
        #region Setup and TearDown

        private IWebDriver _driver;
        private ExtentTest test;

        /// <summary>
        /// Runs before each test is executed
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            // extent reports setup
            test = ExtentTestManager.StartTest(TestContext.Properties["TestCaseName"].ToString(),
                TestContext.Properties["TestCaseDescription"].ToString())
                .AssignCategory("Smoke");

            // browser setup
            _driver = SetUp(_BT);
            _driver.Manage().Window.Maximize();

        }

        /// <summary>
        /// Runs after each test has executed
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
        [TestProperty("TestArea", "Applicant")]
        [TestProperty("TestCaseName", "Applicant Send Email")]
        [TestProperty("TestCaseDescription", "Send an email to an applicant using the Email button")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Applicant_Send_Email()
        {
            // pages
            var applicantProfilePage = new ApplicantProfilePages(_driver);
            var applicantMenu = new ApplicantAdminMenu(_driver);
            var email = new EmailPage(_driver);
            var landingPage = new DefaultLandingPage(_driver);
            var applicantPages = new ApplicantPages(_driver);

            // workflows
            var searchWorkflows = new SearchWorkflows(_driver);
            var applicationWorkflows = new ApplicationWorkflows(_driver, test);

            // test data
            var applicantData = new ApplicantGenerator();
            var emailBody = Faker.TextFaker.Sentence();

            try
            {
                // create applicant
                BrowseTo(BaseUrls["ApplitrackLandingPage"], _driver);
                landingPage.ClickExternalLogin();
                _driver.SwitchToPopup();
                applicantPages.ClickEmploymentApplicationTab();
                applicationWorkflows.FillOutPersonalInfo(applicantData);
                applicantPages.EmploymentApplicationPages.ClickNextPage();
                applicantPages.EmploymentApplicationPages.ClickPostalAddress();
                applicationWorkflows.FillOutPermanentAddress(applicantData.Address);
                _driver.SwitchToDefaultFrame();
                _driver.SwitchToFrameById("AppDataPage");
                applicantData.AppNo = applicantPages.EmploymentApplicationPages.GetAppNo();
                test.Log(LogStatus.Info, "AppNo is: " + applicantData.AppNo);
                _driver.SwitchToDefaultFrame();
                applicantPages.EmploymentApplicationPages.ClickSaveAsDraft();
                _driver.ClosePopup();

                // login
                BrowseTo(BaseUrls["ApplitrackLoginPage"], _driver);
                test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);
                var loginWorkflow = new LoginWorkflows(_driver);
                loginWorkflow.LoginAsSuperUser();

                // Open the applicant page
                BrowseTo($"{_driver.Url}?AppNo={applicantData.AppNo}", _driver);
                _driver.SwitchToFrameById("App"+applicantData.AppNo);

                // Click the Email button
                applicantProfilePage.Toolbar.ClickEmailButton();
                test.Log(LogStatus.Pass, "Click the Email button");

                // Send the email
                _driver.SwitchToPopup();
                _driver.SwitchToFrameByClass("cke_wysiwyg_frame");
                email.EnterEmailBody(emailBody);
                _driver.SwitchToDefaultFrame();
                email.ClickSendMessageButton();
                test.Log(LogStatus.Pass, "Click the Send Message button");

                // Assert that the email address is displayed on the page
                Assert.IsTrue(email.IsEmailAddressDisplayed(applicantData.Email), "Expected email: " + applicantData.Email + " is not on the screen");
                test.Log(LogStatus.Pass, "The email address: " + applicantData.Email + " appears on the page");
                _driver.ClosePopup();

                _driver.SwitchToDefaultFrame();
                _driver.SwitchToFrameById("App"+applicantData.AppNo);
                applicantMenu.ClickCommuncationLog();

                _driver.SwitchToFrameById("MainContentsIFrame");

                Assert.IsTrue(applicantProfilePage.CommunicationLogPage.IsCommunicationDisplayed(emailBody), "Email is not displayed on the Communication Log page");
                test.Log(LogStatus.Pass, "Email is displayed on the Communication Log page");
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
