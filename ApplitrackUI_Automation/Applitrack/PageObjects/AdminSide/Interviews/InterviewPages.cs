using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Interviews
{
    public class InterviewPages : BasePageObject
    {
        private readonly IWebDriver _driver;

        #region Constructor

        public InterviewPages(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        #endregion

        #region Related Pages

        private InterviewsDashboardPage _dashboardPage;
        /// <summary>
        /// The 'My Interviews' page accessed from Interviews on the main menu
        /// </summary>
        public InterviewsDashboardPage DashboardPage
        {
            get { return _dashboardPage ?? (_dashboardPage = new InterviewsDashboardPage(_driver)); }
        }

        private CreateInterviewPages _createInterviewPage;
        /// <summary>
        /// The 'Create Interview' page accessed from Interviews > Create Interview
        /// </summary>
        public CreateInterviewPages CreateInterviewPages
        {
            get { return _createInterviewPage ?? (_createInterviewPage = new CreateInterviewPages(_driver)); }
        }

        private MyInterviewsPage _myInterviewsPage;
        /// <summary>
        /// The 'My Interviews' page that loads after clicking Interviews on the main menu
        /// </summary>
        public MyInterviewsPage MyInterviewsPage
        {
            get { return _myInterviewsPage ?? (_myInterviewsPage = new MyInterviewsPage(_driver)); }
        }

        #endregion

        #region Page Actions
        #endregion
    }
}
