using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Setup
{
    public class SetupPages : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public SetupPages(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion


        #region Related Pages

        private ApplicantSettingsPages _applicantSettingsPages;
        /// <summary>
        /// The 'Applicant Settings' pages accessed from Setup > Applicant Settings
        /// </summary>
        public ApplicantSettingsPages ApplicantSettingsPages
        {
            get { return _applicantSettingsPages ?? (_applicantSettingsPages = new ApplicantSettingsPages(_driver)); }
        }

        #endregion


        #region UI Selectors

        #endregion


        #region Page Actions

        #endregion
    }
}
