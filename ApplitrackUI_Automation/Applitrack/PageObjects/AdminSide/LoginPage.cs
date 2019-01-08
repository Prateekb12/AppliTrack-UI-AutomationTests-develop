using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide
{
    public class LoginPage : BasePageObject
    {
        private readonly IWebDriver _driver;

        #region Constructors

        public LoginPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }
 
        #endregion

        #region UI Selectors

        [FindsBy(How = How.Id, Using = "UserID")]
        [CacheLookup]
        private IWebElement UserNameField { get; set; }

        [FindsBy(How = How.Id, Using = "Password")]
        [CacheLookup]
        private IWebElement PasswordField { get; set; }

        [FindsBy(How = How.Id, Using = "Login")]
        [CacheLookup]
        private IWebElement LoginButton { get; set; }

        #endregion

        #region Page Actions

        /// <summary>
        /// Enter the password into the field
        /// </summary>
        /// <param name="pwd">The password to be entered.</param>
        public void EnterPwd(string pwd)
        {
            PasswordField.Clear();
            PasswordField.SendKeys(pwd);
        }

        /// <summary>
        /// Enter the username into the field
        /// </summary>
        /// <param name="username">The username to be entered.</param>
        public void EnterUsername(string username)
        {
            UserNameField.Clear();
            UserNameField.SendKeys(username);
        }

        /// <summary>
        /// Click the Login button
        /// </summary>
        public void ClickLogin()
        {
            LoginButton.WaitAndClick(_driver);
        }

        #endregion
    }
}