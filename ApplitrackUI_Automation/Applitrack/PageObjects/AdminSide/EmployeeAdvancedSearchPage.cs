using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using IDMPageObjects.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide
{
    public class EmployeeAdvancedSearchPage : BasePageObject, IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructors

        public EmployeeAdvancedSearchPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        [FindsBy(How = How.CssSelector, Using = "legend.SearchHeader")]
        private IWebElement PageHeader { get; set; }

        #endregion

        #region Page Actions

        public bool IsDisplayed()
        {
            try
            {
                PageHeader.WaitRetry(_driver);
                return PageHeader.Displayed && PageHeader.Text.Contains("Employee Status");
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        #endregion
    }
}
