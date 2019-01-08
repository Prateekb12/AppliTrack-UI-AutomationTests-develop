using System;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using ApplitrackUITests.PageObjects.EmployeeSide;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.EmployeePortalTests
{
    [TestClass]
    public class EmployeePortalRegressionTests : ApplitrackUIBase
    {
        #region Setup and TearDown

        private string BT; //Pass BT as it changes between methods
        private IWebDriver Driver;
        private ExtentTest test;
        // TODO put this in app.config
        private string url = BaseUrls["ApplitrackLoginPage"] + "/onlineapp/_employee.aspx"; 

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
            BrowseTo(url, Driver);

            test.Log(LogStatus.Info, "Starting test at URL: " + url);
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
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Employees")]
        [TestProperty("TestCaseName", "Employee Portal Form Submit")]
        [TestProperty("TestCaseDescription", "Start and submit a new form in the employee portal")]
        [TestProperty("UsesHardcodedData", "false")]
        [Ignore] // TODO form was changed in the system, figure out whats different
        public void Employee_Portal_Form_Submit()
        {
            var employeePortalPages = new EmployeePortalPages(Driver);

            var employeeData = new EmployeeData();

            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            try  //Contains Contents of Test
            {
                // Login
                Driver.SwitchToFrameById("HomeDataPage");
                employeePortalPages.LoginPage.EnterEmailAddress(employeeData.email);
                employeePortalPages.LoginPage.EnterPassword(employeeData.password);
                employeePortalPages.LoginPage.ClickLogin();
                test.Log(LogStatus.Pass, "Logging in as employee: " + employeeData.email);

                // Click on the 'Forms' tab
                Driver.SwitchToDefaultFrame();
                employeePortalPages.ClickFormsTab();
                test.Log(LogStatus.Pass, "Clicking on the 'Forms' tab");

                // Click the 'New Form' button
                Driver.SwitchToFrameById("FormsDataPage");
                employeePortalPages.FormsTabPage.ClickNewForm();
                test.Log(LogStatus.Pass, "Clicking on the 'New Form' button");

                // Click Start Form
                employeePortalPages.FormsTabPage.ClickStartForm();
                test.Log(LogStatus.Pass, "Click the 'Start Form' link.");

                // Click OK, Continue
                Driver.SwitchToFrameById("IFrameFormSent");
                Assert.IsTrue(employeePortalPages.FormsTabPage.StartFormPageIsLoaded(), "The New Form page did not load");
                test.Log(LogStatus.Pass, "The New Form page loaded");

                employeePortalPages.FormsTabPage.ClickOkContinue();
                test.Log(LogStatus.Pass, "Clicking the 'OK, Continue' button");

                // Enter the required fields on the form
                Driver.SwitchToFrameById("IFrameFormSent");
                employeePortalPages.FormsTabPage.EnterRequiredFields();
                test.Log(LogStatus.Pass, "Required fields populated");

                // Submit the form
                employeePortalPages.FormsTabPage.ClickSubmit();
                test.Log(LogStatus.Pass, "Form submitted");

                // Check to see if the correct screen is displayed
                Assert.IsTrue(employeePortalPages.FormsTabPage.SubmittedSceenDisplayed());
                test.Log(LogStatus.Pass, "The screen indicating the form has been submitted is displayed");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Employees")]
        [TestProperty("TestCaseName", "Employee Portal Form View")]
        [TestProperty("TestCaseDescription", "Start, submit, and view a new form in the employee portal")]
        [TestProperty("UsesHardcodedData", "false")]
        [Ignore] // TODO form was changed in the system, figure out whats different
        public void Employee_Portal_Form_View()
        {
            //var employeePortalPages = new EmployeePages(Driver);
            var employeePortalPages = new EmployeePortalPages(Driver);
            var mainMenu = new MainMenu(Driver);

            var employeeData = new EmployeeData();

            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            try  //Contains Contents of Test
            {
                // Login
                Driver.SwitchToFrameById("HomeDataPage");
                employeePortalPages.LoginPage.EnterEmailAddress(employeeData.email);
                employeePortalPages.LoginPage.EnterPassword(employeeData.password);
                employeePortalPages.LoginPage.ClickLogin();
                test.Log(LogStatus.Pass, "Logging in as employee: " + employeeData.email);

                // Click on the 'Forms' tab
                Driver.SwitchToDefaultFrame();
                employeePortalPages.ClickFormsTab();
                test.Log(LogStatus.Pass, "Clicking on the 'Forms' tab");

                // Click the 'New Form' button
                Driver.SwitchToFrameById("FormsDataPage");
                employeePortalPages.FormsTabPage.ClickNewForm();
                test.Log(LogStatus.Pass, "Clicking on the 'New Form' button");

                // Click Start Form
                employeePortalPages.FormsTabPage.ClickStartForm();
                test.Log(LogStatus.Pass, "Click the 'Start Form' link.");

                // Click OK, Continue
                Driver.SwitchToFrameById("IFrameFormSent");
                Assert.IsTrue(employeePortalPages.FormsTabPage.StartFormPageIsLoaded(), "The New Form page did not load");
                test.Log(LogStatus.Pass, "The New Form page loaded");

                employeePortalPages.FormsTabPage.ClickOkContinue();
                test.Log(LogStatus.Pass, "Clicking the 'OK, Continue' button");
                test.Log(LogStatus.Pass, "Form key is: " + employeePortalPages.FormsTabPage.FormKey);

                // Enter the required fields on the form
                Driver.SwitchToFrameById("IFrameFormSent");
                employeePortalPages.FormsTabPage.EnterRequiredFields();
                test.Log(LogStatus.Pass, "Required fields populated");

                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("FormsDataPage");
                employeePortalPages.FormsTabPage.FindFormGuid();
                test.Log(LogStatus.Pass, "Form GUID is: " + employeePortalPages.FormsTabPage.FormGuid);

                // Submit the form
                Driver.SwitchToFrameById("IFrameFormSent");
                employeePortalPages.FormsTabPage.ClickSubmit();
                test.Log(LogStatus.Pass, "Form submitted");

                // Check to see if the correct screen is displayed
                Assert.IsTrue(employeePortalPages.FormsTabPage.SubmittedSceenDisplayed());
                test.Log(LogStatus.Pass, "The screen indicating the form has been submitted is displayed");

                // Check to see if the form can be viewed
                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("FormsDataPage");
                employeePortalPages.FormsTabPage.ClickView();
                Assert.IsTrue(employeePortalPages.FormsTabPage.ViewFormPageDisplayed(), "The submitted form page was not displayed");
                test.Log(LogStatus.Pass, "The page containing the submitted form is displayed");
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
