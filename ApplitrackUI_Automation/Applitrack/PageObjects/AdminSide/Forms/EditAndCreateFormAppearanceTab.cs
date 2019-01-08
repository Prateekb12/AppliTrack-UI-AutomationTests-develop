using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Forms
{
    public class EditAndCreateFormAppearanceTab : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public EditAndCreateFormAppearanceTab(IWebDriver driver)
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
        #endregion
    }
}
