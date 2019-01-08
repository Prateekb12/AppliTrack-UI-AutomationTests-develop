using System;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.ApplicantSide;
using ApplitrackUITests.PageObjects.ApplicantSide.EmploymentApplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.LandingPageTests
{
    [TestClass]
    public class LandingPageSmokeTests : ApplitrackUIBase
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
                .AssignCategory("Smoke");

            // browser setup
            Driver = SetUp(_BT);
            Driver.Manage().Window.Maximize();
            BrowseTo(BaseUrls["ApplitrackLandingPage"], Driver);

            test.Log(LogStatus.Info, "Starting test at URL: " + BaseUrls["ApplitrackLandingPage"]);
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
        /// <summary>
        /// User Story:
        /// As an internal applicant
        /// When I navigate to the homepage
        /// And click the Log in link
        /// And click the Start link in the new window
        /// I want to begin the application process
        /// </summary>
        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Applicant")]
        [TestProperty("TestCaseName", "Start as Internal Applicant")]
        [TestProperty("TestCaseDescription", "Start as Internal Applicant on the Landing Page")]
        [TestProperty("UsesHardcodedData", "false")]
        public void LandingPage_Start_As_Internal_Applicant()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var landingPage = new DefaultLandingPage(Driver);
            var applicantPages = new ApplicantPages(Driver);

            try  //Contains Contents of Test
            {
                landingPage.ClickInternalLogin();
                test.Log(LogStatus.Pass, "Click Internal Login");

                applicantPages.SwitchWindows();
                Driver.SwitchToFrameById("HomeDataPage");
                applicantPages.ClickStart();
                test.Log(LogStatus.Pass, "Clicking Start");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        /// <summary>
        /// User Story:
        /// As an external applicant
        /// When I navigate to the homepage
        /// And click the Log in link
        /// And click the Start link in the new window
        /// I want to begin the application process
        /// </summary>
        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Applicant")]
        [TestProperty("TestCaseName", "Start as External Applicant")]
        [TestProperty("TestCaseDescription", "Start as External Applicant on the Landing Page")]
        [TestProperty("UsesHardcodedData", "false")]
        public void LandingPage_Start_As_External_Applicant()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var landingPage = new DefaultLandingPage(Driver);
            var applicantPages = new ApplicantPages(Driver);

            try  //Contains Contents of Test
            {
                landingPage.ClickExternalLogin();
                test.Log(LogStatus.Pass, "Click External Login");

                applicantPages.SwitchWindows();
                Driver.SwitchToFrameById("HomeDataPage");

                applicantPages.ClickStart();
                test.Log(LogStatus.Pass, "Clicking Start");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        /// <summary>
        /// Go through each link on the page and make sure it is working
        /// </summary>
        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Applicant")]
        [TestProperty("TestCaseName", "Check Landing Page for Broken Links")]
        [TestProperty("TestCaseDescription", "Check Landing Page for Broken Links")]
        [TestProperty("UsesHardcodedData", "false")]
        [Ignore] // timeouts causing test to fail on QA2
        public void LandingPage_Check_For_Broken_Links()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var landingPage = new DefaultLandingPage(Driver);

            try  //Contains Contents of Test
            {
                landingPage.CheckForBrokenLinks();
                test.Log(LogStatus.Pass, "Checked for broken links");
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }

        /*
        /// <summary>
        /// User Story:
        /// As an applicant
        /// When I navigate to the homepage
        /// And I click on a Featured Job link
        /// I should see the appropriate job
        /// </summary>
        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Applicant")]
        [TestProperty("TestCaseID", "TC1952")]
        [TestProperty("TestCaseName", "Check Featured Job Links")]
        public void Check_Featured_Job_Links()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var landingPage = new Pages.DefaultLandingPage(Driver);

            try  //Contains Contents of Test
            {
                landingPage.ClickFeaturedJobLinks();
                //Driver.Quit();

            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }
         */

        /*
        /// <summary>
        /// User Story:
        /// As an applicant
        /// When I navigate to the homepage
        /// And I click on a Job Category link
        /// I should see all open jobs related to that category
        /// </summary>
        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Applicant")]
        [TestProperty("TestCaseID", "TC1953")]
        [TestProperty("TestCaseName", "Check Job Categories Links")]
        public void Check_Job_Categories_Links()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var landingPage = new Pages.DefaultLandingPage(Driver);

            try  //Contains Contents of Test
            {
                landingPage.ClickJobCategoryLinks();
                //Driver.Quit();
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }
         */

        /*
        /// <summary>
        /// User Story:
        /// As an applicant
        /// When I navigate to the homepage
        /// And I click on a location link
        /// I should see all open jobs in that location
        /// </summary>
        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Applicant")]
        [TestProperty("TestCaseID", "TC1954")]
        [TestProperty("TestCaseName", "Check Location Links")]
        public void Check_Location_Links()
        {
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var landingPage = new Pages.DefaultLandingPage(Driver);

            try  //Contains Contents of Test
            {
                landingPage.ClickLocationsLinks();
                //Driver.Quit();
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }
         */

        #endregion
    }
}