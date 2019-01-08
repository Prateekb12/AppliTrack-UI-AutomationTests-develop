using System;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide.Applicants;
using ApplitrackUITests.PageObjects.AdminSide.Forms;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using ApplitrackUITests.PageObjects.ApplicantSide;
using ApplitrackUITests.PageObjects.ApplicantSide.EmploymentApplication;
using ApplitrackUITests.PageObjects.Menu;
using ApplitrackUITests.WorkFlows;
using Automation.Framework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;


namespace ApplitrackUITests.TestCases.FormTests
{
    [TestClass]
    public class FormRegressionTests : ApplitrackUIBase
    {

        #region Test Setup and TearDown

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

        // This is ignored because the form wizard sometimes requires clicking 'Next Page' twice, but sometimes it works after clicking it once
        // This was causing the test to fail
        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Forms")]
        [TestProperty("TestCaseName", "Form Send to Applicant - Deny")]
        [TestProperty("TestCaseDescription", "Send the 'Approve and Deny Form for testing' to an applicant, test deny functionality.")]
        [TestProperty("UsesHardcodedData", "true")]
        [Ignore]
        public void Form_SendTo_Applicant_Deny()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var mainMenu = new MainMenu(Driver);
            var formMenu = new SubMenuForms(Driver);
            var formPages = new FormPages(Driver);
            var applicantProfilePage = new ApplicantProfilePages(Driver);
            var applicantPage = new ApplicantPages(Driver);
            var applicantMenu = new ApplicantAdminMenu(Driver);
            var applicantProfilePages = new ApplicantProfilePages(Driver);

            var searchWorkflows = new SearchWorkflows(Driver);
            var formWorkflows = new FormWorkflows(Driver, test);

