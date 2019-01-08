using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using Automation.Framework.Extensions;
using IDMPageObjects.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Applicants.HireWizard
{
    public class HireWizardPage : BasePageObject, IApplitrackPage
    {

        private readonly IWebDriver _driver;

        #region Constructor

        public HireWizardPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region Related Pages

        private FCEmployeeInfoPage _fcEmployeeInfoPage;
        public FCEmployeeInfoPage FCEmployeeInfoPage => _fcEmployeeInfoPage ??
                                                        (_fcEmployeeInfoPage = new FCEmployeeInfoPage(_driver));

        private ConfirmationPage _confirmationPage;
        public ConfirmationPage ConfirmationPage => _confirmationPage ??
                                                    (_confirmationPage = new ConfirmationPage(_driver));

        private FinishedPage _finishedPage;
        public FinishedPage FinishedPage => _finishedPage ?? (_finishedPage = new FinishedPage(_driver));

        #endregion

        #region UI Selectors

        // Table for the initial page of the hired wizard
        [FindsBy(How = How.CssSelector, Using = "#HiredWizard")]
        private IWebElement HiredTable { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[value='Next']")]
        private IWebElement NextButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[value='No']")]
        private IWebElement NoRadioButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[value='Yes']")]
        private IWebElement YesRadioButton { get; set; }

        #endregion

        #region Page Actions

        public bool IsDisplayed()
        {
            _driver.WaitForIt(HiredTable);
            return HiredTable.Displayed;
        }

        /// <summary>
        /// Click the 'Next' button
        /// </summary>
        public void ClickNext()
        {
            NextButton.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Select the 'No' radio button
        /// </summary>
        public void SelectNo()
        {
            NoRadioButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Select the 'Yes' radio button
        /// </summary>
        public void SelectYes()
        {
            YesRadioButton.WaitAndClick(_driver);
        }

        #endregion

    }
}

