using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Setup
{
    public class ApplicantSettingsPages
    {
        private IWebDriver _driver;

        #region Constructor

        public ApplicantSettingsPages(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion


        #region Related Pages

        private EditPositionListPages _editPositionListPages;
        /// <summary>
        /// The 'Edit Position List' page accessed from Setup > Applicant Settings > Edit Position List
        /// </summary>
        public EditPositionListPages EditPositionListPages
        {
            get { return _editPositionListPages ?? (_editPositionListPages = new EditPositionListPages(_driver)); }
        }

        private ApplicantSettingsManageInternalPages _manageInternalPages;
        /// <summary>
        /// The 'Manage Internal Application Pages' page accessed from Setup > Applicant Settings > Manage Application Pages  > Manage Internal Pages
        /// </summary>
        public ApplicantSettingsManageInternalPages ManageInternalPages
        {
            get
            {
                return _manageInternalPages ?? (_manageInternalPages = new ApplicantSettingsManageInternalPages(_driver));
            }
        }

        private ApplicantSettingsEditPage _editPage;
        /// <summary>
        /// The 'Edit Page' page accessed from anywhere in the system where it is possible to edit application pages
        /// </summary>
        public ApplicantSettingsEditPage EditPage
        {
            get { return _editPage ?? (_editPage = new ApplicantSettingsEditPage(_driver)); }
        }

        #endregion


        #region UI Selectors
        #endregion


        #region Page Actions
        #endregion
    }
}
