using System;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Applicants
{
    public class ApplicantAdminDashboardPage : BasePageObject, IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructor

        public ApplicantAdminDashboardPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        [FindsBy(How = How.CssSelector, Using = ".brand")]
        private IWebElement BrandText { get; set; }

        // The 'Overall Statistics' link on the applicant dashboard
        [FindsBy(How = How.PartialLinkText, Using = "Overall Statistics")]
        private IWebElement OverallStatisticsLink { get; set; }

        // The 'Applicant Name' textbox in the 'Search' pane
        [FindsBy(How = How.CssSelector, Using = "input#SearchName")]
        private IWebElement ApplicantNameSearchTextBox { get; set; }

        // The 'Submitted Only' checkbox in the 'Search' pane
        [FindsBy(How = How.CssSelector, Using = "input#SubmittedOnly")]
        private IWebElement SubmittedOnlyCheckBox { get; set; }

        // The 'Search' button in the 'Search' pane
        [FindsBy(How = How.CssSelector, Using = "button#ButtonSubmitSearch")]
        private IWebElement SearchButton { get; set; }

        // The 'Applicant Logins in Last 24 Hours' count
        [FindsBy(How = How.Id, Using = "MainContentControl_Lbl24HrLogins")]
        private IWebElement LoginCount { get; set; }

        // The header text of the applicant dashboard, should contain "Applicant Dashboard Activity and Statistics"
        [FindsBy(How = How.CssSelector, Using = "#Search .caption h2")]
        private IWebElement ApplicantDashboardHeaderText { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions
        public bool IsDisplayed()
        {
            try
            {
                return BrandText.WaitRetry(_driver).Text.Contains("Applicants");
            }
            catch (NoSuchElementException)
            {
               return false;
            }
        }

        public string GetDashboardHeaderText()
        {
            return ApplicantDashboardHeaderText.Text;
        }

        public void EnterSearchApplicantName(string name)
        {
            ApplicantNameSearchTextBox.WaitRetry(_driver).SendKeys(name);
        }

        public void MarkSubmittedOnly()
        {
            SubmittedOnlyCheckBox.WaitRetry(_driver).Click();
        }

        public void ClickSearch()
        {
            SearchButton.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the Overall Statistics link on the Applicant Dashboard
        /// </summary>
        public void ClickOverallStatistics()
        {
            OverallStatisticsLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Get the number from the 'Applicant Logins in Last 24 Hours'
        /// </summary>
        /// <returns>The number of logins as an integer</returns>
        public int GetLoginCount()
        {
            return Int32.Parse(LoginCount.Text);
        }

        #endregion
    }
}
