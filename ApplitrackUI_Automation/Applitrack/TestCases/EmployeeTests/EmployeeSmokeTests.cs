using System;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide.Applicants;
using ApplitrackUITests.PageObjects.AdminSide.CommonPages;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using ApplitrackUITests.WorkFlows;
using Automation.Framework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.EmployeeTests
{
    [TestClass]
    public class EmployeeSmokeTests : ApplitrackUIBase
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
            BaseTearDown(Driver);
        }

        #endregion

        #region Test Cases

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "create New Employee")]
        [TestProperty("TestCaseName", "Validate Employee Creation")]
        [TestProperty("TestCaseDescription", "Validate Employee Creation")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Employee_Create_New()
        {
            var mainMenu = new MainMenu(Driver);
            var empPages = new SubMenuEmployees(Driver);

            try
            {
                mainMenu.ClickEmployees();
                empPages.ClickCreateNewEmployee();
                Driver.SwitchToFrameById("MainContentsIFrame");
                test.Log(LogStatus.Info, "Navigate to Employees > Create New Employee");

                empPages.ClickBlankForm();
                test.Log(LogStatus.Info, "Click Blank Form");

                Driver.SwitchToFrameById("tabs_Panel");
                empPages.SelectEmpTitleItem("Mr.");
                empPages.FillEmployeeFirstName("Andrew");
                empPages.FillEmployeeLastName("Adams");
                empPages.FillEmployeeAddress1("Test");
                empPages.FillEmployeeCity("Test");
                Driver.SwitchToDefaultFrame();
                test.Log(LogStatus.Info, "Enter employee info");

                Driver.SwitchToFrameById("MainContentsIFrame");
                empPages.ClickSaveButton();
                test.Log(LogStatus.Info, "Click save");

                mainMenu.CheckAlert();

                empPages.ClickSaveButton();
                test.Log(LogStatus.Info, "Click save again");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Employee")]
        [TestProperty("TestCaseName", "Validate Employee Deletion")]
        [TestProperty("TestCaseDescription", "Validate Employee Deletion")]
        [TestProperty("UsesHardcodedData", "true")]
        [Ignore] // TODO unignore this once employee deletion is working, and/or when the new QA environment is up
        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"|DataDirectory|\DataFiles\CrossBrowser2.csv", "CrossBrowser2#csv", DataAccessMethod.Sequential)]
        public void Employee_Delete()
        {
            //Override Default BrowserType with DataSource BrowserType
            //BT = Convert.ToString(testContextInstance.DataRow["BrowserType"]);

            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var mainMenu = new MainMenu(Driver);
            var empPages = new SubMenuEmployees(Driver);

            try  //Contains Contents of Test
            {
                //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

                mainMenu.ClickEmployees();
                empPages.ClickEmployeeList();
                test.Log(LogStatus.Pass, "Navigate to Employees > Employee List");

                Driver.SwitchToFrameById("MainContentsIFrame");

                empPages.DeleteEmployeeFromList("Adams,   Andrew");
                test.Log(LogStatus.Pass, "Select the employee");
                               
                Driver.SwitchToDefaultFrame();
                             
                empPages.ClickDelete();
                empPages.ClickDeleteEmployee();
                test.Log(LogStatus.Pass, "Click delete button");

                empPages.ConfirmDeleteEmployee();
                test.Log(LogStatus.Pass, "Confirm deletion");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Employee")]
        [TestProperty("TestCaseName", "Employee Send Email")]
        [TestProperty("TestCaseDescription", "Send an email to an employee using the Email button")]
        [TestProperty("UsesHardcodedData", "true")]
        [Ignore] // TODO fix failure when IDM is turned off
        public void Employee_Send_Email()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            // pages
            // the Applicant Profile page shares some of the same buttons as the employee page
            // TODO create a new page object for employees
            var applicantProfilePage = new ApplicantProfilePages(Driver);
            var email = new EmailPage(Driver);

            // workflows
            var searchWorkflows = new SearchWorkflows(Driver);

            // test data
            const string empNo = "484";
            const string empName = "Automation Employee";
            const string empEmail = "applitrackautoemployee@gmail.com";

            // randomly generate an email subject in order to verify the email was sent correctly
            var rand = new Random();
            var emailBody = rand.Next().ToString();

            try  //Contains Contents of Test
            {
                //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

                // Open the employee page
                searchWorkflows.OpenEmployeeUsingSearch(empNo, empName);

                // Click the Email button
                Driver.SwitchToFrameById("Emp"+empNo);
                applicantProfilePage.Toolbar.ClickEmailButton();

                // Send the email
                Driver.SwitchToPopup();
                Driver.SwitchToFrameByClass("cke_wysiwyg_frame");
                email.EnterEmailBody(emailBody);
                Driver.SwitchToDefaultFrame();
                email.ClickSendMessageButton();
                test.Log(LogStatus.Pass, "Click the Send Message button");

                // Assert that the email address is displayed on the page
                Assert.IsTrue(email.IsEmailAddressDisplayed(empEmail), "Expected email: " + empEmail + " is not on the screen");
                test.Log(LogStatus.Pass, "The email address: " + empEmail + " appears on the page");
                Driver.ClosePopup();

                // TODO figure out why this is failing on teamcity - error message -Could not find part of the path
                // Check to see if the email was sent
                //Assert.IsTrue(GmailApi.FindEmail(emailBody), "The email was not found in the gmail inbox");
                //test.Log(LogStatus.Pass, "Email found in gmail inbox");
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
