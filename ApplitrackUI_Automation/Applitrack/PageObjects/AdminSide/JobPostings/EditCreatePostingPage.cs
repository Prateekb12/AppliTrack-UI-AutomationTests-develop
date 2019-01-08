using System;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ApplitrackUITests.PageObjects.AdminSide.JobPostings
{
    public class EditCreatePostingPage : BasePageObject, IApplitrackPage
    {
        private IWebDriver _driver;

        #region Constructor

        public EditCreatePostingPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        [FindsBy(How = How.CssSelector, Using = "span#Title")]
        private IWebElement HeaderText { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input#Title")]
        private IWebElement TitleField { get; set; }

        // The 'Main' tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Main']")]
        private IWebElement MainTabLink { get; set; }

        // The 'Description' tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Description']")]
        private IWebElement DescriptionTabLink { get; set; }

        // The 'Assigned Application Pages' tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Assigned Application Pages']")]
        private IWebElement AssignedApplicationPagesTabLink { get; set; }

        // The 'Per Posting Questions' tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Per Posting Questions']")]
        private IWebElement PerPostingQuestionsTabLink { get; set; }

        // The 'Posting Tools' tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Posting Tools']")]
        private IWebElement PostingToolsTabLink { get; set; }

        // The 'Forms' tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Forms']")]
        private IWebElement FormsTabLink { get; set; }

        // The 'Advertise' tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Advertise']")]
        private IWebElement AdvertiseTabLink { get; set; }

        // The 'Save' button
        [FindsBy(How = How.Id, Using = "Save")] 
        private IWebElement SaveButton { get; set; }

        // The 'Save and Next' button
        [FindsBy(How = How.Id, Using = "SaveAndNext")] 
        private IWebElement SaveAndNextButton { get; set; }

        // The 'Preview' button
        [FindsBy(How = How.Id, Using = "ViewLiveButton")] 
        private IWebElement PreviewButton { get; set; }

        // The ID of the Job Posting
        [FindsBy(How = How.Id, Using = "CurrentID")]
        private IWebElement JobID { get; set; }

        #endregion

        #region Related Pages

        private EditCreatePostingMainTab _mainTab;
        /// <summary>
        /// The 'Main' tab page
        /// </summary>
        public EditCreatePostingMainTab MainTab
        {
            get { return _mainTab ?? (_mainTab = new EditCreatePostingMainTab(_driver)); }
        }

        private EditCreatePostingDescriptionTab _descriptionTab;
        /// <summary>
        /// The 'Description' tab page
        /// </summary>
        public EditCreatePostingDescriptionTab DescriptionTab
        {
            get { return _descriptionTab ?? (_descriptionTab = new EditCreatePostingDescriptionTab(_driver)); }
        }
        
        private EditCreatePostingAssignedApplicationsTab _assignedApplicaiontsTab;
        /// <summary>
        /// The 'Assigned Applications' tab page
        /// </summary>
        public EditCreatePostingAssignedApplicationsTab AssignedApplicationsTab
        {
            get { return _assignedApplicaiontsTab ?? (_assignedApplicaiontsTab = new EditCreatePostingAssignedApplicationsTab(_driver)); }
        }

        private EditCreatePostingPerPostingQuestionsTab _perPostingQuestionsTab;
        /// <summary>
        /// The 'Per Posting Questions' tab page
        /// </summary>
        public EditCreatePostingPerPostingQuestionsTab PerPostingQuestionsTab
        {
            get
            {
                return _perPostingQuestionsTab ??
                       (_perPostingQuestionsTab = new EditCreatePostingPerPostingQuestionsTab(_driver));
            }
        }

        private EditCreatePostingPostingToolsTab _postingToolsTab;
        /// <summary>
        /// The 'Posting Tools' tab page
        /// </summary>
        public EditCreatePostingPostingToolsTab PostingToolsTab
        {
            get { return _postingToolsTab ?? (_postingToolsTab = new EditCreatePostingPostingToolsTab(_driver)); }
        }

        private EditCreatePostingFormsTab _formsTab;
        /// <summary>
        /// The 'Forms' tab page
        /// </summary>
        public EditCreatePostingFormsTab FormsTab
        {
            get { return _formsTab ?? (_formsTab = new EditCreatePostingFormsTab(_driver)); }
        }

        private EditCreatePostingsAdvertiseTab _advertiseTab;
        /// <summary>
        /// The 'Advertise' tab page
        /// </summary>
        public EditCreatePostingsAdvertiseTab AdvertiseTab
        {
            get { return _advertiseTab ?? (_advertiseTab = new EditCreatePostingsAdvertiseTab(_driver)); }
        }

        #endregion

        #region Page Actions

        public bool IsDisplayed()
        {
            return HeaderText.WaitRetry(_driver).Text.Contains("Job Posting");
        }

        /// <summary>
        /// Check to see of the Title field contains a given value
        /// </summary>
        /// <param name="jobTitle">The value to check for</param>
        /// <returns>True if the field contains the value, false otherwise</returns>
        public bool TitleFieldContains(string jobTitle)
        {
            return TitleField.GetAttribute("value").Contains(jobTitle);
        }

        /// <summary>
        /// Click the "Main" tab on the Edit/Create Job Posting page
        /// </summary>
        public void ClickMainTab ()
        {
            MainTabLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click on the "Description" tab on the Edit/Create Job Posting page
        /// </summary>
        public void ClickDescriptionTab()
        {
            DescriptionTabLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click on the "Assigned Application Pages" tab on the Edit/Create Job Posting page
        /// </summary>
        public void ClickAssignedApplicationPagesTab()
        {
            AssignedApplicationPagesTabLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the "Per Posting Questions" tab on the Edit/Create Job Posting page
        /// </summary>
        public void ClickPerPostingQuestionsTab()
        {
            PerPostingQuestionsTabLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the "Posting Tools" tab on the Edit/Create Job Posting page
        /// </summary>
        public void ClickPostingToolsTab()
        {
            PostingToolsTabLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the "Forms" tab on the Edit/Create Job Posting page
        /// </summary>
        public void ClickFormsTab()
        {
            FormsTabLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the "Advertise" tab on the Edit/Create Job Posting page
        /// </summary>
        public void ClickAdvertiseTab()
        {
            AdvertiseTabLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the "Save" button on the Edit/Create Job Posting page
        /// </summary>
        public void ClickSaveButton()
        {
            SaveButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the "Save and Next" button on the Edit/Create Job Posting page
        /// </summary>
        public void ClickSaveAndNextButton()
        {
            SaveAndNextButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the "Preview" button on the Edit/Create Job Posting page
        /// </summary>
        public void ClickPreviewButton()
        {
            PreviewButton.WaitAndClick(_driver); 
        }

        public int JobId { get; set; }
        /// <summary>
        /// Get the ID of a Job Posting. The Job ID is only created after the posting has been saved. If it has not been saved, the ID will be "0"
        /// </summary>
        /// <returns>The ID of the Job Posting</returns>
        public void GetJobId()
        {
            // wait until the div containing the Job ID is populated
            WebDriverWait wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 5));
            wait.Until(d => !d.FindElement(By.Id("CurrentID")).GetAttribute("value").Equals("0"));

            JobId = int.Parse(JobID.GetAttribute("value"));
        }

        /// <summary>
        /// Get the text of the Preview button.
        /// </summary>
        /// <returns>The text of the Preview button</returns>
        public string GetPreviewButtonText()
        {
            // wait until the button text changes after saving
            WebDriverWait wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 5));
            wait.Until(d => d.FindElement(By.Id("ViewLiveButton")).GetAttribute("value").Contains("ID"));

            return PreviewButton.GetAttribute("value");
        }

        /// <summary>
        /// Check to see of the job is saved by checking to see if it has an ID
        /// </summary>
        /// <returns>True if an ID was created, false otherwise</returns>
        public bool JobIsSaved()
        {
            return !JobId.Equals("0");
        }

        #endregion



    }
}
