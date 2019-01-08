using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Applicants
{
    public class ApplicantProfileOnlineApplicationPage : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public ApplicantProfileOnlineApplicationPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // The header for the Online Application PDF
        [FindsBy(How = How.CssSelector, Using = "#pageContainer1 .textLayer div")]
        private IWebElement OnlineApplicationHeader { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Get the header of the online application PDF
        /// </summary>
        /// <returns>The header of the online application PDF</returns>
        public string GetHeaderText()
        {
            return OnlineApplicationHeader.Text;
        }

        #endregion

    }
}
