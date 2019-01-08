using System;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.ApplicantSide.EmploymentApplication
{
    public class ConfirmationPage : BasePageObject, IApplitrackPage
    {
        private IWebDriver _driver;

        #region Constructor

        public ConfirmationPage(IWebDriver driver)
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

        // TODO
        public bool IsDisplayed()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Make sure that the indicated step is completed by checking to see if the error appears or not
        /// </summary>
        /// <param name="stepName">The name of the step to check for</param>
        /// <returns>True if the step name does not appear on the screen, false otherwise</returns>
        public bool StepIsCompleted(string stepName)
        {
            _driver.SwitchToDefaultFrame();
            _driver.SwitchToFrameById("AppDataPage");
            var stepLink = _driver.FindElements(By.ClassName("goToLink"));
            foreach (var link in stepLink)
            {
                if (link.Text.Contains("[ Go to the " + stepName + " step ]"))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}
