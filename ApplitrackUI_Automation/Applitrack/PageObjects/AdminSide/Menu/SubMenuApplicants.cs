using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using ApplitrackUITests.Helpers;
using Automation;
using Automation.Framework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Menu
{
    public class SubMenuApplicants : BasePageObject
    {
        private readonly IWebDriver _driver;

        #region Constructors

        public SubMenuApplicants(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        //Page Objects for this page
        [FindsBy(How = How.CssSelector, Using = "li.Header")]
        private IWebElement MenuHeader { get; set; }

        //Page Objects for this page
        [FindsBy(How = How.LinkText, Using = "Applicant Dashboard")]
        private IWebElement ApplicantDashboard { get; set; }

        //Vacancies Menu and Sub Menu
        [FindsBy(How = How.LinkText, Using = "Vacancies by Category")]
        private IWebElement VacanciesByCategory { get; set; }

        //Vacancies By Location Menu and Sub Menu
        [FindsBy(How = How.LinkText, Using = "Vacancies by Location")]
        private IWebElement VacanciesByLocation { get; set; }

        //Category Pipelines Menu and Sub Menu
        [FindsBy(How = How.LinkText, Using = "Category Pipelines")]
        private IWebElement CategoryPipelines { get; set; }

        //Position Pools Menu and Sub Menu
        [FindsBy(How = How.LinkText, Using = "Position Pools")]
        private IWebElement PositionPools { get; set; }

        //Actions, Notes and Status Menu and Sub Menu
        [FindsBy(How = How.LinkText, Using = "Actions, Notes and Status")]
        private IWebElement ActionsNotesStatus { get; set; }

        [FindsBy(How = How.LinkText, Using = "Search Form")]
        private IWebElement SearchForm { get; set; }

        // 'Online Application' on the Applicant Profile submenu
        [FindsBy(How = How.PartialLinkText, Using = "Online Application")]
        private IWebElement OnlineApplicationLink { get; set; }

        // 'References' on the Applicant Profile submenu
        [FindsBy(How = How.PartialLinkText, Using = "References")]
        private IWebElement ReferencesLink { get; set; }

        // 'View all applicants' link from Applicants > Vacancies by Category > 'Category' > 'Job'
        [FindsBy(How = How.PartialLinkText, Using = "View all applicants")]
        private IWebElement ViewAllApplicantsLink { get; set; }

        // 'Delete their file' link on all pages containing a scrolltable
        [FindsBy(How = How.PartialLinkText, Using = "Delete their file")]
        private IWebElement DeleteTheirFileLink { get; set; }

        // 'Delete Checked Applicants' link that appears after clicking 'Delete their file'
        [FindsBy(How = How.PartialLinkText, Using = "Delete Checked Applicants")]
        private IWebElement DeleteCheckedApplicantsLink { get; set; }

        #endregion

        #region Page Actions

        /// <summary>
        /// Get the name of each link from the menu
        /// Used for Vacancies by Category and Vacancies by Location
        /// </summary>
        /// <returns>An enumerable of categories</returns>
        public IEnumerable<string> GetMenuItems()
        {
            var categories = _driver.FindElements(By.CssSelector("ul.menu li a.has-child"));
            foreach (var element in categories)
            {
                if (IsJobLink(element))
                {
                    yield return element.Text;
                }
            }
        }

        /// <summary>
        /// Check to see of the element is a category link
        /// </summary>
        /// <param name="element"></param>
        /// <returns>True if the link is a category, false otherwise</returns>
        private bool IsJobLink(IWebElement element)
        {
            return element.GetAttribute("href").Contains("#NavFeed-Vacancies-ListJobs");
        }

        /// <summary>
        /// Click the Applicant Dashboard link on the Applicants submenu
        /// </summary>
        public void ClickApplicantDashboard()
        {
            ApplicantDashboard.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the Vacancies by Category link on the Applicants submenu
        /// </summary>
        public void ClickVacanciesByCategory()
        {
            VacanciesByCategory.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the Vacancies by Location link on the Applicants submenu
        /// </summary>
        public void ClickVacanciesByLocation()
        {
            VacanciesByLocation.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the Search Form link on the Applicants submenu
        /// </summary>
        public void ClickSearchForm()
        {
            SearchForm.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// The 'Online Application' item on the applicant profile menu
        /// </summary>
        public void ClickOnlineApplication()
        {
            OnlineApplicationLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'References' item on the applicant profile menu
        /// </summary>
        public void ClickReferences()
        {
            ReferencesLink.WaitAndClick(_driver);

        }

        /// <summary>
        /// Click a category on the menu
        /// </summary>
        /// <param name="category">The category to click</param>
        public void ClickCategory(string category)
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
            var categoryLink = _driver.FindElement(By.PartialLinkText(category));
            categoryLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click a job in the menu
        /// </summary>
        public void ClickJob()
        {
            var jobLink = _driver.FindElement(By.Id("ListViewCatJobs_ctrl0_LIDataItem"));
            jobLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'View All Applicants' link
        /// </summary>
        public void ClickViewAllApplicants()
        {
            ViewAllApplicantsLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Delete their file' link
        /// </summary>
        public void ClickDeleteTheirFile()
        {
            DeleteTheirFileLink.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the 'Delete Checked Applicants' link
        /// This link appears after clicking 'Delete their file'
        /// </summary>
        public void ClickDeleteCheckedApplicants()
        {
            DeleteCheckedApplicantsLink.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Get the count of records from the View All Applicants menu item
        /// </summary>
        /// <returns>The number of applicants from the menu</returns>
        public string GetMenuCount()
        {
            var parent = ViewAllApplicantsLink.GetParentElement();
            var count = parent.FindElement(By.CssSelector("span.count.pull-right")).Text;
            return count;
        }

        /// <summary>
        /// Get the Job ID from the menu header from Vacancies by Category
        /// </summary>
        /// <returns>The ID of the selected job</returns>
        public string GetJobIdHeader()
        {
            waitForIt(_driver);
            var jobHeader = _driver.FindElement(By.Id("Li2")).Text;
            var jobId = Regex.Split(jobHeader, @"\W+");
            return jobId[1];
        }

        #endregion

    }
}
