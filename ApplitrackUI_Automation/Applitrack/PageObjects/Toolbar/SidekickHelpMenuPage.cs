using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.Toolbar
{
    public class SidekickHelpMenuPage : ToolbarHelpMenuPage
    {
        private IWebDriver _driver;

        #region Constructors

        public SidekickHelpMenuPage(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        [FindsBy(How = How.CssSelector, Using = "a.fa-learning_center_logo")]
        protected override IWebElement HelpMenu { get; set; }

        #endregion


        #region Page Actions
        #endregion
    }
}
