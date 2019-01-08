using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Menu
{
    public class SubMenuMyAccount : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public SubMenuMyAccount(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        [FindsBy(How = How.PartialLinkText, Using = "User Preferences")]
        private IWebElement UserPreferencesLink { get; set; }
        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Click the 'User Preferences' link in the 'My Account' submenu
        /// </summary>
        public void ClickUserPreferences()
        {
            UserPreferencesLink.WaitAndClick(_driver);
        }
        #endregion
    }
}