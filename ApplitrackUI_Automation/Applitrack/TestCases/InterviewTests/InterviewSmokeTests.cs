using System;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide.Interviews;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using ApplitrackUITests.PageObjects.Menu;
using ApplitrackUITests.WorkFlows;
using Automation.Framework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.InterviewTests
{
    [TestClass]
    public class InterviewSmokeTests : ApplitrackUIBase
    {

        #region Setup and TearDown

        private IWebDriver Driver;
        private ExtentTest test;

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
                .AssignCategory("Smoke");

            // browser setup
            Driver = SetUp(_BT);
            Driver.Manage().Window.Maximize();
            BrowseTo(BaseUrls["ApplitrackLoginPage"], Driver);

            test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

            // login
            var loginWorkflow = new LoginWorkflows(Driver);
            loginWorkflow.LoginAsSuperUser();
        }

        /// <summary>
        /// Runs after each test
        /// </summary>
        [TestCleanup]
        public void TestTearDown()
        {
            //LogTearDown(); // End the test and write the log
            BaseTearDown(Driver);
        }

        #endregion

        #region Test Cases



        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "create New Interview")]
        [TestProperty("TestCaseName", "Validate Interview Creation")]
        [TestProperty("TestCaseDescription", "Validate Interview Creation")]
        [TestProperty("UsesHardcodedData", "false")]
        [Ignore]
        public void Interview_Create_New()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var mainMenu = new MainMenu(Driver);
            var interviewMenu = new SubMenuInterviews(Driver);
            var interviewPages = new InterviewPages(Driver);

            var interviewData = new InterviewData();

            try  //Contains Contents of Test
            {
                //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

                mainMenu.ClickInterviews();
                interviewMenu.ClickCreateInterview();
                test.Log(LogStatus.Pass, "Navigate to Interviews > Create Interview");

                mainMenu.SwitchToWindow("Manage Interview Series");
                interviewPages.CreateInterviewPages.StartTab.SelectCreateNew();
                test.Log(LogStatus.Pass, "Click Add New Interview");

                interviewPages.CreateInterviewPages.StartTab.SelectGeneralRecruiting();
                test.Log(LogStatus.Pass, "Click Add New Interview Type");

                interviewPages.CreateInterviewPages.ClickNext();
                test.Log(LogStatus.Pass, "Click Next");

                interviewPages.CreateInterviewPages.SeriesDetailsTab.EnterTitle(interviewData.InterviewTitle);
                test.Log(LogStatus.Pass, "Entering title: " + interviewData.InterviewTitle);

                interviewPages.CreateInterviewPages.ClickSummaryTab();
                test.Log(LogStatus.Pass, "Click Summary tab");

                interviewPages.CreateInterviewPages.ClickSaveFinish();
                test.Log(LogStatus.Pass, "Click Save and Finish");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        /// <summary>
        ///
        /// </summary>
        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Delete Interview")]
        [TestProperty("TestCaseName", "Validate Interview Deletion")]
        [TestProperty("TestCaseDescription", "Validate Interview Deletion")]
        [TestProperty("UsesHardcodedData", "true")]
        [Ignore]
        public void Interview_Delete()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var mainMenu = new MainMenu(Driver);
            var interviewPages = new InterviewPages(Driver);
            var interviewData = new InterviewData();

            try  //Contains Contents of Test
            {
                //test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLoginPage"]);

                mainMenu.ClickInterviews();
                test.Log(LogStatus.Pass, "Navigate to Interviews");

                Driver.SwitchToFrameById("MainContentsIFrame");
                interviewPages.MyInterviewsPage.SelectInterview(interviewData.InterviewTitle);
                test.Log(LogStatus.Pass, "Select the interview from the list");

                Driver.SwitchToPopup();
                interviewPages.CreateInterviewPages.SeriesDetailsTab.ClickDelete();
                test.Log(LogStatus.Pass, "Click delete");

                interviewPages.CreateInterviewPages.SeriesDetailsTab.ConfirmDeleteInterview();
                test.Log(LogStatus.Pass, "Confirm the deletion");

                Driver.ClosePopup();
                test.Log(LogStatus.Pass, "Close interview deletion window");
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
