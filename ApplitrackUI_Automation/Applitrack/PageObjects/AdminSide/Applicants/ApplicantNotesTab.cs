using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Applicants
{
    public class ApplicantNotesTab : BasePageObject, IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructor

        public ApplicantNotesTab(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region Related Pages
        #endregion

        #region UI Selectors

        [FindsBy(How = How.CssSelector, Using = "input[name*='Hired'][value='Yes']")]
        private IWebElement HiredYesRadioButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name*='Hired'][value='No']")]
        private IWebElement HiredNoRadioButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input#SaveOnly")]
        private IWebElement SaveButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "a[title='Close (esc)']")]
        private IWebElement CloseButton { get; set; }

        #endregion

        #region Page Actions

        public bool IsDisplayed()
        {
            return _driver.FindElement(By.Id("BodyContent")).Displayed;
        }

        /// <summary>
        /// Select the 'Yes' radio button in the 'Hired' pane
        /// </summary>
        public void SelectHiredYes()
        {
            HiredYesRadioButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Select the 'No' radio button in the 'Hired' pane
        /// </summary>
        public void SelectHiredNo()
        {
            HiredNoRadioButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Save' button at the bottom of the window
        /// </summary>
        public void ClickSave()
        {
            SaveButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Close the 'Tools' modal window
        /// </summary>
        public void Close()
        {
            CloseButton.WaitAndClick(_driver);
        }

        #endregion

    }
}
