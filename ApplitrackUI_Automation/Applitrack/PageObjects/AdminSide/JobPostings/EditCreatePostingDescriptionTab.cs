using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.JobPostings
{
    public class EditCreatePostingDescriptionTab : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public EditCreatePostingDescriptionTab(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // The description text box
        [FindsBy(How = How.ClassName, Using = "cke_editable")]
        private IWebElement DescriptionTextBox { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Enter text into the description text box
        /// </summary>
        /// <param name="description">The text to enter</param>
        public void EnterDescription(string description)
        {
            DescriptionTextBox.Clear();
            DescriptionTextBox.SendKeys(description);
        }

        #endregion

    }
}
