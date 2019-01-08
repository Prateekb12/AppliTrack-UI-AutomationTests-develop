using System;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ApplitrackUITests.PageObjects.AdminSide
{
    public class StrongeInstallPage : BasePageObject, IApplitrackPage
    {
        private IWebDriver _driver;

        #region Constructor

        public StrongeInstallPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        // The 'Your name (for signature)' textbox
        [FindsBy(How = How.Id, Using = "TextboxContactSignature")]
        private IWebElement YourNameField { get; set; }

        // The checkbox to agree to the TOS
        [FindsBy(How = How.Id, Using = "CheckBoxAcceptLicense")]
        private IWebElement TosCheckBox { get; set; }

        // The Install button
        [FindsBy(How = How.Id, Using = "ButtonInstall")]
        private IWebElement InstallButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "h3")]
        private IWebElement InstallStatusText { get; set; }
        #endregion

        #region Page Actions

        public bool IsDisplayed()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Enter the given name into the 'Your name (for signature) field
        /// </summary>
        /// <param name="name"></param>
        public void EnterYourName(string name)
        {
            YourNameField.SendKeys(name);
        }

        /// <summary>
        /// Mark the checkbox to agree to the Frontline TOS
        /// </summary>
        public void MarkTosBox()
        {
            // using a loop as sometimes the checkbox will not be marked on the first try
            do
            {
                TosCheckBox.WaitRetry(_driver).Click();
            } while (!TosCheckBox.Selected);
        }

        /// <summary>
        /// Click the Install button
        /// </summary>
        public void ClickInstallButton()
        {
            InstallButton.WaitRetry(_driver).WaitAndClick(_driver);
        }

        /// <summary>
        /// Make sure that the install was successful
        /// </summary>
        /// <returns>True if successful, false otherwise</returns>
        public bool InstallSucceeded()
        {
            return InstallStatusText.Text.Contains("Success!");
        }

        #endregion
    }
}
