using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using ApplitrackUITests.PageObjects.PageTypes;


namespace ApplitrackUITests.PageObjects.Jefferson
{
    public class FederatedLoginPage : BasePageObject, IApplitrackPage
    {
        private IWebDriver _driver;

        #region Constructor

        public FederatedLoginPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement UserNameField { get; set; }

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement PasswordField { get; set; }

        [FindsBy(How = How.ClassName, Using = "btn-primary")]
        private IWebElement LoginButton { get; set; }

        [FindsBy(How = How.ClassName, Using = "navbar-brand")]
        private IWebElement NavBar { get; set; }


        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        public bool IsDisplayed()
        {
            return NavBar.Text.Contains("IDM Federation with OIDC");
        }

        public void Login(string username,string password)
        {
            UserNameField.SendKeys(username);
            PasswordField.SendKeys(password);
            LoginButton.Click();
        }

        #endregion
    }
}
