using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.JobPostings
{
    public class CreateNewRequisitionPage : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public CreateNewRequisitionPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // The "A blank form" link
        [FindsBy(How = How.LinkText, Using = "A blank form")] 
        private IWebElement FromBlankForm { get; set; }

        // The "A template" link
        [FindsBy(How = How.LinkText, Using = "A template")] 
        private IWebElement FromTemplate { get; set; }

        // The "My previous requisitions" link
        [FindsBy(How = How.LinkText, Using = "My previous requisitions")] 
        private IWebElement FromMyPreviousRequisitions { get; set; }

        // The "Any previous requisitions" link
        [FindsBy(How = How.LinkText, Using = "Any previous requisition")] 
        private IWebElement FromAnyPreviousRequisition { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Click the "A blank form" link in the "Start From" page
        /// </summary>
        public void ClickFromBlankForm()
        {
            FromBlankForm.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the "A template" link in the "Start From" page
        /// </summary>
        public void ClickFromTemplate()
        {
            FromTemplate.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the "My previous requisitions" link in the "Start From" page
        /// </summary>
        public void ClickFromMyPreviousRequisitions()
        {
            FromMyPreviousRequisitions.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the "Any previous requisitions" link in the "Start From" page
        /// </summary>
        public void ClickFromAnyPreviousRequisition()
        {
            FromAnyPreviousRequisition.WaitAndClick(_driver);
        }

        #endregion

    }
}
