using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using By = OpenQA.Selenium.Extensions.By;
using OpenQA.Selenium.Support.UI;
using System.Threading;


namespace Automation.Pages
{
    public class JobPostingsPages : BasePageObject
    {
        //For This Page
        private IWebDriver Driver;

        #region Page Objects

        /*
         * Create New Posting Create From links
         */
        // The "A blank form" link
        [FindsBy(How = How.LinkText, Using = "A blank form")] 
        private IWebElement FromBlankForm { get; set; }

        // The "A template" link
        [FindsBy(How = How.LinkText, Using = "A template")] 
        private IWebElement FromTemplate { get; set; }

        // The "An existing open posting" link
        [FindsBy(How = How.LinkText, Using = "An existing open posting")] 
        private IWebElement FromExistingOpenPosting { get; set; }

        // The "An existing posting (open or closed)" link
        [FindsBy(How = How.LinkText, Using = "An existing posting (open or closed)")] 
        private IWebElement FromExistingPostingOpenOrClosed { get; set; }

        /*
         * Common elements on the Edit/Create Job Posting page
         */
        // The Save button
        [FindsBy(How = How.Id, Using = "Save")] 
        private IWebElement SaveButton { get; set; }

        // The Save and Next button
        [FindsBy(How = How.Id, Using = "SaveAndNext")] 
        private IWebElement SaveAndNextButton { get; set; }

        // The Preview button
        [FindsBy(How = How.Id, Using = "ViewLiveButton")] 
        private IWebElement PreviewButton { get; set; }

        // The ID of the Job Posting
        [FindsBy(How = How.Id, Using = "CurrentID")]
        private IWebElement JobID { get; set; }

        /*
         * Elements on the Main tab
         */
        // The Main tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Main']")]
        private IWebElement MainTab { get; set; }

        // Title field in the 'Title' pane
        [FindsBy(How = How.Id, Using = "Title")] 
        private IWebElement JobPostingTitleField { get; set; }

        // 'Position Type' dropdown
        [FindsBy(How = How.Id, Using = "ListAsType")]
        private IWebElement PositionTypeDropDown { get; set; }

        // 'Location' dropdown
        [FindsBy(How = How.Id, Using = "Location")]
        private IWebElement LocationDropDown { get; set; }
 
        // Yes radio button in the 'Display Info' pane
        [FindsBy(How = How.Id, Using = "ReqOpenRangeAggregate_RadioButtonOpenYes")]
        private IWebElement DisplayInfoYesRadioButton { get; set; }

        // No radio button in the 'Display Info' pane
        [FindsBy(How = How.Id, Using = "ReqOpenRangeAggregate_RadioButtonOpenNo")]
        private IWebElement DisplayInfoNoRadioButton { get; set; }

        // Based on Schedule radio button in the 'Display Info' pane
        [FindsBy(How = How.Id, Using = "ReqOpenRangeAggregate_RadioButtonOpenRange")]
        private IWebElement DisplayInfoBasedOnScheduleRadioButton { get; set; }

        // 'Open from' field in the 'Display Info' pane when 'Based on Schedule' is selected
        [FindsBy(How = How.Id, Using = "ReqOpenRangeAggregate_ListViewOpenRanges_ctrl0_TextBoxRangeStart")]
        private IWebElement DisplayInfoOpenFromField { get; set; }

        // 'thru' field in the 'Display Info' pane when 'Based on Schedule' is selected
        [FindsBy(How = How.Id, Using = "ReqOpenRangeAggregate_ListViewOpenRanges_ctrl0_TextBoxRangeEnd")]
        private IWebElement DisplayInfoThruField { get; set; }

        // The 'Date Posted' field in the 'Display Info' pane when 'Yes' is selected
        [FindsBy(How = How.Id, Using = "DatePosted")]
        private IWebElement DisplayInfoDatePostedField { get; set; }

        // Error message that appears when entering an invalid date in the 'Date Posted' field
        [FindsBy(How = How.Id, Using = "ctl13")]
        private IWebElement ValidDateRequiredMessage { get; set; }

