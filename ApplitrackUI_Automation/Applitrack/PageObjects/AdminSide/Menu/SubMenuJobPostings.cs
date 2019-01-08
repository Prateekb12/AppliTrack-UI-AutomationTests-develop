using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ApplitrackUITests.Helpers;
using Automation;
using Automation.Framework.Extensions;
using IDMPageObjects.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;


namespace ApplitrackUITests.PageObjects.Menu
{
    public class SubMenuJobPostings : BasePageObject
    {
        //For This Page
        private IWebDriver _driver;

        //Page Objects for this page
        [FindsBy(How = How.LinkText, Using = "Job Postings Dashboard")] 
        private IWebElement JobPostingsDashboard { get; set; }

        [FindsBy(How = How.LinkText, Using = "List All Active Postings")] 
        private IWebElement ListAllActivePostings { get; set; }

        [FindsBy(How = How.LinkText, Using = "Create New Posting")] 
        private IWebElement CreateNewPosting { get; set; }

        [FindsBy(How = How.LinkText, Using = "Open Postings By Category")] 
        private IWebElement OpenPostingsByCategory { get; set; }

        [FindsBy(How = How.LinkText, Using = "Active Postings By Category")] 
        private IWebElement ActivePostingsByCategory { get; set; }

        [FindsBy(How = How.LinkText, Using = "All Postings By Status")] 
        private IWebElement AllPostingsByStatus { get; set; }

        // TODO: requision inbox
        /*
        [FindsBy(How = How.LinkText, Using = "All Postings By Status")] 
        private IWebElement CreateNewPosting { get; set; }
         */

        [FindsBy(How = How.LinkText, Using = "Create New Requisition")] 
        private IWebElement CreateNewRequisition { get; set; }

        [FindsBy(How = How.LinkText, Using = "My Draft Requisitions")] 
        private IWebElement MyDraftRequsitions { get; set; }

        [FindsBy(How = How.LinkText, Using = "My Requisitions In Process")] 
        private IWebElement MyRequisitionsInProcess { get; set; }

        [FindsBy(How = How.LinkText, Using = "My Open Requisitions")] 
        private IWebElement MyOpenRequisitions { get; set; }

        [FindsBy(How = How.LinkText, Using = "My Closed Requisitions")] 
        private IWebElement MyClosedRequisitions { get; set; }

        [FindsBy(How = How.LinkText, Using = "My Denied Requisitions")] 
        private IWebElement MyDeniedRequisitions { get; set; }

        [FindsBy(How = How.LinkText, Using = "Approved By Me -Active")] 
        private IWebElement ApprovedByMeActive { get; set; }

        [FindsBy(How = How.LinkText, Using = "Approved By Me -InActive")] 
        private IWebElement ApprovedByMeInactive { get; set; }

        [FindsBy(How = How.LinkText, Using = "Close An Approved Posting")] 
        private IWebElement CloseAnApprovedPosting { get; set; }

        [FindsBy(How = How.LinkText, Using = "All Active Requisitions")] 
        private IWebElement AllActiveRequisitions { get; set; }

        [FindsBy(How = How.LinkText, Using = "All Inactive Requisitions")] 
        private IWebElement AllInactiveRequisitions { get; set; }

        [FindsBy(How = How.LinkText, Using = "Create New Template")] 
        private IWebElement CreateNewTemplate { get; set; }

        [FindsBy(How = How.LinkText, Using = "List Existing")] 
        private IWebElement ListExisting { get; set; }


        public SubMenuJobPostings(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void ClickJobPostingsDashboard()
        {
            JobPostingsDashboard.WaitAndClick(_driver);
        }

        public void ClickCreateNewPosting()
        {
            CreateNewPosting.WaitAndClick(_driver);
        }

        public void ClickListAllActivePostings()
        {
            ListAllActivePostings.WaitAndClick(_driver);
        }

        public void ClickCreateNewRequisition()
        {
            CreateNewRequisition.WaitAndClick(_driver);
        }

        public void ClickAllActiveRequisitions()
        {
            AllActiveRequisitions.WaitAndClick(_driver);
        }

        public void ClickCreateNewTemplate()
        {
            CreateNewTemplate.WaitAndClick(_driver);
        }

        public void ClickListExisting()
        {
            ListExisting.WaitAndClick(_driver);
        }

        public void ClickOpenPostingsByCategory()
        {
            OpenPostingsByCategory.WaitAndClick(_driver);
        }

        public void ClickActivePostingsByCategory()
        {
            ActivePostingsByCategory.WaitAndClick(_driver);
        }

        public void ClickCategory(string category)
        {
            _driver.WaitForIt();
            var categoryLink = _driver.FindElement(By.PartialLinkText(category));
            categoryLink.WaitAndClick(_driver);
        }

        public void ClickMyDraftRequisitions()
        {
            MyDraftRequsitions.WaitAndClick(_driver);
        }

        public void ClickMyClosedRequisitions()
        {
            MyClosedRequisitions.WaitAndClick(_driver);
        }

        public void ClickAllPostingsByStatus()
        {
            AllPostingsByStatus.WaitAndClick(_driver);
        }

        public bool StatusExists(string status)
        {
            var statusLink = _driver.FindElement(By.PartialLinkText(status));
            return statusLink.Displayed;
        }

        public string GetMenuCount(string status)
        {
            var statusLocator = By.PartialLinkText(status);
            _driver.WaitForLocator(statusLocator);
            var statusLink = _driver.FindElement(statusLocator);
            var parent = statusLink.GetParentElement();
            var count = parent.FindElement(By.CssSelector("span.count.pull-right")).Text;
            return count;
        }

        /// <summary>
        /// Get the name of each job category from the menu
        /// Used for Job Postings by Category
        /// </summary>
        /// <returns>An enumerable of categories</returns>
        public IEnumerable<string> GetJobPostingsByCategory()
        {
            var categories = _driver.FindElements(By.CssSelector("ul.menu li a"));
            foreach (var element in categories)
            {
                if (IsCategoryLink(element))
                {
                    // remove any numbers and carriage returns
                    yield return Regex.Replace(element.Text, @"[\d-]", string.Empty).Replace("\r\n", string.Empty);
                }
            }
        }

        /// <summary>
        /// Check to see of the element is a category link
        /// Also ignores the 'All Job Postings' category as the Scroll Table does not contain the category header
        /// </summary>
        /// <param name="element">The link to check</param>
        /// <returns>True if the link is a category, false otherwise</returns>
        private bool IsCategoryLink(IWebElement element)
        {
            return element.GetAttribute("href").Contains("ItemViewer.aspx?SearchName=JobPostingsSearch") 
                && !String.IsNullOrWhiteSpace(element.Text)
                && !element.Text.Contains("All Job Postings");
        }
    }
}

