using System;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Applicants
{
    public class ApplicantNotesPages : BasePageObject, IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructor

        public ApplicantNotesPages(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region Related Pages

        private ApplicantNotesTab _applicantNotesTab;

        public ApplicantNotesTab ApplicantNotesTab => _applicantNotesTab ??
                                                      (_applicantNotesTab = new ApplicantNotesTab(_driver));
        #endregion

        #region UI Selectors
        #endregion

        #region Page Actions

        public bool IsDisplayed()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
