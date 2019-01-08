/**
 * For the Applicant pages accessed from:
 * - the Default Landing Page
 * - logging in as an applicant
 */

using System;
using System.Collections.Generic;
using ApplitrackUITests.Helpers;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ApplitrackUITests.PageObjects.ApplicantSide.EmploymentApplication
{
    public class ApplicantPages : BasePageObject
    {
        private readonly IWebDriver _driver;
        
        #region Constructors

        public ApplicantPages(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region Related Pages

        private EmploymentApplicationPages _employmentApplicationPages;
        public EmploymentApplicationPages EmploymentApplicationPages
        {
            get
            {
                return _employmentApplicationPages ??
                       (_employmentApplicationPages = new EmploymentApplicationPages(_driver));
            }
        }
        #endregion

        #region UI Selectors

        /*
         * Before logging in
         */

        [FindsBy(How = How.Id, Using = "PrimaryID")]
        private IWebElement EmailAddressField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "html body form#form1 div#right-side div div#PanelHomeRight div.RightToDoItem input")]
        private IWebElement StartLink { get; set; }

        [FindsBy(How = How.Id, Using = "Password")]
        private IWebElement PasswordField { get; set; }

        /*
         * After logging in
         */

        [FindsBy(How = How.CssSelector, Using = "#Tabs > table:nth-child(1) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(11) > a:nth-child(1)")]
        private IWebElement FormsTab { get; set; }

        // The list of forms on the left
        [FindsBy(How = How.ClassName, Using = "NavLeft_Item")]
        private IList<IWebElement> FormList { get; set; }

        [FindsBy(How = How.Id, Using = "LinkNextFormBG")]
        private IWebElement NextPageButton { get; set; }

        /*
         * Forms
         */

        [FindsBy(How = How.Id, Using = "ButtonApproveStep")]
        private IWebElement ApproveButton { get; set; }

        [FindsBy(How = How.Id, Using = "ButtonDenyStep")]
        private IWebElement DenyButton { get; set; }

        [FindsBy(How = How.LinkText, Using = "Employment Application")]
        private IWebElement EmploymentApplicationTab { get; set; }

        #endregion

        #region Properties

        public string FormGuid { get; set; }

        #endregion

        #region Page Actions

        // TODO move this to the helper class
        public void SwitchWindows()
        {
            string currentHandle = _driver.CurrentWindowHandle; // the main window
            string otherHandle = string.Empty; // the popup window
            IReadOnlyCollection<string> windowHandles = _driver.WindowHandles; // contains all currently open windows

            // find the popup window
            foreach (string handle in windowHandles)
            {
                if (handle != currentHandle)
                {
                    otherHandle = handle;
                    break;
                }
            }

            _driver.SwitchTo().Window(otherHandle);
        }

        /// <summary>
        /// Click the Start link on the right hand side of the screen in order to begin the application process
        /// </summary>
        public void ClickStart()
        {
            Console.Out.WriteLineAsync("Page: Attempting to Click the Start button to begin the application process.");
            StartLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Forms' tab
        /// </summary>
        public void ClickFormsTab()
        {
            SwitchWindows();
            FormsTab.WaitAndClick(_driver);
        }

        // TODO Refactor this to not return the form key
        /// <summary>
        /// Select the form by the name of the form from the left navigation
        /// This will select the first instance of the form in the list
        /// </summary>
        /// <param name="formName">The name of the form to be selected</param>
        public string SelectForm(string formName)
        {
            // The formGuid is a system generated string that uniquely identifies the form that was sent
            // It should be used to locate the form in the list of forms sent to an applicant
            var formGuid = string.Empty;
            // Go through each form in the list, if the name matches, select it
            // This will select the first instance of the form in the list
            foreach (var form in FormList)
            {
                if (!form.Text.Contains(formName)) continue;
                formGuid = GetKey(form.GetAttribute("id"));
                form.WaitAndClick(_driver);
                break;
            }
            return formGuid;
        }

        /// <summary>
        /// Get the key from the first instance of the formName in the list of forms sent to the applicant
        /// </summary>
        /// <param name="formName">The name of the form to find</param>
        /// <returns>A key that uniquely identifies the form that was sent</returns>
        public string GetFormGuid(string formName)
        {
            // The formGuid is a system generated string that uniquely identifies the form that was sent
            // It should be used to locate the form in the list of forms sent to an applicant
            var formGuid = string.Empty;
            // Go through each form in the list, if the name matches, select it
            // This will select the first instance of the form in the list
            foreach (var form in FormList)
            {
                if (!form.Text.Contains(formName)) continue;
                formGuid = GetKey(form.GetAttribute("id"));
            }
            FormGuid = formGuid;
            return formGuid;
        }


        // TODO Refactor this using code from SelectForm method, make it public?
        /// <summary>
        /// Get the value generated by the system in order to locate the form elsewhere in the system
        /// Take the substring in order to remove 'NavItem' from it
        /// </summary>
        /// <param name="formId">The "id" of the form from the list of forms displayed while logged in as an applicant</param>
        private string GetKey(string formId)
        {
            return formId.Substring(7);
        }

        /// <summary>
        /// Click the 'Next Page' button at the bottom of the window
        /// </summary>
        public void ClickNextPage()
        {
            NextPageButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Enter the applicants name to digitally sign the form
        /// </summary>
        /// <param name="appName"></param>
        public void EnterDigitalSignature(string appName)
        {
            var digitalSignatureField = _driver.FindElements(By.CssSelector("div#ViewContainer input"));
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(120));
            wait.Until(d => digitalSignatureField[0].Displayed);
            wait.Until(d => digitalSignatureField[2].Displayed);
            digitalSignatureField[0].SendKeys(appName);
            digitalSignatureField[2].WaitAndClick(_driver);
        }

        /// <summary>
        /// Verify the 'Approve' button exists
        /// </summary>
        /// <returns>True if the button exists, false otherwise</returns>
        public bool ApproveButtonExists()
        {
            return ApproveButton.Displayed;
        }

        /// <summary>
        /// Verify the 'Deny' button exists
        /// </summary>
        /// <returns>True if the button exists, false otherwise</returns>
        public bool DenyButtonExists()
        {
            waitForIt(_driver, DenyButton);
            return DenyButton.Displayed;
        }

        public void ClickApprove()
        {
            waitForIt(_driver, ApproveButton);
            ApproveButton.WaitAndClick(_driver);
        }

        public void ClickDeny()
        {
            DenyButton.WaitAndClick(_driver);
        }

        public void ClickEmploymentApplicationTab()
        {
            EmploymentApplicationTab.WaitRetry(_driver).Click();
        }

        #endregion

    }
}