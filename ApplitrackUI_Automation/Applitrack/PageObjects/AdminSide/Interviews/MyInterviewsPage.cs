using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Interviews
{
    public class MyInterviewsPage : BasePageObject
    {
        private readonly IWebDriver _driver;

        #region Constructor

        public MyInterviewsPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // The 'Edit Series' link that appears after clicking a record in the interviews list
        [FindsBy(How = How.LinkText, Using = "Edit Series")]
        private IWebElement EditSeriesLink { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        public void SelectInterview(string seriesName)
        {
            var selectLink = _driver.FindElement(By.PartialLinkText(seriesName));
            selectLink.WaitAndClick(_driver);
            EditSeriesLink.WaitAndClick(_driver);
        }

        #endregion
    }
}
