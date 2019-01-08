using System;
using System.Collections.Generic;
using System.Threading;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.ApplicantSide
{
    public class FitPages : BasePageObject, IApplitrackPage
    {
        private IWebDriver _driver;

        #region Constructor

        public FitPages(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }
        #endregion

        #region UI Selectors

        // the header of the page containing the percentage complete
        [FindsBy(How = How.Id, Using = "StepStatus")]
        private IWebElement StepStatus { get; set; }

        [FindsBy(How = How.Id, Using = "NextButton")]
        private IWebElement OkNextButton { get; set; }

        // The row for each question
        [FindsBy(How = How.ClassName, Using = "QuestionRow")]
        private IList<IWebElement> QuestionRow { get; set; }

        // The element that appears after completing a fit assessment
        [FindsBy(How = How.Id, Using = "LblApplicantDoneMessage")]
        private IWebElement DoneMessageText { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Check to see if the Fit page is displayed
        /// </summary>
        /// <returns>Returns true if the page is displayed, false otherwise</returns>
        public bool IsDisplayed()
        {
            _driver.SwitchToDefaultFrame();
            _driver.SwitchToFrameById("AppDataPage");
            Thread.Sleep(TimeSpan.FromSeconds(6));
            _driver.SwitchToFrameById("PolarisInline");
            return StepStatus.Displayed;
        }

        /// <summary>
        /// Click the 'Ok, Next' button
        /// </summary>
        public void ClickOkNext()
        {
            OkNextButton.WaitAndClick(_driver);
        }

        /// <summary>
        /// Check to see if the 'Ok, Next' button is displayed.
        /// This is used to decide which page type is displayed
        /// </summary>
        /// <returns>True if the page containing the 'Ok, Next' button is displayed, false otherwise</returns>
        public bool OkNextDisplayed()
        {
            try
            {
                _driver.SwitchToDefaultFrame();
                _driver.SwitchToFrameById("AppDataPage");
                Thread.Sleep(TimeSpan.FromSeconds(1));
                _driver.SwitchToFrameById("PolarisInline");
                _driver.SwitchToFrameById("PolarisQuestions");

                return OkNextButton.Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Check to see if the question fit page is displayed.
        /// This is used to decide which page type is displayed
        /// </summary>
        /// <returns>True if the question page is displayed, false otherwise</returns>
        public bool QuestionPageDisplayed()
        {
            try
            {
                _driver.SwitchToDefaultFrame();
                _driver.SwitchToFrameById("AppDataPage");
                Thread.Sleep(TimeSpan.FromSeconds(1));
                _driver.SwitchToFrameById("PolarisInline");
                _driver.SwitchToFrameById("PolarisQuestions");

                return _driver.FindElement(By.Id("Item_QuestionTable")).Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Check to see of the multiple choice fit page is displayed
        /// This is used to decide which page type is displayed
        /// </summary>
        /// <returns>True if the multiple choice page is displayed, false otherwise</returns>
        public bool MultipleChoicePageDisplayed()
        {
            try
            {
                _driver.SwitchToDefaultFrame();
                _driver.SwitchToFrameById("AppDataPage");
                Thread.Sleep(TimeSpan.FromSeconds(1));
                _driver.SwitchToFrameById("PolarisInline");

                return StepStatus.Text.Contains("Multiple Choice");
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Randomly select answers to fit questions
        /// </summary>
        public void SelectRandomOptions()
        {
            var rand = new Random();

            foreach (var row in QuestionRow)
            {
                IList<IWebElement> questionOptions = row.FindElements(By.ClassName("QuestionOption"));
                questionOptions[rand.Next(0, questionOptions.Count)].WaitAndClick(_driver);
            }
        }

        /// <summary>
        /// Randomly select answers to multiple choice fit questions
        /// </summary>
        public void SelectRandomMultipleChoice()
        {
            var rand = new Random();

            _driver.SwitchToDefaultFrame();
            _driver.SwitchToFrameById("AppDataPage");
            Thread.Sleep(TimeSpan.FromSeconds(1));
            _driver.SwitchToFrameById("PolarisInline");
            _driver.SwitchToFrameById("PolarisQuestions");

            QuestionRow[rand.Next(0, QuestionRow.Count)].FindElement(By.TagName("input")).WaitAndClick(_driver);
        }

        /// <summary>
        /// Check to see if the assessment has been completed
        /// </summary>
        /// <returns>True if the assessment was completed successfully, false otherwise</returns>
        public bool AssessmentCompleted()
        {
            _driver.SwitchToDefaultFrame();
            _driver.SwitchToFrameById("AppDataPage");
            return DoneMessageText.Displayed;
        }
        #endregion

    }
}
