using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Automation.Framework.Extensions;
using By = OpenQA.Selenium.By;

namespace ApplitrackUITests.PageObjects.Toolbar
{
    public class ApplitrackSearchPage : ToolbarSearchPage 
    {
        //For This Page
        private IWebDriver _driver;

        #region Constructors

        public ApplitrackSearchPage(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        #endregion

        #region UI Selectors 

        [FindsBy(How = How.Id, Using = "TextBoxSearchPopover")]
        private IWebElement PopOverSearchBox { get; set; }

        [FindsBy(How = How.Id, Using = "SearchPopover")]
        private IWebElement SearchPopover { get; set; }

        // the div containing the search results
        [FindsBy(How = How.Id, Using = "SearchSectionResults")]
        protected override IWebElement SearchResults { get; set; }

        // The Applicants tab in the search page
        [FindsBy(How = How.CssSelector, Using = "#LIApplicants > a")]
        private IWebElement ApplicantsTabLink { get; set; }

        // The Employees tab in the search page 
        [FindsBy(How = How.CssSelector, Using = "#LIEmployees > a")]
        private IWebElement EmployeesTabLink { get; set; }

        // The Vacancies tab in the search page 
        [FindsBy(How = How.CssSelector, Using = "#LIVacancies > a")]
        private IWebElement VacanciesTabLink { get; set; }

        // The Users tab in the search page 
        [FindsBy(How = How.CssSelector, Using = "#LIUsers > a")]
        private IWebElement UsersTabLink { get; set; }

        // A list containing all search page 
        [FindsBy(How = How.CssSelector, Using = "div#SearchSectionResults div.SplashInboxItem a")]
        private IList<IWebElement> SearchResultsLinks { get; set; }

        #endregion

        #region Page Actions

        /// <summary>
        /// Verify that the search page is displayed
        /// </summary>
        public override bool IsDisplayed()
        {
            return SearchPopover.Displayed;
        }

        public override bool UsersAreDisplayed()
        {
            return UsersTabLink.Displayed;
        }

        /// <summary>
        /// Enter text into the popover search textbox
        /// </summary>
        /// <param name="searchTerm">The person to be searched for</param>
        public override void EnterSearchText(string searchTerm)
        {
            Console.Out.WriteLineAsync("Popover Enter text to search for " + searchTerm);
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(d => PopOverSearchBox.Displayed);
            PopOverSearchBox.WaitAndClick(_driver);
            PopOverSearchBox.Clear();
            PopOverSearchBox.SendKeys(searchTerm);
        }

        public override void OpenApplicant(string result = null)
        {
            if (String.IsNullOrWhiteSpace(result))
            {
                ClickFirstApplicantResult();
            }
            else
            {
                SelectSearchResult(result);
            }

            OpenApplication();
        }

        public override void OpenEmployee(string result = null)
        {
            ClickEmployeeTab();

            if (String.IsNullOrWhiteSpace(result))
            {
                ClickFirstEmployeeResult();
            }
            else
            {
                SelectSearchResult(result);
            }

            OpenEmployeeRecord();
        }

        public override void OpenJobPosting(string result = null)
        {
            ClickVacanciesTab();

            if (String.IsNullOrWhiteSpace(result))
            {
                ClickFirstVacancyResult();
            }
            else
            {
                SelectSearchResult(result);
            }

            OpenVacancy();
        }

        public override void OpenUser(string result = null)
        {
            ClickUsersTab();

            if (String.IsNullOrWhiteSpace(result))
            {
                ClickFirstUserResult();
            }
            else
            {
                SelectSearchResult(result);
            }
        }

        /// <summary>
        /// Click the 'Vacancies' tab on the search page 
        /// </summary>
        private void ClickVacanciesTab()
        {
            VacanciesTabLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Users' tab on the search page
        /// </summary>
        private void ClickUsersTab()
        {
            UsersTabLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Employee' tab on the search page 
        /// </summary>
        private void ClickEmployeeTab()
        {
            EmployeesTabLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Open the page for a search result using linktext
        /// </summary>
        /// <param name="result">The text of the record to be opened</param>
        public override void SelectSearchResult(string result)
        {
            var name = _driver.FindElement(By.LinkText(result));
            name.WaitAndClick(_driver);

            var openApplicationLink = _driver.FindElement(By.CssSelector(".popover-content a"));
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(openApplicationLink));
            openApplicationLink.WaitAndClick(_driver);
        }
    
        // The Applicant Number
        private string AppNo { get; set; }

        // The Vacancy Number
        private string JobId { get; set; }

        // The User Number
        private string UserNo { get; set; }

        // The Employee Number
        private string EmpNo { get; set; }

        /// <summary>
        /// Click the first applicant record in the search results
        /// </summary>
        private void ClickFirstApplicantResult()
        {
            // Remove "Applicants_" from this result to get the applicants Id number
            AppNo = SearchResultsLinks[0].GetAttribute("id").Substring(11);
            SearchResultsLinks[0].WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Open Application' link that appears after clicking an applicant record in the search results
        /// </summary>
        private void OpenApplication()
        {
            var openApplicationLink = _driver.FindElement(By.LinkText("Open Application"));
            openApplicationLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the first vacancy record in the search results
        /// </summary>
        private void ClickFirstVacancyResult()
        {
            // Remove "Vacancies_" from this result to get the applicants Id number
            JobId = SearchResultsLinks[0].GetAttribute("id").Substring(10);
            SearchResultsLinks[0].WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Open Vavancy' link that appears after clicking a vacancy record in the search results
        /// </summary>
        private void OpenVacancy()
        {
            var openVacancyLink = _driver.FindElement(By.LinkText("Show Applicants"));
            openVacancyLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the first user record in the search results
        /// </summary>
        private void ClickFirstUserResult()
        {
            UserNo = SearchResultsLinks[0].GetAttribute("id").Substring(6);
            SearchResultsLinks[0].WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the first employee record in the search results
        /// </summary>
        private void ClickFirstEmployeeResult()
        {
            // Remove "Employees_" to get the employee number
            EmpNo = SearchResultsLinks[0].GetAttribute("id").Substring(10);
            SearchResultsLinks[0].WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Open Employee Record' link that appears after clicking an employee record in the search results
        /// </summary>
        private void OpenEmployeeRecord()
        {
            var openEmployeeRecordLink = _driver.FindElement(By.LinkText("Open Employee Record"));
            openEmployeeRecordLink.WaitAndClick(_driver);
        }

        #endregion

        // Each applicant page uses an iframe with an ID of "AppXXXX" where XXXX is the applicant number
        /// <summary>
        /// Verify that the applicant page is displayed
        /// </summary>
        public override bool ApplicantsAreDisplayed()
        {
            return ApplicantsTabLink.Displayed;
        }

        // This page should display "JobID: XXXX" where XXXX is the JobId
        /// <summary>
        /// Verify that the vacancy page is displayed
        /// </summary>
        public override bool JobPostingsAreDisplayed()
        {
            return VacanciesTabLink.Displayed;
        }

        /// <summary>
        /// Verify that the 'Email User' link appears after clicking a user record
        /// </summary>
        public bool EmailUserLinkAppears
        {
            get { return _driver.FindElement(By.LinkText("Email User")).Displayed; }
        }

        /// <summary>
        /// Verify that the 'Edit User' link appears after clicking a user record
        /// </summary>
        public bool EditUserLinkAppears
        {
            get { return _driver.FindElement(By.LinkText("Edit User")).Displayed; }
        }

        // Each Employee page uses an iframe with an ID of "EmpXXXX" where XXXX is the employee number
        /// <summary>
        /// Verify that the employee page is displayed after opening an employee record
        /// </summary>
        public override bool EmployeesAreDisplayed()
        {
            return EmployeesTabLink.Displayed;
        }

        public override void ClickApplicantsAdvancedSearch()
        {
            throw new NotImplementedException();
        }

        public override void ClickEmployeesAdvancedSearch()
        {
            throw new NotImplementedException();
        }
    }
}
