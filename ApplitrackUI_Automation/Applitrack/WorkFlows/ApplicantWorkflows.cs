using System;
using System.Threading;
using ApplitrackUITests.DataModels;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide.Applicants;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using Automation.Framework.Extensions;
using OpenQA.Selenium;

namespace ApplitrackUITests.WorkFlows
{
    public class ApplicantWorkflows : ApplitrackUIBase
    {
        private readonly IWebDriver _driver;

        public ApplicantWorkflows(IWebDriver driver)
        {
            _driver = driver;
        }

        public void DeleteApplicant(IApplicant applicant)
        {
            var applicantAdminPages = new ApplicantAdminPages(_driver);
            var applicantMenu = new SubMenuApplicants(_driver);

            _driver.SwitchToDefaultFrame();

            // navigate directly to the Applicants page
            BrowseTo($"{BaseUrls["ApplitrackLoginPage"]}/onlineapp/admin/_admin.aspx?Destination=Applicants", _driver);

            applicantAdminPages.Dashboard.EnterSearchApplicantName($"{applicant.LastName}, {applicant.FirstName}");

            // Unmark 'Submitted Only' because it is marked by default
            applicantAdminPages.Dashboard.MarkSubmittedOnly();

            applicantAdminPages.Dashboard.ClickSearch();

            // Mark employee record in table
            _driver.SwitchToFrameById(By.Id("MainContentsIFrame"));
            applicantAdminPages.SearchResultsPage.MarkItem(applicant.AppNo.ToString());

            // Click 'Delete their file > Delete Checked Applicants'
            _driver.SwitchToDefaultFrame();
            Thread.Sleep(TimeSpan.FromSeconds(1)); // wait because the menu sometimes dissapears if clicked too fast
            applicantMenu.ClickDeleteTheirFile();

            Thread.Sleep(TimeSpan.FromSeconds(1)); // wait because 'Delete Checked Applicants' doesnt always get clicked
            applicantMenu.ClickDeleteCheckedApplicants();

            // Confirm deletion
            applicantAdminPages.VacanciesByCategoryPages.ConfirmDeletion();
        }
    }
}