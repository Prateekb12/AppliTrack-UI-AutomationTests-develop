using System;
using System.Collections.Specialized;
using System.Threading;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using ApplitrackUITests.PageObjects.ApplicantSide;
using ApplitrackUITests.PageObjects.Menu;
using ApplitrackUITests.WorkFlows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;


namespace ApplitrackUITests.TestCases.JobTests
{
    [TestClass]
    public class JobRegressionTests : ApplitrackUIBase
    {
        #region Setup and TearDown

        private IWebDriver Driver;
        private ExtentTest test;

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

        /// <summary>
        /// Given I am creating a job posting from a blank form
        /// When I enter the Job Title and Position Type
        /// And I click Save
        /// Then the job posting should be associated with a Job ID
        /// </summary>
        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Create simple job posting from a blank form")]
        [TestProperty("TestCaseDescription", "Create simple job from a blank form")]
        [TestProperty("UsesHardcodedData", "false")]
        public void JobPosting_Create_From_Blank_Form()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);
            var jobData = new JobData();
            var jobPostingWorkflows = new JobPostingWorkflows(Driver);

            try //Contains Contents of Test
            {
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickCreateNewPosting();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > Create New Posting");

                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.CreateNewPostingPage.ClickFromBlankForm();
                test.Log(LogStatus.Pass, "Click Create from: A blank form");

                Driver.SwitchToFrameById("tabs_Panel");

                jobPostingPages.EditAndCreateJobPostingPage.MainTab.EnterJobTitle(jobData.JobTitle);
                test.Log(LogStatus.Pass, "Enter job title: " + jobData.JobTitle);

                jobPostingPages.EditAndCreateJobPostingPage.MainTab.SelectPositionType(jobData.PositionType);
                test.Log(LogStatus.Pass, "Selected Position Type: " + jobData.PositionType);

                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("MainContentsIFrame");

                jobPostingPages.EditAndCreateJobPostingPage.ClickSaveButton();
                test.Log(LogStatus.Pass, "Click save button");

                jobPostingPages.EditAndCreateJobPostingPage.GetJobId();

                Assert.IsTrue(jobPostingPages.EditAndCreateJobPostingPage.JobIsSaved());
                test.Log(LogStatus.Pass, "Verify job is saved, Job ID: " + jobPostingPages.EditAndCreateJobPostingPage.JobId);

                test.Log(LogStatus.Info, "Clean up: inactivate job");
                jobPostingWorkflows.InactivateJobPosting(jobPostingPages.EditAndCreateJobPostingPage.JobId);
                test.Log(LogStatus.Pass, "Job inactivated");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        /// <summary>
        /// Given I am creating a job posting from a blank form
        /// When I select a Position Type
        /// Then Administration should be in the list
        /// </summary>
        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Position Type Drop-down Contains Defaults")]
        [TestProperty("TestCaseDescription", "")]
        [TestProperty("UsesHardcodedData", "false")]
        [Ignore] // TODO figure out why this is flaky
        public void JobPosting_Position_Type_Contains_Defaults()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);

            try //Contains Contents of Test
            {
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickCreateNewPosting();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > Create New Posting");

                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.CreateNewPostingPage.ClickFromBlankForm();
                test.Log(LogStatus.Pass, "Click Create from: A blank form");

                Driver.SwitchToFrameById("tabs_Panel");

                jobPostingPages.EditAndCreateJobPostingPage.MainTab.SelectPositionType("Administration");
                Assert.AreEqual(jobPostingPages.EditAndCreateJobPostingPage.MainTab.GetSelectedPositionType(), "Administration");
                test.Log(LogStatus.Pass, "Job Posting drop-down contains 'Administration'");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        /// <summary>
        /// Given I am creating a job posting from a blank form
        /// When I select a Location
        /// Then District Office should be in the list
        /// </summary>
        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Location Drop-down Contains Defaults")]
        [TestProperty("TestCaseDescription", "")]
        [TestProperty("UsesHardcodedData", "false")]
        [Ignore] // TODO figure out why this is flaky
        public void JobPosting_Location_Contains_Defaults()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);

            try //Contains Contents of Test
            {
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickCreateNewPosting();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > Create New Posting");

                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.CreateNewPostingPage.ClickFromBlankForm();
                test.Log(LogStatus.Pass, "Click Create from: A blank form");

                Driver.SwitchToFrameById("tabs_Panel");

                jobPostingPages.EditAndCreateJobPostingPage.MainTab.SelectLocation("District Office");
                Assert.AreEqual(jobPostingPages.EditAndCreateJobPostingPage.MainTab.GetSelectedLocation(), "District Office");
                test.Log(LogStatus.Pass, "Location drop-down contains 'District Office'");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        /// <summary>
        /// Given I am creating a job posting from a blank form
        /// When I enter a date in an invalid format
        /// Then a warning should open indicating that a valid date is required
        /// </summary>
        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Create job posting with invalid date posted")]
        [TestProperty("TestCaseDescription", "Attempt to create a job with an invalid Date Posted date")]
        [TestProperty("UsesHardcodedData", "false")]
        public void JobPosting_Create_With_Invalid_Date_Posted()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);

            try //Contains Contents of Test
            {
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickCreateNewPosting();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > Create New Posting");

                Driver.SwitchToFrameById("MainContentsIFrame");

                jobPostingPages.CreateNewPostingPage.ClickFromBlankForm();
                test.Log(LogStatus.Pass, "Click Create from: A blank form");

                Driver.SwitchToFrameById("tabs_Panel");

                jobPostingPages.EditAndCreateJobPostingPage.MainTab.EnterDatePosted("invalid date format!");
                Assert.IsFalse(jobPostingPages.EditAndCreateJobPostingPage.MainTab.IsValidDatePosted());
                test.Log(LogStatus.Pass,
                    "'Valid date required' message appears after entering an invalid date in the 'Date Posted' field");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        /// <summary>
        /// Given I am creating a job posting from a blank form
        /// When I attempt to save the posting without entering a title
        /// Then an alert should open indicating that the title is required
        /// </summary>
        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Create job posting without title")]
        [TestProperty("TestCaseDescription", "Attempt to create a job posting without a Title")]
        [TestProperty("UsesHardcodedData", "false")]
        public void JobPosting_Create_Without_Title()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);

