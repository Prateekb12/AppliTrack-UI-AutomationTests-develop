using System;
using System.Linq;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.Toolbar;
using ApplitrackUITests.TestBaseCases;
using ApplitrackUITests.WorkFlows;
using Automation.Framework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.MainScreenTests
{
    [TestClass]
    public class MainScreenRegressionTests : MainScreenBaseTests
    {
        #region Setup and TearDown

        protected override IWebDriver Driver { get; set; }
        protected override ExtentTest test { get; set; }

        /// <summary>
        ///Test Initialize Contains items to 
        ///run before each [TestMethod]
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            // extent reports setup
            test = ExtentTestManager.StartTest(TestContext.Properties["TestCaseName"].ToString(),
                    TestContext.Properties["TestCaseDescription"].ToString())
                .AssignCategory("Regression");

            // browser setup
            Driver = SetUp(_BT); //Stand up Driver and Log Test
            Driver.Manage().Window.Maximize();
            BrowseTo(BaseUrls["ApplitrackLoginPage"], Driver);

            test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);
        }

        /// <summary>
        /// Runs after each test
        /// </summary>
        [TestCleanup]
        public void TestTearDown()
        {
            BaseTearDown(Driver);
        }

        #endregion

        #region Test Cases

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Toolbar")]
        [TestProperty("TestCaseName", "Toolbar Form Inbox Notification Workflow functions")]
        [TestProperty("TestCaseDescription",
            "Make sure a forms inbox notification displays, can be marked read, and can be deleted after creating the requisite data")]
        [TestProperty("UsesHardcodedData", "true")]
        public void Toolbar_Inbox_FormNotification_ValidateWorkflow()
        {
            ValidateNotificationWorkflow(new FormInboxNotificationGenerator());
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Toolbar")]
        [TestProperty("TestCaseName", "Toolbar Job Posting Notification Workflow functions")]
        [TestProperty("TestCaseDescription",
            "Make sure a job posting requisition notification displays, can be marked read, and can be deleted after creating the requisite data")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Toolbar_Inbox_RequisitonApproval_ValidateWorkflow()
        {
            ValidateNotificationWorkflow(new RequisitionNotificationGenerator());
        }

        [TestMethod]
        [TestCategory("Regression")]
        [TestProperty("TestArea", "Toolbar")]
        [TestProperty("TestCaseName", "Toolbar Interview Time Slot Notification Workflow functions")]
        [TestProperty("TestCaseDescription",
            "Make sure an interview notification displays, can be marked read, and can be deleted after creating the requisite data")]
        [TestProperty("UsesHardcodedData", "true")]
        public void Toolbar_Inbox_InterviewTimeSlot_ValidateWorkflow()
        {
            ValidateNotificationWorkflow(new InterviewNotificationGenerator());
        }

        #endregion

        #region Helper Test Methods

        private void ValidateNotificationWorkflow(INotificationGenerator dataGenerator)
        {
            try //Contains Contents of Test
            {
                //Set up notification
                test.Log(LogStatus.Info, "Setting up data");
                dataGenerator.CreateNotificationData();
                test.Log(LogStatus.Info, "Setting up data complete");

                new LoginWorkflows(Driver).LoginAsSuperUser();
                var toolbar = ToolbarFactory.Get(Driver);

                toolbar.WaitForLoad();
                VerifyUnreadItemPresent(toolbar, dataGenerator.ExpectedResult);
                VerifyMarkAsRead(toolbar, dataGenerator.ExpectedResult);
                ValidatePopupContent(toolbar, dataGenerator.ExpectedResult);
                VerifyMarkAsReadPersisted(toolbar, dataGenerator.ExpectedResult);
                VerifyDeleteItem(toolbar, dataGenerator.ExpectedResult);
                VerifyDeletePersisted(toolbar, dataGenerator.ExpectedResult);
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
            finally
            {
                dataGenerator.DeleteNotificationData();
            }
        }

        private void VerifyMarkAsRead(Toolbar toolbar, NotificationResult expectedResult)
        {
            var numMatchesPreAction = toolbar.InboxPage.GetMatchingUnreadItems(expectedResult.Title).Count();
            toolbar.InboxPage.MarkAsRead(expectedResult.Title);
            test.Log(LogStatus.Pass, "Mark item as read");

            var numMatchesPostAction = toolbar.InboxPage.GetMatchingUnreadItems(expectedResult.Title).Count();
            Assert.IsTrue(numMatchesPostAction < numMatchesPreAction, $"The number of items before marking ({numMatchesPreAction}) should be greater than after ({numMatchesPostAction})");
            Assert.IsTrue(!toolbar.InboxPage.IsEmpty(), "The inbox should not be empty");
        }

        private void ValidatePopupContent(Toolbar toolbar, NotificationResult expectedResult)
        {
            var parentHandle = Driver.CurrentWindowHandle;
            Driver.SwitchToPopup();

            toolbar.WaitForLoad();
            var frame = Driver.FindElement(By.Id(expectedResult.PopupInfo.FrameId));
            Assert.IsTrue(frame.GetAttribute("src").ToLower().Contains(expectedResult.PopupInfo.Url.ToLower()), $"The popup's main iFrame does not contain the expected URL.  Expected value:{expectedResult.PopupInfo.Url}  Actual value: {frame.GetAttribute("src")}");
            Assert.IsTrue(toolbar.IsTextOnScreen(Driver, expectedResult.PopupInfo.Content), $"Expected text '{expectedResult.PopupInfo.Content}' not found on page");

            Driver.SwitchTo().Window(parentHandle);
        }

        private void VerifyMarkAsReadPersisted(Toolbar toolbar, NotificationResult expectedResult)
        {
            var numMatchesPreAction = toolbar.InboxPage.GetMatchingUnreadItems(expectedResult.Title).Count();
            Driver.Refresh();
            test.Log(LogStatus.Pass, "Refresh page");

            toolbar.WaitForLoad();
            ClickAndVerifyInboxOpens(toolbar);

            var numMatchesPostAction = toolbar.InboxPage.GetMatchingUnreadItems(expectedResult.Title).Count();
            Assert.IsTrue(numMatchesPostAction.Equals(numMatchesPreAction), $"The number of items before refreshing ({numMatchesPreAction}) should match the number after ({numMatchesPostAction})");
            Assert.IsTrue(!toolbar.InboxPage.IsEmpty(), "The inbox should not be empty");
        }

        private void VerifyDeleteItem(Toolbar toolbar, NotificationResult expectedResult)
        {
            var numMatchesPreDelete = toolbar.InboxPage.GetMatchingItems(expectedResult.Title).Count();
            Assert.IsTrue(numMatchesPreDelete > 0, "There are no matching items to delete.");

            toolbar.InboxPage.DeleteNotification(expectedResult.Title);
            test.Log(LogStatus.Pass, "Delete the inbox item.");

            var numMatchesPostDelete = toolbar.InboxPage.GetMatchingItems(expectedResult.Title).Count();
            Assert.IsTrue(numMatchesPostDelete < numMatchesPreDelete, "No items were deleted after performing delete action.");
        }

        private void VerifyDeletePersisted(Toolbar toolbar, NotificationResult expectedResult)
        {
            var numMatchesPreAction = toolbar.InboxPage.GetMatchingItems(expectedResult.Title).Count();
            Driver.Refresh();
            test.Log(LogStatus.Pass, "Refresh page");

            toolbar.WaitForLoad();
            ClickAndVerifyInboxOpens(toolbar);

            var numMatchesPostAction = toolbar.InboxPage.GetMatchingItems(expectedResult.Title).Count();
            Assert.IsTrue(numMatchesPostAction.Equals(numMatchesPreAction), $"The number of items before refreshing ({numMatchesPreAction}) should match the number after ({numMatchesPostAction})");
        }

        #endregion
    }
}
