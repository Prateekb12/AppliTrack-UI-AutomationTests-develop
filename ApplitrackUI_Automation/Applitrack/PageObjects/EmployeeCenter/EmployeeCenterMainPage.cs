using System;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ApplitrackUITests.PageObjects.EmployeeCenter
{
    public class EmployeeCenterMainPage : BasePageObject, IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructors

        public EmployeeCenterMainPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        [FindsBy(How = How.CssSelector, Using = "#fl-app-ec-dashboard")]
        private IWebElement EcDashboard { get; set; }

        #endregion

        #region Page Actions

        /// <summary>
        /// See if the Employee Center application is displayed
        /// </summary>
        /// <returns>Returns true if EC is opened, false otherwise</returns>
        public bool IsDisplayed()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#fl-app-ec-dashboard")));

            try
            {
                return EcDashboard.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        #endregion

    }
}