            try //Contains Contents of Test
            {
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickCreateNewPosting();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > Create New Posting");

                Driver.SwitchToFrameById("MainContentsIFrame");

                jobPostingPages.CreateNewPostingPage.ClickFromBlankForm();
                test.Log(LogStatus.Pass, "Click Create from: A blank form");

                jobPostingPages.EditAndCreateJobPostingPage.ClickSaveButton();
                test.Log(LogStatus.Pass, "Click the Save button before entering a Title");

                Assert.IsTrue(AlertDetect(Driver));
                Assert.IsTrue(CloseAlertAndGetItsText(Driver).Contains("Title is required"));
                test.Log(LogStatus.Pass, "Alert opens indicating that the title is required.");

                jobPostingPages.EditAndCreateJobPostingPage.ClickSaveAndNextButton();
                test.Log(LogStatus.Pass, "Click the Save and Next button before entering a Title");

                Assert.IsTrue(AlertDetect(Driver));
                Assert.IsTrue(CloseAlertAndGetItsText(Driver).Contains("Title is required"));
                test.Log(LogStatus.Pass, "Alert opens indicating that the title is required.");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        /// <summary>
        /// Given I am creating a job posting from a blank form
        /// When I enter a Title but do not select a Position Type
        /// And I attempt to save
        /// Then an alert should open indicating that the position type is required
        /// </summary>
        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Create job posting without a position type")]
        [TestProperty("TestCaseDescription", "Attempt to create a job posting with a title but without a position type")]
        [TestProperty("UsesHardcodedData", "false")]
        public void JobPosting_Create_Without_Position_Type()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);
            var jobData = new JobData();

            try //Contains Contents of Test
            {
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickCreateNewPosting();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > Create New Posting");

                Driver.SwitchToFrameById("MainContentsIFrame");

                jobPostingPages.CreateNewPostingPage.ClickFromBlankForm();
                test.Log(LogStatus.Pass, "Click Create from: A blank form");

                Driver.SwitchToFrameById("tabs_Panel");
                jobPostingPages.EditAndCreateJobPostingPage.MainTab.EnterJobTitle(jobData.JobTitle);
                test.Log(LogStatus.Pass, "Enter a Title: " + jobData.JobTitle);

                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("MainContentsIFrame");

                jobPostingPages.EditAndCreateJobPostingPage.ClickSaveButton();
                test.Log(LogStatus.Pass, "Click the Save button before entering a Position Type");

                Assert.IsTrue(AlertDetect(Driver));
                Assert.IsTrue(CloseAlertAndGetItsText(Driver).Contains("Position Type is required"));
                test.Log(LogStatus.Pass, "Alert opens indicating that the Position Type is required.");

                jobPostingPages.EditAndCreateJobPostingPage.ClickSaveAndNextButton();
                test.Log(LogStatus.Pass, "Click the Save and Next button before entering a Position Type");

                Assert.IsTrue(AlertDetect(Driver));
                Assert.IsTrue(CloseAlertAndGetItsText(Driver).Contains("Position Type is required"));
                test.Log(LogStatus.Pass, "Alert opens indicating that the Position Type is required.");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        /// <summary>
        /// Given I am creating a job from a blank form
        /// When I attempt to switch tabs without entering a title
        /// Then an alert should open indicating that the title is required
        /// </summary>
        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Switch tabs without entering a Job Posting title")]
        [TestProperty("TestCaseDescription", "Attempt to switch tabs before entering a Title for a job posting")]
        [TestProperty("UsesHardcodedData", "false")]
        public void JobPosting_Switch_Tabs_Without_Entering_Title()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);

            try //Contains Contents of Test
            {
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickCreateNewPosting();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > Create New Posting");

                Driver.SwitchToFrameById("MainContentsIFrame");

                jobPostingPages.CreateNewPostingPage.ClickFromBlankForm();
                test.Log(LogStatus.Pass, "Click Create from: A blank form");

                jobPostingPages.EditAndCreateJobPostingPage.ClickDescriptionTab();
                test.Log(LogStatus.Pass, "Click the Description tab before entering a title");

                Assert.IsTrue(AlertDetect(Driver));
                Assert.IsTrue(CloseAlertAndGetItsText(Driver).Contains("Title is required"));
                test.Log(LogStatus.Pass, "Alert opens indicating that the title is required.");

                jobPostingPages.EditAndCreateJobPostingPage.ClickAssignedApplicationPagesTab();
                test.Log(LogStatus.Pass, "Click the Assigned Application Pages tab before entering a title");

                Assert.IsTrue(AlertDetect(Driver));
                Assert.IsTrue(CloseAlertAndGetItsText(Driver).Contains("Title is required"));
                test.Log(LogStatus.Pass, "Alert opens indicating that the title is required.");

                jobPostingPages.EditAndCreateJobPostingPage.ClickPerPostingQuestionsTab();
                test.Log(LogStatus.Pass, "Click the Per Posting Questions tab before entering a title");

                Assert.IsTrue(AlertDetect(Driver));
                Assert.IsTrue(CloseAlertAndGetItsText(Driver).Contains("Title is required"));
                test.Log(LogStatus.Pass, "Alert opens indicating that the title is required.");

                jobPostingPages.EditAndCreateJobPostingPage.ClickPostingToolsTab();
                test.Log(LogStatus.Pass, "Click the Per Posting Questions tab before entering a title");

                Assert.IsTrue(AlertDetect(Driver));
                Assert.IsTrue(CloseAlertAndGetItsText(Driver).Contains("Title is required"));
                test.Log(LogStatus.Pass, "Alert opens indicating that the title is required.");

                jobPostingPages.EditAndCreateJobPostingPage.ClickFormsTab();
                test.Log(LogStatus.Pass, "Click the Forms tab before entering a title");

                Assert.IsTrue(AlertDetect(Driver));
                Assert.IsTrue(CloseAlertAndGetItsText(Driver).Contains("Title is required"));
                test.Log(LogStatus.Pass, "Alert opens indicating that the title is required.");

                jobPostingPages.EditAndCreateJobPostingPage.ClickAdvertiseTab();
                test.Log(LogStatus.Pass, "Click the Advertise tab before entering a title");

                Assert.IsTrue(AlertDetect(Driver));
                Assert.IsTrue(CloseAlertAndGetItsText(Driver).Contains("Title is required"));
                test.Log(LogStatus.Pass, "Alert opens indicating that the title is required.");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        /// <summary>
        /// Given I am creating a job posting from a blank form
        /// When I attempt to preview the job posting without saving
        /// Then an alert should open indicating that saving the post is required before previewing
        /// </summary>
        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Click Preview without saving")]
        [TestProperty("TestCaseDescription", "Attempt to preview the job posting without saving")]
        [TestProperty("UsesHardcodedData", "false")]
        public void JobPosting_Preview_Without_Saving()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);

