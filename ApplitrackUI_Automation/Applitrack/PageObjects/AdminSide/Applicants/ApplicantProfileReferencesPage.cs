using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Applicants
{
    public class ApplicantProfileReferencesPage : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public ApplicantProfileReferencesPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // Left rotate button for PDFs
        [FindsBy(How = How.Id, Using = "BtnRotateLeft")]
        private IWebElement RotateLeftButton { get; set; }

        // Right rotate button for PDFs
        [FindsBy(How = How.Id, Using = "BtnRotateRight")]
        private IWebElement RotateRightButton { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Click the first link with the given text
        /// </summary>
        /// <param name="item">The text of the item</param>
        public void ClickItem(string item)
        {
            var itemLink = _driver.FindElement(By.PartialLinkText(item));
            itemLink.WaitAndClick(_driver);
        }

        public bool PdfRotateButtonsDisplayed()
        {
            var buttonsDisplayed = RotateLeftButton.Displayed && RotateRightButton.Displayed;
            return buttonsDisplayed;
        }

        /// <summary>
        /// Click the 'Right' rotate button for PDFs
        /// </summary>
        public void ClickRotateRight()
        {
            RotateRightButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Left' rotate button for PDFs
        /// </summary>
        public void ClickRotateLeft()
        {
            RotateLeftButton.WaitAndClick(_driver);
        }

        #endregion

    }
}