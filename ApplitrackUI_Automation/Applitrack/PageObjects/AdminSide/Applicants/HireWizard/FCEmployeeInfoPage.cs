using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using IDMPageObjects.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Applicants.HireWizard
{
    public class FCEmployeeInfoPage : BasePageObject, IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructor

        public FCEmployeeInfoPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region Related Pages
        #endregion

        #region UI Selector

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCName")]
        private IWebElement ApplicantNameText { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCFirstName")]
        private IWebElement FirstNameTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCMiddleName")]
        private IWebElement MiddleNameTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCLastName")]
        private IWebElement LastNameTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCBirthDate")]
        private IWebElement DateOfBirthTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCExternalID")]
        private IWebElement ExternalIdTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCEmployeeSSN")]
        private IWebElement SocialSecurityNumberTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCStreet1")]
        private IWebElement Street1TextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCStreet2")]
        private IWebElement Street2TextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCCity")]
        private IWebElement CityTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCState")]
        private IWebElement StateTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCZip")]
        private IWebElement ZipTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCPersonalEmail")]
        private IWebElement PersonalEmailTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCWorkEmail")]
        private IWebElement WorkEmailTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCPersonalPhoneNbr")]
        private IWebElement PrimaryPhoneNumberTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCPrimaryPhoneType")]
        private IWebElement PrimaryPhoneTypeDropDown { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_ctl03")]
        private IWebElement InvalidSSNMessage { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_ctl08")]
        private IWebElement InvalidPhoneMessage { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCToggleEmployeeAssignment")]
        private IWebElement CreateEmployeeAssignmentCheckBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "select.SupervisorSelect")]
        private IWebElement SupervisorDropDown { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCAssignmentDate")]
        private IWebElement AssignmentDateTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCJobType")]
        private IWebElement JobTypeDropDown { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCPositionName")]
        private IWebElement PositionNameDropDown { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCLocation")]
        private IWebElement LocationDropDown { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCDepartment")]
        private IWebElement DepartmentDropDown { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCPaySchedule")]
        private IWebElement PayScheduleDropDown { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCPayLane")]
        private IWebElement PayLaneDropDown { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#HiredWizard_FCTransfer_ctrl0_FCPayStep")]
        private IWebElement PayStepDropDown { get; set; }

        #endregion

        #region Page Actions

        private string WaitAndGetText(IWebElement element)
        {
            _driver.WaitForIt(element);
            return element.GetAttribute("value");
        }

        public bool IsDisplayed()
        {
            _driver.WaitForIt(ApplicantNameText);
            return ApplicantNameText.Displayed;
        }

        /// <summary>
        /// Get the applicants name from the header
        /// </summary>
        public string GetApplicantName()
        {
            _driver.WaitForIt(ApplicantNameText);
            return ApplicantNameText.Text;
        }

        /// <summary>
        /// Get the text from the 'First Name' field
        /// </summary>
        public string GetFirstName()
        {
            return WaitAndGetText(FirstNameTextBox);
        }

        /// <summary>
        /// Get the text from the 'Last Name' field
        /// </summary>
        public string GetLastName()
        {
            return WaitAndGetText(LastNameTextBox);
        }

        /// <summary>
        /// Get the text from the 'Social Security Number' field
        /// </summary>
        public string GetSocialSecurityNumber()
        {
            return WaitAndGetText(SocialSecurityNumberTextBox);
        }

        /// <summary>
        /// Get the text from the 'Street 1' field
        /// </summary>
        public string GetStreet1()
        {
            return WaitAndGetText(Street1TextBox);
        }

        /// <summary>
        /// Get the text from the 'Street 2' field
        /// </summary>
        public string GetStreet2()
        {
            return WaitAndGetText(Street2TextBox);
        }

        /// <summary>
        /// Get the text from the 'City' field
        /// </summary>
        public string GetCity()
        {
            return WaitAndGetText(CityTextBox);
        }

        /// <summary>
        /// Get the text from the 'State' field
        /// </summary>
        public string GetState()
        {
            return WaitAndGetText(StateTextBox);
        }

        /// <summary>
        /// Get the text from the 'Zip' field
        /// </summary>
        public string GetZip()
        {
            return WaitAndGetText(ZipTextBox);
        }

        /// <summary>
        /// Get the text from the 'Personal Email' field
        /// </summary>
        public string GetPersonalEmail()
        {
            return WaitAndGetText(PersonalEmailTextBox);
        }

        /// <summary>
        /// Get the text from the 'Primary Phone Number' field
        /// </summary>
        public string GetPrimaryPhoneNumber()
        {
            return WaitAndGetText(PrimaryPhoneNumberTextBox);
        }

        /// <summary>
        /// Enter a value into the 'Social Security Number' field
        /// </summary>
        /// <param name="ssn">The value to enter</param>
        public void EnterSocialSecurityNumber(string ssn)
        {
            SocialSecurityNumberTextBox.WaitRetry(_driver).Clear();
            SocialSecurityNumberTextBox.SendKeys(ssn);
        }

        /// <summary>
        /// Enter a value into the 'Primary Phone Number' field
        /// </summary>
        /// <param name="phoneNumber">The value to enter</param>
        public void EnterPrimaryPhoneNumber(string phoneNumber)
        {
            PrimaryPhoneNumberTextBox.Clear();
            PrimaryPhoneNumberTextBox.SendKeys(phoneNumber);
        }

        /// <summary>
        /// Check to see if the invalid message is displayed for the 'Social Security Number' field
        /// </summary>
        /// <returns>True if the message is displayed, false otherwise</returns>
        public bool IsInvalidSSNMessageDisplayed()
        {
            _driver.WaitForIt(InvalidSSNMessage);
            return InvalidSSNMessage.Displayed &&
                   InvalidSSNMessage.Text.Contains("Invalid SSN or SSN Format (xxx-xx-xxxx)");
        }

        /// <summary>
        /// Check to see if the invalid message is displayed for the 'Primary Phone Number' field
        /// </summary>
        /// <returns>True if the message is displayed, false otherwise</returns>
        public bool IsInvalidPhoneMessageDisplayed()
        {
            _driver.WaitForIt(InvalidPhoneMessage);
            return InvalidPhoneMessage.Displayed &&
                   InvalidPhoneMessage.Text.Contains("Phone number must be 10 or fewer numerical characters");
        }

        /// <summary>
        /// Enter a value into the 'First Name' field
        /// </summary>
        /// <param name="firstName">The value to enter</param>
        public void EnterFirstName(string firstName)
        {
            FirstNameTextBox.Clear();
            FirstNameTextBox.SendKeys(firstName);
        }

        /// <summary>
        /// Enter a value into the 'Last Name' field
        /// </summary>
        /// <param name="lastName">The value to enter</param>
        public void EnterLastName(string lastName)
        {
            LastNameTextBox.Clear();
            LastNameTextBox.SendKeys(lastName);
        }

        /// <summary>
        /// Enter a value into the 'Street 1' field
        /// </summary>
        /// <param name="street">The value to enter</param>
        public void EnterStreet1(string street)
        {
            Street1TextBox.Clear();
            Street1TextBox.SendKeys(street);
        }

        /// <summary>
        /// Enter a value into the 'City' field
        /// </summary>
        /// <param name="city">The value to enter</param>
        public void EnterCity(string city)
        {
            CityTextBox.Clear();
            CityTextBox.SendKeys(city);
        }

        /// <summary>
        /// Enter a value into the 'State' field
        /// </summary>
        /// <param name="state">The value to enter</param>
        public void EnterState(string state)
        {
            StateTextBox.Clear();
            StateTextBox.SendKeys(state);
        }

        /// <summary>
        /// Enter a value into the 'Zip' field
        /// </summary>
        /// <param name="zip">The value to enter</param>
        public void EnterZip(string zip)
        {
            ZipTextBox.Clear();
            ZipTextBox.SendKeys(zip);
        }

        /// <summary>
        /// Mark the 'Would you like to create an employee assignment for this applicant?' checkbox
        /// </summary>
        public void MarkCreateEmployeeAssignment()
        {
            CreateEmployeeAssignmentCheckBox.WaitRetry(_driver).Click();
        }

        #endregion


    }
}
