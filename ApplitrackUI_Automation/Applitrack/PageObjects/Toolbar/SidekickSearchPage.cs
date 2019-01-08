using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Automation.Framework.Extensions;
using OpenQA.Selenium.Support.UI;
using ApplitrackUITests.Helpers;

namespace ApplitrackUITests.PageObjects.Toolbar
{
    public class SidekickSearchPage : ToolbarSearchPage
    {
        private readonly IWebDriver _driver;
        private CommonWaits _commonWaits;

        #region Constructors

        public SidekickSearchPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            _commonWaits = new CommonWaits(_driver);
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        [FindsBy(How = How.CssSelector, Using = "input.search")]
        private IWebElement SearchBox { get; set; }

        // the div containing the search results
        [FindsBy(How = How.CssSelector, Using = ".sk--search-content")]
        protected override IWebElement SearchResults { get; set; }

        // the headers for the search categories: Applicants, Employees, Job Postings, Users
        [FindsBy(How = How.CssSelector, Using = ".sidekick-search-header")]
        private IList<IWebElement> SearchHeaders { get; set; }

        // search results for each category: Applicants, Employees, Job Postings, Users
        [FindsBy(How = How.CssSelector, Using = ".sidekick-search-resultset")]
        private IList<IWebElement> ResultSets { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div.sidekick-search-header-options > a")]
        private IList<IWebElement> AdvancedSearchLinks { get; set; }

        #endregion

        #region Page Actions

        private enum ResultTypes { Applicant, Employee, JobPosting, User }

        /// <summary>
        /// Verify that the search page is displayed
        /// </summary>
        public override bool IsDisplayed()
        {
            try
            {
                return !SearchResults.Text.Contains("Error fetching");
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Open the given search result
        /// </summary>
        /// <param name="searchResult">The search result to open</param>
        private void OpenResult(IWebElement searchResult)
        {
            searchResult.WaitAndClick(_driver);
            // switch focus to the newly opened browser tab
            _driver.SwitchToPopup();
        }

        /// <summary>
        /// Select the first search result in the given result type
        /// </summary>
        /// <param name="resultType">The result type to select from</param>
        private void SelectFirstResult(ResultTypes resultType)
        {
            var searchResult = ResultSets[(int) resultType].FindElement(By.CssSelector("a"));
            OpenResult(searchResult);
        }

        public override void SelectSearchResult(string result)
        {
            var searchResult = _driver.FindElement(By.PartialLinkText(result));
            OpenResult(searchResult);
        }

        public override void EnterSearchText(string searchTerm)
        {
            SearchBox.Clear();
            SearchBox.SendKeys(searchTerm);
        }

        public override void OpenApplicant(string result = null)
        {
            if (String.IsNullOrWhiteSpace(result))
            {
                SelectFirstResult(ResultTypes.Applicant);
            }
            else
            {
                SelectSearchResult(result);
            }
        }

        public override void OpenEmployee(string result = null)
        {
            if (String.IsNullOrWhiteSpace(result))
            {
                SelectFirstResult(ResultTypes.Employee);
            }
            else
            {
                SelectSearchResult(result);
            }
        }

        public override void OpenJobPosting(string result = null)
        {
            if (String.IsNullOrWhiteSpace(result))
            {
                SelectFirstResult(ResultTypes.JobPosting);
            }
            else
            {
                SelectSearchResult(result);
            }
        }

        public override void OpenUser(string result = null)
        {
            if (String.IsNullOrWhiteSpace(result))
            {
                SelectFirstResult(ResultTypes.User);
            }
            else
            {
                SelectSearchResult(result);
            }
        }

        public override bool ApplicantsAreDisplayed()
        {
            return CategoryIsDisplayed("Applicants");
        }

        public override bool EmployeesAreDisplayed()
        {
            return CategoryIsDisplayed("Employees");
        }

        public override bool JobPostingsAreDisplayed()
        {
            return CategoryIsDisplayed("Job Postings");
        }

        public override bool UsersAreDisplayed()
        {
            return CategoryIsDisplayed("Users");
        }

        private bool CategoryIsDisplayed(string category)
        {
            try
            {
                WaitForSearchResults();
                return SearchHeaders.Any(header => header.Text.IndexOf(category, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public override void ClickApplicantsAdvancedSearch()
        {
            WaitForSearchResults();
            AdvancedSearchLinks[0].WaitAndClick(_driver);
            _commonWaits.WaitForLoadingScreen();
        }

        public override void ClickEmployeesAdvancedSearch()
        {
            WaitForSearchResults();
            AdvancedSearchLinks[1].WaitAndClick(_driver);
            _commonWaits.WaitForLoadingScreen();
        }

        private void WaitForSearchResults()
        {
            // wait for the 'Loading' screen to disppear and for the search results to be displayed
            _driver.Wait().Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h4.sidekick-search-header")));
        }

        #endregion
    }
}
