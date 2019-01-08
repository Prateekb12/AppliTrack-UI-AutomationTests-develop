using ApplitrackUITests.Helpers;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Applicants
{
    public class ApplicantProfileToolbar : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public ApplicantProfileToolbar(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // "Tools" button in the header
        [FindsBy(How = How.CssSelector, Using = "button.btn")]
        private IWebElement ToolsButton { get; set; }

        // "Tools" option: Log in as Applicant
        [FindsBy(How = How.LinkText, Using = "Log in as Applicant")]
        private IWebElement LogInAsApplicantOption { get; set; }

        // "New Form" button in the toolbar
        [FindsBy(How = How.CssSelector, Using = "#ActionButtons > a:nth-child(3)")]
        private IWebElement NewFormButton { get; set; }

        // "Email" button in the toolbar
        [FindsBy(How = How.CssSelector, Using = "[title*='Email'")]
        private IWebElement EmailButton { get; set; }

        // "Notes" button in the toolbar
        [FindsBy(How = How.CssSelector, Using = "[title*='Notes'")]
        private IWebElement NotesButton { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Click the 'Tools' button and select 'Log in as Applicant'
        /// </summary>
        public void LoginAsApplicant()
        {
            ToolsButton.WaitRetry(_driver).Click();
            LogInAsApplicantOption.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the 'New Form' button in the toolbar
        /// </summary>
        public void ClickNewFormButton()
        {
            NewFormButton.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the 'Email' button in the toolbar
        /// </summary>
        public void ClickEmailButton()
        {
            EmailButton.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the 'Notes' button in the toolbar
        /// </summary>
        public void ClickNotes()
        {
            NotesButton.WaitRetry(_driver).Click();
        }

        #endregion


    }
}
