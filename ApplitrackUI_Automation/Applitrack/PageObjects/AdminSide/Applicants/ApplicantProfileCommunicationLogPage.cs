using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace ApplitrackUITests.PageObjects.AdminSide.Applicants
{
    public class ApplicantProfileCommunicationLogPage : BasePageObject, IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructor

        public ApplicantProfileCommunicationLogPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        [FindsBy(How = How.CssSelector, Using = "div.MessageBody")]
        private IWebElement MessageBody { get; set; }
        #endregion

        #region Page Actions

        public bool IsDisplayed()
        {
            throw new NotImplementedException();
        }

        public bool IsCommunicationDisplayed(string emailBody)
        {
            MessageBody.WaitRetry(_driver);
            return IsTextOnScreen(_driver, emailBody);
        }

        #endregion
    }
}
