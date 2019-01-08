using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.EmployeeSide
{
    public class EmployeePortalPages : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public EmployeePortalPages(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // The 'Log Off' link
        [FindsBy(How = How.PartialLinkText, Using = "Log Off")]
        private IWebElement LogOffLink { get; set; }

        // The "Forms" tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Forms']")]
        private IWebElement FormsTab { get; set; }

        #endregion

        #region Related Pages

        private EmployeePortalLoginPage _loginPage;
        /// <summary>
        /// The Employee Portal login page
        /// </summary>
        public EmployeePortalLoginPage LoginPage
        {
            get { return _loginPage ?? (_loginPage = new EmployeePortalLoginPage(_driver)); }
        }

        private EmployeePortalFormsTab _formsTabPage;

        public EmployeePortalFormsTab FormsTabPage
        {
            get { return _formsTabPage ?? (_formsTabPage = new EmployeePortalFormsTab(_driver)); }
        }

        #endregion

        #region Page Actions

        /// <summary>
        /// Click the "Forms" tab in the employee portal
        /// </summary>
        public void ClickFormsTab()
        {
            FormsTab.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Log Off' link
        /// </summary>
        public void ClickLogOff()
        {
            LogOffLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Check to see if the user is logged in
        /// </summary>
        /// <returns>True if the user was logged in, false otherwise</returns>
        public bool IsLoggedIn()
        {
            try
            {
                return _driver.FindElement(By.Id("WelcomeLabel")).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Check to see if the user is logged off
        /// </summary>
        /// <returns>True if the user was logged off, false otherwise</returns>
        public bool IsLoggedOff()
        {
            return IsTextOnScreen(_driver, "logged out");
        }

        #endregion

    }
}
