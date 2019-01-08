using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Interviews
{
    public class CreateInterviewStartTab : BasePageObject 
    {
        private readonly IWebDriver _driver;

        #region Constructor

        public CreateInterviewStartTab(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // The 'Create new interview series' radio button
        [FindsBy(How = How.Id, Using = "Wizard1_RadioButtonSeriesActionCreateNew")]
        private IWebElement CreateNewRadioButton { get; set; }

        // The 'General Recruiting - not associated with a specific posting or pool' radio button
        [FindsBy(How = How.Id, Using = "Wizard1_CreateNewOptionForGeneralRecruiting")]
        private IWebElement GeneralRecruitingRadioButton { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Select the 'Create new interview series' radio button
        /// </summary>
        public void SelectCreateNew()
        {
            CreateNewRadioButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Select the 'General Recruiting - not associated with a specific posting or pool' radio button
        /// </summary>
        public void SelectGeneralRecruiting()
        {
            GeneralRecruitingRadioButton.WaitAndClick(_driver);
        }

        #endregion
    }
}
