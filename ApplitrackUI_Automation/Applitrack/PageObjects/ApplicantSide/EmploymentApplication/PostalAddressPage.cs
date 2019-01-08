using System;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation.Framework.Extensions;
using IDMPageObjects.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ApplitrackUITests.PageObjects.ApplicantSide.EmploymentApplication
{
    public class PostalAddressPage : IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructor

        public PostalAddressPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        [FindsBy(How = How.CssSelector, Using = "input#DBAppPermanentStreet")]
        private IWebElement NumberAndStreetTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input#DBAppPermanentAptNbr")]
        private IWebElement AptNumberTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input#DBAppPermanentCity")]
        private IWebElement CityTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "select#DBAppPermanentState")]
        private IWebElement StateProvinceDropDown { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input#DBAppPermanentZip")]
        private IWebElement ZipPostalCodeTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "select#DBAppPermanentCountry")]
        private IWebElement CountryDropDown { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name=DBAppPermanentAreaCde]")]
        private IWebElement DayPhoneAreaCodeTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name=DBAppPermanentPhn]")]
        private IWebElement DayPhoneTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name=DBAppHomeAreaCde]")]
        private IWebElement HomeCellPhoneAreaCodeTextBox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name=DBAppHomePhone]")]
        private IWebElement HomeCellPhoneTextBox { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions
        /// <summary>
        /// See if the Postal Address page is displayed
        /// </summary>
        /// <returns>True if the page is displayed, false otherwise</returns>
        public bool IsDisplayed()
        {
            _driver.WaitForIt(NumberAndStreetTextBox);

            try
            {
                return NumberAndStreetTextBox.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Enter text into the 'Number and Street' text box for the Permanent Address
        /// </summary>
        /// <param name="numberAndStreet">The applicants number and street</param>
        public void EnterPermanentNumberAndStreet(string numberAndStreet)
        {
            NumberAndStreetTextBox.WaitRetry(_driver).SendKeys(numberAndStreet);
        }

        /// <summary>
        /// Enter text into the 'Apt. Number' text box for the Permanent Address
        /// </summary>
        /// <param name="aptNumber">The applicants apartment number</param>
        public void EnterPermanentAptNumber(string aptNumber)
        {
            AptNumberTextBox.SendKeys(aptNumber);
        }

        /// <summary>
        /// Enter text into the 'City' text box for the Permanent Address
        /// </summary>
        /// <param name="city">The applicants city</param>
        public void EnterPermanentCity(string city)
        {
            CityTextBox.SendKeys(city);
        }

        /// <summary>
        /// Select a value from the 'State/Province' drop down for the Permanent Address
        /// </summary>
        /// <param name="state">The applicants state or province</param>
        public void SelectPermanentStateProvince(string state)
        {
            var stateProvinceDropDown = new SelectElement(StateProvinceDropDown);
            stateProvinceDropDown.SelectByText(state);
        }

        /// <summary>
        /// Enter text into the 'Zip/Postal Code' text box for the  Permanent Address
        /// </summary>
        /// <param name="zip">The applicants zip code</param>
        public void EnterPermanentZipPostalCode(string zip)
        {
            ZipPostalCodeTextBox.SendKeys(zip);
        }

        /// <summary>
        /// Select a value from the 'Country' drop down for the Permanent Address
        /// </summary>
        /// <param name="country">The applicants country</param>
        public void SelectPermanentCountry(string country)
        {
            var countryDropDown = new SelectElement(CountryDropDown);
            countryDropDown.SelectByText(country);
        }

        /// <summary>
        /// Enter text into the 'Daytime Phone' text box for the Permanent Address
        /// </summary>
        /// <param name="phone">The applicants phone number. Must consist of only 10 digits.</param>
        public void EnterPermanentDayPhone(string phone)
        {
            DayPhoneAreaCodeTextBox.SendKeys(phone.Substring(0, 3));
            DayPhoneTextBox.SendKeys(phone.Substring(3));
        }

        /// <summary>
        /// Enter text into the 'Home/Cell Phone' text box for the Permanent Address
        /// </summary>
        /// <param name="phone">The applicants home or cell number. Must consist of only 10 digits.</param>
        public void EnterPermanentCellPhone(string phone)
        {
            HomeCellPhoneAreaCodeTextBox.SendKeys(phone.Substring(0, 3));
            HomeCellPhoneTextBox.SendKeys(phone.Substring(3));           
        }

        #endregion


    }
}
