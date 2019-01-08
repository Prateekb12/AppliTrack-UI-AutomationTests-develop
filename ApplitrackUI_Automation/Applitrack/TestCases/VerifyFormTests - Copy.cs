using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.IE;
using Automation;


namespace Automation
{
    [TestClass]
    public class VerifyNewFormTests : BaseFrameWork
    {

        //***********************************************
        //This Section Contains the needed SetUp Activites
        //for all the tests to execute below. 
        //***********************************************

        //Class Vars
        private string BT; //Pass BT as it changes between methods
        private IWebDriver Driver;
        //Load Test Helpers

        /// <summary>
        ///Test Initialize Contains items to 
        ///run before each [TestMethod]
        /// </summary>
        [TestInitialize]
        public void testInit()
        {
            //Browser Default 
            //Value Stored in App.Config 
            BT = _BT;
            //To override the Default BrowserType
            // Un-comment and Set Value below 
            //string BT = "firefox"; 
        }


        /// <summary>
        /// Build From Page Objects: builds a test directly from Page Object Actions
        /// </summary>
        /// 
        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Menu")]
        [TestProperty("TestCaseID", "TC1310")]
        [TestProperty("TestCaseName", "Validate Forms Sub Menu")]

        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"|DataDirectory|\DataFiles\CrossBrowser2.csv", "CrossBrowser2#csv", DataAccessMethod.Sequential)]

        public void ValidateFormsMenu()
        {
            //Override Default BrowserType with DataSource BrowserType
            //BT = Convert.ToString(testContextInstance.DataRow["BrowserType"]);

            Driver = SetUp(BT); //Stand up Driver and Log Test
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var LoginData = new Data.LoginData();
            var LogingPage = new Pages.LoginPage(Driver);
            var MainMenu1 = new Pages.MainMenu1(Driver);
            var Form = new Pages.SubMenuForms(Driver);


            try  //Contains Contents of Test
            {
                BrowseTo(_baseURL, Driver);
                LogingPage.EnterUsername(LoginData.UserName);
                LogingPage.EnterPwd(LoginData.Password);
                LogingPage.ClickLogin();
                MainMenu1.ClickForms();
                Form.VerifyFormsSubMenu();

                Driver.Quit();

            }
            catch (Exception e) //On Error Do
            {
                OnError(e, Driver);
                throw;
            }
        }


        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Forms Menu")]
        [TestProperty("TestCaseID", "TC1312")]
        [TestProperty("TestCaseName", "Validate Design And Packets SubMenu")]

        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"|DataDirectory|\DataFiles\CrossBrowser2.csv", "CrossBrowser2#csv", DataAccessMethod.Sequential)]

        public void ValidateDesignAndPacketsSubMenu()
        {
            //Override Default BrowserType with DataSource BrowserType
            //BT = Convert.ToString(testContextInstance.DataRow["BrowserType"]);

            Driver = SetUp(BT); //Stand up Driver and Log Test
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var LoginData = new Data.LoginData();
            var LogingPage = new Pages.LoginPage(Driver);
            var MainMenu1 = new Pages.MainMenu1(Driver);
            var Form = new Pages.SubMenuForms(Driver);


            try  //Contains Contents of Test
            {
                BrowseTo(_baseURL, Driver);
                LogingPage.EnterUsername(LoginData.UserName);
                LogingPage.EnterPwd(LoginData.Password);
                LogingPage.ClickLogin();
                MainMenu1.ClickForms();
                Form.ClickDesignFormsandPackets();

               

               // Driver.Quit();

            }
            catch (Exception e) //On Error Do
            {
                OnError(e, Driver);
                throw;
            }
        }



    }

}
