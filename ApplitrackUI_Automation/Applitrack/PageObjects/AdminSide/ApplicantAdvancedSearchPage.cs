using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide
{
    public class ApplicantAdvancedSearchPage : BasePageObject, IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructors

        public ApplicantAdvancedSearchPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        [FindsBy(How = How.CssSelector, Using = "div.PageTitleRight")]
        private IWebElement PageHeader { get; set; }

        #endregion

        #region Page Actions

        public bool IsDisplayed()
        {
            try
            {
                PageHeader.WaitRetry(_driver, sleepSeconds: 5);
                return PageHeader.Text.Contains("Select criteria to find applicants that match.");
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        #endregion
    }
}
