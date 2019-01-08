using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace Automation.Pages
{
    public class SubMenuForms : BasePageObject
    {
        //For This Page
        private IWebDriver Driver;
       
        //Page Objects for this page
        [FindsBy(How = How.Id, Using = "")] //Main Menu Tab
        [CacheLookup]
        public IWebElement NO { get; set; }

         public SubMenuForms (IWebDriver Driver)
        {
            this.Driver = Driver;
            PageFactory.InitElements(Driver, this);
        
        }

        //Page Actions

         public void ClickNO()
         {
             Console.Out.WriteLineAsync("Page: Attempting to Click Back Icon");
             try
             {
                 NO.Click();
                 BaseWaitForPageToLoad(Driver, 10);
             }
             catch (Exception e) { throw e; }
         }
    
        public void VerifyFormsMenu()
        {
           Console.Out.WriteLineAsync("Verifying Form Menu Elements");
           try
           {
               Assert.IsTrue(IsElementEnabled(NO), "Assert: MainMenuTab is Enabled");
               Assert.IsTrue(IsElementEnabled(NO), "Assert: Applicants is Enabled");
               Assert.IsTrue(IsElementEnabled(NO), "Assert: Employees is Enabled");
               Assert.IsTrue(IsElementEnabled(NO), "Assert: JobPostings is Enabled");
               Assert.IsTrue(IsElementEnabled(NO), "Assert: Users is Enabled");
           }
           catch (Exception e)
           {
               Console.Out.WriteLineAsync("Failed: Verifying Form Menu Elements");
               throw e;
           }
        }
    }
}
