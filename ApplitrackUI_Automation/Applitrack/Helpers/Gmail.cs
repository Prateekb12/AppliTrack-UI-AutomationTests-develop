using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.Helpers
{
    public class Gmail : BasePageObject
    {

        private IWebDriver _driver;

        public Gmail(IWebDriver Driver)
        {
            this._driver = Driver;
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.Id, Using = "Email")]
        private IWebElement GEmailAccountInput { get; set; }

        [FindsBy(How = How.Id, Using = "Passwd")]
        private IWebElement GmailPasswordField { get; set; }

        [FindsBy(How = How.Id, Using = "next")]
        private IWebElement GNext { get; set; }

        [FindsBy(How = How.Id, Using = "signIn")]
        private IWebElement SignInGmail { get; set; }

        [FindsBy(How = How.Id, Using = "gbqfq")]
        private IWebElement SearchInput { get; set; }

        [FindsBy(How = How.Id, Using = "gbqfb")]
        private IWebElement StartSearch { get; set; }

        private string GEmailUrl = "https://accounts.google.com/ServiceLogin?service=mail&continue=https://mail.google.com/mail/#identifier";
        private string GmailAddress = "applitrackautoemployee@gmail.com";
        private string GmailPassword = "@pplitrack";

        // method taken from IDM automation tests
        /// <summary>
        /// Log into the gmail account
        /// </summary>
        public void LogIntoTestEmailAccount()
        {
            _driver.Navigate().GoToUrl(GEmailUrl);
            GEmailAccountInput.Clear();
            GEmailAccountInput.SendKeys(GmailAddress);
            GNext.WaitAndClick(_driver);
            GmailPasswordField.Clear();
            GmailPasswordField.SendKeys(GmailPassword);
            SignInGmail.WaitAndClick(_driver);
            System.Threading.Thread.Sleep(1500);
        }

        // method taken from IDM automation tests
        /// <summary>
        /// Search for the email containing the specified subject line
        /// </summary>
        /// <param name="subjectline">The subject line of the email to search for</param>
        /// <returns>True if the email was found, false otherwise</returns>
        public bool SearchForEmail(string subjectline)
        {
            //var searchInput = Driver.FindElement(By.Id("gbqfq"));
            //var startSearch = Driver.FindElement(By.Id("gbqfb"));
            SearchInput.Clear();
            SearchInput.SendKeys(subjectline);
            StartSearch.WaitAndClick(_driver);
            int i = 0;
            int c = 0;
            do
            {
                System.Threading.Thread.Sleep(750);
                _driver.SwitchTo().DefaultContent();
                var results = _driver.FindElements(By.XPath("//*[contains(@class, 'TC')]"));
                c = 0;
                foreach (var result in results)
                {
                    bool visibile = result.Displayed;
                    if (visibile)
                    {
                        c++;
                    }
                }
                i++;
            }
            while (c > 0 && i < 30);

            var Tables = _driver.FindElements(By.XPath("//*[contains(@class, 'F cf zt')]"));
            foreach (var table in Tables)
            {
                bool visibility = table.Displayed;
                if (visibility == true)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
