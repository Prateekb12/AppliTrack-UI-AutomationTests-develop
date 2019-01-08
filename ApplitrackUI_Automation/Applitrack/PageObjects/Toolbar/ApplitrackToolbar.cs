using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.Toolbar
{
    public class ApplitrackToolbar : Toolbar
    {
        private readonly IWebDriver _driver;

        #region Constructors

        public ApplitrackToolbar(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // The toolbar
        [FindsBy(How = How.Id, Using = "AT_Toolbar")]
        protected override IWebElement MainToolbar { get; set; }

        [FindsBy(How = How.Id, Using = "TopNavSearch")]
        protected override IWebElement SearchElement { get; set; }

        // The user name
        [FindsBy(How = How.Id, Using = "UserMenu")]
        protected override IWebElement UserMenu { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".topnav-help")]
        protected override IWebElement HelpButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#TopInbox")]
        protected override IWebElement InboxButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#TopNavNews")]
        private IWebElement NewsButton { get; set; }

        #endregion

        #region Related Pages

        private ApplitrackSearchPage _searchPage;
        /// <summary>
        /// The search page that opens after clicking the Search button
        /// </summary>
        public override ToolbarSearchPage SearchPage
        {
            get { return _searchPage ?? (_searchPage = new ApplitrackSearchPage(_driver)); }
        }

        private ApplitrackInboxPage _inboxPage;
        /// <summary>
        /// The inbox page that opens after clicking the Inbox button
        /// </summary>
        public override ToolbarInboxPage InboxPage
        {
            get { return _inboxPage ?? (_inboxPage = new ApplitrackInboxPage(_driver)); }
        }

        private ApplitrackNewsFeedPage _newsFeedPage;
        /// <summary>
        /// The news feed page that opens after clicking the RSS button
        /// </summary>
        public ApplitrackNewsFeedPage NewsFeedPage
        {
            get { return _newsFeedPage ?? (_newsFeedPage = new ApplitrackNewsFeedPage(_driver)); }
        }

        private ApplitrackHelpMenuPage _helpMenuPage;
        /// <summary>
        /// The help menu that opens after clicking the Help button
        /// </summary>
        public override ToolbarHelpMenuPage HelpMenuPage
        {
            get { return _helpMenuPage ?? (_helpMenuPage = new ApplitrackHelpMenuPage(_driver)); }
        }

        private ApplitrackUserMenuPage _userMenuPage;
        /// <summary>
        /// The user menu that opens after clicking the User Name
        /// </summary>
        public override ToolbarUserMenuPage UserMenuPage
        {
            get { return _userMenuPage ?? (_userMenuPage = new ApplitrackUserMenuPage(_driver)); }
        }

        #endregion

        #region Page Actions

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

        /// <summary>
        /// Click the news button on the toolbar
        /// </summary>
        public void ClickNews()
        {
            NewsButton.WaitAndClick(_driver);
        }

        public override void Logout()
        {
            ClickUserMenu();
            UserMenuPage.ClickSignOut();
        }

        public override void WaitForLoad()
        {
            //TODO: implement based on legacy DOM element
        }

        #endregion

    }
}
