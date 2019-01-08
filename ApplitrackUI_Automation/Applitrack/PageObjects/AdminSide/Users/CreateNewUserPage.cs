using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using ApplitrackUITests.Helpers;

namespace ApplitrackUITests.PageObjects.AdminSide.Users
{
    public class CreateNewUserPage : BasePageObject, IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructor

        public CreateNewUserPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI selectors

        // The page header
        [FindsBy(How = How.CssSelector, Using = "span#Title")]
        private IWebElement HeaderText { get; set; }

        // 'User ID' field in the 'Login Info' section
        [FindsBy(How = How.CssSelector, Using = "#TextBoxUserID")]
        private IWebElement ShortnameField { get; set; }

        // 'Password' field in the 'Login Info' section
        [FindsBy(How = How.Id, Using = "TextBoxPassword")]
        private IWebElement PasswordField { get; set; }
 
        // 'Real Name' text box in the 'Employee Information' section
        [FindsBy(How = How.Id, Using = "TextBoxRealName")]
        private IWebElement RealNameField { get; set; }

        // 'Email' field in the 'Employee Information' section
        [FindsBy(How = How.Id, Using = "TextBoxEmail")]
        private IWebElement EmailField { get; set; }

        // 'Save New User' button on the 'Edit User' page
        [FindsBy(How = How.Id, Using = "Save")]
        private IWebElement SaveButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name='SaveAndClose']")]
        private IWebElement SaveAndClose { get; set; }

        #endregion

        #region Page Actions

        public bool IsDisplayed()
        {
            return HeaderText.WaitRetry(_driver).Text.Contains("Edit User");
        }

        /// <summary>
        /// Check to see of the Shortname field contains a given value
        /// </summary>
        /// <param name="userName">The value to check for</param>
        /// <returns>True if the field contains the value, false otherwise</returns>
        public bool ShortNameFieldContains(string userName)
        {
            return ShortnameField.GetAttribute("value").Contains(userName);
        }

        /// <summary>
        /// Enter a value in the 'User ID' field on the 'Edit User' page
        /// </summary>
        /// <param name="shortName">The User ID to be entered</param>
        public void EnterShortName(string shortName)
        {
            ShortnameField.WaitAndClick(_driver);
            ShortnameField.SendKeys(shortName);
        }

        /// <summary>
        /// Enter a value in the 'Password' field on the 'Edit User' page
        /// </summary>
        /// <param name="password">The password to be entered</param>
        public void EnterPassword(string password)
        {
            PasswordField.WaitAndClick(_driver);
            PasswordField.Clear();
            PasswordField.SendKeys(password);
        }

        /// <summary>
        /// Enter a value in the 'Real Name' field on the 'Edit User' page
        /// </summary>
        /// <param name="realName">The real name to be entered</param>
        public void EnterRealName(string realName)
        {
            RealNameField.WaitAndClick(_driver);
            RealNameField.SendKeys(realName);
            // press <TAB> to prevent the field filling in with a "0"
            RealNameField.SendKeys(Keys.Tab);
            ShortnameField.WaitAndClick(_driver);
        }

        /// <summary>
        /// Enter a value in the 'Email' field on the 'Edit User' page
        /// </summary>
        /// <param name="email">The email to be entered</param>
        public void EnterEmail(string email)
        { 
            EmailField.WaitAndClick(_driver);
            EmailField.SendKeys(email);
        }

        /// <summary>
        /// Click the 'Save New User' button on the 'Edit User' page
        /// </summary>
        public void ClickSaveButton()
        {
            SaveButton.WaitAndClick(_driver);
        }


        /// <summary>
        /// Click the 'Save and Close' button on the 'Edit User' page
        /// </summary>
        public void ClickSaveAndCloseButton()
        {
            SaveAndClose.WaitRetry(_driver).Click();
        }

        #endregion


    }
}