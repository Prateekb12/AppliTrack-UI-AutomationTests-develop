using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Automation;

namespace Automation.Pages
{
    public class "PageName Here" : BasePageObject
    {

        //For This Page
        private IWebDriver Driver;

        // Add objects below

        //[FindsBy(How = How.LinkText, Using = "Main")]
        //private IWebElement Main { get; set; }

        //[FindsBy(How = How.Id, Using = "Password")]
        //private IWebElement Password { get; set; }

        //[FindsBy(How = How.Id, Using = "Login")]
        //private IWebElement LoginButton { get; set; }

        //public    WelcomePage(IWebDriver Driver)
        //    {
        //        // TODO: Complete member initialization
        //this.Driver = Driver;
        //    }}


        //Page Constructor
        public Applicant(IWebDriver Driver)
        {
            this.Driver = Driver;
            PageFactory.InitElements(Driver, this);
        }

        //public void VerifyLogin()
        //{
        //    IsElementVisible(Dashboard);
        //    //Assert.AreEqual("Login Failed", LogError.Text.ToString());
        //    Console.WriteLine("User Looged IN");
        //}

    }
}