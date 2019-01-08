using System;
using System.Collections.Generic;
using System.Threading;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.DataModels;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.WorkFlows;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.AdminSide.Applicants;
using ApplitrackUITests.PageObjects.AdminSide.Applicants.HireWizard;
using ApplitrackUITests.PageObjects.ApplicantSide.EmploymentApplication;
using Automation.Framework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.FCIntegrationTests
{
    [TestClass]
    public class FCIntegrationSmokeTests : ApplitrackUIBase
    {
        #region Setup and TearDown

        private IWebDriver _driver;
        private ExtentTest _test;
        private IApplicant _applicantData;

        // use this to determine if the applicant was created and then delete them if they are
        private bool _isApplicantCreated;

        /// <summary>
        /// Test Initialize Contains items to run before each [TestMethod]
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            // extent reports setup
            _test = ExtentTestManager.StartTest(TestContext.Properties["TestCaseName"].ToString(),
                TestContext.Properties["TestCaseDescription"].ToString())
                .AssignCategory("Smoke");

            // browser setup
            _driver = SetUp(_BT);
            _driver.Manage().Window.Maximize();

            _test.Log(LogStatus.Info, "Begin applicant creation");

            // create applicant
            BrowseTo(BaseUrls["ApplitrackLandingPage"], _driver);
            _test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLandingPage"]);
            var landingPage = new DefaultLandingPage(_driver);
            var applicantPages = new ApplicantPages(_driver);
            var applicationWorkflows = new ApplicationWorkflows(_driver, _test);
            var applicantProfile = new ApplicantProfilePages(_driver);
            var hireWizard = new HireWizardPage(_driver);

            _applicantData = new ApplicantGenerator();

            landingPage.ClickExternalLogin();

            _driver.SwitchToPopup();

            applicantPages.ClickEmploymentApplicationTab();

            applicationWorkflows.FillOutPersonalInfo(_applicantData);

            applicantPages.EmploymentApplicationPages.ClickNextPage();

            applicantPages.EmploymentApplicationPages.ClickPostalAddress();

            applicationWorkflows.FillOutPermanentAddress(_applicantData.Address);

            _driver.SwitchToDefaultFrame();
            _driver.SwitchToFrameById("AppDataPage");
            _applicantData.AppNo = applicantPages.EmploymentApplicationPages.GetAppNo();
            _test.Log(LogStatus.Info, "AppNo is: " + _applicantData.AppNo);

            _driver.SwitchToDefaultFrame();
            applicantPages.EmploymentApplicationPages.ClickSaveAsDraft();

            _driver.ClosePopup();

            _isApplicantCreated = true;

            // login to admin
            BrowseTo(BaseUrls["ApplitrackLoginPage"], _driver);
            _test.Log(LogStatus.Info, "Logging into system at URL: " + BaseUrls["ApplitrackLoginPage"]);
            var loginWorkflow = new LoginWorkflows(_driver);
            loginWorkflow.LoginAsSuperUser();

            // Navigate to the Notes page because selenium has trouble interacting with the window
            BrowseTo(BaseUrls["ApplitrackLoginPage"] + @"/onlineapp/admin/Action-LeaveNote.aspx?AppNo=" + _applicantData.AppNo + @"&HideSaveAndClose=1", _driver);

            // Hire the applicant
            applicantProfile.ApplicantNotesPages.ApplicantNotesTab.SelectHiredYes();
            _test.Log(LogStatus.Info, "Select 'Yes' for hired.");

            applicantProfile.ApplicantNotesPages.ApplicantNotesTab.ClickSave();
            _test.Log(LogStatus.Info, "Click 'Save'");

            hireWizard.ClickNext();
            _test.Log(LogStatus.Info, "Click 'Next'");

            // Select 'No' for Aesop
            if (FeatureFlags.ThirdParty.Aesop.Enabled)
            {
                hireWizard.SelectNo();
                _test.Log(LogStatus.Info, "Select 'No' for Aesop");
                hireWizard.ClickNext();
                _test.Log(LogStatus.Info, "Click 'Next'");
            }

            // Select 'No' for HR Files
            if (FeatureFlags.Employees.UsesEmployees)
            {
                hireWizard.SelectNo();
                _test.Log(LogStatus.Info, "Select 'No' for HR Files");
                hireWizard.ClickNext();
                _test.Log(LogStatus.Info, "Click 'Next'");
            }

            // Select 'Yes' for FC
            hireWizard.SelectYes();
            _test.Log(LogStatus.Info, "Select 'Yes' for FC");
            hireWizard.ClickNext();
            _test.Log(LogStatus.Info, "Click 'Next'");
        }

        [TestCleanup]
        public void TestTearDown()
        {
            if (_isApplicantCreated)
            {
                var applicantWorkflows = new ApplicantWorkflows(_driver);
                applicantWorkflows.DeleteApplicant(_applicantData);
            }

            BaseTearDown(_driver);
        }

        #endregion

        #region Test Cases

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "FCIntegration")]
        [TestProperty("TestCaseName", "Hiring Wizard FC Page Displayed")]
        [TestProperty("TestCaseDescription", "Verify that the FC page is displayed in the hiring wizard")]
        [TestProperty("UsesHardcodedData", "true")]
        public void FCIntegration_WizardPage_Displayed()
        {
            var hireWizard = new HireWizardPage(_driver);

            try
            {
                // Assert that the 'Frontline Central Employee Information' page is displayed
                Assert.IsTrue(hireWizard.FCEmployeeInfoPage.IsDisplayed(),
                    "The 'Frontline Central Employee Information' page was not displayed correctly");
                _test.Log(LogStatus.Pass, "The 'Employee Information' page is displayed");

                // Assert that the values in the fields match with the information entered when applying for the job
                Assert.AreEqual(_applicantData.RealName, hireWizard.FCEmployeeInfoPage.GetApplicantName(),
                    "The applicants name in the header is incorrect");
                _test.Log(LogStatus.Pass, "The header displays the name: " + _applicantData.RealName);

                Assert.AreEqual(_applicantData.FirstName, hireWizard.FCEmployeeInfoPage.GetFirstName(),
                    "The value in the 'First Name' field does not match: " + _applicantData.FirstName);
                _test.Log(LogStatus.Pass, "The 'First Name' field matches " + _applicantData.FirstName);

                Assert.AreEqual(_applicantData.LastName, hireWizard.FCEmployeeInfoPage.GetLastName(),
                    "The value in the 'Last Name' field does not match: " + _applicantData.LastName);
                _test.Log(LogStatus.Pass, "The 'Last Name' field matches " + _applicantData.LastName);

                // TODO uncomment when HR-2205 is fixed
                //Assert.AreEqual(_applicantData.SocialSecurityNumber, hireWizard.FCEmployeeInfoPage.GetSocialSecurityNumber(),
                //    "The value in the 'Social Security' field does not match: " + _applicantData.SocialSecurityNumber);
                //_test.Log(LogStatus.Pass, "The 'Social Security' field matches " + _applicantData.SocialSecurityNumber);

                Assert.AreEqual(_applicantData.Address.NumberAndStreet, hireWizard.FCEmployeeInfoPage.GetStreet1(),
                    "The value in the 'Street 1' field does not match: " + _applicantData.Address.NumberAndStreet);
                _test.Log(LogStatus.Pass, "The 'Street 1' field matches " + _applicantData.Address.NumberAndStreet);

                Assert.AreEqual(_applicantData.Address.AptNumber, hireWizard.FCEmployeeInfoPage.GetStreet2(),
                    "The value in the 'Street 2' field does not match: " + _applicantData.Address.AptNumber);
                _test.Log(LogStatus.Pass, "The 'Street 2' field matches " + _applicantData.Address.AptNumber);

                Assert.AreEqual(_applicantData.Address.City, hireWizard.FCEmployeeInfoPage.GetCity(),
                    "The value in the 'City' field does not match: " + _applicantData.Address.City);
                _test.Log(LogStatus.Pass, "The 'City' field matches " + _applicantData.Address.City);

                Assert.AreEqual(_applicantData.Address.State, hireWizard.FCEmployeeInfoPage.GetState(),
                    "The value in the 'State' field does not match: " + _applicantData.Address.State);
                _test.Log(LogStatus.Pass, "The 'State' field matches " + _applicantData.Address.State);

                Assert.AreEqual(_applicantData.Address.Zip, hireWizard.FCEmployeeInfoPage.GetZip(),
                    "The value in the 'Zip' field does not match: " + _applicantData.Address.Zip);
                _test.Log(LogStatus.Pass, "The 'Zip' field matches " + _applicantData.Address.Zip);

                Assert.AreEqual(_applicantData.Email, hireWizard.FCEmployeeInfoPage.GetPersonalEmail(),
                    "The value in the 'Personal Email' field does not match: " + _applicantData.Email);
                _test.Log(LogStatus.Pass, "The 'Personal Email' field matches " + _applicantData.Email);

                Assert.AreEqual(_applicantData.Address.DaytimePhone, hireWizard.FCEmployeeInfoPage.GetPrimaryPhoneNumber(),
                    "The 'Primary Number' field does not match: " + _applicantData.Address.DaytimePhone);
                _test.Log(LogStatus.Pass, "The 'Phone Number' field matches: " + _applicantData.Address.DaytimePhone);
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "FCIntegration")]
        [TestProperty("TestCaseName", "Hiring Wizard Final Page Displayed")]
        [TestProperty("TestCaseDescription", "Verify that the final page is displayed in the wizard after hiring")]
        [TestProperty("UsesHardcodedData", "true")]
        public void FCIntegration_FinalPage_Displayed()
        {
            var hireWizard = new HireWizardPage(_driver);

            try
            {
                // TODO remove this once HR-2205 is fixed - we should be able to click next here without re-entering in the SSN
                hireWizard.FCEmployeeInfoPage.EnterSocialSecurityNumber(_applicantData.SocialSecurityNumber);

                // Click 'Next' and assert that the Confirmation page is displayed
                hireWizard.ClickNext();
                _test.Log(LogStatus.Info, "Click 'Next'");
                Assert.IsTrue(hireWizard.ConfirmationPage.IsDisplayed(), "The Confirmation page is not displayed");
                _test.Log(LogStatus.Pass, "The Confirmation page is displayed");

                Assert.IsTrue(hireWizard.ConfirmationPage.WillFrontlineCentralRecordBeCreated(), "The FC confirmation is not displayed");
                _test.Log(LogStatus.Pass, "The FC confirmation is displayed");

                // Click 'Finish' and assert that the page indicating the data has been sent is displayed
                hireWizard.ConfirmationPage.ClickFinish();
                _test.Log(LogStatus.Info, "Click 'Finish'");
                Assert.IsTrue(hireWizard.FinishedPage.IsDisplayed(), "The final page is not displayed");
                _test.Log(LogStatus.Pass, "The final page is displayed");

                Assert.IsTrue(hireWizard.FinishedPage.IsApplicantMarkedAsHired(), "The message indicating that the applicant was marked as hired is not displayed");
                _test.Log(LogStatus.Pass, "The message indicates that the applicant was marked as hired");

                Assert.IsTrue(hireWizard.FinishedPage.IsFrontlineCentralRecordCreated(), "The message indicating that the FC record was created is not displayed");
                _test.Log(LogStatus.Pass, "The message indicates that the FC record was created successfully");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "FCIntegration")]
        [TestProperty("TestCaseName", "Hire Wizard Field Validation")]
        [TestProperty("TestCaseDescription", "Verify that the wizard performs validation on required fields and data input")]
        [TestProperty("UsesHardcodedData", "true")]
        public void FCIntegration_HireWizard_Field_Validation()
        {
            var hireWizard = new HireWizardPage(_driver);

            try
            {
                hireWizard.FCEmployeeInfoPage.EnterFirstName("");
                hireWizard.ClickNext();
                Assert.IsTrue(hireWizard.FCEmployeeInfoPage.IsDisplayed(), "The 'Frontline Central Employee Information' is not displayed");
                hireWizard.FCEmployeeInfoPage.EnterFirstName(_applicantData.FirstName);
                _test.Log(LogStatus.Pass, "The 'First Name' field is required");

                hireWizard.FCEmployeeInfoPage.EnterLastName("");
                hireWizard.ClickNext();
                Assert.IsTrue(hireWizard.FCEmployeeInfoPage.IsDisplayed(), "The 'Frontline Central Employee Information' is not displayed");
                hireWizard.FCEmployeeInfoPage.EnterLastName(_applicantData.LastName);
                _test.Log(LogStatus.Pass, "The 'Last Name' field is required");

                hireWizard.FCEmployeeInfoPage.EnterStreet1("");
                hireWizard.ClickNext();
                Assert.IsTrue(hireWizard.FCEmployeeInfoPage.IsDisplayed(), "The 'Frontline Central Employee Information' is not displayed");
                hireWizard.FCEmployeeInfoPage.EnterStreet1(_applicantData.Address.NumberAndStreet);
                _test.Log(LogStatus.Pass, "The 'Street 1' field is required");

                hireWizard.FCEmployeeInfoPage.EnterCity("");
                hireWizard.ClickNext();
                Assert.IsTrue(hireWizard.FCEmployeeInfoPage.IsDisplayed(), "The 'Frontline Central Employee Information' is not displayed");
                hireWizard.FCEmployeeInfoPage.EnterCity(_applicantData.Address.City);
                _test.Log(LogStatus.Pass, "The 'City' field is required");

                hireWizard.FCEmployeeInfoPage.EnterState("");
                hireWizard.ClickNext();
                Assert.IsTrue(hireWizard.FCEmployeeInfoPage.IsDisplayed(), "The 'Frontline Central Employee Information' is not displayed");
                hireWizard.FCEmployeeInfoPage.EnterState(_applicantData.Address.State);
                _test.Log(LogStatus.Pass, "The 'State' field is required");

                hireWizard.FCEmployeeInfoPage.EnterZip("");
                hireWizard.ClickNext();
                Assert.IsTrue(hireWizard.FCEmployeeInfoPage.IsDisplayed(), "The 'Frontline Central Employee Information' is not displayed");
                hireWizard.FCEmployeeInfoPage.EnterZip(_applicantData.Address.Zip);
                _test.Log(LogStatus.Pass, "The 'Zip' field is required");

                const string invalidSsn = "999";
                hireWizard.FCEmployeeInfoPage.EnterSocialSecurityNumber(invalidSsn);
                _test.Log(LogStatus.Info, "Enter into the 'Social Security Number' field: " + invalidSsn);

                hireWizard.ClickNext();
                _test.Log(LogStatus.Info, "Click 'Next'");

                // Make sure that the confirmation page isnt displayed
                Assert.IsTrue(hireWizard.FCEmployeeInfoPage.IsDisplayed(), "The 'Frontline Central Employee Information' is not displayed");
                Assert.IsTrue(hireWizard.FCEmployeeInfoPage.IsInvalidSSNMessageDisplayed(), "The invalid SSN message is not displayed");
                _test.Log(LogStatus.Pass, "The 'Social Security Number' field displays an error message if the number is invalid");

                // Enter a valid SSN
                hireWizard.FCEmployeeInfoPage.EnterSocialSecurityNumber(_applicantData.SocialSecurityNumber);

                const string invalidPhone = "999-111-2222";
                hireWizard.FCEmployeeInfoPage.EnterPrimaryPhoneNumber(invalidPhone);
                _test.Log(LogStatus.Info, "Enter into the 'Primary Phone' field: " + invalidPhone);

                hireWizard.ClickNext();
                _test.Log(LogStatus.Info, "Click 'Next'");

                // Make sure that the confirmation page isnt displayed
                Assert.IsTrue(hireWizard.FCEmployeeInfoPage.IsDisplayed(), "The 'Frontline Central Employee Information' is not displayed");
                _test.Log(LogStatus.Pass, "The 'Frontline Central Employee Information' page is still displayed if next is clicked and an invalid phone number is entered");

                Assert.IsTrue(hireWizard.FCEmployeeInfoPage.IsInvalidPhoneMessageDisplayed(), "The invalid phone number message is not displayed");
                _test.Log(LogStatus.Pass, "The 'Personal Phone Number' field displays an error message if the number is invalid");

                // Enter a valid phone number
                hireWizard.FCEmployeeInfoPage.EnterPrimaryPhoneNumber(_applicantData.Address.DaytimePhone);

                // Make sure all the employee assignment fields are enabled after marking the checkbox
                hireWizard.FCEmployeeInfoPage.MarkCreateEmployeeAssignment();
                _test.Log(LogStatus.Info, "Mark the employee assignment checkbox");

                // Make sure that the wizard doesnt display the confirmation screen if the required fields arent filled out
                hireWizard.ClickNext();
                _test.Log(LogStatus.Info, "Click 'Next'");
                Assert.IsTrue(hireWizard.FCEmployeeInfoPage.IsDisplayed(), "The FC Employee Info page should be displayed if the required fields are not filled out");
                _test.Log(LogStatus.Pass, "The FC Employee Info page is displayed after clicking next without filling out all required fields");

                //  Make sure that the employee assignment fields are disabled after making the checkbox
                hireWizard.FCEmployeeInfoPage.MarkCreateEmployeeAssignment();
                _test.Log(LogStatus.Info, "Un-mark the employee assignment checkbox");

                hireWizard.ClickNext();
                _test.Log(LogStatus.Info, "Click 'Next'");
                Assert.IsTrue(hireWizard.ConfirmationPage.IsDisplayed(), "The Confirmation page is not displayed after clicking next and filling out all required fields");
                _test.Log(LogStatus.Pass, "The Confirmation page is displayed after clicking next with all required fields populated");
            }
            catch (Exception e)
            {
                HandleException(e, _driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("IntegrationSmoke")]
        [TestProperty("TestArea", "FCIntegration")]
        [TestProperty("TestCaseName", "Applicant Data Sent to FC")]
        [TestProperty("TestCaseDescription", "Verify that the correct applicant data is sent to FC after hiring")]
        [TestProperty("UsesHardcodedData", "true")]
        [Ignore] // TODO decide if we should move this test to an API level / integration level test in another solution
        public void FCIntegration_ApplicantData_Sent_to_FC()
        {
            // page objects
            var applicantProfile = new ApplicantProfilePages(_driver);
            var hireWizard = new HireWizardPage(_driver);

            // api setup
            const int orgId = 20014;
            var startDateTime = DateTime.UtcNow;

            try  //Contains Contents of Test
            {
                // Finish the wizard
                hireWizard.ClickNext();
                _test.Log(LogStatus.Info, "Click 'Next'");
                hireWizard.ConfirmationPage.ClickFinish();
                _test.Log(LogStatus.Info, "Click 'Finish'");

                // wait for the data to be transferred
                Thread.Sleep(TimeSpan.FromSeconds(20));

                var parameters = new Dictionary<string, object>
                {
                    {"filter[fullName]", _applicantData.RealName},
                    {"sort[CreatedUtc]", "ASC"},
                    {"include", "PhoneNumbers,Emails,Addresses"}
                };

                var employeeApiContent = ApiHelpers.GetOrgEmployees(orgId, parameters);

                // name
                Assert.AreEqual(_applicantData.FirstName, (string)employeeApiContent["data"][0]["attributes"]["firstName"],
                    "The first name was not transferred correctly");
                Assert.AreEqual(_applicantData.LastName, (string)employeeApiContent["data"][0]["attributes"]["lastName"],
                    "The last name was not transferred correctly");
                _test.Log(LogStatus.Pass, "The applicants first and last name were transferred");

                // email
                Assert.AreEqual(_applicantData.Email, (string)employeeApiContent["included"][0]["attributes"]["emailAddress"],
                    "The email was not transferred correctly");
                Assert.AreEqual(true, (bool)employeeApiContent["included"][0]["attributes"]["isPrimary"],
                    "The email was not marked as primary");
                _test.Log(LogStatus.Pass, "The applicants email address was transferred and marked as primary");

                // phone
                Assert.AreEqual(_applicantData.Address.DaytimePhone, (string)employeeApiContent["included"][2]["attributes"]["number"],
                    "The phone number was not transferred correctly");
                Assert.AreEqual(true, (bool)employeeApiContent["included"][2]["attributes"]["isPrimary"],
                    "The phone number was not marked as primary");
                _test.Log(LogStatus.Pass, "The applicants phone number was transferred and marked as primary");

                // address
                Assert.AreEqual(true, (bool)employeeApiContent["included"][1]["attributes"]["isPrimary"],
                    "The address was not marked as primary");
                Assert.AreEqual(_applicantData.Address.NumberAndStreet, (string)employeeApiContent["included"][1]["attributes"]["street1"],
                    "The address was not transferred correctly");
                Assert.AreEqual(_applicantData.Address.AptNumber, (string)employeeApiContent["included"][1]["attributes"]["street2"],
                    "The apartment number/street2 was not transferred correctly");
                Assert.AreEqual(_applicantData.Address.City, (string)employeeApiContent["included"][1]["attributes"]["city"],
                    "The city was not transferred correctly");
                Assert.AreEqual(_applicantData.Address.State, (string)employeeApiContent["included"][1]["attributes"]["state"],
                    "The state was not transferred correctly");
                Assert.AreEqual(_applicantData.Address.Zip, (string)employeeApiContent["included"][1]["attributes"]["zip"],
                    "The zip code was not transferred correctly");
                _test.Log(LogStatus.Pass, "The applicants address was transferred and marked as primary");


                // cleanup - delete applicant from FC
                var toDate = startDateTime.AddHours(1);

                // set employee to terminated by changing 'statusId' to 2
                var employeeStatus = new
                {
                    data = new
                    {
                        attributes = new
                        {
                            statusId = 2,
                            from = startDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                            to = toDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                            statusChangeReason = "for deletion",
                            statusChangeReasonId = ""
                        }
                    }
                };

                var newStatus = ApiHelpers.PostEmployeeStatus((string) employeeApiContent["data"][0]["id"], JsonConvert.SerializeObject(employeeStatus));
                ApiHelpers.DeleteEmployee((string)employeeApiContent["data"][0]["id"], (int)newStatus["data"]["attributes"]["ownerVersionNumber"]);

            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, _driver);
                throw;
            }
        }

        #endregion
    }
}