            try //Contains Contents of Test
            {
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickCreateNewPosting();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > Create New Posting");

                Driver.SwitchToFrameById("MainContentsIFrame");

                jobPostingPages.CreateNewPostingPage.ClickFromBlankForm();
                test.Log(LogStatus.Pass, "Click Create from: A blank form");

                jobPostingPages.EditAndCreateJobPostingPage.ClickPreviewButton();
                test.Log(LogStatus.Pass, "Click the Preview button before saving");

                Assert.IsTrue(AlertDetect(Driver));
                Assert.IsTrue(CloseAlertAndGetItsText(Driver).Contains("You must save the posting first"));
                test.Log(LogStatus.Pass, "Alert opens indicating that saving the post is required.");
            }
            catch (Exception e) //On Error Do
            {
                
                HandleException(e, Driver);
                throw;
            }
        }

        /// <summary>
        /// Given I am creating a job posting from a blank form
        /// When I select the 'Based on Schedule' radio button in the Display Info pane
        /// And I enter dates in the past for both the 'Open From' and 'Thru' fields
        /// And I click save
        /// Then the job posting should be closed
        /// </summary>
        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Job is closed when Open From/Thru dates have already passed")]
        [TestProperty("TestCaseDescription", "Check to see that the job posting is closed when the Open From/Thru dates are in the past")]
        [TestProperty("UsesHardcodedData", "false")]
        public void JobPosting_Job_Closed_Based_On_Schedule()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);
            var jobData = new JobData();
            DateTime thisDay = DateTime.Today;

            try //Contains Contents of Test
            {
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickCreateNewPosting();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > Create New Posting");

                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.CreateNewPostingPage.ClickFromBlankForm();
                test.Log(LogStatus.Pass, "Click Create from: A blank form");

                Driver.SwitchToFrameById("tabs_Panel");

                jobPostingPages.EditAndCreateJobPostingPage.MainTab.EnterJobTitle(jobData.JobTitle);
                test.Log(LogStatus.Pass, "Enter job title: " + jobData.JobTitle);

                jobPostingPages.EditAndCreateJobPostingPage.MainTab.SelectPositionType(jobData.PositionType);
                test.Log(LogStatus.Pass, "Selected Position Type: " + jobData.PositionType);

                jobPostingPages.EditAndCreateJobPostingPage.MainTab.SelectBasedOnScheduleDisplayInfoRadioButton();
                Assert.IsTrue(jobPostingPages.EditAndCreateJobPostingPage.MainTab.BasedOnScheduleDisplayInfoFieldsVisible());
                test.Log(LogStatus.Pass,
                    "The 'Open from' and 'thru' fields are visible after selecting the 'Based on Schedule' radio button");

                jobPostingPages.EditAndCreateJobPostingPage.MainTab.EnterOpenFromDate(thisDay.AddDays(-2).ToString("d"));
                jobPostingPages.EditAndCreateJobPostingPage.MainTab.EnterThruDate(thisDay.AddDays(-1).ToString("d"));
                test.Log(LogStatus.Pass,
                    "Enter " + thisDay.AddDays(-2).ToString("d") + " into 'Open from' and " +
                    thisDay.AddDays(-1).ToString("d") + " into 'thru'");

                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.EditAndCreateJobPostingPage.ClickSaveButton();
                test.Log(LogStatus.Pass, "Click save button");

                Assert.IsTrue(jobPostingPages.EditAndCreateJobPostingPage.GetPreviewButtonText().Contains("Closed"));
                test.Log(LogStatus.Pass, "The job is Closed");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        /// <summary>
        /// Given I am creating a job posting from a blank form
        /// When I select the 'No' radio button in the Display Info pane
        /// And I click save
        /// Then the job posting should be closed
        /// </summary>
        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Job Closed When 'No' Radio Button Selected")]
        [TestProperty("TestCaseDescription", "Job Closed when 'No' radio button selected")]
        [TestProperty("UsesHardcodedData", "false")]
        public void JobPosting_Job_Closed_When_No_RadioButton_Selected()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);
            var jobPostingWorkflows = new JobPostingWorkflows(Driver);
            var jobData = new JobData();
            DateTime thisDay = DateTime.Today;

            try //Contains Contents of Test
            {
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickCreateNewPosting();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > Create New Posting");

                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.CreateNewPostingPage.ClickFromBlankForm();
                test.Log(LogStatus.Pass, "Click Create from: A blank form");

                Driver.SwitchToFrameById("tabs_Panel");

                jobPostingPages.EditAndCreateJobPostingPage.MainTab.EnterJobTitle(jobData.JobTitle);
                test.Log(LogStatus.Pass, "Enter job title: " + jobData.JobTitle);

                jobPostingPages.EditAndCreateJobPostingPage.MainTab.SelectPositionType(jobData.PositionType);
                test.Log(LogStatus.Pass, "Selected Position Type: " + jobData.PositionType);

                jobPostingPages.EditAndCreateJobPostingPage.MainTab.SelectDisplayInfoNoRadioButton();
                test.Log(LogStatus.Pass, "Select the 'No' radio button");

                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.EditAndCreateJobPostingPage.ClickSaveButton();
                test.Log(LogStatus.Pass, "Click save button");

                Assert.IsTrue(jobPostingPages.EditAndCreateJobPostingPage.GetPreviewButtonText().Contains("Closed"));
                test.Log(LogStatus.Pass, "The job is Closed");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        /// <summary>
        /// Given I am creating a job posting from a blank form
        /// When I select the 'Depends on Internal/External'  radio button
        /// And I select the 'No' radio buttons for both Internal and External
        /// Then the job should be closed
        /// </summary>
        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Job Closed When 'No' Selected for Internal and External Applicants")]
        [TestProperty("TestCaseDescription", "Check to see that the job posting is closed when the 'No' radio button is selected for both Internal and External applicants")]
        [TestProperty("UsesHardcodedData", "false")]
        public void JobPosting_Job_Closed_When_DependsOnInternalExternal_No_RadioButtons_Selected()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);
            var jobPostingWorkflows = new JobPostingWorkflows(Driver);
            var jobData = new JobData();
            DateTime thisDay = DateTime.Today;

            try //Contains Contents of Test
            {
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickCreateNewPosting();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > Create New Posting");

                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.CreateNewPostingPage.ClickFromBlankForm();
                test.Log(LogStatus.Pass, "Click Create from: A blank form");

                Driver.SwitchToFrameById("tabs_Panel");

                jobPostingPages.EditAndCreateJobPostingPage.MainTab.EnterJobTitle(jobData.JobTitle);
                test.Log(LogStatus.Pass, "Enter job title: " + jobData.JobTitle);

                jobPostingPages.EditAndCreateJobPostingPage.MainTab.SelectPositionType(jobData.PositionType);
                test.Log(LogStatus.Pass, "Selected Position Type: " + jobData.PositionType);

                jobPostingPages.EditAndCreateJobPostingPage.MainTab.SelectDependsOnInternalExternalRadioButton();
                jobPostingPages.EditAndCreateJobPostingPage.MainTab.SelectInternalNoRadioButton();
                jobPostingPages.EditAndCreateJobPostingPage.MainTab.SelectExternalNoRadioButton();
                test.Log(LogStatus.Pass, "Select the 'No' radio buttons for Internal and External applicants");

                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.EditAndCreateJobPostingPage.ClickSaveButton();
                test.Log(LogStatus.Pass, "Click save button");

                Assert.IsTrue(jobPostingPages.EditAndCreateJobPostingPage.GetPreviewButtonText().Contains("Closed"));
                test.Log(LogStatus.Pass, "The job is Closed");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Create Job Requisition from Blank Form")]
        [TestProperty("TestCaseDescription", "Create simple job requisition from a blank form")]
        [TestProperty("UsesHardcodedData", "false")]
        public void JobRequisition_Create_From_Blank_Form()
        {
            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);
            var jobPostingWorkflows = new JobPostingWorkflows(Driver);
            var jobData = new JobData();

            try
            {
                // Navigate to Job Postings > Create New Requisition
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickCreateNewRequisition();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > Create New Requisition");

                // Click 'A blank form'
                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.CreateNewRequisitionPage.ClickFromBlankForm();
                test.Log(LogStatus.Pass, "Click Create from: A blank form");

                // Enter Title
                Driver.SwitchToFrameById("tabs_Panel");
                jobPostingPages.EditAndCreateJobRequisitionPage.MainTab.EnterJobTitle(jobData.JobTitle);
                test.Log(LogStatus.Pass, "Enter requisition title: " + jobData.JobTitle);

                // Enter other required data...
                jobPostingPages.EditAndCreateJobRequisitionPage.MainTab.SelectPositionType(jobData.PositionType);
                test.Log(LogStatus.Pass, "Selected Position Type: " + jobData.PositionType);

                // Save
                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.EditAndCreateJobRequisitionPage.ClickSaveButton();
                test.Log(LogStatus.Pass, "Click save button");

                // Verify requisition was created
                jobPostingPages.EditAndCreateJobRequisitionPage.GetJobId();
                Assert.IsTrue(jobPostingPages.EditAndCreateJobRequisitionPage.JobIsSaved());
                test.Log(LogStatus.Pass, "Verify requisition is saved, Job ID: " + jobPostingPages.EditAndCreateJobRequisitionPage.JobId);

                // Cleanup
                test.Log(LogStatus.Info, "Clean up: inactivate job");
                jobPostingWorkflows.InactivateJobRequisition(jobPostingPages.EditAndCreateJobRequisitionPage.JobId);
                test.Log(LogStatus.Pass, "Job inactivated");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Job Requisition Create Draft")]
        [TestProperty("TestCaseDescription", "Create simple job requisition from a blank form and check to see if it is listed in 'My Draft Requisitions'")]
        [TestProperty("UsesHardcodedData", "false")]
        [Ignore] // TODO figure out why this is flaky
        public void JobRequisition_Create_Draft()
        {
            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);
            var jobPostingWorkflows = new JobPostingWorkflows(Driver);
            var jobData = new JobData();

            try
            {
                test.Log(LogStatus.Pass, "Starting at URL: " + BaseUrls["ApplitrackLoginPage"]);

                // Navigate to Job Postings > Create New Requisition
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickCreateNewRequisition();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > Create New Requisition");

                // Click 'A blank form'
                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.CreateNewRequisitionPage.ClickFromBlankForm();
                test.Log(LogStatus.Pass, "Click Create from: A blank form");

                // Enter Title
                Driver.SwitchToFrameById("tabs_Panel");
                jobPostingPages.EditAndCreateJobRequisitionPage.MainTab.EnterJobTitle(jobData.JobTitle);
                test.Log(LogStatus.Pass, "Enter requisition title: " + jobData.JobTitle);

                // Enter other required data...
                jobPostingPages.EditAndCreateJobRequisitionPage.MainTab.SelectPositionType(jobData.PositionType);
                test.Log(LogStatus.Pass, "Selected Position Type: " + jobData.PositionType);

                // Save
                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.EditAndCreateJobRequisitionPage.ClickSaveButton();
                test.Log(LogStatus.Pass, "Click save button");

                // Get the Requisition ID
                jobPostingPages.EditAndCreateJobRequisitionPage.GetJobId();
                test.Log(LogStatus.Pass, "Requisition is saved, Job ID: " + jobPostingPages.EditAndCreateJobRequisitionPage.JobId);

                // Navigate to Job Postings > My Draft Requisitions
                Driver.SwitchToDefaultFrame();
                mainMenu.ClickMainMenuTab();
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickMyDraftRequisitions();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > My Draft Requisitions");

                // Check to see if Requisition is in the list
                Driver.SwitchToFrameById("MainContentsIFrame");
                Assert.IsTrue(jobPostingPages.MyDraftRequisitionsPage.RequisitionInList(jobPostingPages.EditAndCreateJobRequisitionPage.JobId));

                // Cleanup
                test.Log(LogStatus.Info, "Clean up: inactivate job");
                jobPostingWorkflows.InactivateJobRequisition(jobPostingPages.EditAndCreateJobRequisitionPage.JobId);
                test.Log(LogStatus.Pass, "Job inactivated");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Job Posting Create New Template")]
        [TestProperty("TestCaseDescription", "Create a new job posting template")]
        [TestProperty("UsesHardcodedData", "false")]
        public void JobPosting_Create_New_Template()
        {
            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);
            var jobPostingWorkflows = new JobPostingWorkflows(Driver);
            var jobData = new JobData();

            try
            {
                // Navigate to job Postings > Create New Template
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickCreateNewTemplate();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > Create New Template");

                // Enter Title
                Driver.SwitchToFrameById("MainContentsIFrame");
                Driver.SwitchToFrameById("tabs_Panel");
                jobPostingPages.EditAndCreateJobPostingPage.MainTab.EnterJobTitle(jobData.TemplateTitle);
                test.Log(LogStatus.Pass, "Enter template title: " + jobData.TemplateTitle);

                // Click Save 
                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.EditAndCreateJobPostingPage.ClickSaveButton();
                test.Log(LogStatus.Pass, "Click save");

                // Get the ID and asser that the template has been saved
                jobPostingPages.EditAndCreateJobPostingPage.GetJobId();
                Assert.IsTrue(jobPostingPages.EditAndCreateJobPostingPage.JobIsSaved());
                test.Log(LogStatus.Pass, "Verify template is saved, Template ID: " + jobPostingPages.EditAndCreateJobPostingPage.JobId);

                // Cleanup - Delete the template
                test.Log(LogStatus.Info, "Begin cleanup");

                // Navigate to List Existing
                Driver.SwitchToDefaultFrame();
                jobPostingsSubMenu.ClickListExisting();
                test.Log(LogStatus.Pass, "Navigate to List Existing");

                Driver.SwitchToFrameById("MainContentsIFrame");
                // TODO move these methods to helper classes
                jobPostingPages.ListAllActivePostingsPage.MarkListingCheckbox(jobPostingPages.EditAndCreateJobPostingPage.JobId);
                jobPostingPages.ListExistingTemplatePage.ClickDeleteTemplate();
                jobPostingPages.ListAllActivePostingsPage.ConfirmInactivation();
                test.Log(LogStatus.Pass, "Deleted template");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Job Posting Open Postings By Category")]
        [TestProperty("TestCaseDescription", "Check to make sure the menu items in Open Postings By Category submenu open the correct category")]
        [TestProperty("UsesHardcodedData", "true")]
        public void JobPosting_Open_Postings_By_Category()
        {
            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);
            StringCollection categories = new StringCollection
            {
                "Administration",
                "IT",
                "Technology"
            };

            try
            {
                // Navigate to Job Postings > Open Postings By Category
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickOpenPostingsByCategory();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > Open Postings By Category");

                foreach (var category in categories)
                {
                    jobPostingsSubMenu.ClickCategory(category);
                    test.Log(LogStatus.Pass, "Checking: " + category);
                    Driver.SwitchToFrameById("MainContentsIFrame");
                    Assert.AreEqual(category, jobPostingPages.PostingsByCategoryPages.GetTableCategoryHeader(), 
                        "The correct Job Postings by Category table is not displayed");
                    test.Log(LogStatus.Pass, "Job category: " + category + " is displayed");
                    Driver.SwitchToDefaultFrame();
                    mainMenu.ClickBackIcon();
                }
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Job Posting Active Postings By Category")]
        [TestProperty("TestCaseDescription", "Check to make sure the menu items in Active Postings By Category submenu open the correct category")]
        [TestProperty("UsesHardcodedData", "false")]
        public void JobPosting_Active_Postings_By_Category()
        {
            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);

            try
            {
                // Navigate to Job Postings > Open Postings By Category
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickActivePostingsByCategory();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > Active Postings By Category");

                foreach (var category in jobPostingsSubMenu.GetJobPostingsByCategory())
                {
                    jobPostingsSubMenu.ClickCategory(category);
                    test.Log(LogStatus.Pass, "Checking: " + category);

                    Driver.SwitchToFrameById("MainContentsIFrame");
                    Assert.AreEqual(category, jobPostingPages.PostingsByCategoryPages.GetTableCategoryHeader(),
                        "The correct Job Postings by Category table is not displayed");
                    test.Log(LogStatus.Pass, "Job category: " + category + " is displayed");

                    Thread.Sleep(TimeSpan.FromSeconds(1)); //test breaks if it goes too fast here...
                    Driver.SwitchToDefaultFrame();
                    mainMenu.ClickBackIcon();
                }
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Job Posting All Postings By Status Menu")]
        [TestProperty("TestCaseDescription", "Check to make sure the menu contains all statuses in the system")]
        [TestProperty("UsesHardcodedData", "false")]
        public void JobPosting_All_Postings_By_Status_Menu()
        {
            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);

            // All the statuses that should appear in the menu
            var jobStatuses = new StringCollection
            {
                "All Postings",
                "Active Postings",
                "Inactive Postings",
                "Open Postings",
                "Open Externally",
                "Open Internally",
                "Closed Postings",
                "Closing Today",
                "Closing This Week"
            };

            try
            {
                // Navigate to Job Postings > All Postings By Status
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickAllPostingsByStatus();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > All Postings By Status");

                foreach (var status in jobStatuses)
                {
                    Assert.IsTrue(jobPostingsSubMenu.StatusExists(status), status + " was not found on the menu");
                    test.Log(LogStatus.Pass, status + " exists in the menu");
                }
            }
            catch (Exception e)
            {
                OnError(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Job Posting All Postings By Status Count")]
        [TestProperty("TestCaseDescription", "Check to make sure the numbers in the menu are correct")]
        [TestProperty("UsesHardcodedData", "false")]
        public void JobPosting_All_Posting_By_Status_Count()
        {
            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingsPages = new JobPostingsPages(Driver);

            // Statuses to check
            var jobStatuses = new StringCollection
            {
                "All Postings",
                "Active Postings",
                "Inactive Postings",
                "Closing This Week"
            };

            try
            {
                // Navigate to Job Postings > All Postings By Status
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickAllPostingsByStatus();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > All Postings By Status");

                foreach (var status in jobStatuses)
                {
                    // Get the count from the menu
                    test.Log(LogStatus.Info, "Checking: " + status);
                    var menuCount = jobPostingsSubMenu.GetMenuCount(status);

                    // Click on the status and get the count
                    jobPostingsSubMenu.ClickCategory(status);
                    Driver.SwitchToFrameById("MainContentsIFrame");
                    var tableCount = jobPostingsPages.AllPostingsByStatusPage.GetJobPostingCount();

                    Assert.AreEqual(menuCount, tableCount, 
                        "The menu number " + menuCount + " does not match the table " + tableCount);
                    test.Log(LogStatus.Pass, "The menu count: " + menuCount + " matches the table count: " + tableCount);

                    Driver.SwitchToDefaultFrame();
                    // If the count is 0, the menu will not change
                    // Therefore we should only click the back icon if the count is not 0
                    if (menuCount != "0")
                    {
                        mainMenu.ClickBackIcon();
                    }
                }
            }
            catch (Exception e)
            {
                OnError(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Job Requisition Approve and Open")]
        [TestProperty("TestCaseDescription", "Create simple job requisition, approve it, test to see if it is open")]
        [TestProperty("UsesHardcodedData", "false")]
        public void JobRequisition_Approve_and_Open()
        {
            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);
            var jobPostingWorkflows = new JobPostingWorkflows(Driver);
            var jobData = new JobData();

            try
            {
                // Navigate to Job Postings > Create New Requisition
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickCreateNewRequisition();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > Create New Requisition");

                // Click 'A blank form'
                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.CreateNewPostingPage.ClickFromBlankForm();
                test.Log(LogStatus.Pass, "Click Create from: A blank form");

                // Enter Title
                Driver.SwitchToFrameById("tabs_Panel");
                jobPostingPages.EditAndCreateJobRequisitionPage.MainTab.EnterJobTitle(jobData.JobTitle);
                test.Log(LogStatus.Pass, "Enter requisition title: " + jobData.JobTitle);

                // Enter other required data...
                jobPostingPages.EditAndCreateJobPostingPage.MainTab.SelectPositionType(jobData.PositionType);
                test.Log(LogStatus.Pass, "Selected Position Type: " + jobData.PositionType);

                // Save
                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.EditAndCreateJobRequisitionPage.ClickSaveButton();
                test.Log(LogStatus.Pass, "Click save button");

                // Verify requisition was created
                jobPostingPages.EditAndCreateJobRequisitionPage.GetJobId();
                test.Log(LogStatus.Pass, "Requisition is saved, Job ID: " + jobPostingPages.EditAndCreateJobRequisitionPage.JobId);

                // Go to Approval Process tab and select final approver
                jobPostingPages.EditAndCreateJobRequisitionPage.ClickApprovalProcessTab();
                Driver.SwitchToFrameById("tabs_Panel");
                jobPostingPages.EditAndCreateJobRequisitionPage.ApprovalProcessTab.SelectFinalApprover("Dr. Michael Ditka (Director of HR)");
                test.Log(LogStatus.Pass, "Selected final approver");

                // Click save
                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.EditAndCreateJobRequisitionPage.ClickSaveButton();
                test.Log(LogStatus.Pass, "Click save");

                // Click Submit Requisition
                jobPostingPages.EditAndCreateJobRequisitionPage.ClickSubmitRequisition();
                test.Log(LogStatus.Pass, "Click Submit Requisition");

                // Select requisition from list
                jobPostingPages.MyUnsubmittedRequisitionPage.SelectJobRequisition(jobPostingPages.EditAndCreateJobRequisitionPage.JobId);
                test.Log(LogStatus.Pass, "Select requisiton from the list");

                // Approve Requisition
                jobPostingPages.EditAndCreateJobRequisitionPage.ClickApproveRequisition();
                jobPostingPages.EditAndCreateJobRequisitionPage.ClickOkToApproveRequisition();

                // Check to see of pop-up text indicates it was approved
                Assert.IsTrue(CloseAlertAndGetItsText(Driver).Contains("Approved"));
                test.Log(LogStatus.Pass, "Alert opens indicating requisition is approved");

                // Check to see if requisition is open
                Assert.IsTrue(jobPostingPages.EditAndCreateJobRequisitionPage.RequisitionIsApproved(), "Requisition is not approved");
                test.Log(LogStatus.Pass, "Requisition is approved");
                Assert.IsTrue(jobPostingPages.EditAndCreateJobRequisitionPage.RequisitionIsOpen(), "Requisition is not open");
                test.Log(LogStatus.Pass, "Job requisition is open");

                // Cleanup
                test.Log(LogStatus.Info, "Clean up: inactivate job");
                jobPostingWorkflows.InactivateJobRequisition(jobPostingPages.EditAndCreateJobRequisitionPage.JobId);
                test.Log(LogStatus.Pass, "Job inactivated");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Job Requisition My Closed Requisitions")]
        [TestProperty("TestCaseDescription", "Create and approve a requisition, close it using the 'No' display info radio button, check the 'My Closed Requisitions' list to see if it is closed")]
        [TestProperty("UsesHardcodedData", "false")]
        public void JobRequisition_My_Closed_Requisitions()
        {
            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);
            var jobPostingWorkflows = new JobPostingWorkflows(Driver);
            var jobData = new JobData();

            try
            {
                // Navigate to Job Postings > Create New Requisition
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickCreateNewRequisition();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > Create New Requisition");

                // Click 'A blank form'
                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.CreateNewPostingPage.ClickFromBlankForm();
                test.Log(LogStatus.Pass, "Click Create from: A blank form");

                // Enter Title
                Driver.SwitchToFrameById("tabs_Panel");
                jobPostingPages.EditAndCreateJobRequisitionPage.MainTab.EnterJobTitle(jobData.JobTitle);
                test.Log(LogStatus.Pass, "Enter requisition title: " + jobData.JobTitle);

                // Enter other required data...
                jobPostingPages.EditAndCreateJobRequisitionPage.MainTab.SelectPositionType(jobData.PositionType);
                test.Log(LogStatus.Pass, "Selected Position Type: " + jobData.PositionType);

                // Save
                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.EditAndCreateJobRequisitionPage.ClickSaveButton();
                test.Log(LogStatus.Pass, "Click save button");

                // Verify requisition was created
                jobPostingPages.EditAndCreateJobRequisitionPage.GetJobId();
                test.Log(LogStatus.Pass, "Requisition is saved, Job ID: " + jobPostingPages.EditAndCreateJobRequisitionPage.JobId);

                // Go to Approval Process tab and select final approver
                jobPostingPages.EditAndCreateJobRequisitionPage.ClickApprovalProcessTab();
                Driver.SwitchToFrameById("tabs_Panel");
                jobPostingPages.EditAndCreateJobRequisitionPage.ApprovalProcessTab.SelectFinalApprover("Dr. Michael Ditka (Director of HR)");
                test.Log(LogStatus.Pass, "Selected final approver");

                // Click save
                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.EditAndCreateJobRequisitionPage.ClickSaveButton();
                test.Log(LogStatus.Pass, "Click save");

                // Click Submit Requisition
                jobPostingPages.EditAndCreateJobRequisitionPage.ClickSubmitRequisition();
                test.Log(LogStatus.Pass, "Click Submit Requisition");

                // Select requisition from list
                jobPostingPages.MyUnsubmittedRequisitionPage.SelectJobRequisition(jobPostingPages.EditAndCreateJobRequisitionPage.JobId);
                test.Log(LogStatus.Pass, "Select requisiton from the list");

                // Approve Requisition
                jobPostingPages.EditAndCreateJobRequisitionPage.ClickApproveRequisition();
                jobPostingPages.EditAndCreateJobRequisitionPage.ClickOkToApproveRequisition();

                // Check to see of pop-up text indicates it was approved
                Assert.IsTrue(CloseAlertAndGetItsText(Driver).Contains("Approved"));
                test.Log(LogStatus.Pass, "Alert opens indicating requisition is approved");

                // Select 'No' for Display Info
                Driver.SwitchToFrameById("tabs_Panel");
                jobPostingPages.EditAndCreateJobRequisitionPage.MainTab.SelectDisplayInfoNoRadioButton();
                test.Log(LogStatus.Pass, "Select the 'No' radio button for Display Info to close the job");

                // Save the requisition
                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.EditAndCreateJobRequisitionPage.ClickSaveButton();
                test.Log(LogStatus.Pass, "Click save button");

                // Navigate to My Closed Requisitions
                Driver.SwitchToDefaultFrame();
                jobPostingsSubMenu.ClickMyClosedRequisitions();
                test.Log(LogStatus.Pass, "Navigate to My Closed Requisitions");

                // Assert that the requisition is in the list
                Driver.SwitchToFrameById("MainContentsIFrame");
                Assert.IsTrue(jobPostingPages.MyDraftRequisitionsPage.RequisitionInList(jobPostingPages.EditAndCreateJobRequisitionPage.JobId));
                test.Log(LogStatus.Pass, "Requisition is in the list");

                // Cleanup
                test.Log(LogStatus.Info, "Clean up: inactivate job");
                jobPostingWorkflows.InactivateJobRequisition(jobPostingPages.EditAndCreateJobRequisitionPage.JobId);
                test.Log(LogStatus.Pass, "Job inactivated");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Job Requisition Update Existing and Preview")]
        [TestProperty("TestCaseDescription", "Create simple job requisition from a blank form, update it, make sure the preview displays the correct information")]
        [TestProperty("UsesHardcodedData", "false")]
        [Ignore] // TODO figure out why this is failing
        public void JobRequisition_Update_Existing_and_Preview()
        {
            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);
            var jobPostingWorkflows = new JobPostingWorkflows(Driver);
            var applicantJobPostingPage = new JobPostingPage(Driver);

            // helpers
            var windowHelper = new WindowHelpers(Driver);

            // set up data to change
            var newTitle = "Changed Title";
            var newPositionType = "AppliTrack Fit Trial";
            var newLocation = "District Office";
            var newDescription = "This is a new description";

            try
            {
                // Create a new job requisition
                jobPostingWorkflows.CreateJobRequisition();
                test.Log(LogStatus.Pass, "Created job requisition with ID: " + jobPostingWorkflows.JobId);

                // Go to Job Postings > All Active Requisitions
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickAllActiveRequisitions();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > All Active Requisitions");

                // Click the previously created requisition
                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.MyUnsubmittedRequisitionPage.SelectJobRequisition(jobPostingWorkflows.JobId);
                test.Log(LogStatus.Pass, "Select job requisition from the list");

                // Change the Title
                Driver.SwitchToFrameById("tabs_Panel");
                jobPostingPages.EditAndCreateJobRequisitionPage.MainTab.EnterJobTitle(newTitle);
                test.Log(LogStatus.Pass, "Change job title to: " + newTitle);

                // Change the Position Type 
                jobPostingPages.EditAndCreateJobRequisitionPage.MainTab.SelectPositionType(newPositionType);
                test.Log(LogStatus.Pass, "Change position type to: " + newPositionType);

                // Change the Location
                jobPostingPages.EditAndCreateJobRequisitionPage.MainTab.SelectLocation(newLocation);
                test.Log(LogStatus.Pass, "Change location to: " + newLocation);

                // Description 
                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.EditAndCreateJobRequisitionPage.ClickDescriptionTab();

                // change the description
                Driver.SwitchToFrameById("tabs_Panel");
                Driver.SwitchToFrameByClass("cke_wysiwyg_frame");
                jobPostingPages.EditAndCreateJobRequisitionPage.DescriptionTab.EnterDescription(newDescription);

                // Save
                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.EditAndCreateJobRequisitionPage.ClickSaveButton();
                test.Log(LogStatus.Pass, "Click the save button");

                // Preview the job
                jobPostingPages.EditAndCreateJobRequisitionPage.ClickPreviewButton();
                windowHelper.SwitchToPopup();

                // Assert that the values have changed

                // Check that the title matches the change
                Assert.IsTrue(applicantJobPostingPage.GetPostedJobTitle().Contains(newTitle), 
                    "expected job title: " + newTitle + " does not match actual: " + applicantJobPostingPage.GetPostedJobTitle());
                test.Log(LogStatus.Pass, "The job title matches: " + newTitle);

                // Check that the position type matches the change
                Assert.AreEqual(newPositionType, applicantJobPostingPage.GetPostedPositionType(), 
                    "expected position type: " + newPositionType + " does not match actual: " + applicantJobPostingPage.GetPostedPositionType());
                test.Log(LogStatus.Pass, "The position type matches: " + newPositionType);

                // Check that the time posted is todays date
                Assert.AreEqual(DateTime.Today.ToShortDateString(), applicantJobPostingPage.GetDatePosted(),
                    "expected date posted: " + DateTime.Today.ToShortDateString() + " does not match actual: " + applicantJobPostingPage.GetDatePosted());
                test.Log(LogStatus.Pass, "The date posted matches: " + DateTime.Today.ToShortDateString());

                // Check that the location matches the change
                Assert.AreEqual(newLocation, applicantJobPostingPage.GetPostedLocation(),
                    "expected position type: " + newLocation+ " does not match actual: " + applicantJobPostingPage.GetPostedLocation());
                test.Log(LogStatus.Pass, "The location matches: " + newLocation);

                // Check that the description matches
                Assert.IsTrue(applicantJobPostingPage.GetPostedDescription().Contains(newDescription), 
                    "expected description: " + newDescription+ " does not match actual: " + applicantJobPostingPage.GetPostedDescription());
                test.Log(LogStatus.Pass, "The description matches: " + newDescription);

                // Close the preview window
                windowHelper.ClosePopup();

                // Cleanup
                test.Log(LogStatus.Info, "Beginning cleanup");

                // Inactivate Job Requisition
                jobPostingWorkflows.InactivateJobRequisition(jobPostingWorkflows.JobId);
                test.Log(LogStatus.Pass, "Job requisition inactivated");
            }
            catch (Exception e)
            {
                HandleException(e, Driver);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Job Postings")]
        [TestProperty("TestCaseName", "Job Posting Displayed in Online Jobs")]
        [TestProperty("TestCaseDescription", "Create simple job posting and check to see if it is listed on the landing page")]
        [TestProperty("UsesHardcodedData", "false")]
        [Ignore] // test is timing out waiting for the page to load
        public void JobPosting_Displayed_in_Online_Jobs()
        {
            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);
            var jobPostingWorkflows = new JobPostingWorkflows(Driver);
            var jobData = new JobData();
            var landingPage = new DefaultLandingPage(Driver);

            try
            {
                // Create new job posting
                mainMenu.ClickJobPostings();
                jobPostingsSubMenu.ClickCreateNewPosting();
                test.Log(LogStatus.Pass, "Navigate to Job Postings > Create New Posting");

                Driver.SwitchToFrameById("MainContentsIFrame");
                jobPostingPages.CreateNewPostingPage.ClickFromBlankForm();
                test.Log(LogStatus.Pass, "Click Create from: A blank form");

                Driver.SwitchToFrameById("tabs_Panel");

                jobPostingPages.EditAndCreateJobPostingPage.MainTab.EnterJobTitle(jobData.JobTitle);
                test.Log(LogStatus.Pass, "Enter job title: " + jobData.JobTitle);

                jobPostingPages.EditAndCreateJobPostingPage.MainTab.SelectPositionType(jobData.PositionType);
                test.Log(LogStatus.Pass, "Selected Position Type: " + jobData.PositionType);

                Driver.SwitchToDefaultFrame();
                Driver.SwitchToFrameById("MainContentsIFrame");

                jobPostingPages.EditAndCreateJobPostingPage.ClickSaveButton();
                test.Log(LogStatus.Pass, "Click save button");

                jobPostingPages.EditAndCreateJobPostingPage.GetJobId();
                test.Log(LogStatus.Pass, "Job ID is: " + jobPostingPages.EditAndCreateJobPostingPage.JobId);

                // Navigate to the online portal
                BrowseTo(BaseUrls["ApplitrackLandingPage"], Driver);

                // Check to see if the job is listed in the jobs list
                landingPage.ClickAllJobs();
                Assert.IsTrue(landingPage.JobIsInList(jobPostingPages.EditAndCreateJobPostingPage.JobId), 
                    "Unable to find Job ID " + jobPostingPages.EditAndCreateJobPostingPage.JobId + " on page");
                test.Log(LogStatus.Pass, "Job is listed on the landing page");

                // cleanup
                test.Log(LogStatus.Info, "Beginning cleanup - inactivating job posting");
                BrowseTo(BaseUrls["ApplitrackLoginPage"], Driver);
                jobPostingWorkflows.InactivateJobPosting(jobPostingPages.EditAndCreateJobPostingPage.JobId);
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
