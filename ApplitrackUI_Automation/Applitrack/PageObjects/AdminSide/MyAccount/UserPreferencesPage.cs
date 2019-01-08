using System.Collections.Generic;
using System.Collections.Specialized;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.MyAccount
{
    public class UserPreferencesPage : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public UserPreferencesPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // The list of preferences available
        [FindsBy(How = How.CssSelector, Using = "form#thisForm table tbody tr td b")]
        private IList<IWebElement> AvailablePreferences { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Get a list of the preferences on the page
        /// </summary>
        /// <returns>A collection of strings containing the name each preference on the page</returns>
        public StringCollection GetActualPreferences()
        {
            var actualPreferences = new StringCollection();
            foreach (var preference in AvailablePreferences)
            {
                actualPreferences.Add(preference.Text);
            }
            return actualPreferences;
        }

        #endregion


    }
}