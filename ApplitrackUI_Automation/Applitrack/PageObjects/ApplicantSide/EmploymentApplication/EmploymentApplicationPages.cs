using System;
using System.Threading;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using By = OpenQA.Selenium.Extensions.By;

namespace ApplitrackUITests.PageObjects.ApplicantSide.EmploymentApplication
{
    public class EmploymentApplicationPages : BasePageObject, IApplitrackPage
    {

        private IWebDriver _driver;

        #region Constructor

        public EmploymentApplicationPages(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion


        #region Related Pages

        private PersonalInfoPage _personalInfoPage;
        /// <summary>
        /// The 'Personal Info' page within the 'Employment Application' tab
        /// </summary>
        public PersonalInfoPage PersonalInfoPage
        {
            get { return _personalInfoPage ?? (_personalInfoPage = new PersonalInfoPage(_driver)); }
        }

        private FitPages _fitPages;
        /// <summary>
        /// The 'TeacherFit', 'TeacherFitSE', and 'AdminFit' pages within the 'Employment Application' tab
        /// </summary>
        public FitPages FitPages
        {
            get { return _fitPages ?? (_fitPages = new FitPages(_driver)); }
        }

        private VacancyDesiredPage _vacancyDesiredPage;
        /// <summary>
        /// The 'Vacancy Desired' page within the 'Employment Application' tab
        /// </summary>
        public VacancyDesiredPage VacancyDesiredPage
        {
            get { return _vacancyDesiredPage ?? (_vacancyDesiredPage = new VacancyDesiredPage(_driver)); }
        }

        private ConfirmationPage _confirmationPage;
        /// <summary>
        /// The 'Confirmation' page within the 'Employment Application' tab
        /// </summary>
        public ConfirmationPage ConfirmationPage
        {
            get { return _confirmationPage ?? (_confirmationPage = new ConfirmationPage(_driver)); }
        }

        private PostalAddressPage _postalAddressPage;
        /// <summary>
        /// The 'Postal Address' page within the 'Employment Application' tab
        /// </summary>
        public PostalAddressPage PostalAddressPage => _postalAddressPage ?? (_postalAddressPage = new PostalAddressPage(_driver));

        #endregion


        #region UI Selectors

        // The 'Next Page' button on the bottom right
        [FindsBy(How = How.Id, Using = "InputNextPage")]
        private IWebElement NextPageButton { get; set; }

        // The 'Save as Draft' button on the buttom left
        [FindsBy(How = How.Id, Using = "InputSaveAsDraft")]
        private IWebElement SaveAsDraftButton { get; set; }

        // The 'Finish and Submit' button on the bottom left
        [FindsBy(How = How.Id, Using = "InputFinishAndSubmit")]
        private IWebElement FinishAndSubmitButton { get; set; }

        private IWebElement PostalAddressLink =>
            _driver.FindElement(By.JQuerySelector("li:contains('Postal Address')"));

        // The 'Vacancy Desired' item on the left navigation menu
        private IWebElement VacancyDesiredLink => _driver.FindElement(By.JQuerySelector("li:contains('Vacancy Desired')")).WaitRetry(_driver);

        // The 'TeacherFit' item on the left navigation menu
        private IWebElement TeacherFitLink => _driver.FindElement(By.JQuerySelector("li:contains('TeacherFit')")).WaitRetry(_driver);

        // The 'TeacherFit SE' item on the left navigation menu
        private IWebElement TeacherFitSeLink => _driver.FindElement(By.JQuerySelector("li:contains('TeacherFit SE')")).WaitRetry(_driver);

        // The 'AdminFit' item on the left navigation menu
        private IWebElement AdminFitLink => _driver.FindElement(By.JQuerySelector("li:contains('AdminFit')")).WaitRetry(_driver);

        #endregion


        #region Page Actions

        // TODO
        public bool IsDisplayed()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check to see if the given title text is on the screen
        /// </summary>
        /// <param name="title">The title to check for</param>
        /// <returns>True if the title is on the screen, false otherwise</returns>
        public bool SectionTitleIsOnScreen(string title)
        {
            // This frame switch assumes that this is an administrative preview
            // An exception will occur if calling this method otherwise
            _driver.SwitchTo().Frame(2);
            return IsTextOnScreen(_driver, title);
        }

        /// <summary>
        /// Click the 'Postal Address' link on the left navigation menu
        /// </summary>
        public void ClickPostalAddress()
        {
            Thread.Sleep(TimeSpan.FromSeconds(10));
            _driver.SwitchToDefaultFrame();
            PostalAddressLink.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the 'Vacancy Desired' link on the left navigation menu
        /// </summary>
        public void ClickVacancyDesired()
        {
            Thread.Sleep(TimeSpan.FromSeconds(10));
            _driver.SwitchToDefaultFrame();
            VacancyDesiredLink.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the 'Position Desired' link on the left navigation menu
        /// </summary>
        public void ClickTeacherFit()
        {
            Thread.Sleep(TimeSpan.FromSeconds(10));
            _driver.SwitchToDefaultFrame();
            TeacherFitLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'AdminFit' link on the left navigation menu
        /// </summary>
        public void ClickAdminFit()
        {
            Thread.Sleep(TimeSpan.FromSeconds(10));
            _driver.SwitchToDefaultFrame();
            AdminFitLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'TeacherFit SE' link on the left navigation menu
        /// </summary>
        public void ClickTeacherFitSe()
        {
            Thread.Sleep(TimeSpan.FromSeconds(10));
            _driver.SwitchToDefaultFrame();
            TeacherFitSeLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Next Page' button
        /// </summary>
        public void ClickNextPage()
        {
            _driver.SwitchToDefaultFrame();
            NextPageButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Save as Draft' button
        /// </summary>
        public void ClickSaveAsDraft()
        {
            _driver.SwitchToDefaultFrame();
            SaveAsDraftButton.WaitAndClick(_driver);

            WaitForFinalScreenText("Your Application has been saved.");
        }

        /// <summary>
        /// Wait for the finished screen to have the given text
        /// </summary>
        /// <param name="text"></param>
        private void WaitForFinalScreenText(string text)
        {
            _driver.SwitchToFrameById("AppDataPage");
            var div = By.CssSelector("div#DivHomeMain div.MainToDoItem");
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(div));
            wait.Until(d => d.FindElement(div).Text.Contains(text));
            _driver.SwitchToDefaultFrame();
        }

        /// <summary>
        /// Click the 'Finish and Submit' button
        /// </summary>
        public void ClickFinishAndSubmit()
        {
            _driver.SwitchToDefaultFrame();
            FinishAndSubmitButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Get the applicant number
        /// </summary>
        /// <returns></returns>
        public int GetAppNo()
        {
            var appNoElement = _driver.FindElement(By.Name("appno"));
            return int.Parse(appNoElement.GetAttribute("value"));
        }

        #endregion

    }
}
