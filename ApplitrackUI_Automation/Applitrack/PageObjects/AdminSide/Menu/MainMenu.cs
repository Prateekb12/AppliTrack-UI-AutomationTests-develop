using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using ApplitrackUITests.Helpers;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;


namespace ApplitrackUITests.PageObjects.AdminSide.Menu
{
    public class MainMenu : BasePageObject
    {
        private readonly IWebDriver _driver;

        #region Constructor

        public MainMenu(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // All links on the currently active menu
        [FindsBy(How = How.CssSelector, Using = "div.additional-block.selected ul.menu li a")]
        private IList<IWebElement> NavMenuLinks { get; set; }

        [FindsBy(How = How.Id, Using = "icon-back")]
        private IWebElement BackIcon { get; set; }

        [FindsBy(How = How.Id, Using = "icon-refresh")]
        private IWebElement RefreshIcon { get; set; }

        [FindsBy(How = How.Id, Using = "NavigationFilter")]
        private IWebElement NavFilterTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".fa-home")]
        private IWebElement MainMenuTab { get; set; }

        [FindsBy(How = How.LinkText, Using = "My Dashboard")]
        private IWebElement MyDashboard { get; set; }

        [FindsBy(How = How.LinkText, Using = "Applicants")]
        private IWebElement Applicants { get; set; }

        [FindsBy(How = How.LinkText, Using = "Employees")]
        private IWebElement Employees { get; set; }

        [FindsBy(How = How.LinkText, Using = "Job Postings")]
        private IWebElement JobPostings { get; set; }

        [FindsBy(How = How.LinkText, Using = "Forms")]
        private IWebElement Forms { get; set; }

        [FindsBy(How = How.LinkText, Using = "Interviews")]
        private IWebElement Interviews { get; set; }

        [FindsBy(How = How.LinkText, Using = "Users")]
        private IWebElement Users { get; set; }

        [FindsBy(How = How.LinkText, Using = "My Account")]
        private IWebElement MyAccount { get; set; }

        [FindsBy(How = How.LinkText, Using = "Tools")]
        private IWebElement Tools { get; set; }

        [FindsBy(How = How.LinkText, Using = "Setup")]
        private IWebElement Setup { get; set; }

        [FindsBy(How = How.LinkText, Using = "Marketplace")]
        private IWebElement MarketPlace { get; set; }

        #endregion

        #region Page Actions

        /// <summary>
        /// Click the back icon on the main menu
        /// </summary>
        public void ClickBackIcon()
        {
            BackIcon.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the refresh icon on the main menu
        /// </summary>
        public void ClickRefreshIcon()
        {
            RefreshIcon.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Enter text in the filter textbox on the main menu
        /// </summary>
        /// <param name="text">The text to be entered into the field</param>
        public void EnterNavFilterText(string text)
        {
            NavFilterTextBox.WaitRetry(_driver).Click();
            NavFilterTextBox.Clear();
            NavFilterTextBox.SendKeys(text);
        }

        /// <summary>
        /// Click the Main Menu tab to return to the root menu
        /// </summary>
        public void ClickMainMenuTab()
        {
            MainMenuTab.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the My Dashboard link on the main menu
        /// </summary>
        public void ClickMyDashboard()
        {
            MyDashboard.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click on the Applicants link on the main menu
        /// </summary>
        public void ClickApplicants()
        {
            Applicants.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the employees link on the main menu
        /// </summary>
        public void ClickEmployees()
        {
            Employees.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the Job Postings link on the main menu
        /// </summary>
        public void ClickJobPostings()
        {
            JobPostings.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the Forms link on the main menu
        /// </summary>
        public void ClickForms()
        {
            Forms.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the Interviews link on the main menu
        /// </summary>
        public void ClickInterviews()
        {
            Interviews.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the Users link on the main menu
        /// </summary>
        public void ClickUsers()
        {
            var waits = new CommonWaits(_driver);
            waits.WaitForLoadingScreen();
            Users.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the My Account link on the main menu
        /// </summary>
        public void ClickMyAccount()
        {
            MyAccount.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the Tools link on the main menu
        /// </summary>
        public void ClickTools()
        {
            Tools.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the 'Setup' link on the main menu
        /// </summary>
        public void ClickSetup()
        {
            Setup.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the 'Marketplace' link on the main menu
        /// </summary>
        public void ClickMarketplace()
        {
            MarketPlace.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Switch to a window using the title
        /// </summary>
        /// <param name="title">The title of the window to switch to</param>
        /// <returns></returns>
        public bool SwitchToWindow(String title)
        {
            var currentWindow = _driver.CurrentWindowHandle;
            var availableWindows = new List<string>(_driver.WindowHandles);

            foreach (string w in availableWindows)
            {
                if (w != currentWindow)
                {
                    _driver.SwitchTo().Window(w);
                    Console.Out.WriteLineAsync("Switched to Window: " + _driver.Title);
                    if (_driver.Title == title)
                    {
                        return true;
                    }
                    else
                    {
                        _driver.SwitchTo().Window(currentWindow);
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Alert message methods
        /// </summary>
        public void CheckAlert()
        {
            try
            {
                IAlert alert = _driver.SwitchTo().Alert();
                alert.Dismiss();
            }
            catch (NoAlertPresentException)
            {
                Console.Out.WriteLineAsync("Alert is not present");
            }
        }

        /// <summary>
        /// Get all the link text from the current menu
        /// </summary>
        /// <returns>A StringCollection of all text from the current menu</returns>
        public StringCollection GetNavLinkText()
        {
            // wait for the menu to actually load
            // this avoids grabbing the incorrect elements
            Thread.Sleep(TimeSpan.FromSeconds(1));
            return NavMenuLinks.GetElementsText();
        }

        #endregion


    }
}
