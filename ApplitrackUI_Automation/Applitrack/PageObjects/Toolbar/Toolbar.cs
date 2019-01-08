using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using Automation.Framework.Extensions;
using IDMPageObjects.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.Toolbar
{
    public abstract class Toolbar : BasePageObject, IApplitrackPage
    {
        private readonly IWebDriver _driver;

        protected Toolbar(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #region Related Pages

        public abstract ToolbarSearchPage SearchPage { get; }

        public abstract ToolbarInboxPage InboxPage { get; }

        public abstract ToolbarHelpMenuPage HelpMenuPage { get; }

        public abstract ToolbarUserMenuPage UserMenuPage { get; }

        #endregion


        #region UI Selectors

        protected abstract IWebElement MainToolbar { get; set; }

        protected abstract IWebElement SearchElement { get; set; }

        protected abstract IWebElement UserMenu { get; set; }

        protected abstract IWebElement HelpButton { get; set; }

        protected abstract IWebElement InboxButton { get; set; }

        #endregion


        #region Page Actions

        /// <summary>
        /// Make sure the toolbar is displayed 
        /// </summary>
        /// <returns></returns>
        public virtual bool IsDisplayed()
        {
            try
            {
                _driver.WaitForIt(MainToolbar);
                return MainToolbar.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Log out of the system
        /// </summary>
        public abstract void Logout();

        /// <summary>
        /// Click the Help icon
        /// </summary>
        public virtual void ClickHelp()
        {
            HelpButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the search button/field
        /// </summary>
        public virtual void ClickSearch()
        {
            SearchElement.WaitAndClick(_driver, 200000);
        }

        /// <summary>
        /// Click the User Menu
        /// </summary>
        public virtual void ClickUserMenu()
        {
            waitForIt(_driver, UserMenu);
            UserMenu.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the Inbox/Notification icon
        /// </summary>
        public virtual void ClickInbox()
        {
            InboxButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Make sure the help menu is displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public abstract bool HelpMenuIsDisplayed();

        /// <summary>
        /// Make sure the inbox/notification window is displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public abstract bool InboxIsDisplayed();

        /// <summary>
        /// Make sure the search window is displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public abstract bool SearchIsDisplayed();

        /// <summary>
        /// Make sure the user menu is displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public abstract bool UserMenuIsDisplayed();

        /// <summary>
        /// Ensure the toolbar is fully loaded
        /// </summary>
        public abstract void WaitForLoad();

        #endregion


    }
}
