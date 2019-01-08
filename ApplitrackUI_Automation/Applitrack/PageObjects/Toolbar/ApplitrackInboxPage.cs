using System;
using System.Collections.Generic;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.Toolbar
{
    public class ApplitrackInboxPage : ToolbarInboxPage
    {
        private IWebDriver _driver;

        #region Constructors

        public ApplitrackInboxPage(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // Inbox that opens after clicking the Inbox Button
        [FindsBy(How = How.Id, Using = "Inbox")]
        protected override IWebElement Inbox { get; set; }

        #endregion

        #region Page Actions

        public override bool IsEmpty()
        {
            //TODO: fix this to map to correct elements
            //<div id="EmptyInbox" class="NoInboxItems ng-hide" ng-show="noItems">There are no items in your Inbox.</div>
            //Strategy: find this element and ensure that the class includes ng-hide
            
            //Inbox.WaitForElement(_driver);
            //return Inbox.GetAttribute("class").Contains("notifications-empty");
            throw new NotImplementedException();
        }

        public override IEnumerable<IWebElement> GetMatchingItems(string matchText)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IWebElement> GetMatchingUnreadItems(string matchText)
        {
            //TODO: fix this to map to correct elements.  Is there a concept of IsUnread in the old UI?
            //<div class="SplashInboxItem"><i class="icon-file-alt"></i><b><a href="#" data-toggle="popover" data-placement="bottom" data-content="<a href=javascript:void(editForm('C9361A31-314C-4615-A4E2-3E8EE590A4DA'))>Open Request</a><table><tr><th>Result</th><th>Date</th></tr><tr><td>Complete</td><td>Apr 24 2017  8:55PM</td></td></tr></table>" data-original-title="Request Details" data-vivaldi-spatnav-clickable="1">Assistant Principal Test Request</a><span class="ItemVerb"> - Approval Needed</span></b><br><span class="AdditionalInfo">From Sample Applicant · Apr 24 2017  8:56PM</span></div>
            //Strategy: find this element and ensure it exists

            throw new NotImplementedException();
        }

        public override void MarkAsRead(string matchText)
        {
            throw new NotImplementedException();
        }

        public override void DeleteNotification(string matchText)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
