using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.Toolbar
{
    public class ApplitrackNewsFeedPage : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructors

        public ApplitrackNewsFeedPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // News feed that opens after clicking the News Button
        [FindsBy(How = How.Id, Using = "RssFeed")]
        private IWebElement RssFeed { get; set; }

        #endregion

        #region Page Actions

        /// <summary>
        /// See if the newsfeed is open
        /// </summary>
        /// <returns>Returns true if the newsfeed is open, false otherwise</returns>
        public bool IsDisplayed()
        {
            return RssFeed.Displayed;
        }

        #endregion
    }
}
