using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Menu
{
    public class SuperSuitNavigation : BasePageObject, IApplitrackPage
    {
        #region Constructor

        private IWebDriver _driver;

        public SuperSuitNavigation(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        [FindsBy(How = How.CssSelector, Using = "ul.sk--menu-items li.sk-nav-item a")]
        private IList<IWebElement> NavMenuLinks { get; set; }

        [FindsBy(How = How.LinkText, Using = "My Dashboard")]
        private IWebElement MyDashboardLink { get; set; }

        [FindsBy(How = How.LinkText, Using = "Applicants")]
        private IWebElement ApplicantLink { get; set; }

        [FindsBy(How = How.LinkText, Using = "Employees")]
        private IWebElement EmployeesLink { get; set; }

        [FindsBy(How = How.LinkText, Using = "Job Postings")]
        private IWebElement JobPostingsLink { get; set; }

        [FindsBy(How = How.LinkText, Using = "Forms")]
        private IWebElement FormsLink { get; set; }

        [FindsBy(How = How.LinkText, Using = "Interviews")]
        private IWebElement InterviewsLink { get; set; }

        [FindsBy(How = How.LinkText, Using = "Users")]
        private IWebElement UsersLink { get; set; }

        [FindsBy(How = How.LinkText, Using = "My Account")]
        private IWebElement MyAccountLink { get; set; }

        [FindsBy(How = How.LinkText, Using = "Tools")]
        private IWebElement ToolsLink { get; set; }

        [FindsBy(How = How.LinkText, Using = "Setup")]
        private IWebElement SetupLink { get; set; }

        [FindsBy(How = How.LinkText, Using = "Marketplace")]
        private IWebElement MarketplaceLink { get; set; }
        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        public bool IsDisplayed()
        {
            throw new System.NotImplementedException();
        }

        public StringCollection GetNavLinkText()
        {
            return NavMenuLinks.GetElementsText();
        }

        public void ClickMyDashboard()
        {
            MyDashboardLink.WaitAndClick(_driver);
        }

        public void ClickApplicants()
        {
            ApplicantLink.WaitAndClick(_driver);
        }
        public void ClickEmployees()
        {
            EmployeesLink.WaitAndClick(_driver);
        }

        public void ClickJobPostings()
        {
            JobPostingsLink.WaitAndClick(_driver);
        }

        public void ClickForms()
        {
            FormsLink.WaitAndClick(_driver);
        }

        public void ClickInterviews()
        {
            InterviewsLink.WaitAndClick(_driver);
        }

        public void ClickUsers()
        {
            UsersLink.WaitAndClick(_driver);
        }

        public void ClickMyAccount()
        {
            MyAccountLink.WaitAndClick(_driver);
        }

        public void ClickTools()
        {
            ToolsLink.WaitAndClick(_driver);
        }

        public void ClickSetup()
        {
            SetupLink.WaitAndClick(_driver);
        }

        public void ClickMarketplace()
        {
            MarketplaceLink.WaitAndClick(_driver);
        }
        #endregion
    }
}