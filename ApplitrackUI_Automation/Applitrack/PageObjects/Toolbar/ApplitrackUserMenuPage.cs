using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.Toolbar
{
    public class ApplitrackUserMenuPage : ToolbarUserMenuPage
    {
        private IWebDriver _driver;

        #region Constructors

        public ApplitrackUserMenuPage(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // The sign out link within the user name menu
        [FindsBy(How = How.Id, Using = "SignOut")]
        protected override IWebElement SignOut { get; set; }

        #endregion

        #region Page Actions
        #endregion
    }
}
