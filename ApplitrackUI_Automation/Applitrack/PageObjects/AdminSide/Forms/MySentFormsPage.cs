using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Forms
{
    public class MySentFormsPage : BasePageObject, IApplitrackPage

    {
        private IWebDriver _driver;

        #region Constructor

        public MySentFormsPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion
        #region UI Selectors
        // the page header
        [FindsBy(How = How.CssSelector, Using = ".ReportHeadTitle")]
        private IWebElement PageHeader { get; set; }
        #endregion

        #region Page Actions
        public bool IsDisplayed()
        {
            try
            {
                return PageHeader.WaitRetry(_driver).Text.Contains("Sent Forms");
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Make sure that the given form name exists
        /// </summary>
        /// <param name="formName"></param>
        /// <returns>True if form exists, false otherwise</returns>
        public bool FormExists(string formName)
        {
            if (IsDisplayed())
            {
                return IsTextOnScreen(_driver, formName);
            }
            return false;
        }
        #endregion
    }
}
