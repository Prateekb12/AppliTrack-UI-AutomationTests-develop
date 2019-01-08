using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Setup
{
    public class ApplicantSettingsEditPage
    {
        private IWebDriver _driver;

        #region Constructor

        public ApplicantSettingsEditPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion


        #region Related Pages
        #endregion


        #region UI Selectors

        // The 'Section Title' textbox in the 'Higher Education' section
        [FindsBy(How = How.Id, Using = "TextBoxHigherEduSectionTitle")]
        private IWebElement HigherEducationTitleTextBox { get; set; }

        // The 'Save Changes' button
        [FindsBy(How = How.Id, Using = "ImageButtonSave")]
        private IWebElement SaveChangesButton { get; set; }

        // The 'Preview Page' button
        [FindsBy(How = How.Id, Using = "BtnPreview")]
        private IWebElement PreviewPageButton { get; set; }

        #endregion


        #region Page Actions

        /// <summary>
        /// Enter a new title into the 'Section Title' textbox in the 'Higher Education' section
        /// </summary>
        /// <param name="title">The title to enter</param>
        public void EnterHigherEducationTitle(string title)
        {
            HigherEducationTitleTextBox.Clear();
            HigherEducationTitleTextBox.SendKeys(title);
        }

        /// <summary>
        /// Click the 'Save Changes' button
        /// </summary>
        public void ClickSaveChanges()
        {
            SaveChangesButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Preview Page' button
        /// </summary>
        public void ClickPreviewPage()
        {
            PreviewPageButton.WaitAndClick(_driver);
        }

        #endregion


    }
}
