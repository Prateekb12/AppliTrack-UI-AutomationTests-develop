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
    public class "TestAreaName" : BaseFrameWork
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
        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Login")]
        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"|DataDirectory|\DataFiles\CrossBrowser2.csv", "CrossBrowser2#csv", DataAccessMethod.Sequential)]

        public void "TestCaseName"()
        {
            //Override Default BrowserType with DataSource BrowserType
            //BT = Convert.ToString(testContextInstance.DataRow["BrowserType"]);
            //Test Information for Logging
            string TCN = "FailTest";
            string TCID = "0000";
            string USID = "0000";

            Driver = SetUp(BT, TCN, TCID, USID); //Stand up Driver and Log Test
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var Login = new Pages.LoginPage(Driver);
           
            try  //Contains Contents of Test
            {

                Console.WriteLine("OpenSite");
                BrowseTo(_baseURL, Driver);
                Login.CheckLoginButton();
                CleanUp(Driver);

            }
            catch (Exception e) //On Error Do
            {
                OnError(e, Driver);
                throw;
            }

        }
   }
}