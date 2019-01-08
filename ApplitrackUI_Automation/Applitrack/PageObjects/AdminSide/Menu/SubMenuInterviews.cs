using System;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;


namespace ApplitrackUITests.PageObjects.Menu
{
    public class SubMenuInterviews : BasePageObject
    {
        //For This Page
        private IWebDriver _driver;

        //Create New User Menu and Sub Menu
        [FindsBy(How = How.LinkText, Using = "Create Interview")] //Product Link
        [CacheLookup]
        public IWebElement CreateInterview { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[value='Yes, Delete Series']")] //Product Link
        [CacheLookup]
        public IWebElement ConfirmDelete { get; set; }

        //Constructor
        public SubMenuInterviews(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);

        }

        //Page actions
        public void ClickCreateInterview()
        {
            Console.Out.WriteLineAsync("Page: Attempting to Create Interview");
            try
            {
                CreateInterview.WaitAndClick(_driver);
            }
            catch (Exception e) { throw e; }
        }

        public void ConfirmDeleteInterview()
        {
            Console.Out.WriteLineAsync("Page: Attempting to confirm Delete the Interview Series");
            try
            {
                ConfirmDelete.WaitAndClick(_driver);
                
            }
            catch (Exception e) { throw e; }
        }

    }
}

