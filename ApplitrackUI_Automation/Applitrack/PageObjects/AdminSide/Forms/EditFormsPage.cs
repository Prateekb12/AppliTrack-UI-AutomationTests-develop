using System.Collections.Generic;
using ApplitrackUITests.Helpers;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Forms
{
    public class EditFormsPage : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public EditFormsPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // the yes button on the "Delete Form(s)" window
        [FindsBy(How = How.Name, Using = "resp")]
        private IWebElement YesButton { get; set; }

        // All form categories in the Edit Forms page
        [FindsBy(How = How.CssSelector, Using = "td > b")]
        private IList<IWebElement> CategoryNames { get; set; }

        // All forms listed in the Edit Forms page
        // TODO use a better selector -- this currently selects things besides form names
        [FindsBy(How = How.CssSelector, Using = "#maintbl > tbody > tr > td")]
        private IList<IWebElement> FormNames { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        public void SelectForm(int formId)
        {
            var scrollTable = new ScrollTable(_driver);
            scrollTable.SelectItem(formId);
        }

        /// <summary>
        /// Check to see if the form exists on the Edit Forms page
        /// </summary>
        /// <param name="formId">The ID of the form to find</param>
        /// <returns>True if the form was found, false otherwise</returns>
        public bool FormExists(int formId)
        {
            var scrollTable = new ScrollTable(_driver);
            return scrollTable.FindItem(formId);
        }

        /// <summary>
        /// Check to see if the form exists on the Edit Forms page
        /// </summary>
        /// <param name="formName">The name of the form to find</param>
        /// <returns>True if the form was found, false otherwise</returns>
        public bool FormExists(string formName)
        {
            var scrollTable = new ScrollTable(_driver);
            return scrollTable.FindItem(formName, FormNames);
        }

        /// <summary>
        /// Check to see if a category exists on the Edit Forms page
        /// </summary>
        /// <param name="categoryName">The name of the category to find</param>
        /// <returns>True if the form was found, false otherwise</returns>
        public bool CategoryExists(string categoryName)
        {
            var scrollTable = new ScrollTable(_driver);
            return scrollTable.FindItem(categoryName, CategoryNames);
        }

        // TODO refactor window switching
        /// <summary>
        /// Click the Yes button to delete the user
        /// </summary>
        public void ConfirmDeletion()
        {
            var parentHandle = _driver.CurrentWindowHandle; // the main window
            var popupHandle = string.Empty; // the popup window
            IReadOnlyCollection<string> windowHandles = _driver.WindowHandles; // contains all currently open windows

            // find the popup window
            foreach (var handle in windowHandles)
            {
                if (handle == parentHandle) continue;
                popupHandle = handle;
                break;
            }

            // switch to the popup window, click <Yes>, and switch back to the main window
            _driver.SwitchTo().Window(popupHandle);
            YesButton.WaitAndClick(_driver);
            _driver.SwitchTo().Window(parentHandle);
        }

        #endregion

    }
}