        // Date field in the 'Display Info' pane when 'Based on Schedule' is selected
        [FindsBy(How = How.Id, Using = "ReqOpenRangeAggregate_ListViewOpenRanges_ctrl0_TextBoxRangeStart")]
        private IWebElement OpenFromDateField { get; set; }

        // 'Depends on Internal/External' radio button in the 'AppliTrack Status' pane
        [FindsBy(How = How.Id, Using = "ReqOpenRangeAggregate_RadioButtonOpenSplit")]
        private IWebElement DependsOnInternalExternalRadioButton { get; set; }

        [FindsBy(How = How.Id, Using = "ReqOpenRangeAggregate_ReqOpenRangeInternal_RadioButtonOpenNo")]
        private IWebElement InternalNoRadioButton { get; set; }

        [FindsBy(How = How.Id, Using = "ReqOpenRangeAggregate_ReqOpenRangeExternal_RadioButtonOpenNo")]
        private IWebElement ExternalNoRadioButton { get; set; }

        // 'Based on Schedule' radio button in the 'AppliTrack Status' pane
        [FindsBy(How = How.Id, Using = "IsOnAdmin_2")]
        private IWebElement BasedOnScheduleStatusRadioButton { get; set; }

        // 'Automatically activate' date field in the 'Applitrack Status' pane when 'Based on Schedule' is selected
        [FindsBy(How = How.Id, Using = "AutoIsOnAdminFrom")]
        private IWebElement AutoActivateDateField { get; set; }

        // 'Automatically deactivate' date field in the 'AppliTrack Status' pane when 'Based on Schedule' is selected
        [FindsBy(How = How.Id, Using = "AutoIsOnAdminTo")]
        private IWebElement AutoDeactivateDateField { get; set; }

        // The 'Note' field in the 'Office Use Only' pane
        [FindsBy(How = How.Id, Using = "Note")]
        private IWebElement JobPostingNoteField { get; set; }

        /*
         * Elements on the Description tab
         */
        // The Description tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Description']")]
        private IWebElement DescriptionTab { get; set; }

        /*
         * Elements on the Assigned Application Pages tab
         */
        // The Assigned Application Pages tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Assigned Application Pages']")]
        private IWebElement AssignedApplicationPagesTab { get; set; }

        /*
         * Elements on the Per Posting Questions tab
         */
        // The Per Posting Questions tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Per Posting Questions']")]
        private IWebElement PerPostingQuestionsTab { get; set; }

        /*
         * Elements on the Posting Tools tab
         */
        // The Posting Tools tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Posting Tools']")]
        private IWebElement PostingToolsTab { get; set; }

        /*
         * Elements on the Forms tab
         */
        // The Forms tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Forms']")]
        private IWebElement FormsTab { get; set; }

        /*
         * Elements on the Advertise tab
         */
        // The Advertise tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Advertise']")]
        private IWebElement AdvertiseTab { get; set; }

        /*
         * Elements on the List All Active Postings page
         */
        // The checkbox in the list
        [FindsBy(How = How.CssSelector, Using = "input[name='ID']")]
        private IList<IWebElement> ListingCheckBox { get; set; }

        // The 'Inactivate Postings' link that appears in the menu after marking a checkbox
        [FindsBy(How = How.LinkText, Using = "Inactivate Postings")]
        private IWebElement InactivatePostingsLink { get; set; }

        // the yes button on the "Inactivate Form(s)" window
        [FindsBy(How = How.Name, Using = "resp")]
        private IWebElement YesButton { get; set; }

        /*
         * Elements on the List Existing Posting Templates page
         */
        // TODO put this in a helper class...
        [FindsBy(How = How.Name, Using = "DeleteRecords")]
        private IWebElement DeleteRecordsButton { get; set; }

        /// <summary>
        /// Initialize all the elements of the Job Posting pages for use in test cases
        /// </summary>
        /// <param name="Driver">The Selenium web driver being used</param>
        public JobPostingsPages(IWebDriver Driver)
        {
            this.Driver = Driver;
            PageFactory.InitElements(Driver, this);
        }

