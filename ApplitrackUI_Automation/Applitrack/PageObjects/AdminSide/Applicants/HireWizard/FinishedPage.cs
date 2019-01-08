using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using IDMPageObjects.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Applicants.HireWizard
{
    public class FinishedPage : BasePageObject, IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructor

        public FinishedPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        /// <summary>
        /// The message indicating whether or not an FC record was created
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_EmployeeResultMessage")]
        private IWebElement EmployeeResultMessage { get; set; }

        /// <summary>
        /// The message indicating whether or not the applicant was marked as hired
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_MarkAsHiredMessage")]
        private IWebElement HiredMessage { get; set; }

        #endregion

        #region Related Pages 
        #endregion

        #region Page Actions

        public bool IsDisplayed()
        {
            try
            {
                _driver.WaitForIt(HiredMessage);
                return HiredMessage.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Make sure that the applicant is marked as hired
        /// </summary>
        /// <returns>True if the page indicates that the applicant was marked as hired, false otherwise</returns>
        public bool IsApplicantMarkedAsHired()
        {
            _driver.WaitForIt(HiredMessage);
            return HiredMessage.Text.Contains("Applicant(s) marked as \"Hired\"");
        }

        /// <summary>
        /// Make sure that the FC record is created
        /// </summary>
        /// <returns>True if the page indicates that the FC record was created, false otherwise</returns>
        public bool IsFrontlineCentralRecordCreated()
        {
            _driver.WaitForIt(EmployeeResultMessage);
            return EmployeeResultMessage.Text.Contains(
                "Applicant tracking has successfully created employee records for these applicants.");
        }

        #endregion
    }
}
