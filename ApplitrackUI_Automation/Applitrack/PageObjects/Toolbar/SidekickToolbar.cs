using System;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ApplitrackUITests.PageObjects.Toolbar
{
    public class SidekickToolbar : Toolbar 
    {
        private readonly IWebDriver _driver;

        #region Constructors

        public SidekickToolbar(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        [FindsBy(How = How.CssSelector, Using = "div.sidekick")]
        protected override IWebElement MainToolbar { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input.search")]
        protected override IWebElement SearchElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button.app-title")]
        private IWebElement AppTitle { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button.app-title")]
        private IWebElement AppSwitcher { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".sk-org-switcher")]
        private IWebElement OrgSwitcher { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".sk-help")]
        protected override IWebElement HelpButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button.sk--control")]
        protected override IWebElement UserMenu { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".sk-notifications")]
        protected override IWebElement InboxButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".sk-org-switcher > .menu")]
        private IWebElement OrgSwitcherMenu { get; set; }

        #endregion

        #region Related Pages

        private SidekickSearchPage _searchPage;
        /// <summary>
        /// The search page that opens after clicking the Search button
        /// </summary>
        public override ToolbarSearchPage SearchPage => _searchPage ?? (_searchPage = new SidekickSearchPage(_driver));

        private SidekickInboxPage _inboxPage;
        /// <summary>
        /// The inbox page that opens after clicking the Inbox button
        /// </summary>
        public override ToolbarInboxPage InboxPage => _inboxPage ?? (_inboxPage = new SidekickInboxPage(_driver));

        private SidekickHelpMenuPage _helpMenuPage;
        /// <summary>
        /// The help menu that opens after clicking the Help button
        /// </summary>
        public override ToolbarHelpMenuPage HelpMenuPage => _helpMenuPage ?? (_helpMenuPage = new SidekickHelpMenuPage(_driver));

        private SidekickUserMenuPage _userMenuPage;
        /// <summary>
        /// The user menu that opens after clicking the User Name
        /// </summary>
        public override ToolbarUserMenuPage UserMenuPage => _userMenuPage ?? (_userMenuPage = new SidekickUserMenuPage(_driver));

        #endregion


        #region Page Actions

        public string ApplicationTitle
        {
            get
            {
                AppTitle.WaitForElement(_driver);
                return AppTitle.Text;
            }
        }

        public override void Logout()
        {
            ClickUserMenu();
            UserMenuPage.ClickSignOut();
        }

        public override bool HelpMenuIsDisplayed()
        {
            return HelpMenuPage.IsDisplayed();
        }

        public override bool InboxIsDisplayed()
        {
            return InboxPage.IsDisplayed();
        }

        public override bool SearchIsDisplayed()
        {
            return SearchPage.IsDisplayed();
        }

        public override bool UserMenuIsDisplayed()
        {
            return UserMenuPage.IsDisplayed();
        }

        public override void WaitForLoad()
        {
            // wait for the 'Loading' screen to disppear and for the search results to be displayed
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(2000));
            wait.Until(d => !d.FindElement(By.Id("Loading")).Displayed);
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.sk--content-right")));
        }

        public void ClickOrgSwitcher()
        {
            OrgSwitcher.WaitAndClick(_driver);
        }

        private void ClickAppSwitcher()
        {
            AppSwitcher.WaitAndClick(_driver);
        }

        public bool OrgSwitcherIsDisplayed()
        {
            try
            {
                return OrgSwitcherMenu.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void SwitchApps(string appName)
        {
            ClickAppSwitcher();
            _driver.FindElement(By.PartialLinkText(appName)).WaitAndClick(_driver);
        }

        public void SwitchOrgs(string orgName)
        {
            ClickOrgSwitcher();
            _driver.FindElement(By.PartialLinkText(orgName)).WaitAndClick(_driver);
        }

        #endregion
    }
}
