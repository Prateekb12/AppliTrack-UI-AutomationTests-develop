using System;
using System.Threading;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using ApplitrackUITests.PageObjects.Menu;
using Automation;
using OpenQA.Selenium;

namespace ApplitrackUITests.WorkFlows
{
    public class JobPostingWorkflows : BaseFrameWork
    {
        private IWebDriver Driver;
        public JobPostingWorkflows(IWebDriver Driver)
        {
            this.Driver = Driver;
        }

        public int JobId { get; set; }

        public void InactivateJobPosting(int jobId)
        {
            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);

            Console.WriteLine("Attemping to inactivate job posting with ID {0}", jobId);

            // navigate to Forms > Design Forms and Packets > Edit Forms
            Driver.SwitchToDefaultFrame();
            mainMenu.ClickMainMenuTab();
            mainMenu.ClickJobPostings();
            jobPostingsSubMenu.ClickListAllActivePostings();

            // select the job posting in the list
            Driver.SwitchToFrameById("MainContentsIFrame");
            jobPostingPages.ListAllActivePostingsPage.MarkListingCheckbox(jobId);

            // inactivate the posting
            Driver.SwitchToDefaultFrame();
            jobPostingPages.ListAllActivePostingsPage.ClickInactivatePostings();
            jobPostingPages.ListAllActivePostingsPage.ConfirmInactivation();
        }

        // TODO
        public void CreateJobRequisition()
        {
            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);
            var jobPostingWorkflows = new JobPostingWorkflows(Driver);
            var jobData = new JobData();

            Driver.SwitchToDefaultFrame();
            mainMenu.ClickMainMenuTab();
            // Navigate to Job Postings > Create New Requisition
            mainMenu.ClickJobPostings();
            jobPostingsSubMenu.ClickCreateNewRequisition();

            // Click 'A blank form'
            Driver.SwitchToFrameById("MainContentsIFrame");
            jobPostingPages.CreateNewPostingPage.ClickFromBlankForm();

            // Enter Title
            Driver.SwitchToFrameById("tabs_Panel");
            jobPostingPages.EditAndCreateJobRequisitionPage.MainTab.EnterJobTitle(jobData.JobTitle);

            // Enter other required data...
            jobPostingPages.EditAndCreateJobRequisitionPage.MainTab.SelectPositionType(jobData.PositionType);

            // Save
            Driver.SwitchToDefaultFrame();
            Driver.SwitchToFrameById("MainContentsIFrame");
            jobPostingPages.EditAndCreateJobRequisitionPage.ClickSaveButton();

            // Get the Requisition ID
            jobPostingPages.EditAndCreateJobRequisitionPage.GetJobId();

            // TODO figure out a more elegant way of doing this
            JobId = jobPostingPages.EditAndCreateJobRequisitionPage.JobId;

            // Go back o the main screen
            Driver.SwitchToDefaultFrame();
            mainMenu.ClickMainMenuTab();
        }

        public void InactivateJobRequisition(int jobId)
        {
            var mainMenu = new MainMenu(Driver);
            var jobPostingsSubMenu = new SubMenuJobPostings(Driver);
            var jobPostingPages = new JobPostingsPages(Driver);

            Console.WriteLine("Attemping to inactivate job posting with ID {0}", jobId);

            // prevent issue where the menu dissapears if you click it too fast
            Thread.Sleep(TimeSpan.FromSeconds(1));

            // Navigate to Job Postings > Click All Active Requisitions
            Driver.SwitchToDefaultFrame();
            mainMenu.ClickMainMenuTab();
            mainMenu.ClickJobPostings();
            jobPostingsSubMenu.ClickAllActiveRequisitions();

            // select the job posting in the list
            Driver.SwitchToFrameById("MainContentsIFrame");
            jobPostingPages.ListAllActivePostingsPage.MarkListingCheckbox(jobId);

            // inactivate the requisition
            Driver.SwitchToDefaultFrame();
            jobPostingPages.ListAllActivePostingsPage.ClickInactivatePostings();
            jobPostingPages.ListAllActivePostingsPage.ConfirmInactivation();
        }
    }
}
