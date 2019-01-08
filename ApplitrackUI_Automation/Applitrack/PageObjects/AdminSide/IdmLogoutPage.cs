using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide
{
    public class IdmLogoutPage : BasePageObject, IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructor 

        public IdmLogoutPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        [FindsBy(How = How.CssSelector, Using = "div.header-message > h1")]
        private IWebElement HeaderMessage { get; set; }

        #endregion

        #region Page Actions

        public bool IsDisplayed()
        {
            try
            {
                waitForIt(_driver, HeaderMessage);
                return HeaderMessage.Displayed && HeaderMessage.Text.Contains("You are signed out.");
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        #endregion
    }
}
