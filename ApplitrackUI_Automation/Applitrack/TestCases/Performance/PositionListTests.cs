using System;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using ApplitrackUITests.PageObjects.AdminSide.Setup;
using ApplitrackUITests.WorkFlows;
using Automation.Framework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.Performance
{
    [TestClass]
    public class PositionListTests : ApplitrackUIBase
    {
        #region Setup and TearDown

        private IWebDriver Driver;
        private ExtentTest test;

        /// <summary>
        /// Test Initialize Contains items to run before each [TestMethod]
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            // extent reports setup
            test = ExtentTestManager.StartTest(TestContext.Properties["TestCaseName"].ToString(),
                TestContext.Properties["TestCaseDescription"].ToString())
                .AssignCategory("Regression");

            // browser setup
            Driver = SetUp(_BT);
            Driver.Manage().Window.Maximize();
            BrowseTo("https://phlaptweb7.applitrack.com/devmichaelf", Driver);
            //BrowseTo(BaseUrls["ApplitrackLoginPage"], Driver);

            var loginWorkflows = new LoginWorkflows(Driver);
            //loginWorkflows.LoginAsSuperUser();
            loginWorkflows.Login("admin", "27157");
        }

        [TestCleanup]
        public void TestTearDown()
        {
            BaseTearDown(Driver);
        }

        #endregion

        #region Test Cases

        [TestMethod]
        [TestCategory("PerformanceRegression")]
        [TestProperty("TestArea", "Performance")]
        [TestProperty("TestCaseName", "Edit All Positions")]
        [TestProperty("TestCaseDescription", "Edit all the positions in the Position List in order to make sure that no server error occurs")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Edit_Position_List()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            // pages
            var mainMenu = new MainMenu(Driver);
            var setupMenu = new SubMenuSetup(Driver);
            var setupPages = new SetupPages(Driver);

            try  //Contains Contents of Test
            {
                var mainWindow = Driver.CurrentWindowHandle; // the main window

                //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

                // Navigate to Setup > Applicant Settings > Edit Position List
                mainMenu.ClickSetup();
                setupMenu.ClickApplicantSettings();
                setupMenu.ClickEditPositionList();
                test.Log(LogStatus.Pass, "Navigate to Setup > Applicant Settings > Edit Position List");

                Driver.SwitchToFrameById("MainContentsIFrame");

                var positions = setupPages.ApplicantSettingsPages.EditPositionListPages.GetPositionList();

                foreach (var position in positions)
                {
                    // click the edit button for the given position
                    setupPages.ApplicantSettingsPages.EditPositionListPages.EditPosition(position);
                    test.Log(LogStatus.Pass, "Edit position: " + position);

                    // switch to the new window and rename the position
                    Driver.SwitchToPopup();
                    Assert.IsTrue(setupPages.ApplicantSettingsPages.EditPositionListPages.EditPositionWindow.IsDisplayed(),
                        "The Edit Position window is not displayed correctly");
                    test.Log(LogStatus.Pass, "The Edit Position window is displayed correctly");
                    setupPages.ApplicantSettingsPages.EditPositionListPages.EditPositionWindow.EnterPositionName(" test");

                    // click save and close
                    test.Log(LogStatus.Pass, "Click the Save and Close button");
                    setupPages.ApplicantSettingsPages.EditPositionListPages.EditPositionWindow.ClickSaveAndClose();

                    // switch back to the main window
                    Driver.SwitchTo().Window(mainWindow);
                    Driver.SwitchToDefaultFrame();
                    Driver.SwitchToFrameById("MainContentsIFrame");
                }
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }
        #endregion

    }
}




