using System;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Forms
{
    public class EditAndCreateFormPropertiesTab : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public EditAndCreateFormPropertiesTab(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // "Standard Form" Form Type radio button
        [FindsBy(How = How.XPath, Using = "//input[@value='Standard Form']")]
        private IWebElement StandardForm { get; set; }

        // "Title" textbox
        [FindsBy(How = How.XPath, Using = "//input[@id='TextboxFormTitle']")]
        private IWebElement FormTitleText { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Select the 'Standard' radio button 
        /// </summary>
        public void ClickStandardFormRadioButton()
        {
            Console.Out.WriteLineAsync("Page: Attempting to Click Standard form Radio button");
            StandardForm.WaitAndClick(_driver);
        }

        /// <summary>
        /// Enter text in the 'Title' field
        /// </summary>
        /// <param name="formTitle">The title of the form to enter</param>
        public void FillOutFormTitle(string formTitle)
        {
            Console.Out.WriteLineAsync("Page: Attempting to Fill out new Form title");
            FormTitleText.Clear();
            FormTitleText.SendKeys(formTitle);
            FormTitleText.SendKeys(Keys.Return);
            Console.Out.WriteLineAsync("Fill out new Form with title:" + formTitle);
        } 

        #endregion
    }
}
