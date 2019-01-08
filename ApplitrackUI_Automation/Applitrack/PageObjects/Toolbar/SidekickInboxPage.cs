using System;
using System.Collections.Generic;
using System.Linq;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.Toolbar
{
    public class SidekickInboxPage : ToolbarInboxPage
    {
        private IWebDriver _driver;

        #region Constructors

        public SidekickInboxPage(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // Inbox that opens after clicking the Inbox Button
        [FindsBy(How = How.CssSelector, Using = "div.sk-notifications ul.menulist")]
        protected override IWebElement Inbox { get; set; }

        #endregion

        #region Page Actions

        public override IEnumerable<IWebElement> GetMatchingItems(string matchText)
        {
            Inbox.WaitForElement(_driver);
            return Inbox.FindElements(By.CssSelector("li.menulist-item"))
                .Where(e => e.Text.Contains(matchText));
        }

        public override IEnumerable<IWebElement> GetMatchingUnreadItems(string matchText)
        {
            Inbox.WaitForElement(_driver);
            return GetMatchingItems(matchText).Where(e => e.GetAttribute("class").Contains("unread"));
        }

        public override bool IsEmpty()
        {
            Inbox.WaitForElement(_driver);
            return Inbox.FindElements(By.CssSelector("li.notifications-empty")).Any();
        }

        public override void MarkAsRead(string matchText)
        {
            var element = GetMatchingItems(matchText).First();
            element.WaitForElement(_driver);
            element.WaitAndClick(_driver);
        }

        public override void DeleteNotification(string matchText)
        {
            var deleteButton = GetMatchingItems(matchText).First().FindElement(By.CssSelector("button.remove"));
            //<button type="button" class="remove fa-li-close" aria-label="remove notification" tabindex="0"></button>
            deleteButton.WaitForElement(_driver);
            deleteButton.WaitAndClick(_driver);
        }

        #endregion
    }
}
