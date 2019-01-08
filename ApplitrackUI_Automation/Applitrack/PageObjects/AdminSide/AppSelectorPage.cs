using ApplitrackUITests.PageObjects.PageTypes;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide
{

    public class AppSelectorPage : IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructors

        /// <summary>
        /// Page that shows up after signing in as a user with access to multiple applications
        /// </summary>
        public AppSelectorPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        /// <summary>
        /// The list of applications the user has access to
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "ul.sk--org-app-list")]
        private IWebElement AppList { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Make sure the page is displayed
        /// </summary>
        /// <returns>True if the page is displayed, false otherwise</returns>
        public bool IsDisplayed()
        {
            return AppList.Displayed;
        }

        /// <summary>
        /// Select the specified application from the list
        /// </summary>
        /// <param name="appName">The name application to open</param>
        public void SelectApp(string appName)
        {
            var appLink = _driver.FindElement(By.PartialLinkText(appName));
            appLink.WaitAndClick(_driver);
        }

        #endregion


    }
}
