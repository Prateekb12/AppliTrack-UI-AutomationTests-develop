using System;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Applicants
{
    public class ApplicantProfilePages : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public ApplicantProfilePages(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region Related Pages

        private ApplicantProfileToolbar _toolbar;
        /// <summary>
        /// The toolbar that appears on the top right hand side of the applicant profile page
        /// </summary>
        public ApplicantProfileToolbar Toolbar
        {
            get { return _toolbar ?? (_toolbar = new ApplicantProfileToolbar(_driver)); }
        }

        private ApplicantProfileQuickLookPage _quickLook;
        /// <summary>
        /// The 'Quick Look' page for the applicant profile.
        /// </summary>
        public ApplicantProfileQuickLookPage QuickLook
        {
            get { return _quickLook ?? (_quickLook = new ApplicantProfileQuickLookPage(_driver)); }
        }

        private ApplicantProfileInterviewsPage _interviews;
        /// <summary>
        /// The 'Interviews' page for the applicant profile
        /// </summary>
        public ApplicantProfileInterviewsPage Interviews
        {
            get { return _interviews ?? (_interviews = new ApplicantProfileInterviewsPage(_driver)); }
        }

        private ApplicantProfileOnlineApplicationPage _onlineApplication;
        /// <summary>
        /// The 'Online Application' page for the applicant profile
        /// </summary>
        public ApplicantProfileOnlineApplicationPage OnlineApplication
        {
            get { return _onlineApplication ?? (_onlineApplication = new ApplicantProfileOnlineApplicationPage(_driver)); }
        }

        private ApplicantProfileListAllFormsPage _listAllForms;
        /// <summary>
        /// The 'List All Forms' page for the applicant profile
        /// </summary>
        public ApplicantProfileListAllFormsPage ListAllForms
        {
            get { return _listAllForms ?? (_listAllForms = new ApplicantProfileListAllFormsPage(_driver)); }
        }

        private ApplicantProfileReferencesPage _referencesPage;
        /// <summary>
        /// The 'References' page for the applicant profile
        /// </summary>
        public ApplicantProfileReferencesPage ReferencesPage
        {
            get { return _referencesPage ?? (_referencesPage = new ApplicantProfileReferencesPage(_driver)); }
        }

        private ApplicantNotesPages _applicantNotesPages;

        public ApplicantNotesPages ApplicantNotesPages => _applicantNotesPages ??
                                                          (_applicantNotesPages = new ApplicantNotesPages(_driver));

        private ApplicantProfileCommunicationLogPage _communicationLogPage;
        public  ApplicantProfileCommunicationLogPage CommunicationLogPage => _communicationLogPage ??
            (_communicationLogPage = new ApplicantProfileCommunicationLogPage(_driver));

        #endregion

    }
}

