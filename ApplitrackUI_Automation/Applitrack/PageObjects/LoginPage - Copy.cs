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
    public class LoginPage : BasePageObject
    {

        //For This Page
        private IWebDriver Driver;

        // Add objects below

        [FindsBy(How = How.Id, Using = "UserID")]
        [CacheLookup]
        private IWebElement UserNameField { get; set; }

        [FindsBy(How = How.Id, Using = "Password")]
        [CacheLookup]
        private IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "Login")]
        [CacheLookup]
        private IWebElement LoginButton { get; set; }

        [FindsBy(How = How.Id, Using = "LoginForm")]
        [CacheLookup]
        private IWebElement LoginLable { get; set; }

        [FindsBy(How = How.Id, Using = "LoginErr")]
        [CacheLookup]
        private IWebElement LogError { get; set; }

        [FindsBy(How = How.Id, Using = "aLogout")]
        [CacheLookup]    // Object for Logout link
        private IWebElement LogOut { get; set; }

        //Page Constructor

        public LoginPage(IWebDriver Driver)
        {
            this.Driver = Driver;
            PageFactory.InitElements(Driver, this);
        }
        
        public void EnterPwd(string pwd)
        {
            try
            {
                Console.Out.WriteLineAsync("EnterPwd: " + pwd );
                Password.Clear();
                Password.SendKeys(pwd);
                            }
            catch (NoSuchElementException e)
            {
                Console.Out.WriteLineAsync("Failed: EnterPwd: " + pwd);
                throw e;
            }
        }

        public void EnterUsername(string username)
        {
            try
            {
                Console.Out.WriteLineAsync("Enter UserName " + username);
                //waitForVisibility(Driver, UserNameField);
                UserNameField.Clear();
                UserNameField.SendKeys(username);
            }
            catch (NoSuchElementException e)
            {

                Console.Out.WriteLineAsync("Fail: Enter UserName " + username);
                throw e;
            }
        }

        public void ClickLogin()
        {
            Console.Out.WriteLineAsync("Click Login");
            try
            {
                LoginButton.Click();
                BaseWaitForPageToLoad(Driver, 10);
            }
            catch (NoSuchElementException e)
            {
                Console.Out.WriteLineAsync("Click Login");
                throw e;
            }

        }
        // Log out
        public void ClickLogOut()
        {
            try
            {
                Console.Out.WriteLineAsync("Click LogOut");
                LogOut.Click();
            }
            catch (NoSuchElementException e)
            {
                Console.Out.WriteLineAsync("Click LogOut");
                throw e;
            }

        }

        public bool homepagedisplayed()
        {
            try
            {
                Console.Out.WriteLineAsync("HomePageDisplayed");
                waitForEnabled(Driver, LoginButton);
                return IsElementEnabled(LoginButton);

            }
            catch (NoSuchElementException e)
            {
                Console.Out.WriteLineAsync("Failed: HomePageDisplayed");
                throw e;
            }
        }

        

	public void CheckLoginButton()
    {
        IsElementEnabled(LoginButton);
        Console.Out.WriteLineAsync("HomePage is displayed.");    
	}

    public void LoginFailed()
    {
        Console.Out.WriteLineAsync("In Login Failed");
        IsElementVisible(LogError);
        Assert.AreEqual("Login Failed", LogError.Text.ToString());
        Console.Out.WriteLineAsync("Login Failed Message dispalyed"); 
    }


    //public static void AssertLoginErrorMessage(Log log, WebDriver driver, String message)
    //{
    //    waitForElementVisible(log, driver, LoginErr, 25);
    //    assertText(log, driver, LoginErr, message, false);
    //}


    }
}

