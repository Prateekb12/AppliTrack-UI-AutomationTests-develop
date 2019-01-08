using System;
using ApplitrackUITests.Helpers;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ApplitrackUITests.PageObjects.EmployeeSide
{
    public class EmployeePortalFormsTab : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public EmployeePortalFormsTab(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        [FindsBy(How = How.XPath, Using = "//font[text()='Forms']")]
        private IWebElement FormsTab { get; set; }

        // The 'New Form' button on the left
        [FindsBy(How = How.ClassName, Using = "NewFormBtn")]
        private IWebElement NewFormLink { get; set; }

        // The 'OK, Continue' button that appears when starting a form
        [FindsBy(How = How.Id, Using = "ButtonSubmitInstructions")]
        private IWebElement OkContinueButton { get; set; }

        // The 'Save as Draft' button that appears when filling out a form
        [FindsBy(How = How.Id, Using = "ButtonSaveAsDraft")]
        private IWebElement SaveAsDraftButton { get; set; }

        // The 'Submit Form' button that appears when filling out a form
        [FindsBy(How = How.Id, Using = "ButtonSubmitStep")]
        private IWebElement SubmitFormButton { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Click the "New Form" button within the "Forms" tab
        /// </summary>
        public void ClickNewForm()
        {
            NewFormLink.WaitAndClick(_driver);
        }

        // TODO add overloads to select forms by IDs, by Names, etc
        /// <summary>
        /// Click "Start Form" for the first form in the list
        /// </summary>
        public void ClickStartForm()
        {
            // TODO improve selector
            _driver.FindElement(By.CssSelector("html body form#form1 div#TopLevel_AppMain div div#DivNewForm fieldset div.MouseOverHighlight div div")).WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the "OK, Continue" button that appears after starting a form
        /// </summary>
        public void ClickOkContinue()
        {
            OkContinueButton.WaitAndClick(_driver);

            // TODO refactor finding the links out of here, maybe create an event?
            _driver.SwitchToDefaultFrame();
            _driver.SwitchToFrameById("FormsDataPage");
            FindFormKey();
            FindEditLink();
            FindDeleteLink();
        }

        /// <summary>
        /// Click the 'Save as Draft' button when filling out a form
        /// </summary>
        public void ClickSaveAsDraft()
        {
            SaveAsDraftButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Submit' button when filling out a form
        /// </summary>
        public void ClickSubmit()
        {
            SubmitFormButton.WaitAndClick(_driver);
        }

        public string FormKey { get; private set; } // use a property to avoid using a variable in the test case

        /// <summary>
        /// Find  the element containing the unique identifier for the sent form 
        /// and set the FormKey property
        /// <returns>The unique identifier of the sent form</returns>
        /// </summary>
        public void FindFormKey()
        {
            // Get the key from the IFrameFormSent tag
            // The key is between "aspx?r=" and "&end"

            var fullUrl = _driver.FindElement(By.Id("IFrameFormSent")).GetAttribute("src");

            var begin = fullUrl.IndexOf("aspx?r=", StringComparison.Ordinal) + "aspx?r=".Length;
            var end = fullUrl.IndexOf("&end", StringComparison.Ordinal);

            var key = fullUrl.Substring(begin, end - begin);

            // Cut off the last two characters because the last characters seem to change after this point
            // Thanks Applitrack....
            key = key.Substring(0, key.Length - 2);

            // Set the FormKey property
            FormKey = key;
        }

        private IWebElement EditLink { get; set; }

        private void FindEditLink()
        {
            EditLink = null;
            var elements = _driver.FindElements(By.ClassName("NavLeft_Item"));

            // there are several elements with the class of "NavLeft_Item"
            // the "Edit" links are the only ones that contain an "onclick"
            // the "onclick" attribute contains the form key
            // this iterates through each element containing "NavLeft_Item"
            // then checks to see if the form key is contained in the "onclick" attribute
            foreach (var e in elements)
            {
                if (string.IsNullOrEmpty(e.GetAttribute("onclick"))) continue;
                if (!e.GetAttribute("onclick").Contains(FormKey)) continue;
                EditLink = e;
                break;
            }
        }

        /// <summary>
        /// Click the 'Edit' button for a form in progress
        /// </summary>
        public void ClickEdit()
        {
            EditLink.WaitAndClick(_driver);
        }

        private IWebElement DeleteLink { get; set; }

        /// <summary>
        /// Click the 'Delete' button for a form in progress
        /// </summary>
        public void ClickDelete()
        {
            if (DeleteLink == null)
            {
                FindDeleteLink();
            }
            if (DeleteLink != null)
            {
                DeleteLink.WaitAndClick(_driver);
            }
        }

        private void FindDeleteLink()
        {
            var parent = EditLink.GetParentElement();
            DeleteLink = null;
            foreach (var e in parent.FindElements(OpenQA.Selenium.By.ClassName("NavLeft_Item")))
            {
                if (!e.Text.Contains("Delete")) continue;
                DeleteLink = e;
                break;
            }
        }

        /// <summary>
        /// The system generated GUID for the form
        /// </summary>
        public string FormGuid { get; private set; }

        /// <summary>
        /// Find the GUID for the form and set the FormGuid property
        /// </summary>
        public void FindFormGuid()
        {
            if (DeleteLink == null) FindDeleteLink();
            if (DeleteLink == null) return;

            var onclick = DeleteLink.GetAttribute("onclick");

            var left = onclick.IndexOf("{DeleteFormSent(\"", StringComparison.Ordinal) + "{DeleteFormSent(\"".Length;
            var right = onclick.IndexOf("\") }", StringComparison.Ordinal);

            var guid = onclick.Substring(left, right - left);

            // Set the FormHash property
            FormGuid = guid;
        }

        /// <summary>
        /// Click the 'View' button for a completed form
        /// </summary>
        public void ClickView()
        {
            var viewLink = FindViewLink();
            if (viewLink != null)
            {
                viewLink.WaitAndClick(_driver);
            }
        }

        private IWebElement FindViewLink()
        {
            IWebElement viewLink = null;

            // find the div containing all the completed forms
            var completedDiv = _driver.FindElement(By.Id("NavLeft_ContentsCompleted"));

            foreach (var e in completedDiv.FindElements(By.ClassName("NavLeft_Item")))
            {
                if (!e.Text.Contains("View") || !e.GetAttribute("id").Contains(FormGuid)) continue;
                viewLink = e;
                break;
            }
            return viewLink;
        }

        // TODO improve this to automatically detect required fields along with field types
        /// <summary>
        /// Populate the required fields for a form
        /// </summary>
        public void EnterRequiredFields()
        {
            EnterEmployeeName();
            EnterDateLeaveRequested();
            EnterTimeOfDayLeaveRequested();
        }

        private void EnterEmployeeName()
        {
            var employeeName = _driver.FindElement(By.Id("ctl05__TraitID3193_TraitID3193"));
            employeeName.SendKeys("Employee Name");
        }

        private void EnterDateLeaveRequested()
        {
            var dateLeaveRequested = _driver.FindElement(By.Id("ctl05__TraitID3194_TraitID3194"));
            dateLeaveRequested.SendKeys("12/31/2016");
        }

        private void EnterTimeOfDayLeaveRequested()
        {
            var timeOfDayDropDown = _driver.FindElement(By.Id("ctl05__TraitID3195_TraitID3195"));
            SelectElement selector = new SelectElement(timeOfDayDropDown);
            selector.SelectByIndex(1);
        }

        /// <summary>
        /// Check to see if the new form page is loaded after clicking the 'Start Form' button
        /// </summary>
        /// <returns>True if the page is loaded, false otherwise</returns>
        public bool StartFormPageIsLoaded()
        {
            try
            {
                waitForIt(_driver, OkContinueButton);
                return OkContinueButton.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Check to see if the submitted screen is displayed
        /// </summary>
        /// <returns>True if it the screen is displayed, false otherwise</returns>
        public bool SubmittedSceenDisplayed()
        {
            // This is the div that should appear after a form is submitted
            return _driver.FindElement(By.Id("DivCompleteText")).Displayed;
        }

        /// <summary>
        /// Check to see if the 'View Form' page is displayed after clicking 'View' for a completed form
        /// </summary>
        /// <returns>True if the page is displayed, false otherwise</returns>
        public bool ViewFormPageDisplayed()
        {
            // The 'IFrameFormSent' IFrame contains the FormGuid within the "src" tag
            return _driver.FindElement(By.Id("IFrameFormSent")).GetAttribute("src").Contains(FormGuid);
        }

        /// <summary>
        /// Check to see if the form is in the In Progress list
        /// </summary>
        /// <returns>True if the form is in the list, false otherwise</returns>
        public bool FormExistsInList()
        {
            FindEditLink();
            return EditLink != null;
        }

        #endregion

    }
}
