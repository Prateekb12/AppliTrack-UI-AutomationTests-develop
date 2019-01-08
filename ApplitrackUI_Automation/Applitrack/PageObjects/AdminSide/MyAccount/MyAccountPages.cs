using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.MyAccount
{
    public class MyAccountPages : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public MyAccountPages(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        #endregion

        #region Related Pages

        private UserPreferencesPage _userPreferencesPage;
        public UserPreferencesPage UserPreferencesPage
        {
            get { return _userPreferencesPage ?? (_userPreferencesPage = new UserPreferencesPage(_driver)); }
        }

        #endregion

        #region Page Actions

        #endregion
    }
}