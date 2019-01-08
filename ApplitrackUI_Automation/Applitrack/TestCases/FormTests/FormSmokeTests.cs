using System;
using System.Threading;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide.Applicants;
using ApplitrackUITests.PageObjects.AdminSide.Forms;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
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
    public class FormSmokeTests : ApplitrackUIBase
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
                .AssignCategory("Smoke");

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
            //LogTearDown();
            BaseTearDown(Driver);
        }

        #endregion

        #region Test Cases

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Forms Menu")]
        [TestProperty("TestCaseName", "Validate Form Creation")]
        [TestProperty("TestCaseDescription", "Verify Form Creation")]
        [TestProperty("UsesHardcodedData", "false")]
        [Ignore]
        public void Form_Create_New_Blank()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var formData = new FormData();
            var mainMenu = new MainMenu(Driver);
            var formMenu = new SubMenuForms(Driver);
            var formPages = new FormPages(Driver);

            var formWorkflow = new FormWorkflows(Driver, test);

            try  //Contains Contents of Test
            {
                //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

                // navigate to Forms > Design Forms and Packets > Create New Form
                mainMenu.ClickForms();
                formMenu.ClickDesignFormsandPackets();
                formMenu.ClickCreateNewForm();
                test.Log(LogStatus.Pass, "Navigate to Forms > Design Forms and Packets > Create New Form");

                // click 'A blank form'
                Driver.SwitchToFrameById("MainContentsIFrame");
                formPages.CreateNewFormPage.ClickBlankForm();
                test.Log(LogStatus.Pass, "Click 'A blank form'");

                // enter form info
                Driver.SwitchToFrameById("tabs_Panel");
                formPages.EditAndCreateFormPage.PropertiesTab.ClickStandardFormRadioButton();
                test.Log(LogStatus.Pass, "Select the 'Standard Form' radio button");

                formPages.EditAndCreateFormPage.PropertiesTab.FillOutFormTitle(formData.FormTitle);
                test.Log(LogStatus.Pass, "Fill out the form title");

                // save
                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("MainContentsIFrame");
                formPages.EditAndCreateFormPage.ClickSaveButton();
                test.Log(LogStatus.Pass, "Click the save button");
                var formId = formPages.EditAndCreateFormPage.GetFormId();
                Console.WriteLine("Form ID: {0}", formId);

                // verify that the form was created
                Driver.SwitchToDefaultFrame();
                mainMenu.ClickMainMenuTab();
                mainMenu.ClickForms();
                formMenu.ClickDesignFormsandPackets();
                formMenu.ClickEditForms();
                Driver.SwitchToFrameById("MainContentsIFrame");
                Assert.IsTrue(formPages.EditFormsPage.FormExists(formId));
                test.Log(LogStatus.Pass, "Verify the form exists");

                // delete the form
                formWorkflow.DeleteForm(formId);
                test.Log(LogStatus.Pass, "Delete the form");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Forms Menu")]
        [TestProperty("TestCaseName", "Validate Form Deletion")]
        [TestProperty("TestCaseDescription", "Verify Form Deletion")]
        [TestProperty("UsesHardcodedData", "false")]
        [Ignore]
        public void Form_Delete_Unused()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var mainMenu = new MainMenu(Driver);
            var formMenu = new SubMenuForms(Driver);
            var formPages = new FormPages(Driver);

            var formWorkflow = new FormWorkflows(Driver, test);

            try  //Contains Contents of Test
            {
                //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

                // create the form and get the id
                var newFormId = formWorkflow.CreateForm();
                test.Log(LogStatus.Pass, "Create the form");

                Console.WriteLine("Attemping to delete form with ID {0}", newFormId);
                // navigate to Forms > Design Forms and Packets > Edit Forms
                Driver.SwitchToDefaultFrame();
                mainMenu.ClickMainMenuTab();
                mainMenu.ClickForms();
                formMenu.ClickDesignFormsandPackets();
                formMenu.ClickEditForms();
                test.Log(LogStatus.Pass, "Navigate to Main Menu > Forms > Design Forms and Packets > Edit Forms");

                // select the form in the list
                Driver.SwitchToFrameById("MainContentsIFrame");
                formPages.EditFormsPage.SelectForm(newFormId);
                test.Log(LogStatus.Pass, "Select the form from the list");

                // delete the form
                Driver.SwitchToDefaultFrame();
                formMenu.ClickDeleteForms();
                test.Log(LogStatus.Pass, "Click Delete Forms");

                formPages.EditFormsPage.ConfirmDeletion();
                test.Log(LogStatus.Pass, "Confirm the deletion");

                // verify that the form was deleted
                Driver.SwitchToDefaultFrame();
                mainMenu.ClickMainMenuTab();
                mainMenu.ClickForms();
                formMenu.ClickDesignFormsandPackets();
                formMenu.ClickEditForms();
                Driver.SwitchToFrameById("MainContentsIFrame");
                Assert.IsFalse(formPages.EditFormsPage.FormExists(newFormId));
                test.Log(LogStatus.Pass, "Form deleted");

                Console.Out.WriteLineAsync("Form Deleted");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        // This is ignored because the form wizard sometimes requires clicking 'Next Page' twice, but sometimes it works after clicking it once
        // This was causing the test to fail
        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Forms")]
        [TestProperty("TestCaseName", "Form Send to Applicant - Approve")]
        [TestProperty("TestCaseDescription", "Send the 'Approve and Deny Form for testing' to an applicant, test approve functionality.")]
        [TestProperty("UsesHardcodedData", "true")]
        [Ignore]
        public void Form_SendTo_Applicant_Approve()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var mainMenu = new MainMenu(Driver);
            var formMenu = new SubMenuForms(Driver);
            var formPages = new FormPages(Driver);
            var applicantProfilePages = new ApplicantProfilePages(Driver);
            var applicantPage = new ApplicantPages(Driver);
            var applicantMenu = new ApplicantAdminMenu(Driver);

            var searchWorkflows = new SearchWorkflows(Driver);
            var formWorkflows = new FormWorkflows(Driver, test);

            try //Contains Contents of Test
            {
                const int formId = 895;
                const string formName = "Approve Form Automated Testing";
                const string appNo = "435";
                const string appName = "Raj email";
                const string employeeNo = "484";

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
                applicantProfilePages.Toolbar.LoginAsApplicant();
                test.Log(LogStatus.Pass, "Log in as applicant");

                // Click on the forms tab and select the "Approval and Deny Form for testing"
                applicantPage.ClickFormsTab();
                test.Log(LogStatus.Pass, "Clicking the 'Forms' tab");
                Driver.SwitchToFrameById("FormsDataPage");
                // TODO: refactor SelectForm() in order to use GetFormKey() instead 
                var formGuid = applicantPage.SelectForm(formName);
                test.Log(LogStatus.Pass, "Selecting " + formName);

                // Fill out an answer and click Next
                Driver.SwitchToDefaultFrame();
                applicantPage.ClickNextPage();
                test.Log(LogStatus.Pass, "Click Next Page");
                // the Next Page button must be clicked twice in order to go to the next page
                // TODO work with developers to fix
                applicantPage.ClickNextPage();
                test.Log(LogStatus.Pass, "Click Next Page again");

                // The IFrames are nested so we need to switch to both
                Driver.SwitchToFrameById("FormsDataPage");
                Driver.SwitchToFrameById("IFrameFormSent");

                // Verify the 'Approve' button exists
                Assert.IsTrue(applicantPage.ApproveButtonExists(), "The 'Approve' button does not appear on the screen");
                test.Log(LogStatus.Pass, "The 'Approve' button exists");

                // Digitally sign and approve the form
                applicantPage.EnterDigitalSignature(appName);
                test.Log(LogStatus.Pass, "Digitally sign the form");

                applicantPage.ClickApprove();
                test.Log(LogStatus.Pass, "Approve the form");

                // switch back to the main window
                Driver.ClosePopup();

                // Navigate to 'List All Forms'
                Driver.SwitchToFrameById("App"+appNo);
                applicantMenu.ClickListAllForms();
                Driver.SwitchToFrameById("MainContentsIFrame");
                test.Log(LogStatus.Pass, "Navigate to 'List All Forms' from the applicant menu");

                // Verify form has been approved
                Assert.IsTrue(applicantProfilePages.ListAllForms.FormIsApproved(formGuid), "The form was not approved");
                test.Log(LogStatus.Pass, "Form was approved");

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

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Forms Menu")]
        [TestProperty("TestCaseName", "Validate Form Creation")]
        [TestProperty("TestCaseDescription", "Verify Form Creation")]
        [TestProperty("UsesHardcodedData", "false")]

        public void Forms_Send_Inactive_Packet()
        {
            var mainMenu = new MainMenu(Driver);
            var formMenu = new SubMenuForms(Driver);
            var formPages = new FormPages(Driver);
            var activeForm = "HR-2472 Active";
            var inactiveForm = "HR-2472 Inactive";

            try
            {
                // Click Forms > Send a Form
                mainMenu.ClickForms();
                formMenu.ClickSendForm();

                // Send form packet with 1 active and 1 inactive form
                Driver.SwitchToFrameById("MainContentsIFrame");
                formPages.SendFormPage.SelectPacketToSendById(19);
                formPages.SendFormPage.ClickContinueWithSelectedForms();
                formPages.SendFormPage.SelectFormRecipient("me");
                formPages.SendFormPage.ClickNext();
                formPages.SendFormPage.ClickNext();
                // wait for form window to pop up
                Thread.Sleep(TimeSpan.FromSeconds(5));
                Driver.SwitchToPopup();
                Driver.ClosePopup();

                // Verify that only active form was sent
                Driver.SwitchToDefaultFrame();
                formMenu.ClickMySentForms();
                Driver.SwitchToFrameById("MainContentsIFrame");
                Assert.IsTrue(formPages.MySentFormsPage.FormExists(activeForm));
                Assert.IsFalse(formPages.MySentFormsPage.FormExists(inactiveForm));
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