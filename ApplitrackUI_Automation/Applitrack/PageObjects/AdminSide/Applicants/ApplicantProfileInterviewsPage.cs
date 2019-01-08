using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Applicants
{
    public class ApplicantProfileInterviewsPage : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public ApplicantProfileInterviewsPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        [FindsBy(How = How.Id, Using = "FillOutEvaluationButton")]
        private IWebElement FillOutEvaluationButton { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Check to see if the 'Fill Out Interview Questionnaire' button is visible
        /// </summary>
        /// <returns>True if the button is displayed, false otherwise</returns>
        public bool IsFillOutInterviewQuestionnaireVisible()
        {
            try
            {
                return FillOutEvaluationButton.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        #endregion

    }
}
