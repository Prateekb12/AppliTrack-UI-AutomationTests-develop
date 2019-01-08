using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.CommonPages
{
    public class EmailPage : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public EmailPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        [FindsBy(How = How.Id, Using = "EmailSubject")]
        private IWebElement EmailSubject { get; set; }

        [FindsBy(How = How.ClassName, Using = "cke_editable")]
        private IWebElement EmailBody { get; set; }

        [FindsBy(How = How.Id, Using = "SendBtn")]
        private IWebElement SendMessageButton { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        public void ClickSendMessageButton()
        {
            SendMessageButton.WaitAndClick(_driver);
        }

        public bool IsEmailAddressDisplayed(string email)
        {
            return IsTextOnScreen(_driver, email);
        }

        public void EnterEmailTitle(string subject)
        {
            EmailSubject.Clear();
            EmailSubject.SendKeys(subject);
        }

        public void EnterEmailBody(string body)
        {
            EmailBody.Clear();
            EmailBody.SendKeys(body);
        }

        #endregion
    }
}
