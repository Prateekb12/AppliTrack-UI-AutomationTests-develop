using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ApplitrackUITests.PageObjects.AdminSide.JobPostings
{
    public class EditCreatePostingMainTab : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public EditCreatePostingMainTab(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

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

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Enter a value in the "Title" field of the "Main" tab
        /// </summary>
        /// <param name="jobTitle">The title to be entered</param>
        public void EnterJobTitle(string jobTitle)
        {
            JobPostingTitleField.Clear();
            JobPostingTitleField.SendKeys(jobTitle);
        }

        /// <summary>
        /// Select a value from the "Position Type" drop down in the "Main" tab 
        /// </summary>
        /// <param name="positionType">The position type to be selected</param>
        public void SelectPositionType(string positionType)
        {
            SelectElement positionTypeDropDown = new SelectElement(PositionTypeDropDown);
            positionTypeDropDown.SelectByText(positionType);
        }

        /// <summary>
        /// Get the selected value from the Position Type drop-down
        /// </summary>
        /// <returns>The selected value from the Position Type drop-down as a string</returns>
        public string GetSelectedPositionType()
        {
            SelectElement positionTypeDropDown = new SelectElement(PositionTypeDropDown);
            return(positionTypeDropDown.SelectedOption.Text);
        }

        /// <summary>
        /// Select a value from the "Location" drop down in the "Main" tab
        /// </summary>
        /// <param name="location">The location to be selected</param>
        public void SelectLocation(string location)
        {
            SelectElement locationDropDown = new SelectElement(LocationDropDown);
            locationDropDown.SelectByText(location);
        }

        /// <summary>
        /// Get the selected value from the Location drop-down
        /// </summary>
        /// <returns>The selected value from the Location drop-down as a string</returns>
        public string GetSelectedLocation()
        {
            SelectElement locationDropDown = new SelectElement(LocationDropDown);
            return (locationDropDown.SelectedOption.Text);
        }

        /// <summary>
        /// Select the "Yes" radio button of the "Display Info" pane on the "Main" tab
        /// </summary>
        public void SelectDisplayInfoYesRadioButton()
        {
            DisplayInfoYesRadioButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Select the "No" radio button of the "Display Info" pane on the "Main" tab
        /// </summary>
        public void SelectDisplayInfoNoRadioButton()
        {
            DisplayInfoNoRadioButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Select the "Based on Schedule" radio button of the "Display Info" pane on the "Main" tab
        /// </summary>
        public void SelectBasedOnScheduleDisplayInfoRadioButton()
        {
            DisplayInfoBasedOnScheduleRadioButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Enter a date in the "Date Posted" field of the "Display Info" pane on the "Main" tab
        /// </summary>
        /// <param name="date">The date to be entered</param>
        public void EnterDatePosted(string date)
        {
            SelectDisplayInfoYesRadioButton();
            DisplayInfoDatePostedField.Clear();
            DisplayInfoDatePostedField.SendKeys(date);
            DisplayInfoDatePostedField.SendKeys(Keys.Return);
        }

        /// <summary>
        /// Enter a date into the 'Open from' field
        /// </summary>
        /// <param name="date">The date to be entered</param>
        public void EnterOpenFromDate(string date)
        {
            OpenFromDateField.SendKeys(date);
        }

        /// <summary>
        /// Enter a date into the 'thru' field
        /// </summary>
        /// <param name="date">The date to be entered</param>
        public void EnterThruDate(string date)
        {
            DisplayInfoThruField.SendKeys(date);
        }

        /// <summary>
        /// Select the 'Depends on Internal/External' radio button in the 'Display Info' pane
        /// </summary>
        public void SelectDependsOnInternalExternalRadioButton()
        {
            DependsOnInternalExternalRadioButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Select the 'No' radio button for Internal applicants
        /// </summary>
        public void SelectInternalNoRadioButton()
        {
            InternalNoRadioButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Select the 'No' radio button for External applicants
        /// </summary>
        public void SelectExternalNoRadioButton()
        {
            ExternalNoRadioButton.WaitAndClick(_driver);
        }

       /// <summary>
        /// Checks to see if a valid date was entered in the 'Date Posted' field
        /// </summary>
        /// <returns>Returns true if the date is valid, false otherwise</returns>
        public bool IsValidDatePosted()
        {
            return !ValidDateRequiredMessage.Displayed;
        }

        /// <summary>
        /// Verify that the 'Open from' and 'thru' fields appear after selecting the 'Based on Schedule' radio button
        /// </summary>
        /// <returns>True if the fields appear, false otherwise</returns>
        public bool BasedOnScheduleDisplayInfoFieldsVisible()
        {
            return OpenFromDateField.Displayed && DisplayInfoThruField.Displayed;
        }

        #endregion

    }
}
