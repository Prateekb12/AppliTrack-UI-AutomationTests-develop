using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Forms
{
    public class CreateNewFormPage : BasePageObject
    {

        private IWebDriver _driver;

        #region Constructor

        public CreateNewFormPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // "A blank form" link
        [FindsBy(How = How.Id, Using = "LinkButtonCreateFromBlank")]
        private IWebElement BlankFormLink { get; set; }

        // "An existing form" link
        [FindsBy(How = How.Id, Using = "LinkButtonCreateFromExisting")]
        private IWebElement ExistingFormLink { get; set; }

        // "Form Construction Service" link
        [FindsBy(How = How.LinkText, Using = "Form Construction Service")]
        private IWebElement FormConstructionServiceLink { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Click the 'A blank form' link on the 'Create New Form' page
        /// </summary>
        public void ClickBlankForm()
        {
            BlankFormLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'An existing form' link on the 'Create New Form' page
        /// </summary>
        public void ClickExistingForm()
        {
            ExistingFormLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Form Construction Service' link on the 'Create New Form' page
        /// </summary>
        public void ClickFormConstructionService()
        {
            FormConstructionServiceLink.WaitAndClick(_driver);
        }

        #endregion

    }
}
