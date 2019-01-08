using System;
using System.Collections.Generic;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ApplitrackUITests.PageObjects.AdminSide.Forms
{
    public class SendFormPage : BasePageObject, IApplitrackPage
    {
        private IWebDriver _driver;

        #region Constructor

        public SendFormPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        // the page header
        [FindsBy(How = How.Id, Using = "LabelPageHeader")]
        private IWebElement PageHeader { get; set; }

        // the "Me" radio button
        [FindsBy(How = How.Id, Using = "Wizard1_RadioButtonRecipientCurrentUser")]
        private IWebElement MeRadioButton { get; set; }

        // the "Continue with Selected Forms" button
        [FindsBy(How = How.Id, Using = "ButtonSelectMultipleForms")]
        private IWebElement ContinueWithSelectedFormsButton { get; set; }

        // The "Select associated applicant" field
        [FindsBy(How = How.Id, Using = "SearchApplicants")]
        private IWebElement SelectAssociatedApplicantField { get; set; }

        // THe "Select associated employee" field
        [FindsBy(How = How.Id, Using = "SearchEmployees")]
        private IWebElement SelectAssociatedEmployeeField { get; set; }

        // The "Next" button that appears after selecting a form to send
        [FindsBy(How = How.Id, Using = "ButtonWizardNext")]
        private IWebElement NextButton { get; set; }

        // The "Close Window" button
        [FindsBy(How = How.Id, Using = "Wizard1_ButtonCloseWindow")]
        private IWebElement CloseWindowButton { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions
        public bool IsDisplayed()
        {
            try
            {
                return PageHeader.WaitRetry(_driver).Text.Contains("Send Form");
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Select the form to send using the form's ID
        /// </summary>
        /// <param name="formId">The ID of the form to select</param>
        public void SelectFormToSendById(int formId)
        {
            // TODO: Make this a pageobject
            var chk = _driver.FindElement(By.Id("CheckboxSelectForm_" + formId));
            chk.ScrollIntoView(_driver);
            chk.WaitAndClick(_driver);
        }

        /// <summary>
        /// Select the form to send using the form packet's ID
        /// </summary>
        /// <param name="formId">The ID of the form packet to select</param>
        public void SelectPacketToSendById(int formId)
        {
            // wait for page to be displayed
            IsDisplayed();
            var chk = _driver.FindElement(By.Id("CheckboxSelectPacket_" + formId)).WaitRetry(_driver);
            chk.ScrollIntoView(_driver);
            chk.Click();
        }

        /// <summary>
        /// Click the 'Continue with Selected Forms' button after selecting a form(s) to deliver in the form delivery wizard
        /// </summary>
        public void ClickContinueWithSelectedForms()
        {
            ContinueWithSelectedFormsButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Select the applicant to deliver the form to
        /// </summary>
        /// <param name="appNo">The applicants number</param>
        public void SelectAssociatedApplicant(string appNo)
        {
            SelectAssociatedApplicantField.SendKeys(appNo);

            // TODO: Make this more robust
            var applicantList = _driver.FindElement(By.Id("ContextApplicant"+appNo));
            applicantList.WaitAndClick(_driver);
        }

        public void SelectAssociatedEmployee(string employeeNo)
        {
            SelectAssociatedEmployeeField.SendKeys(employeeNo);

            // TODO: make this more robust
            var employeeList = _driver.FindElement(By.ClassName("ac_over"));
            employeeList.WaitAndClick(_driver);
        }

        /// <summary>
        /// Select a radio button for who the form will be sent to
        /// </summary>
        /// <param name="recipient"></param>
        public void SelectFormRecipient(string recipient)
        {
            switch (recipient)
            {
                case "me":
                    MeRadioButton.WaitRetry(_driver).Click();
                    break;
            }
        }

        /// <summary>
        /// Click the 'Next' button in the form delivery wizard
        /// </summary>
        public void ClickNext()
        {
            // wait for the page to load
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait.Until(d => !_driver.FindElement(By.Id("VeilLoadingBackground")).Displayed);
            NextButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Finish and Deliver' button after selecing a form to deliver in the form delivery wizard
        /// </summary>
        public void ClickFinishAndDeliver()
        {
            // wait for the page to load
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(120));
            wait.Until(d => !_driver.FindElement(By.Id("VeilLoadingBackground")).Displayed);
            wait.Until(d => NextButton.Displayed);
            NextButton.ScrollIntoView(_driver);

            NextButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Close Window' button that appears after sending a form from applicant admin page
        /// Then switch back to the main window
        /// </summary>
        public void ClickCloseWindow()
        {
            var currentHandle = _driver.CurrentWindowHandle; // the main window
            var mainHandle = string.Empty; // the main window
            IReadOnlyCollection<string> windowHandles = _driver.WindowHandles; // contains all currently open windows

            // wait for the page to load
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait.Until(d => !_driver.FindElement(By.Id("VeilLoadingBackground")).Displayed);
            wait.Until(d => CloseWindowButton.Displayed);

            CloseWindowButton.WaitAndClick(_driver);

            // find the popup window
            foreach (var handle in windowHandles)
            {
                if (handle == currentHandle) continue;
                mainHandle = handle;
                break;
            }

            _driver.SwitchTo().Window(mainHandle);
        }

        public void ClickSaveAsDraft()
        {
            _driver.FindElement(By.Id("ButtonSaveAsDraft")).WaitAndClick(_driver);
        }


        #endregion
    }
}
