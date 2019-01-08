using ApplitrackUITests.Helpers;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.JobPostings
{
    public class MyUnsubmittedRequisitionPage : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public MyUnsubmittedRequisitionPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        public void SelectJobRequisition(int jobId)
        {
            var scrollTable = new ScrollTable(_driver);
            scrollTable.ClickItemId(jobId);
        }

        #endregion


    }
}
