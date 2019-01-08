using System;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using By = OpenQA.Selenium.Extensions.By;


namespace ApplitrackUITests.PageObjects.AdminSide.Users
{
    public class ManageUserAccessPage : BasePageObject, IApplitrackPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        #region Constructor

        public ManageUserAccessPage(IWebDriver driver)
        {
            this._driver = driver;
            this._wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(BaseFrameWork._DefWaitTimeOutSec));
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        [FindsBy(How = How.CssSelector, Using = "#fl-app-universal-useraccess h1")]
        private IWebElement ManageUserAccessLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "a[ng-click=\"go.toPreviousAppAccess()\"]")]
        private IWebElement ClassicApplicationAccessLink { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[ng-click=\"go.toPreviousAppAccess()\"]")]
        private IWebElement ClassicApplicationAccessButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "li.sk-nav-item > a.sk--main-menu-item-container")]
        private IWebElement HomeLink { get; set; }
        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Verify that the Shared Application Access page is displayed
        /// </summary>
        /// <returns>True if the page is displayed, false otherwise</returns>
        public bool IsDisplayed()
        {
            try
            {
                WaitForPageToLoad();
                return ManageUserAccessLabel.Displayed &&
                    ManageUserAccessLabel.Text.IndexOf("Manage User Access", StringComparison.OrdinalIgnoreCase) >=0;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Click the 'Classic Application Access' link in the top banner
        /// </summary>
        public void ClickViewClassicLink()
        {
            WaitForPageToLoad();
            ClassicApplicationAccessLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'View Classic' button in the top banner
        /// </summary>
        public void ClickViewClassicButton()
        {
            WaitForPageToLoad();
            ClassicApplicationAccessButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Wait for the Angular loading screen to finish
        /// </summary>
        internal void WaitForPageToLoad()
        {
            _wait.Until(d => d.FindElement(
                By.CssSelector("div.super-loader"))
                .GetAttribute("class")
                .Contains("ng-hide"));
            _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("div.cover")));
        }

        #endregion
    }
}