        #endregion

        #region Start From page actions

        /// <summary>
        /// Click the "A blank form" link in the "Start From" page
        /// </summary>
        public void ClickFromBlankForm()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                FromBlankForm.Click();
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Click the "A template" link in the "Start From" page
        /// </summary>
        public void ClickFromTemplate()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                FromTemplate.Click();
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Click the "An existing open posting" link in the "Start From" page
        /// </summary>
        public void ClickFromExistingOpenPosting()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                FromExistingOpenPosting.Click();
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Click the "An existing posting" link in the "Start From" page
        /// </summary>
        public void ClickFromExistingPostingOpenOrClosed()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                FromExistingPostingOpenOrClosed.Click();
            }
            catch (Exception e) { throw e; }
        }

        #endregion

        #region Common Edit/Create Job Posting page actions

        /// <summary>
        /// Click the "Save" button on the Edit/Create Job Posting page
        /// </summary>
        public void ClickSaveButton()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                SaveButton.Click();
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Click the "Save and Next" button on the Edit/Create Job Posting page
        /// </summary>
        public void ClickSaveAndNextButton()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                SaveAndNextButton.Click();
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Click the "Preview" button on the Edit/Create Job Posting page
        /// </summary>
        public void ClickPreviewButton()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                PreviewButton.Click(); 
            }
            catch (Exception e) { throw e; }
        }

        public string JobId { get; set; }
        /// <summary>
        /// Get the ID of a Job Posting. The Job ID is only created after the posting has been saved. If it has not been saved, the ID will be "0"
        /// </summary>
        /// <returns>The ID of the Job Posting</returns>
        public void GetJobId()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);

                // wait until the div containing the Job ID is populated
                WebDriverWait wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
                wait.Until(d => !d.FindElement(By.Id("CurrentID")).GetAttribute("value").Equals("0"));

                JobId = JobID.GetAttribute("value");
            }
            catch (Exception e) { throw e; }
        }

        public bool JobIsSaved()
        {
            return !JobId.Equals("0");
        }

        /// <summary>
        /// Get the text of the Preview button.
        /// </summary>
        /// <returns>The text of the Preview button</returns>
        public string GetPreviewButtonText()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);

                // wait until the button text changes after saving
                WebDriverWait wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
                bool txt = wait.Until(d => d.FindElement(By.Id("ViewLiveButton")).GetAttribute("value").Contains("ID"));

                return PreviewButton.GetAttribute("value");
            }
            catch (Exception e) { throw e; }
        }

        #endregion

        #region Main Tab Functions

        /// <summary>
        /// Click the "Main" tab on the Edit/Create Job Posting page
        /// </summary>
        public void ClickMainTab ()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                MainTab.Click();
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Enter a value in the "Title" field of the "Main" tab
        /// </summary>
        /// <param name="jobTitle">The title to be entered</param>
        public void EnterJobTitle(string jobTitle)
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                JobPostingTitleField.SendKeys(jobTitle);
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Select a value from the "Position Type" drop down in the "Main" tab 
        /// </summary>
        /// <param name="positionType">The position type to be selected</param>
        public void SelectPositionType(string positionType)
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                SelectElement positionTypeDropDown = new SelectElement(PositionTypeDropDown);
                positionTypeDropDown.SelectByText(positionType);
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Get the selected value from the Position Type drop-down
        /// </summary>
        /// <returns>The selected value from the Position Type drop-down as a string</returns>
        public string GetSelectedPositionType()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                SelectElement positionTypeDropDown = new SelectElement(PositionTypeDropDown);
                return(positionTypeDropDown.SelectedOption.Text);
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Select a value from the "Location" drop down in the "Main" tab
        /// </summary>
        /// <param name="location">The location to be selected</param>
        public void SelectLocation(string location)
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                SelectElement locationDropDown = new SelectElement(LocationDropDown);
                locationDropDown.SelectByText(location);
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Get the selected value from the Location drop-down
        /// </summary>
        /// <returns>The selected value from the Location drop-down as a string</returns>
        public string GetSelectedLocation()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                SelectElement locationDropDown = new SelectElement(LocationDropDown);
                return (locationDropDown.SelectedOption.Text);
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Select the "Yes" radio button of the "Display Info" pane on the "Main" tab
        /// </summary>
        public void SelectDisplayInfoYesRadioButton()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                DisplayInfoYesRadioButton.Click();
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Select the "No" radio button of the "Display Info" pane on the "Main" tab
        /// </summary>
        public void SelectDisplayInfoNoRadioButton()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                DisplayInfoNoRadioButton.Click();
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Select the "Based on Schedule" radio button of the "Display Info" pane on the "Main" tab
        /// </summary>
        public void SelectBasedOnScheduleDisplayInfoRadioButton()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                DisplayInfoBasedOnScheduleRadioButton.Click();
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Enter a date in the "Date Posted" field of the "Display Info" pane on the "Main" tab
        /// </summary>
        /// <param name="date">The date to be entered</param>
        public void EnterDatePosted(string date)
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                SelectDisplayInfoYesRadioButton();
                DisplayInfoDatePostedField.Clear();
                DisplayInfoDatePostedField.SendKeys(date);
                DisplayInfoDatePostedField.SendKeys(Keys.Return);
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Verify that the 'Open from' and 'thru' fields appear after selecting the 'Based on Schedule' radio button
        /// </summary>
        /// <returns>True if the fields appear, false otherwise</returns>
        public bool BasedOnScheduleDisplayInfoFieldsVisible()
        {
            try
            {
                return (IsElementVisible(OpenFromDateField) && IsElementVisible(DisplayInfoThruField));
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Enter a date into the 'Open from' field
        /// </summary>
        /// <param name="date">The date to be entered</param>
        public void EnterOpenFromDate(string date)
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                OpenFromDateField.SendKeys(date);
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Enter a date into the 'thru' field
        /// </summary>
        /// <param name="date">The date to be entered</param>
        public void EnterThruDate(string date)
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                DisplayInfoThruField.SendKeys(date);
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Checks to see if a valid date was entered in the 'Date Posted' field
        /// </summary>
        /// <returns>Returns true if the date is valid, false otherwise</returns>
        public bool IsValidDatePosted()
        {
            try
            {
                return !(IsElementVisible(ValidDateRequiredMessage));
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Select the 'Based on Schedule' radio button in the 'Display Info' pane 
        /// </summary>
        public void SelectBasedOnScheduleStatusRadioButton()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                BasedOnScheduleStatusRadioButton.Click();
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Verify that the 'Automatically activate...' and ' Automatically deactivate...' fields appear after selecing the 'Based on Schedule'
        /// radio button in the 'AppliTrack Status' pane
        /// </summary>
        /// <returns>True if the fields appear, false otherwise</returns>
        public bool BasedOnScheduleStatusFieldsVisible()
        {
            try
            {
                return (IsElementVisible(AutoActivateDateField) && IsElementVisible(AutoDeactivateDateField));
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Select the 'Depends on Internal/External' radio button in the 'Display Info' pane
        /// </summary>
        public void SelectDependsOnInternalExternalRadioButton()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                DependsOnInternalExternalRadioButton.Click();
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Select the 'No' radio button for Internal applicants
        /// </summary>
        public void SelectInternalNoRadioButton()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                InternalNoRadioButton.Click();
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Select the 'No' radio button for External applicants
        /// </summary>
        public void SelectExternalNoRadioButton()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                ExternalNoRadioButton.Click();
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Enter some text in the "Note" field of the "Office Use Only" pane in the "Main" tab
        /// </summary>
        /// <param name="note">The note text to be entered</param>
        public void EnterNote(string note)
        {
            try
            {
                JobPostingNoteField.SendKeys(note);
                BaseWaitForPageToLoad(Driver, 100);
            }
            catch (Exception e) { throw e; }

        }

        #endregion

        #region Description tab functions

        /// <summary>
        /// Click on the "Description" tab on the Edit/Create Job Posting page
        /// </summary>
        public void ClickDescriptionTab()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                DescriptionTab.Click();
            }
            catch (Exception e) { throw e; }
        }

        #endregion

        #region Assigned Application Pages tab functions

        /// <summary>
        /// Click on the "Assigned Application Pages" tab on the Edit/Create Job Posting page
        /// </summary>
        public void ClickAssignedApplicationPagesTab()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                AssignedApplicationPagesTab.Click();
            }
            catch (Exception e) { throw e; }
        }

        #endregion

        #region Per Posting Questions tab functions

        /// <summary>
        /// Click the "Per Posting Questions" tab on the Edit/Create Job Posting page
        /// </summary>
        public void ClickPerPostingQuestionsTab()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                PerPostingQuestionsTab.Click();
            }
            catch (Exception e) { throw e; }
        }

        #endregion

        #region Posting Tools tab functions

        /// <summary>
        /// Click the "Posting Tools" tab on the Edit/Create Job Posting page
        /// </summary>
        public void ClickPostingToolsTab()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                PostingToolsTab.Click();
            }
            catch (Exception e) { throw e; }
        }

        #endregion

        #region Forms tab functions

        /// <summary>
        /// Click the "Forms" tab on the Edit/Create Job Posting page
        /// </summary>
        public void ClickFormsTab()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                FormsTab.Click();
            }
            catch (Exception e) { throw e; }
        }

        #endregion

        #region Advertise tab functions

        /// <summary>
        /// Click the "Advertise" tab on the Edit/Create Job Posting page
        /// </summary>
        public void ClickAdvertiseTab()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                AdvertiseTab.Click();
            }
            catch (Exception e) { throw e; }
        }

        #endregion

        #region List All Active Postings page functions

        /// <summary>
        /// Mark the checkbox associated to a Job ID.
        /// </summary>
        /// <param name="jobId">The Job ID to be found and marked.</param>
        public void MarkListingCheckbox(string jobId)
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);

                int ndx = -1; // the index of the checkbox containing the ID

                while (ndx == -1)
                {
                    for (int i = 0; i < ListingCheckBox.Count(); i++)
                    {
                        if (ListingCheckBox[i].GetAttribute("value") == jobId)
                        {
                            ndx = i;
                            break;
                        }
                    }

                    // Mark the checkbox if the Job ID is found
                    if (ndx > -1)
                    {
                        //((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", ListingCheckBox[ndx]);
                        ScrollToElement(ListingCheckBox[ndx]);
                        ListingCheckBox[ndx].Click();
                    }
                    // If its not found, go to the next page
                    else
                    {
                        ((IJavaScriptExecutor)Driver).ExecuteScript("page('Down');");
                    }
                }
            }
            catch (Exception e) { throw e; }
        }

        private void ScrollToElement(IWebElement element)
        {
            try
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Click the 'Inactivate Postings' menu link when a record is marked in the 'List All Active Job Postings' page
        /// </summary>
        public void ClickInactivatePostings()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 100);
                InactivatePostingsLink.Click();
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Click the Yes button to inactivate a form
        /// </summary>
        public void ConfirmInactivation()
        {
            try
            {
                BaseWaitForPageToLoad(Driver, 10);
                string parentHandle = Driver.CurrentWindowHandle; // the main window
                string popupHandle = string.Empty; // the popup window
                IReadOnlyCollection<string> windowHandles = Driver.WindowHandles; // contains all currently open windows

                // find the popup window
                foreach (string handle in windowHandles)
                {
                    if (handle != parentHandle)
                    {
                        popupHandle = handle;
                        break;
                    }
                }

                // switch to the popup window, click <Yes>, and switch back to the main window
                Driver.SwitchTo().Window(popupHandle);
                YesButton.Click();
                Driver.SwitchTo().Window(parentHandle);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        public void ClickDeleteTemplate()
        {
            DeleteRecordsButton.Click();
            BaseWaitForPageToLoad(Driver, 100);
        }

        public bool TemplateExists()
        {
            throw new NotImplementedException();
        }
    }
}