            try //Contains Contents of Test
            {
                const int formId = 896;
                const string formName = "Deny Form Automated Testing";
                const string appNo = "1";
                const string appName = "Sample Applicant";
                const string employeeNo = "484";
                const string employeeName = "Automation Employee";

                //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

                // Navigate to Forms > Send a Form
                mainMenu.ClickForms();
                formMenu.ClickSendForm();
                test.Log(LogStatus.Pass, "Navigate to Forms > Send a Form");

                // Select "Approval and Deny Form for testing"
                Driver.SwitchToFrameById("MainContentsIFrame");
                formPages.SendFormPage.SelectFormToSendById(formId);
                test.Log(LogStatus.Pass, "Select form: " + formId);

                formPages.SendFormPage.ClickContinueWithSelectedForms();
                test.Log(LogStatus.Pass, "Click 'Continue with Selected Forms'");

                // Assign the form to appno1
                formPages.SendFormPage.SelectAssociatedApplicant(appNo);
                test.Log(LogStatus.Pass, "Select Applicant Number: " + appNo);

                // Assign the form to an employee
                formPages.SendFormPage.SelectAssociatedEmployee(employeeNo);
                test.Log(LogStatus.Pass, "Select Employee Number: " + employeeNo);

                formPages.SendFormPage.ClickNext();
                test.Log(LogStatus.Pass, "Click 'Next'");

                // Send the form
                formPages.SendFormPage.ClickFinishAndDeliver();
                test.Log(LogStatus.Pass, "Click 'Finish and Deliver'");

                // Open the applicant page
                searchWorkflows.OpenApplicantUsingSearch(appNo, appName);
                test.Log(LogStatus.Pass, "Opened applicant page for: " + appNo + " " + appName);

                // Login as applicant
                Driver.SwitchToFrameById("App"+appNo);
                applicantProfilePage.Toolbar.LoginAsApplicant();
                test.Log(LogStatus.Pass, "Log in as applicant");

                // Click on the forms tab and select the "Approval and Deny Form for testing"
                applicantPage.ClickFormsTab();
                test.Log(LogStatus.Pass, "Clicking the 'Forms' tab");
                Driver.SwitchToFrameById("FormsDataPage");
                // TODO: refactor formGuid out of the test
                var formGuid = applicantPage.SelectForm(formName);
                test.Log(LogStatus.Pass, "Selecting " + formName);

                // Fill out an answer and click Next
                Driver.SwitchToDefaultFrame();
                applicantPage.ClickNextPage();
                // Click next twice to go to form
                test.Log(LogStatus.Pass, "Click Next Page");
                // the Next Page button must be clicked twice in order to go to the next page
                // TODO work with developers to fix
                applicantPage.ClickNextPage();
                test.Log(LogStatus.Pass, "Click Next Page again");

                // The IFrames are nested so we need to switch to both
                Driver.SwitchToFrameById("FormsDataPage");
                Driver.SwitchToFrameById("IFrameFormSent");

                // Verify the 'Deny' button exists
                Assert.IsTrue(applicantPage.DenyButtonExists(), "The 'Deny' button does not appear on the screen");
                test.Log(LogStatus.Pass, "The 'Deny' button exists");

                // Digitally sign and approve the form
                applicantPage.EnterDigitalSignature(appName);
                test.Log(LogStatus.Pass, "Digitally sign the form");

                applicantPage.ClickDeny();
                test.Log(LogStatus.Pass, "Deny the form");

                // switch back to the main window
                Driver.ClosePopup();

                // Navigate to 'List All Forms'
                Driver.SwitchToFrameById("App"+appNo);
                applicantMenu.ClickListAllForms();
                Driver.SwitchToFrameById("MainContentsIFrame");
                test.Log(LogStatus.Pass, "Navigate to 'List All Forms' from the applicant menu");

                // Verify form has been approved
                Assert.IsTrue(applicantProfilePages.ListAllForms.FormIsDenied(formGuid), "The form was not denied");
                test.Log(LogStatus.Pass, "Form was denied");

                // Cleanup - delete the form
                test.Log(LogStatus.Info, "Beginning cleanup");
                formWorkflows.DeleteSentForm(formGuid);
                test.Log(LogStatus.Pass, "Delete sent form");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        // TODO should this be included in the regression suite??
        // TODO fix the assert at the end
        //[TestMethod]
        //[TestCategory("Regression")]
        //[TestProperty("TestArea", "Forms")]
        //[TestProperty("TestCaseID", "")]
        //[TestProperty("TestCaseName", "")]
        //public void Form_SendTo_Applicant_Delete()
        //{
        //    Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

        //    ExtentTest test = ExtentTestManager.StartTest("Form Send to Applicant - Delete", 
        //        "Send a form to an applicant, test deletion of sent form")
        //        .AssignCategory("Regression");

        //    var mainMenu = new Pages.MainMenu(Driver);
        //    var formMenu = new Pages.SubMenuForms(Driver);
        //    var formPages = new Pages.FormPages(Driver);
        //    var applicantProfilePage = new Pages.ApplicantProfilePages(Driver);
        //    var applicantPage = new Pages.ApplicantPages(Driver);
        //    var applicantMenu = new Pages.ApplicantAdminMenu(Driver);
        //    var applicantAdminPages = new Pages.ApplicantAdminPages(Driver);

        //    var searchWorkflows = new SearchWorkflows(Driver);

        //    var formWorkflows = new FormWorkflows(Driver, test);
        //    var formData = new Data.FormData();
        //    var formName = formData.FormTitle;

        //    var windowHelpers = new WindowHelpers(Driver);

        //    try //Contains Contents of Test
        //    {
        //        //const string formId = "435";
        //        //const string formName = "Approve and Deny Form for testing";
        //        const string appNo = "1";
        //        const string appName = "Sample Applicant";

        //       //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

        //        // create a new form
        //        var formId = formWorkflows.CreateForm();

        //        // navigate back to the main menu
        //        Driver.SwitchToDefaultFrame();
        //        mainMenu.ClickMainMenuTab();

        //        // Navigate to Forms > Send a Form
        //        mainMenu.ClickForms();
        //        formMenu.ClickSendForm();
        //        test.Log(LogStatus.Pass, "Navigate to Forms > Send a Form");

        //        // Select "Approval and Deny Form for testing"
        //        Driver.SwitchToFrameById("MainContentsIFrame");

        //        formPages.SelectFormToSendById(formId);
        //        test.Log(LogStatus.Pass, "Select form: " + formId);

        //        formPages.ClickContinueWithSelectedForms();
        //        test.Log(LogStatus.Pass, "Click 'Continue with Selected Forms'");

        //        formPages.ClickNext();
        //        test.Log(LogStatus.Pass, "Click 'Next'");

        //        // Assign the form to appno1
        //        formPages.SelectAssociatedApplicant(appNo);
        //        test.Log(LogStatus.Pass, "Select Applicant Number: " + appNo);

        //        formPages.ClickNext();
        //        test.Log(LogStatus.Pass, "Click 'Next'");

        //        // Send the form
        //        formPages.ClickFinishAndDeliver();
        //        test.Log(LogStatus.Pass, "Click 'Finish and Deliver'");

        //        // Open the applicant page
        //        searchWorkflows.OpenApplicantUsingSearch(appNo, appName);
        //        test.Log(LogStatus.Pass, "Opened applicant page for: " + appNo + " " + appName);

        //        // Login as applicant
        //        Driver.SwitchToFrameById("App"+appNo);
        //        applicantProfilePage.LoginAsApplicant();

        //        // Click on the forms tab and select the "Approval and Deny Form for testing"
        //        applicantPage.ClickFormsTab();
        //        test.Log(LogStatus.Pass, "Clicking the 'Forms' tab");
        //        Driver.SwitchToFrameById("FormsDataPage");

        //        var formGuid = applicantPage.GetFormKey(formName);
        //        // switch back to the main window
        //        Driver.ClosePopup();

        //        // Navigate to 'List All Forms'
        //        Driver.SwitchToFrameById("App"+appNo);
        //        applicantMenu.ClickListAllForms();
        //        Driver.SwitchToFrameById("MainContentsIFrame");

        //        // Cleanup - delete the form
        //        formWorkflows.DeleteSentForm(formGuid);

        //        // TODO figure out why this doesnt work!
        //        Assert.IsFalse(applicantAdminPages.FormWasSent(formGuid), "Form was not deleted");
        //        test.Log(LogStatus.Pass, "Form was deleted");

        //        Driver.SwitchToDefaultFrame();
        //        mainMenu.ClickMainMenuTab();
        //        formWorkflows.DeleteForm(formId);

        //        // End the test
        //        //Driver.Quit();
        //    }
        //    catch (Exception e) //On Error Do
        //    {
        //        
        //        HandleException(e, Driver);
        //        throw;
        //    }
        //}

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Forms")]
        [TestProperty("TestCaseName", "Form Send to Applicant - using Main Menu")]
        [TestProperty("TestCaseDescription", "Send a form to an applicant using the 'Send a Form' option from the main menu")]
        [TestProperty("UsesHardcodedData", "true")]
        public void Form_SendTo_Applicant_using_Main_Menu()
        {
            // pages
            var mainMenu = new MainMenu(Driver);
            var formMenu = new SubMenuForms(Driver);
            var formPages = new FormPages(Driver);
            var applicantPage = new ApplicantPages(Driver);
            var applicantMenu = new ApplicantAdminMenu(Driver);
            var applicantProfilePages = new ApplicantProfilePages(Driver);

            // workflows
            var searchWorkflows = new SearchWorkflows(Driver);
            var formWorkflows = new FormWorkflows(Driver, test);

            // data
            var formData = new FormData();
            var formName = formData.FormTitle;
            const string appNo = "2593";
            const string appName = "Kevin Pavao";

            try //Contains Contents of Test
            {
                //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

                // create a new form
                var formId = formWorkflows.CreateForm();
                test.Log(LogStatus.Info, "Created new form: " + formId + ": " + formName);

                // navigate back to the main menu
                Driver.SwitchToDefaultFrame();
                mainMenu.ClickMainMenuTab();

                // Navigate to Forms > Send a Form
                mainMenu.ClickForms();
                formMenu.ClickSendForm();
                test.Log(LogStatus.Pass, "Navigate to Forms > Send a Form");

                // Select "Approval and Deny Form for testing"
                Driver.SwitchToFrameById("MainContentsIFrame");
                formPages.SendFormPage.SelectFormToSendById(formId);
                test.Log(LogStatus.Pass, "Select form: " + formId);

                formPages.SendFormPage.ClickContinueWithSelectedForms();
                test.Log(LogStatus.Pass, "Click 'Continue with Selected Forms'");

                formPages.SendFormPage.ClickNext();
                test.Log(LogStatus.Pass, "Click 'Next'");

                // Assign the form to appno1
                formPages.SendFormPage.SelectAssociatedApplicant(appNo);
                test.Log(LogStatus.Pass, "Select Applicant Number: " + appNo);

                formPages.SendFormPage.ClickNext();
                test.Log(LogStatus.Pass, "Click 'Next'");

                // Send the form
                formPages.SendFormPage.ClickFinishAndDeliver();
                test.Log(LogStatus.Pass, "Click 'Finish and Deliver'");

                // Open the applicant page
                searchWorkflows.OpenApplicantUsingSearch(appNo, appName);
                test.Log(LogStatus.Pass, "Opened applicant page for: " + appNo + " " + appName);

                // Login as applicant
                Driver.SwitchToFrameById("App"+appNo);
                applicantProfilePages.Toolbar.LoginAsApplicant();
                test.Log(LogStatus.Pass, "Log in as applicant");

                // Click on the forms tab and select the "Approval and Deny Form for testing"
                applicantPage.ClickFormsTab();
                test.Log(LogStatus.Pass, "Clicking the 'Forms' tab");
                Driver.SwitchToFrameById("FormsDataPage");

                var formGuid = applicantPage.GetFormGuid(formName);
                // switch back to the main window
                Driver.ClosePopup();

                // Navigate to 'List All Forms'
                Driver.SwitchToFrameById("App"+appNo);
                applicantMenu.ClickListAllForms();
                Driver.SwitchToFrameById("MainContentsIFrame");
                test.Log(LogStatus.Pass, "Navigate to 'List All Forms' from the applicant menu");

                // Verify the form was sent
                Assert.IsTrue(applicantProfilePages.ListAllForms.FormWasSent(formGuid), "The form was not sent.");
                test.Log(LogStatus.Pass, "Check the list to verify that the form was sent");

                // Cleanup - delete the form
                test.Log(LogStatus.Info, "Beginning cleanup");
                formWorkflows.DeleteSentForm(formGuid);
                test.Log(LogStatus.Pass, "Delete sent form");

                Driver.SwitchToDefaultFrame();
                mainMenu.ClickMainMenuTab();
                formWorkflows.DeleteForm(formId);
                test.Log(LogStatus.Pass, "Delete created form");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Forms")]
        [TestProperty("TestCaseName", "Form Send to Applicant - using Applicant Menu")]
        [TestProperty("TestCaseDescription", "Send a form to an applicant using the 'Send a Form' menu option from the applicant menu")]
        [TestProperty("UsesHardcodedData", "true")]
        public void Form_SendTo_Applicant_using_Applicant_Menu()
        {
            // pages 
            var mainMenu = new MainMenu(Driver);
            var formPages = new FormPages(Driver);
            var applicantProfilePage = new ApplicantProfilePages(Driver);
            var applicantPage = new ApplicantPages(Driver);
            var applicantMenu = new ApplicantAdminMenu(Driver);
            var applicantProfilePages = new ApplicantProfilePages(Driver);

            // workflows
            var searchWorkflows = new SearchWorkflows(Driver);
            var formWorkflows = new FormWorkflows(Driver, test);

            // data
            var formData = new FormData();
            var formName = formData.FormTitle;
            const string appNo = "2593";
            const string appName = "Kevin Pavao";

            try //Contains Contents of Test
            {
                // create a new form
                var formId = formWorkflows.CreateForm();
                test.Log(LogStatus.Info, "Created new form: " + formId + ": " + formName);

                // Open the applicant page
                searchWorkflows.OpenApplicantUsingSearch(appNo, appName);
                test.Log(LogStatus.Pass, "Opened applicant page for: " + appNo + " " + appName);

                // Open the Send Form window
                Driver.SwitchToFrameById("App"+appNo);
                applicantMenu.ClickSendAForm();
                Driver.SwitchToPopup();

                // Select the newly created form 
                formPages.SendFormPage.SelectFormToSendById(formId);
                test.Log(LogStatus.Pass, "Select form: " + formId);

                formPages.SendFormPage.ClickContinueWithSelectedForms();
                test.Log(LogStatus.Pass, "Click 'Continue with Selected Forms'");

                // This page should automatically select the 'Applicant' radio button
                // Therefore we can just click next...
                formPages.SendFormPage.ClickNext();
                test.Log(LogStatus.Pass, "Click 'Next Page'");

                // Send the form
                formPages.SendFormPage.ClickFinishAndDeliver();
                test.Log(LogStatus.Pass, "Click 'Finish and Deliver'");

                // Close the window and switch back to the main window
                Driver.ClosePopup();

                // Login as applicant
                Driver.SwitchToFrameById("App"+appNo);
                applicantProfilePage.Toolbar.LoginAsApplicant();
                test.Log(LogStatus.Pass, "Log in as applicant");

                // Click on the forms tab and select the "Approval and Deny Form for testing"
                applicantPage.ClickFormsTab();
                test.Log(LogStatus.Pass, "Clicking the 'Forms' tab");
                Driver.SwitchToFrameById("FormsDataPage");

                // Getting the form hash key is the only way to determine whether or not the form was sent
                //var formGuid = applicantPage.GetFormGuid(formName);
                applicantPage.GetFormGuid(formName);

                // switch back to the main window
                Driver.ClosePopup();

                // Navigate to 'List All Forms'
                Driver.SwitchToFrameById("App"+appNo);
                applicantMenu.ClickListAllForms();
                Driver.SwitchToFrameById("MainContentsIFrame");
                test.Log(LogStatus.Pass, "Navigate to 'List All Forms' from the applicant menu");

                // Verify the form was sent
                Assert.IsTrue(applicantProfilePages.ListAllForms.FormWasSent(applicantPage.FormGuid), "The form was not sent.");
                test.Log(LogStatus.Pass, "Check the list to verify that the form was sent");

                // Cleanup - delete the form
                test.Log(LogStatus.Info, "Beginning cleanup");
                formWorkflows.DeleteSentForm(applicantPage.FormGuid);
                test.Log(LogStatus.Pass, "Delete sent form");

                Driver.SwitchToDefaultFrame();
                mainMenu.ClickMainMenuTab();
                formWorkflows.DeleteForm(formId);
                test.Log(LogStatus.Pass, "Delete created form");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Forms")]
        [TestProperty("TestCaseName", "Form Send to Applicant - using New Form")]
        [TestProperty("TestCaseDescription", "Send a form to an applicant using the 'New Form' toolbar from the applicant page")]
        [TestProperty("UsesHardcodedData", "true")]
        [Ignore] // TODO figure out why this is flaky
        public void Form_SendTo_Applicant_using_New_Form()
        {
            // pages
            var mainMenu = new MainMenu(Driver);
            var formPages = new FormPages(Driver);
            var applicantProfilePage = new ApplicantProfilePages(Driver);
            var applicantPage = new ApplicantPages(Driver);
            var applicantMenu = new ApplicantAdminMenu(Driver);
            var applicantProfilePages = new ApplicantProfilePages(Driver);

            // workflows
            var searchWorkflows = new SearchWorkflows(Driver);
            var formWorkflows = new FormWorkflows(Driver, test);

            // data
            var formData = new FormData();
            var formName = formData.FormTitle;
            const string appNo = "2593";
            const string appName = "Kevin Pavao";

            try //Contains Contents of Test
            {
                // create a new form
                var formId = formWorkflows.CreateForm();
                test.Log(LogStatus.Info, "Created new form: " + formId + ": " + formName);

                // Open the applicant page
                searchWorkflows.OpenApplicantUsingSearch(appNo, appName);
                test.Log(LogStatus.Pass, "Opened applicant page for: " + appNo + " " + appName);

                // Click the 'New Form' button and switch to the new window
                Driver.SwitchToFrameById("App"+appNo);
                applicantProfilePage.Toolbar.ClickNewFormButton();
                Driver.SwitchToPopup();

                formPages.SendFormPage.SelectFormToSendById(formId);
                test.Log(LogStatus.Pass, "Select form: " + formId);

                formPages.SendFormPage.ClickContinueWithSelectedForms();
                test.Log(LogStatus.Pass, "Click 'Continue with Selected Forms'");

                // TODO create method(s) to select other radio buttons
                // This page should automatically select the 'Applicant' radio button
                // Therefore we can just click next...
                formPages.SendFormPage.ClickNext();
                test.Log(LogStatus.Pass, "Click 'Next Page'");

                // Send the form
                formPages.SendFormPage.ClickFinishAndDeliver();
                test.Log(LogStatus.Pass, "Click 'Finish and Deliver'");

                // Close the window and switch back to the main window
                Driver.ClosePopup();

                // Login as applicant
                Driver.SwitchToFrameById("App"+appNo);
                applicantProfilePage.Toolbar.LoginAsApplicant();
                test.Log(LogStatus.Pass, "Log in as applicant");

                // Click on the forms tab and select the "Approval and Deny Form for testing"
                applicantPage.ClickFormsTab();
                test.Log(LogStatus.Pass, "Clicking the 'Forms' tab");
                Driver.SwitchToFrameById("FormsDataPage");

                var formGuid = applicantPage.GetFormGuid(formName);

                // switch back to the main window
                Driver.ClosePopup();

                // Navigate to 'List All Forms'
                Driver.SwitchToFrameById("App"+appNo);
                applicantMenu.ClickListAllForms();
                Driver.SwitchToFrameById("MainContentsIFrame");
                test.Log(LogStatus.Pass, "Navigate to 'List All Forms' from the applicant menu");

                // Verify the form was sent
                Assert.IsTrue(applicantProfilePages.ListAllForms.FormWasSent(formGuid), "The form was not sent.");
                test.Log(LogStatus.Pass, "Check the list to verify that the form was sent");

                // Cleanup - delete the form
                test.Log(LogStatus.Info, "Beginning cleanup");
                formWorkflows.DeleteSentForm(formGuid);
                test.Log(LogStatus.Pass, "Delete sent form");

                Driver.SwitchToDefaultFrame();
                mainMenu.ClickMainMenuTab();
                formWorkflows.DeleteForm(formId);
                test.Log(LogStatus.Pass, "Delete created form");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        // TODO come up with a more descriptive name for the test
        // TODO figure out why /deleteunansweredforms.aspx doesnt work
        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Forms")]
        [TestProperty("TestCaseName", "")]
        [TestProperty("TestCaseDescription", "")]
        [TestProperty("UsesHardcodedData", "true")]
        [Ignore]
        public void Form_Blank_Form_Deletion()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var mainMenu = new MainMenu(Driver);
            var formPages = new FormPages(Driver);
            var applicantProfilePages = new ApplicantProfilePages(Driver);
            var applicantPage = new ApplicantPages(Driver);
            var applicantMenu = new ApplicantAdminMenu(Driver);

            var searchWorkflows = new SearchWorkflows(Driver);
            var formWorkflows = new FormWorkflows(Driver, test);

            var formData = new FormData();
            var formName = formData.FormTitle;

            var windowHelpers = new WindowHelpers(Driver);

            try //Contains Contents of Test
            {
                const string appNo = "1";
                const string appName = "Sample Applicant";

                //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

                // create a new form
                var formId = formWorkflows.CreateForm();
                test.Log(LogStatus.Info, "Created new form: " + formId + ": " + formName);

                // Open the applicant page
                searchWorkflows.OpenApplicantUsingSearch(appNo, appName);
                test.Log(LogStatus.Pass, "Opened applicant page for: " + appNo + " " + appName);
                
                // Click the 'New Form' button and switch to the new window
                Driver.SwitchToFrameById("App"+appNo);
                applicantProfilePages.Toolbar.ClickNewFormButton();
                Driver.SwitchToPopup();

                formPages.SendFormPage.SelectFormToSendById(formId);
                test.Log(LogStatus.Pass, "Select form: " + formId);

                formPages.SendFormPage.ClickContinueWithSelectedForms();
                test.Log(LogStatus.Pass, "Click 'Continue with Selected Forms'");

                // TODO create method(s) to select other radio buttons
                // This page should automatically select the 'Applicant' radio button
                // Therefore we can just click next...
                formPages.SendFormPage.ClickNext();
                test.Log(LogStatus.Pass, "Click 'Next Page'");

                // Send the form
                formPages.SendFormPage.ClickFinishAndDeliver();
                test.Log(LogStatus.Pass, "Click 'Finish and Deliver'");

                // Close the window and switch back to the main window
                //formPages.ClickCloseWindow();
                Driver.ClosePopup();

                // Login as applicant
                Driver.SwitchToFrameById("App"+appNo);
                applicantProfilePages.Toolbar.LoginAsApplicant();
                test.Log(LogStatus.Pass, "Log in as applicant");

                // Click on the forms tab and select the "Approval and Deny Form for testing"
                applicantPage.ClickFormsTab();
                test.Log(LogStatus.Pass, "Clicking the 'Forms' tab");
                Driver.SwitchToFrameById("FormsDataPage");

                var formGuid = applicantPage.GetFormGuid(formName);

                // switch back to the main window
                Driver.ClosePopup();

                // Navigate to 'List All Forms'
                Driver.SwitchToFrameById("App"+appNo);
                applicantMenu.ClickListAllForms();
                Driver.SwitchToFrameById("MainContentsIFrame");

                // Verify the form was sent
                Assert.IsTrue(applicantProfilePages.ListAllForms.FormWasSent(formGuid), "The form was not sent.");

                // click 'Edit Form'
                applicantProfilePages.ListAllForms.EditForm(formGuid);
                Driver.SwitchToPopup();
                //formPages.ClickSaveAsDraft();
                Driver.ClosePopup();

                BrowseTo(BaseUrls["ApplitrackLoginPage"] + "/onlineapp/admin/maintenance/deleteunansweredformssent.aspx", Driver);

                /*
                // Cleanup - delete the form
                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("App"+appNo);
                Driver.SwitchToFrameById("MainContentsIFrame");
                applicantAdminPages.DeleteForm(formGuid);
                AlertAccept(Driver);
                AlertAccept(Driver);
                test.Log(LogStatus.Pass, "Form was deleted");
                 */
            }
            catch (Exception e) //On Error Do
            {
                
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Forms")]
        [TestProperty("TestCaseName", "Forms Category by Status Loads")]
        [TestProperty("TestCaseDescription", "Make sure that the Forms > View Submitted Forms by Category > Applicant Screening > Teacher Candidate > Status pages load")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Forms_Category_By_Status_Loads()
        {
            // page objects
            var mainMenu = new MainMenu(Driver);
            var formsMenu = new SubMenuForms(Driver);
            var formPages = new FormPages(Driver);

            // test data
            const string expectedMenuHeader = "By Status";

            try
            {
                // Navigate to Forms > View Submitted Forms by Category > Applicant Screening > Teacher Candidate
                mainMenu.ClickForms();
                formsMenu.ClickViewSubmittedFormsByCategory();
                formsMenu.ClickCategory("Applicant Screening");
                formsMenu.ClickCategory("Teacher Candidate");
                test.Log(LogStatus.Pass, "Navigate to Forms > View Submitted Forms by Category > Applicant Screening > Teacher Candidate");

                // Click By Status
                formsMenu.ClickCategory("By Status");
                test.Log(LogStatus.Pass, "Click 'By Status' on the menu");

                // Assert that the menu header is correct 
                var actualHeaderText = formsMenu.GetByStatusHeaderText();
                Assert.AreEqual(expectedMenuHeader, actualHeaderText, "Expected header text: " + expectedMenuHeader + " Actual header text: " + actualHeaderText);
                test.Log(LogStatus.Pass, "The header text is: " + actualHeaderText);

                // Click a menu item and assert that the page is displayed
                formsMenu.ClickCategory("Older");
                test.Log(LogStatus.Pass, "Click 'Older' on the menu");

                Driver.SwitchToFrameById("MainContentsIFrame");
                Assert.IsTrue(formPages.SearchPage.IsDisplayed());
                test.Log(LogStatus.Pass, "The page is displayed");
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
