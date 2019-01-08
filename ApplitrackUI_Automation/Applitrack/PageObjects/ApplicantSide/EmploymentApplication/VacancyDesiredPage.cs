using System;
using System.Threading;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Automation.Framework.Extensions;

namespace ApplitrackUITests.PageObjects.ApplicantSide.EmploymentApplication
{
    public class VacancyDesiredPage : BasePageObject, IApplitrackPage
    {

        private IWebDriver _driver;

        #region Constructor

        public VacancyDesiredPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region Related Pages
        #endregion

        #region UI Selectors
        #endregion

        #region Page Actions

        // TODO
        public bool IsDisplayed()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Select the checkbox for the given vacancy using the Job ID
        /// </summary>
        /// <param name="jobId">The ID of the job to select</param>
        public void SelectVacancy(int jobId)
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
            _driver.SwitchToFrameById("AppDataPage");
            var jobLink = _driver.FindElement(By.PartialLinkText("JobID " + jobId)).WaitRetry(_driver);
            jobLink.ScrollIntoView(_driver);

            // mark the job textbox
            jobLink.GetParentElement().GetParentElement().FindElement(By.TagName("input")).WaitRetry(_driver).Click();
        }

        #endregion


    }
}
