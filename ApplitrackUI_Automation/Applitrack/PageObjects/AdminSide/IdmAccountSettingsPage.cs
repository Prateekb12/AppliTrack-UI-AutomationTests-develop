using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using IDMPageObjects.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide
{
    public class IdmAccountSettingsPage : BasePageObject, IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructors

        public IdmAccountSettingsPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        [FindsBy(How = How.CssSelector, Using = "h1#qa-pagetitle")]
        private IWebElement PageHeader { get; set; }

        [FindsBy(How = How.CssSelector, Using = "a.link-to-rp")]
        private IWebElement BackToRecruitLink { get; set; }
        #endregion

        #region Page Actions

        public bool IsDisplayed()
        {
            try
            {
                _driver.WaitForIt(PageHeader);
                return PageHeader.Displayed && PageHeader.Text.Contains("Account Settings");
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Click the 'Back to Recruiting & Hiring' link in the top left of the page.
        /// </summary>
        public void ClickBackToRecruit()
        {
            BackToRecruitLink.Click();
        }

        #endregion
    }
}
