using System;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ApplitrackUITests.PageObjects.AdminSide.Forms
{
    public class EditAndCreateFormPage : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public EditAndCreateFormPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        // "Permission" tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Permissions']")]
        private IWebElement FormPermissionTabLink { get; set; }

        // "Workflow" tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Workflow']")]
        private IWebElement FormWorkflowTabLink { get; set; }

        // "Form Content" tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Form Content']")]
        private IWebElement FormContentTabLink { get; set; }

        // "Appearance" tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Appearance']")]
        private IWebElement FormAppearanceTabLink { get; set; }

        [FindsBy(How = How.Id, Using = "Save")]
        private IWebElement SaveButton { get; set; }

        // The status message that appears after saving a form or switching between tabs
        [FindsBy(How = How.Id, Using = "StatusMessage")]
        private IWebElement StatusMessage { get; set; }

        // The ID of the form on the Edit/Create page
        [FindsBy(How = How.Id, Using = "CurrentID")] 
        private IWebElement FormId { get; set; }

        #endregion

        #region Related Pages

        private EditAndCreateFormPropertiesTab _propertiesTab;
        /// <summary>
        /// The 'Properties' tab page
        /// </summary>
        public EditAndCreateFormPropertiesTab PropertiesTab
        {
            get { return _propertiesTab ?? (_propertiesTab = new EditAndCreateFormPropertiesTab(_driver)); }
        }

        private EditAndCreateFormPermissionsTab _permissionsTab;
        /// <summary>
        /// The 'Permissions' tab page
        /// </summary>
        public EditAndCreateFormPermissionsTab PermissionsTab
        {
            get { return _permissionsTab ?? (_permissionsTab = new EditAndCreateFormPermissionsTab(_driver)); }
        }

        private EditAndCreateFormWorkflowTab _workflowTab;
        /// <summary>
        /// The 'Workflow' tab page
        /// </summary>
        public EditAndCreateFormWorkflowTab WorkflowTab 
        {
            get { return _workflowTab ?? (_workflowTab = new EditAndCreateFormWorkflowTab(_driver)); }
        }

        private EditAndCreateFormFormContentTab _formContentTab;
        /// <summary>
        /// The 'Form Content' tab page
        /// </summary>
        public EditAndCreateFormFormContentTab FormContentTab
        {
            get { return _formContentTab ?? (_formContentTab = new EditAndCreateFormFormContentTab(_driver)); }
        }

        private EditAndCreateFormAppearanceTab _appearanceTab;
        /// <summary>
        /// The 'Appearance' tab page
        /// </summary>
        public EditAndCreateFormAppearanceTab AppearanceTab
        {
            get { return _appearanceTab ?? (_appearanceTab = new EditAndCreateFormAppearanceTab(_driver)); }
        }

        #endregion

        #region Page Actions

        /// <summary>
        /// Click the 'Permission' tab on the 'Form' page
        /// </summary>
        public void ClickFormPermissionTab()
        {
            Console.Out.WriteLineAsync("Page: Attempting to Click Form Permission Tab");
            FormPermissionTabLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Workflow' tab on the 'Form' page
        /// </summary>
        public void ClickFormWorkflowTab()
        {
            Console.Out.WriteLineAsync("Page: Attempting to Click Form Workflow Tab");
            FormWorkflowTabLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Content' tab on the 'Form' page
        /// </summary>
        public void ClickFormContentTab()
        {
            Console.Out.WriteLineAsync("Page: Attempting to Click Form Content Tab");
            FormContentTabLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Appearance' tab on the 'Form' page
        /// </summary>
        public void ClickFormAppearanceTab()
        {
            Console.Out.WriteLineAsync("Page: Attempting to Click Form Appearance Tab");
            FormAppearanceTabLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Save' button on the form page
        /// </summary>
        public void ClickSaveButton()
        {
            Console.Out.WriteLineAsync("Page: Attempting to Click Save button");
            SaveButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Used to verify that the status message when creating/editing a form is correct
        /// </summary>
        /// <returns>The text of the status message</returns>
        public string StatusMessageText()
        {
            return StatusMessage.Text;
        }

        /// <summary>
        /// Get the ID of a recently created form. Can be used to find the ID of the form to delete using GetFormToDelete(string ID)
        /// </summary>
        /// <returns>The ID of a form</returns>
        public int GetFormId()
        {
            // wait until the form id element is populated
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => FormId.GetAttribute("value") != 0.ToString());

            return int.Parse(FormId.GetAttribute("value"));
        }


        #endregion

    }
}
