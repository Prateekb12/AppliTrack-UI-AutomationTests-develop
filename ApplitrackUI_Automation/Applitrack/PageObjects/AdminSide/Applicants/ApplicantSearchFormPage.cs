using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Applicants
{
    public class ApplicantSearchFormPage : BasePageObject, IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructors

        public ApplicantSearchFormPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        [FindsBy(How = How.CssSelector, Using = "div.PageTitleText")]
        private IWebElement PageTitle { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div.PageTitleRight")]
        private IWebElement PageSubTitle { get; set; }

        #endregion

        #region Page Actions

        public bool IsDisplayed()
        {
            return PageTitle.WaitRetry(_driver).Text.Equals("Find by Search Form") &&
                PageSubTitle.WaitRetry(_driver).Text.Contains("Select criteria to find applicants that match.");
        }

        #endregion

    }
}
