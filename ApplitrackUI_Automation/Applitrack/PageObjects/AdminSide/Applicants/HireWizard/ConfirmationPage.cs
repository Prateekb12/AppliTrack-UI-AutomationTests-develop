using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using Automation.Framework.Extensions;
using IDMPageObjects.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Applicants.HireWizard
{
    public class ConfirmationPage : BasePageObject, IApplitrackPage
    {

        private readonly IWebDriver _driver;

        #region Constructor

        public ConfirmationPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region Related Pages 
        #endregion

        #region UI Selectors

        /// <summary>
        /// The message indicating whether or not the applicant will be marked as hired
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_divConfirmMarkAsHired")]
        private IWebElement ApplicantTrackingConfirmation { get; set; }

        /// <summary>
        /// The message indicating whether or not the applicant will be transferred to FC
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_divConfirmFC")]
        private IWebElement FrontlineCentralConfirmation { get; set; }

        /// <summary>
        /// The finish button at the bottom of the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "input[value='Finish']")]
        private IWebElement FinishButton{ get; set; }

        #endregion

        #region Page Actions

        public bool IsDisplayed()
        {
            try
            {
                return ApplicantTrackingConfirmation.WaitRetry(_driver).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Verify that the confirmation page indicates that an FC record will be created
        /// </summary>
        /// <returns>True if the page indicates that the record will be created, false otherwise</returns>
        public bool WillFrontlineCentralRecordBeCreated()
        {
            _driver.WaitForIt(FrontlineCentralConfirmation);
            return FrontlineCentralConfirmation.Displayed &&
                FrontlineCentralConfirmation.Text.Contains("A Frontline Central record will be created for this applicant.");
        }

        /// <summary>
        /// Click the 'Finish' button
        /// </summary>
        public void ClickFinish()
        {
            FinishButton.WaitAndClick(_driver);
        }

        #endregion
    }
}
