using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.Toolbar
{
    public abstract class ToolbarSearchPage : BasePageObject, IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructors

        protected ToolbarSearchPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors 

        // the div containing the search results
        protected abstract IWebElement SearchResults { get; set; }

        #endregion

        #region Page Actions

        /// <summary>
        /// Verify that the search page is displayed
        /// </summary>
        public virtual bool IsDisplayed()
        {
            try
            {
                return SearchResults.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Select the given search result.
        /// </summary>
        /// <param name="result"></param>
        public abstract void SelectSearchResult(string result);

        /// <summary>
        /// Enter a value to search for.
        /// </summary>
        /// <param name="searchTerm"></param>
        public abstract void EnterSearchText(string searchTerm);

        /// <summary>
        /// Open an applicant record. If no result parameter is supplied, the first record will be opened.
        /// </summary>
        /// <param name="result"></param>
        public abstract void OpenApplicant(string result = null);

        /// <summary>
        /// Open an employee record. If no result parameter is supplied, the first record will be opened. 
        /// </summary>
        /// <param name="result"></param>
        public abstract void OpenEmployee(string result = null);

        /// <summary>
        /// Open an employee record. If no result parameter is supplied, the first record will be opened.
        /// </summary>
        /// <param name="result"></param>
        public abstract void OpenJobPosting(string result = null);

        /// <summary>
        /// Open an employee record. If no result parameter is supplied, the first record will be opened.
        /// </summary>
        /// <param name="result"></param>
        public abstract void OpenUser(string result = null);

        /// <summary>
        /// Make sure that the applicants are displayed in the search results.
        /// </summary>
        /// <returns></returns>
        public abstract bool ApplicantsAreDisplayed();

        /// <summary>
        /// Make sure that employees are displayed in the search results.
        /// </summary>
        /// <returns></returns>
        public abstract bool EmployeesAreDisplayed();

        /// <summary>
        /// Make sure that job postings are displayed in the search results.
        /// </summary>
        /// <returns></returns>
        public abstract bool JobPostingsAreDisplayed();

        /// <summary>
        /// Make sure that users are displayed in the search results.
        /// </summary>
        /// <returns></returns>
        public abstract bool UsersAreDisplayed();

        /// <summary>
        /// Click the 'Advanced Search' link for Applicants
        /// </summary>
        public abstract void ClickApplicantsAdvancedSearch();

        /// <summary>
        /// Click the 'Advanced Search' link for Employees
        /// </summary>
        public abstract void ClickEmployeesAdvancedSearch();

        #endregion
    }
}
