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
    public class SubMenuApplicants : BasePageObject
    {
        //For This Page
        private IWebDriver Driver;

        //Page Objects for this page
        [FindsBy(How = How.CssSelector, Using = "li.Header")] //Product Link
        [CacheLookup]
        public IWebElement MenuHeader { get; set; }

        //Page Objects for this page
        [FindsBy(How = How.LinkText, Using = "Applicant Dashboard")] //Product Link
        [CacheLookup]
        public IWebElement ApplicantDashboard { get; set; }

        //Vacancies Menu and Sub Menu
        [FindsBy(How = How.LinkText, Using = "Vacancies by Category")] //Product Link
        [CacheLookup]
        public IWebElement VacanciesByCategory { get; set; }
                

        //Vacancies By Location Menu and Sub Menu
        [FindsBy(How = How.LinkText, Using = "Vacancies by Location")] //Product Link
        [CacheLookup]
        public IWebElement VacanciesByLocation { get; set; }


        //Category Pipelines Menu and Sub Menu
        [FindsBy(How = How.LinkText, Using = "Category Pipelines")] //Product Link
        [CacheLookup]
        public IWebElement CategoryPipelines { get; set; }
       
        //Position Pools Menu and Sub Menu
        [FindsBy(How = How.LinkText, Using = "Position Pools")] //Product Link
        [CacheLookup]
        public IWebElement PositionPools { get; set; }
       
    
        //Actions, Notes and Status Menu and Sub Menu
        [FindsBy(How = How.LinkText, Using = "Actions, Notes and Status")] //Product Link
        [CacheLookup]
        public IWebElement ActionsNotesStatus { get; set; }

        
        public SubMenuApplicants(IWebDriver Driver)
        {
            this.Driver = Driver;
            PageFactory.InitElements(Driver, this);

        }

       //Category Pipelines    
       public void ClickCategoryPipelines()
            {
                Console.Out.WriteLineAsync("Page: Attempting to Click VacanciesByCategory Link");
                try
                {
                    CategoryPipelines.Click();
                }
                catch (Exception e) { throw e; }
            } 

        public void ClickApplicantDashboard()
        {
            Console.Out.WriteLineAsync("Page: Attempting to Click ApplicantDashboard Link");
            try
            {
                ApplicantDashboard.Click();
                BaseWaitForPageToLoad(Driver, 10);

            }
            catch (Exception e) { throw e; }
        }

        public void ClickVacanciesByCategory()
        {
            Console.Out.WriteLineAsync("Page: Attempting to Click VacanciesByCategory Link");
            try
            {
                VacanciesByCategory.Click();
                BaseWaitForPageToLoad(Driver, 10);
            }
            catch (Exception e) { throw e; }
        }

        public void ClickPositionPools()
        {
            Console.Out.WriteLineAsync("Page: Attempting to Click PositionPools Link");
            try
            {
                PositionPools.Click();
                BaseWaitForPageToLoad(Driver, 10);
            }
            catch (Exception e) { throw e; }
        }
    
        public void ClickActionsNotesStatus()
        {
            Console.Out.WriteLineAsync("Page: Attempting to Click ActionsNotesStatus Link");
            try
            {
                ActionsNotesStatus.Click();
                BaseWaitForPageToLoad(Driver, 10);
            }
            catch (Exception e) { throw e; }
        }

        public void VerifyApplicantsSubMenu()
        {
            Console.Out.WriteLineAsync("Page: Attempting to VerifyApplicantsSubMenu");
            try
            {
                Assert.IsTrue(IsElementEnabled(MenuHeader), "Assert: SubMenuHeader is enabled");
                Assert.IsTrue(MenuHeader.Text == "Applicants", "Assert: SubMenuHeader Text is Corrects"); 
                Assert.IsTrue(IsElementEnabled(ApplicantDashboard), "Assert: ApplicantDashboard Enabled");
                Assert.IsTrue(IsElementEnabled(VacanciesByCategory), "Assert: VacanciesByCategory Enasbled");
                Assert.IsTrue(IsElementEnabled(VacanciesByLocation), "Assert: VacanciesByLocation Enabled");
                Assert.IsTrue(IsElementEnabled(CategoryPipelines), "Assert: CategoryPipelines Enabled");
                Assert.IsTrue(IsElementEnabled(PositionPools), "Assert: PositionPools Enabled");
                Assert.IsTrue(IsElementEnabled(ActionsNotesStatus), "Assert: ActionsNotesStatus Enabled");
                Assert.IsTrue(IsElementEnabled(PositionPools), "Assert: PositionPools Enabled");
                //Partial -- more to add     

            }
            catch (Exception e) { throw e; }
        }    
    }
}
