using ApplitrackUITests.PageObjects.PageTypes;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Setup
{
    public class ApplicantSettingsManageInternalPages : ScrollTableType
    {
        private IWebDriver _driver;

        #region Constructor

        public ApplicantSettingsManageInternalPages(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion


        #region Related Pages
        #endregion


        #region UI Selectors
        #endregion


        #region Page Actions

        /// <summary>
        /// Click the 'edit' button for the given page name
        /// </summary>
        /// <param name="pageName">The name of the page to edit</param>
        public void ClickEdit(string pageName)
        {
            var pageSpan = _driver.FindElement(By.CssSelector("span[title='" + pageName + "']"));
            var editLink = pageSpan.FindElement(By.PartialLinkText("edit"));
            editLink.WaitAndClick(_driver);
        }

        #endregion

    }
}
