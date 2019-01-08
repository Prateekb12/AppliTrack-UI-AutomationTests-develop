using System;
using System.Threading;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Setup
{
    public class EditPositionWindow : BasePageObject, IApplitrackPage
    {
        private IWebDriver _driver;

        #region Constructor

        public EditPositionWindow(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion


        #region Related Pages
        #endregion


        #region UI Selectors

        [FindsBy(How = How.Id, Using = "LabelPageHeader")]
        private IWebElement PageHeader { get; set; }

        [FindsBy(How = How.Id, Using = "TextBoxPosition")]
        private IWebElement PositionTextBox { get; set; }

        [FindsBy(How = How.Id, Using = "ButtonPositionSave")]
        private IWebElement SaveAndCloseButton { get; set; }

        [FindsBy(How = How.Id, Using = "UsesStrongePipelines")]
        private IWebElement ShpCheckBox { get; set; }

        #endregion


        #region Page Actions

        /// <summary>
        /// Make sure the page is displayed correctly
        /// </summary>
        /// <returns></returns>
        public bool IsDisplayed()
        {
            try
            {
                return PageHeader.Displayed && PageHeader.Text.Contains("Edit Position");
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionName"></param>
        public void EnterPositionName(string positionName)
        {
            PositionTextBox.SendKeys(positionName);
        }

        /// <summary>
        /// Click the Save and Close button
        /// </summary>
        public void ClickSaveAndClose()
        {
            SaveAndCloseButton.WaitAndClick(_driver);
            // wait for the window to close
            Thread.Sleep(TimeSpan.FromSeconds(3));
        }

        /// <summary>
        /// Make sure the "Uses Stronge Pipelines" checkbox is displayed on the page
        /// </summary>
        /// <returns>True if the checkbox is displayed, false otherwise</returns>
        public bool ShpCheckboxIsDisplayed()
        {
            try
            {
                return ShpCheckBox.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        #endregion
    }
}
