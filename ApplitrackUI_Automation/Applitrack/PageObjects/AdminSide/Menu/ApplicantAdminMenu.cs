/*
 * Menu for each applicant admin mode
 */

using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.Menu
{
    public class ApplicantAdminMenu : BasePageObject 
    {
        //For This Page
        private IWebDriver _driver;

        [FindsBy(How = How.PartialLinkText, Using = "Interviews")]
        private IWebElement Interviews { get; set; }

        [FindsBy(How = How.PartialLinkText, Using = "List All Forms")]
        private IWebElement ListAllForms { get; set; }

        [FindsBy(How = How.PartialLinkText, Using = "Send a Form")]
        private IWebElement SendAForm { get; set; }

        [FindsBy(How = How.PartialLinkText, Using = "Communication Log")]
        private IWebElement CommunicationLog { get; set; }

        public ApplicantAdminMenu(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        // Click the 'List All Forms' link from the applicant menu
        public void ClickListAllForms()
        {
            ListAllForms.WaitAndClick(_driver);
        }

        public void ClickSendAForm()
        {
            SendAForm.WaitAndClick(_driver);
        }

        public void ClickCommuncationLog()
        {
            CommunicationLog.WaitAndClick(_driver);
        }

        public void ClickInterviews()
        {
            Interviews.WaitAndClick(_driver);
        }
    }

}
