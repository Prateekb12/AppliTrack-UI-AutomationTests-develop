using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.Toolbar
{
    public class SidekickUserMenuPage : ToolbarUserMenuPage 
    {
        private IWebDriver _driver;

        #region Constructors

        public SidekickUserMenuPage(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        [FindsBy(How = How.PartialLinkText, Using = "Sign Out")]
        protected override IWebElement SignOut { get; set; }

        [FindsBy(How = How.CssSelector, Using = "a.sk--settings")]
        private IWebElement AccountSettingsLink { get; set; }

        #endregion

        #region Page Actions

        public override void ClickAccountSettings()
        {
            AccountSettingsLink.WaitAndClick(_driver);
        }

        #endregion
    }
}
