using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Interviews
{
    public class CreateInterviewPages : BasePageObject
    {
        private readonly IWebDriver _driver;

        #region Constructor

        public CreateInterviewPages(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // The 'Start' tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Start']")]
        private IWebElement StartTabLink { get; set; }

        // The 'Series Details' tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Series Details']")]
        private IWebElement SeriesDetailsTabLink { get; set; }

        // The 'Sessions' tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Sessions']")]
        private IWebElement SessionsTabLink { get; set; }

        // The 'Participants' tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Participants']")]
        private IWebElement ParticipantsTabLink { get; set; }

        // The 'Questionnaires' tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Questionnaires']")]
        private IWebElement QuestionnairesTabLink { get; set; }

        // The 'Summary' tab
        [FindsBy(How = How.XPath, Using = "//font[text()='Summary']")]
        private IWebElement SummaryTabLink { get; set; }

        // The 'Next' button on the bottom right of the page
        [FindsBy(How = How.Id, Using = "ButtonWizardNext")]
        private IWebElement NextButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".StepNavigationButtonContainer input[value='Save and Finish']")] //Product Link
        [CacheLookup]
        public IWebElement SaveFinishButton { get; set; }

        #endregion

        #region Related Pages

        private CreateInterviewStartTab _startTab;
        public CreateInterviewStartTab StartTab
        {
            get { return _startTab ?? (_startTab = new CreateInterviewStartTab(_driver)); }
        }

        private CreateInterviewSeriesDetailsTab _seriesDetailsTab;
        public CreateInterviewSeriesDetailsTab SeriesDetailsTab
        {
            get { return _seriesDetailsTab ?? (_seriesDetailsTab = new CreateInterviewSeriesDetailsTab(_driver)); }
        }

        private CreateInterviewSessionsTab _sessionsTab;
        public CreateInterviewSessionsTab SessionsTab
        {
            get { return _sessionsTab ?? (_sessionsTab = new CreateInterviewSessionsTab(_driver)); }
        }

        private CreateInterviewParticipantsTab _participantsTab;
        public CreateInterviewParticipantsTab ParticipantsTab
        {
            get { return _participantsTab ?? (_participantsTab = new CreateInterviewParticipantsTab(_driver)); }
        }

        private CreateInterviewQuestionnairesTab _questionnairesTab;
        public CreateInterviewQuestionnairesTab QuestionnairesTab
        {
            get { return _questionnairesTab ?? (_questionnairesTab = new CreateInterviewQuestionnairesTab(_driver)); }
        }

        private CreateInterviewSummaryTab _summaryTab;
        public CreateInterviewSummaryTab SummaryTab
        {
            get { return _summaryTab ?? (_summaryTab = new CreateInterviewSummaryTab(_driver)); }
        }

        #endregion

        #region Page Actions

        /// <summary>
        /// Click the 'Start' tab
        /// </summary>
        public void ClickStartTab()
        {
            StartTabLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Series Details' tab
        /// </summary>
        public void ClickSeriesDetailsTab()
        {
            SeriesDetailsTabLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Sessions' tab
        /// </summary>
        public void ClickSessionsTab()
        {
            SessionsTabLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Participants' tab
        /// </summary>
        public void ClickParticipantsTab()
        {
            ParticipantsTabLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Questionnaires' tab
        /// </summary>
        public void ClickQuestionnairesTab()
        {
            QuestionnairesTabLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Summary' tab
        /// </summary>
        public void ClickSummaryTab()
        {
            SummaryTabLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Next' button to move to the next step if the wizard
        /// </summary>
        public void ClickNext()
        {
            NextButton.WaitAndClick(_driver);
        }

        public void ClickSaveFinish()
        {
            SaveFinishButton.WaitAndClick(_driver);
        }

        #endregion
    }
}
