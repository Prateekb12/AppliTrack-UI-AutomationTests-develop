using System.Collections.Generic;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.ApplicantSide
{
    public class JobPostingPage : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public JobPostingPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // The job title
        [FindsBy(How = How.ClassName, Using = "title")]
        private IWebElement JobTitle { get; set; }

        // Collection of elements containing data about the job including Position Type, Date Posted, Location, and Description
        [FindsBy(How = How.ClassName, Using = "normal")]
        private IList<IWebElement> PostedElements { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Get the job title from the preview/live posting page
        /// </summary>
        /// <returns>The job title</returns>
        public string GetPostedJobTitle()
        {
            return JobTitle.Text.Trim();
        }

        /// <summary>
        /// Get the position type from the preview/live posting page
        /// </summary>
        /// <returns>The position type</returns>
        public string GetPostedPositionType()
        {
            return PostedElements[0].Text.Trim();
        }

        public string GetDatePosted()
        {
            return PostedElements[1].Text.Trim();
        }

        public string GetPostedLocation()
        {
            return PostedElements[2].Text.Trim();
        }

        public string GetPostedDescription()
        {
            return PostedElements[3].Text.Trim();
        }

        #endregion


    }
}
