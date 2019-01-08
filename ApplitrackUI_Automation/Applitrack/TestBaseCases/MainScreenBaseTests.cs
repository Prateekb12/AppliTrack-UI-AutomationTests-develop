using System;
using System.Linq;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.Toolbar;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestBaseCases
{
    public abstract class MainScreenBaseTests : ApplitrackUIBase
    {
        protected abstract IWebDriver Driver { get; set; }
        protected abstract ExtentTest test { get; set; }

        public void VerifyUnreadItemPresent(Toolbar toolbar, NotificationResult expectedResult)
        {
            ClickAndVerifyInboxOpens(toolbar);

            Assert.IsTrue(toolbar.InboxPage.GetMatchingUnreadItems(expectedResult.Title).Any(), "An unread Item is not present");
            Assert.IsTrue(!toolbar.InboxPage.IsEmpty(), "The inbox should not be empty");
            //TODO: verify notification header and date?
            //Assert.IsTrue(toolbar.InboxPage.ContainsText(matchName), "Form title is not displayed");
        }

        public void ClickAndVerifyInboxOpens(Toolbar toolbar)
        {
            toolbar.ClickInbox();
            test.Log(LogStatus.Pass, "Click the Inbox button");

            Assert.IsTrue(toolbar.InboxIsDisplayed(), "The inbox did not open");
            test.Log(LogStatus.Pass, "The inbox opened");
        }
    }
}
