using System.Collections.Generic;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.Toolbar
{
    public abstract class ToolbarInboxPage : BasePageObject, IApplitrackPage
    {
        private IWebDriver _driver;

        #region Constructors

        public ToolbarInboxPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        protected abstract IWebElement Inbox { get; set; }

        #endregion


        #region Page Actions

        /// <summary>
        /// See if the inbox is opened
        /// </summary>
        /// <returns>Returns true if the inbox is opened, false otherwise</returns>
        public virtual bool IsDisplayed()
        {
            Inbox.WaitForElement(_driver);
            return Inbox.Displayed;
        }

        /// <summary>
        /// Determine whether notification items are present or not in the inbox
        /// </summary>
        /// <returns></returns>
        public abstract bool IsEmpty();

        /// <summary>
        /// Return all inbox notification items matching supplied body text
        /// </summary>
        /// <param name="matchText"></param>
        /// <returns></returns>
        public abstract IEnumerable<IWebElement> GetMatchingItems(string matchText);

        /// <summary>
        /// Return all unread inbox notification items matching supplied body text
        /// </summary>
        /// <param name="matchText"></param>
        /// <returns></returns>
        public abstract IEnumerable<IWebElement> GetMatchingUnreadItems(string matchText);

        /// <summary>
        /// Mark the first notification item found matching the text as read
        /// </summary>
        public abstract void MarkAsRead(string matchText);

        /// <summary>
        /// Delete the first notification item found matching the text
        /// </summary>
        public abstract void DeleteNotification(string matchText);

        #endregion
    }
}
