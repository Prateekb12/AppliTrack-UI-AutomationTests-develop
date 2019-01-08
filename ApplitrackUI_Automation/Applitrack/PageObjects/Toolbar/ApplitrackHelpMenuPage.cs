using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.Toolbar
{
    public class ApplitrackHelpMenuPage : ToolbarHelpMenuPage
    {
        private IWebDriver _driver;

        #region Constructors

        public ApplitrackHelpMenuPage(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // TODO use a better selector
        [FindsBy(How = How.CssSelector, Using = ".fa-graduation-cap")]
        protected override IWebElement HelpMenu { get; set; }

        #endregion

        #region Page Actions
        #endregion
    }
}
