using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.Toolbar;
using Automation;
using OpenQA.Selenium;

namespace ApplitrackUITests.WorkFlows
{
    public class SearchWorkflows : BaseFrameWork
    {
        private readonly IWebDriver _driver;

        public SearchWorkflows(IWebDriver driver)
        {
            this._driver = driver;
        }

        /// <summary>
        /// Open the applicant page using the search functionality.
        /// </summary>
        /// <param name="appNo">The applicant's number</param>
        /// <param name="appName">The applicant's name</param>
        public void OpenApplicantUsingSearch(string appNo, string appName)
        {
            // page objects
            var toolbar = ToolbarFactory.Get(_driver);

            // Search for the applicant
            _driver.SwitchToDefaultFrame();
            toolbar.ClickSearch();
            toolbar.SearchPage.EnterSearchText(appNo);

            // Open the applicant page
            toolbar.SearchPage.SelectSearchResult(appNo);
        }

        public void OpenEmployeeUsingSearch(string empNo, string empName)
        {
            // page objects
            var toolbar = ToolbarFactory.Get(_driver);

            _driver.SwitchToDefaultFrame();
            toolbar.ClickSearch();
            toolbar.SearchPage.EnterSearchText(empNo);

            // Open the employeepage
            toolbar.SearchPage.OpenEmployee(empName);
        }
    }
}