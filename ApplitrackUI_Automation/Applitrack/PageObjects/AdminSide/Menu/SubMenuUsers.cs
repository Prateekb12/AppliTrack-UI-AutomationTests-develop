using Automation;
using Automation.Framework.Extensions;
using IDMPageObjects.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using ApplitrackUITests.Helpers;

namespace ApplitrackUITests.PageObjects.AdminSide.Menu
{
    public class SubMenuUsers : BasePageObject
    {
        private readonly IWebDriver _driver;

        #region Constructor

        public SubMenuUsers(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // 'List all users' link in the Users menu.
        [FindsBy(How = How.LinkText, Using = "List all users")]
        private IWebElement ListAllUsersLink { get; set; }

        // 'Create a new user' link in the Users menu.
        [FindsBy(How = How.LinkText, Using = "Create a new user")]
        private IWebElement CreateNewUser { get; set; }

        // 'Application Access' link in the Users menu.
        [FindsBy(How = How.PartialLinkText, Using = "Application Access")]
        private IWebElement ApplicationAccessLink { get; set; }

        // 'Manager User Access' link in the users menu. Links to the SAAP.
        [FindsBy(How = How.PartialLinkText, Using = "Manage User Access")]
        private IWebElement ManageUserAccessLink { get; set; }

        #endregion

        #region Page Actions

        /// <summary>
        /// Click 'Create a new user' on the menu.
        /// </summary>
        public void ClickCreateNewUser()
        {
            waitForIt(_driver, CreateNewUser);
            CreateNewUser.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'List all users' link on the menu.
        /// </summary>
        public void ClickUsersList()
        {
            ListAllUsersLink.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the 'Application Access' link on the menu.
        /// </summary>
        public void ClickApplicationAccess()
        {
            _driver.WaitForIt(ApplicationAccessLink);
            ApplicationAccessLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Manager User Access' link on the menu.
        /// This will only appear when the 'Integration.IDM.SAAP.Enabled' flag is on
        /// </summary>
        public void ClickManageUserAccess()
        {
            _driver.WaitForIt(ManageUserAccessLink);
            ManageUserAccessLink.WaitAndClick(_driver);
        }
        #endregion



    }
}
