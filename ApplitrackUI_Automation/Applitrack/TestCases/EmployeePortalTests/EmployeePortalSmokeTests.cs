using System;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using ApplitrackUITests.PageObjects.EmployeeSide;
using Automation.Framework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.EmployeePortalTests
{
    [TestClass]
    public class EmployeePortalSmokeTests : ApplitrackUIBase
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
                .AssignCategory("Smoke");

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
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Employees")]
        [TestProperty("TestCaseName", "Employee Portal Login")]
        [TestProperty("TestCaseDescription", "Test valid employee portal login")]
        [TestProperty("UsesHardcodedData", "true")]
        public void Employee_Portal_Login()
        {
            var employeePortalPages = new EmployeePortalPages(Driver);

            var employeeData = new EmployeeData();

            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            try  //Contains Contents of Test
            {
                Driver.SwitchToFrameById("HomeDataPage");

                employeePortalPages.LoginPage.EnterEmailAddress(employeeData.email);
                employeePortalPages.LoginPage.EnterPassword(employeeData.password);
                employeePortalPages.LoginPage.ClickLogin();

                Driver.SwitchToFrameById("EmployeeDataPage");
                Assert.IsTrue(employeePortalPages.IsLoggedIn(), "Employee was not logged in");
                test.Log(LogStatus.Pass, "Log in as employee: " + employeeData.email + "was successful");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Employees")]
        [TestProperty("TestCaseName", "Employee Portal Logout")]
        [TestProperty("TestCaseDescription", "Test employee portal logout")]
        [TestProperty("UsesHardcodedData", "true")]
        public void Employee_Portal_Logout()
        {
            var employeePortalPages = new EmployeePortalPages(Driver);
            var mainMenu = new MainMenu(Driver);

            var employeeData = new EmployeeData();

            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            try  //Contains Contents of Test
            {
                Driver.SwitchToFrameById("HomeDataPage");

                // Login
                employeePortalPages.LoginPage.EnterEmailAddress(employeeData.email);
                employeePortalPages.LoginPage.EnterPassword(employeeData.password);
                employeePortalPages.LoginPage.ClickLogin();
                Driver.SwitchToFrameById("EmployeeDataPage");
                test.Log(LogStatus.Pass, "Logging in as employee: " + employeeData.email);

                // Log off
                Driver.SwitchToDefaultFrame();
                employeePortalPages.ClickLogOff();
                Assert.IsTrue(employeePortalPages.IsLoggedOff(), "Employee was not logged off");
                test.Log(LogStatus.Pass, "Log off was successful");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Employees")]
        [TestProperty("TestCaseName", "Employee Portal Form Create New")]
        [TestProperty("TestCaseDescription", "Start a new form in the employee portal")]
        [TestProperty("UsesHardcodedData", "true")]
        public void Employee_Portal_Form_Create_New()
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

                Driver.SwitchToFrameById("EmployeeDataPage");
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

                // Save as Draft
                Driver.SwitchToFrameById("IFrameFormSent");
                employeePortalPages.FormsTabPage.ClickSaveAsDraft();
                test.Log(LogStatus.Pass, "Saving as draft");

                // Check to see if the form was assigned
                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("FormsDataPage");
                Assert.IsTrue(employeePortalPages.FormsTabPage.FormExistsInList(), "Form was not found");
                test.Log(LogStatus.Pass, "The form was assigned to the employee");

                // Cleanup
                test.Log(LogStatus.Info, "Beginning cleanup");
                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("FormsDataPage");
                employeePortalPages.FormsTabPage.ClickDelete();
                // An alert opens after clicking delete
                AlertAccept(Driver);
                test.Log(LogStatus.Pass, "Form deleted");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Employees")]
        [TestProperty("TestCaseName", "Employee Portal Form Edit")]
        [TestProperty("TestCaseDescription", "Start and edit a new form in the employee portal")]
        [TestProperty("UsesHardcodedData", "true")]
        public void Employee_Portal_Form_Edit()
        {
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

                Driver.SwitchToFrameById("EmployeeDataPage");
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

                // Save as Draft
                Driver.SwitchToFrameById("IFrameFormSent");
                employeePortalPages.FormsTabPage.ClickSaveAsDraft();
                test.Log(LogStatus.Pass, "Saving as draft");

                // get the unique id
                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("FormsDataPage");
                employeePortalPages.FormsTabPage.FindFormKey();
                test.Log(LogStatus.Pass, "Form key is: " + employeePortalPages.FormsTabPage.FormKey);

                // Check to see if the form was assigned
                Assert.IsTrue(employeePortalPages.FormsTabPage.FormExistsInList(), "Form was not found");
                test.Log(LogStatus.Pass, "The form was assigned to the employee");

                // Check to see if you can edit the form
                employeePortalPages.FormsTabPage.ClickEdit();
                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("FormsDataPage");
                Driver.SwitchToFrameById("IFrameFormSent");
                Assert.IsTrue(employeePortalPages.FormsTabPage.StartFormPageIsLoaded(), "Edit form did not work");
                test.Log(LogStatus.Pass, "The form can be edited");

                // Cleanup
                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("FormsDataPage");
                employeePortalPages.FormsTabPage.ClickDelete();
                // An alert opens after clicking delete
                AlertAccept(Driver);
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Employees")]
        [TestProperty("TestCaseName", "Employee Portal Delete Form")]
        [TestProperty("TestCaseDescription", "Start and delete a new form in the employee portal")]
        [TestProperty("UsesHardcodedData", "true")]
        public void Employee_Portal_Form_Delete()
        {
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

                Driver.SwitchToFrameById("EmployeeDataPage");
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

                // Save as Draft
                Driver.SwitchToFrameById("IFrameFormSent");
                employeePortalPages.FormsTabPage.ClickSaveAsDraft();
                test.Log(LogStatus.Pass, "Saving as draft");

                // Cleanup
                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("FormsDataPage");
                employeePortalPages.FormsTabPage.ClickDelete();
                // An alert opens after clicking delete
                AlertAccept(Driver);
                test.Log(LogStatus.Pass, "Click delete and accept the alert");

                // Refresh the page after deletion - the page does not reload after deleting
                Driver.Refresh();
                Driver.SwitchToDefaultFrame();
                employeePortalPages.ClickFormsTab();

                Driver.SwitchToFrameById("FormsDataPage");
                Assert.IsFalse(employeePortalPages.FormsTabPage.FormExistsInList(), "Form was not deleted properly");
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