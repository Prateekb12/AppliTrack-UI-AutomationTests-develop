using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Menu
{
    public class SubMenuSetup : BasePageObject
    {
        private readonly IWebDriver _driver;

        #region Constructor

        public SubMenuSetup(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region Related Pages
        #endregion

        #region UI Selectors

        // The 'Applicant Settings' link from Setup >
        [FindsBy(How = How.LinkText, Using = "Applicant Settings")]
        private IWebElement ApplicantSettingsLink { get; set; }

        // The 'Edit Position List' link from Setup > Applicant Settings
        [FindsBy(How = How.LinkText, Using = "Edit Position List")]
        private IWebElement EditPositionListLink { get; set; }

        // The 'Manage Application Pages' link From Setup > Applicant Settings
        [FindsBy(How = How.LinkText, Using = "Manage Application Pages")]
        private IWebElement ManageApplicationPagesLink { get; set; }

        // The 'Manage Internal Pages' link from Setup > Applicant Settings > Manage Application Pages
        [FindsBy(How = How.LinkText, Using = "Manage Internal Pages")]
        private IWebElement ManageInternalPagesLink { get; set; }

        #endregion

        #region Page Actions

        /// <summary>
        /// Click the 'Applicant Settings' link from Setup
        /// </summary>
        public void ClickApplicantSettings()
        {
            ApplicantSettingsLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Edit Position List' link from Setup > Applicant Settings
        /// </summary>
        public void ClickEditPositionList()
        {
            EditPositionListLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Manage Application Pages' link from Setup > Applicant Settings
        /// </summary>
        public void ClickManageApplicationPages()
        {
            ManageApplicationPagesLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Manage Internal Pages' link from Setup > Applicant Settings > Manage Application Pages
        /// </summary>
        public void ClickManageInternalPages()
        {
            ManageInternalPagesLink.WaitAndClick(_driver);
        }

        #endregion
    }
}
