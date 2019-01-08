using ApplitrackUITests.Helpers;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ApplitrackUITests.PageObjects.ApplicantSide.EmploymentApplication
{
    public class PersonalInfoPage : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public PersonalInfoPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion


        #region Related Pages
        #endregion


        #region UI Selectors
        // The 'First' name field on the 'Personal Info' page
        [FindsBy(How = How.Id, Using = "TextBoxAppFirstName")]
        private IWebElement FirstNameTextBox { get; set; }

        //  The 'Last' name field on the 'Personal Info' page
        [FindsBy(How = How.Id, Using = "TextBoxAppLastName")]
        private IWebElement LastNameTextBox { get; set; }

        // The 'Email Address' field on the 'Personal Info' page
        [FindsBy(How = How.Id, Using = "TextBoxAppEmailAddress")]
        private IWebElement EmailAddressTextBox { get; set; }

        // The 'Confirm Email' field on the 'Personal Info' page
        [FindsBy(How = How.Id, Using = "TextBoxConfirmAppEmailAddress")]
        private IWebElement ConfirmEmailAddressTextBox { get; set; }

        // The 'Social Security Number' field on the 'Personal Info' page
        [FindsBy(How = How.Id, Using = "TextBoxAppSSN")]
        private IWebElement SocialSecurityNumberTextBox { get; set; }

        [FindsBy(How = How.Id, Using ="TextBoxConfirmAppSSN")]
        private IWebElement ConfirmSocialSecurityNumberTextBox { get; set; }

        // The 'Password' field on the 'Personal Info' page
        [FindsBy(How = How.Id, Using = "TextBoxAppPassword")]
        private IWebElement PasswordTextBox { get; set; }

        // The 'Confirm Password' field on the 'Personal Info' page
        [FindsBy(How = How.Id, Using = "TextBoxConfirmPassword")]
        private IWebElement ConfirmPasswordTextBox { get; set; }

        // The 'Secret Question' drop-down on the 'Personal Info' page
        [FindsBy(How = How.Id, Using = "DropDownListSecretQuestion")]
        private IWebElement SecretQuestionDropDown { get; set; }

        // The 'Secret Answer' field on the 'Personal Info' page
        [FindsBy(How = How.Id, Using = "TextBoxSecretAnswer")]
        private IWebElement SecretAnswerTextBox { get; set; }
        #endregion


        #region Page Actions

        /// <summary>
        /// Enter text into the 'First Name' text box
        /// </summary>
        /// <param name="firstName">The applicants first name</param>
        public void EnterFirstName(string firstName)
        {
            FirstNameTextBox.WaitRetry(_driver).SendKeys(firstName);
        }

        /// <summary>
        /// Enter text into the 'Last Name' text box
        /// </summary>
        /// <param name="lastName">The applicants last name</param>
        public void EnterLastName(string lastName)
        {
            LastNameTextBox.SendKeys(lastName);
        }

        /// <summary>
        /// Enter text into the 'Email Address' text box
        /// </summary>
        /// <param name="emailAddress">The applicants email address</param>
        public void EnterEmailAddress(string emailAddress)
        {
            EmailAddressTextBox.SendKeys(emailAddress);
        }

        /// <summary>
        /// Enter text into the 'Confirm Email' text box
        /// </summary>
        /// <param name="emailAddress">The applicants email address</param>
        public void EnterConfirmEmail(string emailAddress)
        {
            ConfirmEmailAddressTextBox.SendKeys(emailAddress);
        }

        /// <summary>
        /// Enter text into the 'Social Security Number' text box
        /// </summary>
        /// <param name="ssn">The applicants social security number</param>
        public void EnterSocialSecurityNumber(string ssn)
        {
            SocialSecurityNumberTextBox.Clear();
            SocialSecurityNumberTextBox.SendKeys(ssn);
        }

        /// <summary>
        /// Enter text into the 'Confirm SSN' text box
        /// </summary>
        /// <param name="ssn">The applicants social security number</param>
        public void EnterConfirmSsn(string ssn)
        {
            ConfirmSocialSecurityNumberTextBox.Clear();
            ConfirmSocialSecurityNumberTextBox.SendKeys(ssn);
        }

        /// <summary>
        /// Enter text into the 'Password' text box
        /// </summary>
        /// <param name="password">The applicants password</param>
        public void EnterPassword(string password)
        {
            PasswordTextBox.SendKeys(password);
        }

        /// <summary>
        /// Enter text into the 'Confirm Password' text box
        /// </summary>
        /// <param name="password">The applicants password</param>
        public void EnterConfirmPassword(string password)
        {
            ConfirmPasswordTextBox.SendKeys(password);
        }

        /// <summary>
        /// Select the first question from the 'Secret Question' drop down
        /// </summary>
        public void SelectSecretQuestion()
        {
            SelectElement secretQuestionDropDown = new SelectElement(SecretQuestionDropDown);
            secretQuestionDropDown.SelectByIndex(1);
        }

        /// <summary>
        /// Select the specified secret question from the 'Secret Question' drop down
        /// </summary>
        /// <param name="secretQuestion">The exact text of the secret question</param>
        public void SelectSecretQuestion(string secretQuestion)
        {
            SelectElement secretQuestionDropDown = new SelectElement(SecretQuestionDropDown);
            secretQuestionDropDown.SelectByText(secretQuestion);
        }

        /// <summary>
        /// Enter text into the 'Secret Answer' text box
        /// </summary>
        /// <param name="secretAnswer">The answer to the secret question</param>
        public void EnterSecretAnswer(string secretAnswer)
        {
            SecretAnswerTextBox.SendKeys(secretAnswer);
        }
        #endregion


    }
}
