using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.Toolbar
{
    public abstract class ToolbarHelpMenuPage : BasePageObject, IApplitrackPage
    {
        private IWebDriver _driver;

        #region Constructors

        public ToolbarHelpMenuPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        protected abstract IWebElement HelpMenu { get; set; }

        #endregion


        #region Page Actions

        /// <summary>
        /// See if the help menu is opened
        /// </summary>
        /// <returns>Returns true if the menu is opened, false otherwise</returns>
        public virtual bool IsDisplayed()
        {
            return HelpMenu.Displayed;
        }

        #endregion
    }
}
