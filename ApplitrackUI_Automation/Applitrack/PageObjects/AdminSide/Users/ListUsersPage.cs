using System;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using ApplitrackUITests.Helpers;

namespace ApplitrackUITests.PageObjects.AdminSide.Users
{
    public class ListUsersPage : ScrollTableType, IApplitrackPage 
    {
        private readonly IWebDriver _driver;

        #region Constructor
        public ListUsersPage(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }
        #endregion

        #region UI Selectors

        // The header text for the List all users page
        [FindsBy(How = How.CssSelector, Using = "b.ReportHeadTitle")]
        private IWebElement HeaderText { get; set; }

        [FindsBy(How = How.Name, Using = "DeleteRecords")]
        private IWebElement DeleteUserButton { get; set; }

        #endregion

        #region Page Actions

        public bool IsDisplayed()
        {
            return HeaderText.WaitRetry(_driver).Text.Contains("Users") && HeaderText.Displayed;
        }

        /// <summary>
        /// Click the 'Delete User' button on the 'Users' page
        /// </summary>
        public void ClickDeleteUser()
        {
            Console.Out.WriteLineAsync("Page: Attempting to Click DeleteUser button");
            DeleteUserButton.WaitAndClick(_driver);
        }

        #endregion

    }
}
