using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.JobPostings
{
    public class ListExistingTemplatePage : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public ListExistingTemplatePage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        [FindsBy(How = How.Name, Using = "DeleteRecords")]
        private IWebElement DeleteRecordsButton { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        public void ClickDeleteTemplate()
        {
            DeleteRecordsButton.WaitAndClick(_driver);
        }

        #endregion
    }
}
