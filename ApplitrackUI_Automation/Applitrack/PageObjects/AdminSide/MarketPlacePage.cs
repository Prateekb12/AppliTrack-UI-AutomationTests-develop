using Automation;
using ApplitrackUITests.PageObjects.PageTypes;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide
{
    public class MarketPlacePage : BasePageObject, IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructors

        public MarketPlacePage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        [FindsBy(How = How.CssSelector, Using = "span.brand2")]
        private IWebElement HeaderText { get; set; }
        #endregion

        #region Related Pages
        public bool IsDisplayed()
        {
            try
            {
                return HeaderText.Text.Contains("Frontline Education Marketplace");
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        #endregion
    }
}
