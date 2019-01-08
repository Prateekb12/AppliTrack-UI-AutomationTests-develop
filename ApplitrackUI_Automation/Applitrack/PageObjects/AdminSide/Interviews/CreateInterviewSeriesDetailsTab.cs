using System;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Interviews
{
    public class CreateInterviewSeriesDetailsTab : BasePageObject
    {
        private readonly IWebDriver _driver;

        #region Constructor

        public CreateInterviewSeriesDetailsTab(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        [FindsBy(How = How.Id, Using = "Wizard1_TextBoxDetailsTitle")]
        private IWebElement InterviewTitleTextbox { get; set; }

        [FindsBy(How = How.Id, Using = "Wizard1_ButtonDeleteSeries")]
        private IWebElement DeleteButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[value='Yes, Delete Series']")] //Product Link
        public IWebElement YesDeleteSeriesButton { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        public void EnterTitle(string title)
        {
            InterviewTitleTextbox.SendKeys(title);
        }

        public void ClickDelete()
        {
            DeleteButton.WaitAndClick(_driver);
        }

        public void ConfirmDeleteInterview()
        {
            Console.Out.WriteLineAsync("Page: Attempting to confirm Delete the Interview Series");
            YesDeleteSeriesButton.WaitAndClick(_driver);
        }

        #endregion
    }
}
