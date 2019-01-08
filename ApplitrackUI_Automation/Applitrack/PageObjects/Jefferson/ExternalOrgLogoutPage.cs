using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using ApplitrackUITests.PageObjects.PageTypes;

namespace ApplitrackUITests.PageObjects.Jefferson
{
   public class ExternalOrgLogoutPage : BasePageObject, IApplitrackPage
   {
        private IWebDriver _driver;

        #region Constructor

        public ExternalOrgLogoutPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        public bool IsDisplayed ()
        {
            return false;
        }

        #endregion
    }
}


