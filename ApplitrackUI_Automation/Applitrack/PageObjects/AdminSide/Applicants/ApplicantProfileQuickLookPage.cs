using System.Net;
using ApplitrackUITests.Helpers;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Applicants
{
    public class ApplicantProfileQuickLookPage : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public ApplicantProfileQuickLookPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Check to see if the resume is displayed on the Quick Look page
        /// </summary>
        /// <returns>True if an image is displayed, false otherwise</returns>
        public bool IsResumeDisplayed()
        {
            try
            {
                var imageUri = _driver.FindElement(By.CssSelector("#DocumentDiv img")).GetAttribute("src");
                return LinkHelpers.GetLinkStatusCode(imageUri) == HttpStatusCode.OK;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        #endregion


    }
}
