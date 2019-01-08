using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using IDMPageObjects.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide
{
    public class DashboardPage : BasePageObject, IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructor
        public DashboardPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }
        #endregion

        #region UI Selectors
        [FindsBy(How = How.CssSelector, Using = "#WelcomeMessage")]
        private IWebElement WelcomeMessageDiv { get; set; }
        #endregion

        #region Page Actions

        public bool IsDisplayed()
        {
            try
            {
                WaitForPageToLoad();
                return WelcomeMessageDiv.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void WaitForPageToLoad()
        {
            _driver.WaitForIt(WelcomeMessageDiv);
        }

        #endregion
    }
}
