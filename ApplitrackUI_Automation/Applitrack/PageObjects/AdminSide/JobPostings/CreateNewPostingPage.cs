using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.JobPostings
{
    public class CreateNewPostingPage : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public CreateNewPostingPage(IWebDriver driver)
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

        // The "An existing open posting" link
        [FindsBy(How = How.LinkText, Using = "An existing open posting")] 
        private IWebElement FromExistingOpenPosting { get; set; }

        // The "An existing posting (open or closed)" link
        [FindsBy(How = How.LinkText, Using = "An existing posting (open or closed)")] 
        private IWebElement FromExistingPostingOpenOrClosed { get; set; }

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
        /// Click the "An existing open posting" link in the "Start From" page
        /// </summary>
        public void ClickFromExistingOpenPosting()
        {
            FromExistingOpenPosting.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the "An existing posting" link in the "Start From" page
        /// </summary>
        public void ClickFromExistingPostingOpenOrClosed()
        {
            FromExistingPostingOpenOrClosed.WaitAndClick(_driver);
        }

        #endregion


    }
}
