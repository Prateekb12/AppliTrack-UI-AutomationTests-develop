using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.EmployeeSide
{
    public class EmployeePortalLoginPage : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public EmployeePortalLoginPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // The "Email address" field used for logging in 
        [FindsBy(How = How.Id, Using = "PrimaryID")] 
        private IWebElement EmailAddressField { get; set; }

        // The "Password" field
        [FindsBy(How = How.Id, Using = "Password")]
        private IWebElement PasswordField { get; set; }

        // The "Log In" button
        [FindsBy(How = How.Id, Using = "LogIn")]
        private IWebElement LoginButton { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Enter the employees email address to log in
        /// </summary>
        /// <param name="email">The employee's email address</param>
        public void EnterEmailAddress(string email)
        {
            EmailAddressField.SendKeys(email);
        }

        /// <summary>
        /// Enter the employee's password
        /// </summary>
        /// <param name="password">The employee's password</param>
        public void EnterPassword(string password)
        {
            PasswordField.SendKeys(password);
        }

        /// <summary>
        /// Click the "Log In" button
        /// </summary>
        public void ClickLogin()
        {
            LoginButton.WaitAndClick(_driver);
        }

        #endregion

    }
}
