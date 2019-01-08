using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Applicants
{
    public class ApplicantAdminPages : BasePageObject
    {
        private readonly IWebDriver _driver;

        #region Constructors

        public ApplicantAdminPages(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region Element Selectors
        #endregion

        #region Related Pages

        private ApplicantAdminDashboardPage _applicantDashboard;
        /// <summary>
        /// The Applicant Dashboard page that appears after clicking on 'Applicants' from the main menu
        /// </summary>
        public ApplicantAdminDashboardPage Dashboard => _applicantDashboard ??
                                                        (_applicantDashboard = new ApplicantAdminDashboardPage(_driver));


        private ApplicantAdminVacanciesByCategoryPages _vacanciesByCategoryPages;
        /// <summary>
        /// The 'Vacancies by Category' pages accessed from Applicants > Vacancies by Category > 'Category' > 'Job' > View all applicants
        /// </summary>
        public ApplicantAdminVacanciesByCategoryPages VacanciesByCategoryPages => _vacanciesByCategoryPages ??
                                                                                  (_vacanciesByCategoryPages = new ApplicantAdminVacanciesByCategoryPages(_driver));

        private ApplicantSearchResultsPage _searchResults;
        /// <summary>
        /// The results page displayed after performing a search using the applicant dashboard
        /// </summary>
        public ApplicantSearchResultsPage SearchResultsPage => _searchResults ??
                                                               (_searchResults = new ApplicantSearchResultsPage(_driver));

        private ApplicantSearchFormPage _searchForm;
        /// <summary>
        /// The 'Find by Search Form page' accessed from Applicants > Search Form
        /// </summary>
        public ApplicantSearchFormPage SearchFormPage => _searchForm ??
                                                         (_searchForm = new ApplicantSearchFormPage(_driver));

        #endregion
    }
}