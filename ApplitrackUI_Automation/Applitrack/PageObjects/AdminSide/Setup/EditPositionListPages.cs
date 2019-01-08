using System;
using System.Collections.Generic;
using ApplitrackUITests.PageObjects.PageTypes;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using By = OpenQA.Selenium.By;

namespace ApplitrackUITests.PageObjects.AdminSide.Setup
{
    public class EditPositionListPages : ScrollTableType, IApplitrackPage
    {
        private IWebDriver _driver;

        #region Constructor

        public EditPositionListPages(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion


        #region Related Pages
        private EditPositionWindow _editPositionWindow;
        /// <summary>
        /// The 'Edit Position List' page accessed from Setup > Applicant Settings > Edit Position List
        /// </summary>
        public EditPositionWindow EditPositionWindow
        {
            get { return _editPositionWindow ?? (_editPositionWindow = new EditPositionWindow(_driver)); }
        }
        #endregion


        #region UI Selectors
        // the page header
        [FindsBy(How = How.ClassName, Using = "ReportHeadTitle")]
        private IWebElement PageTitle { get; set; }
        #endregion


        #region Page Actions

        /// <summary>
        /// Check to see if the page is displayed
        /// </summary>
        /// <returns>True if the page header appears correctly, false otherwise</returns>
        public bool IsDisplayed()
        {
            return PageTitle.Displayed && PageTitle.Text.Contains("Edit Position List");
        }

        public IList<string> GetPositionList()
        {
            IList<string> positionIds = new List<string>();
            foreach (var position in ItemCheckboxes)
            {
                if (!position.GetAttribute("value").Equals(""))
                {
                    positionIds.Add(position.GetAttribute("value"));
                }
            }
            return positionIds;
        }

        public void EditPosition(string positionId)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("EditPosition('" + positionId + "')");
        }

        /// <summary>
        /// Click the 'Edit' button for a position category
        /// </summary>
        /// <param name="categoryName">The name of the category to edit</param>
        public void EditPositionCategory(string categoryName = null)
        {
            // if the category isnt specified, find the first category in the list
            if (String.IsNullOrWhiteSpace(categoryName))
            {
                categoryName = _driver.FindElement(By.CssSelector("#maintbl > tbody > #rowNbr0 > td > b")).Text;
            }

            ((IJavaScriptExecutor) _driver).ExecuteScript("EditPositionCategory('" + categoryName + "')");
        }

        #endregion


    }
}
