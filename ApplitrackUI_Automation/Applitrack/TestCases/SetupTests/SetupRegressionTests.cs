using System;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using ApplitrackUITests.PageObjects.AdminSide.Setup;
using ApplitrackUITests.PageObjects.ApplicantSide;
using ApplitrackUITests.PageObjects.ApplicantSide.EmploymentApplication;
using ApplitrackUITests.WorkFlows;
using Automation.Framework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.SetupTests
{
    [TestClass]
    public class SetupRegressionTests : ApplitrackUIBase
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
        [TestProperty("TestArea", "Setup")]
        [TestProperty("TestCaseName", "Setup Applicant Settings Edit Section Title")]
        [TestProperty("TestCaseDescription", "Edit the Higher Education section title for the Education application page and make sure the changes were saved properly.")]
        [TestProperty("UsesHardcodedData", "true")]
        public void Setup_ApplicantSettings_Edit_Section_Title()
        {
            // page objects
            var mainMenu = new MainMenu(Driver);
            var setupMenu = new SubMenuSetup(Driver);
            var applicantPages = new ApplicantPages(Driver);
            var setupPages = new SetupPages(Driver);

            // test data
            const string pageName = "Education";
            var originalSectionTitle = "Colleges, Universities, and Technical Schools Attended";
            var applicationPageData = new ApplicationPageData();
            var expectedSectionTitle = originalSectionTitle + applicationPageData.SectionTitle;

            try
            {
                // Navigate to Setup > Applicant Settings > Manage Application Pages > Manage Internal Pages
                mainMenu.ClickSetup();
                setupMenu.ClickApplicantSettings();
                setupMenu.ClickManageApplicationPages();
                setupMenu.ClickManageInternalPages();
                test.Log(LogStatus.Pass, "Navigate to Setup > Applicant Settings > Manage Application Pages > Manage Internal Pages");

                // Click 'Edit' for the 'Education' page
                Driver.SwitchToFrameById("MainContentsIFrame");
                setupPages.ApplicantSettingsPages.ManageInternalPages.ClickEdit(pageName);
                test.Log(LogStatus.Pass, "Click on the edit link for " + pageName);

                // Under 'Higher Education', change the 'Section Title'
                setupPages.ApplicantSettingsPages.EditPage.EnterHigherEducationTitle(expectedSectionTitle);
                test.Log(LogStatus.Pass, "Enter: '" + expectedSectionTitle + "' into the Section Title for Higher Education");

                setupPages.ApplicantSettingsPages.EditPage.ClickSaveChanges();
                test.Log(LogStatus.Pass, "Click 'Save Changes'");

                // View the page and assert that the title changed
                setupPages.ApplicantSettingsPages.EditPage.ClickPreviewPage();
                test.Log(LogStatus.Pass, "Click 'Preview Page'");

                Driver.SwitchToPopup();

                Assert.IsTrue(applicantPages.EmploymentApplicationPages.SectionTitleIsOnScreen(expectedSectionTitle), 
                    expectedSectionTitle + " was not on the screen");
                test.Log(LogStatus.Pass, expectedSectionTitle + " was on the screen.");

                // Change the section title back to the original
                Driver.ClosePopup();
                Driver.SwitchToFrameById("MainContentsIFrame");
                setupPages.ApplicantSettingsPages.EditPage.EnterHigherEducationTitle(originalSectionTitle);
                setupPages.ApplicantSettingsPages.EditPage.ClickSaveChanges();
                test.Log(LogStatus.Info, "Changed section title back to: " + originalSectionTitle);
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
