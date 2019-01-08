using ApplitrackUITests.PageObjects.AdminSide.JobPostings;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;


namespace ApplitrackUITests.PageObjects
{
    public class JobPostingsPages : BasePageObject
    {
        //For This Page
        private IWebDriver _driver;

        #region Constructor

        public JobPostingsPages(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        #endregion

        #region Related Pages

        private JobPostingsDashboardPage _dashboardPage;

        public JobPostingsDashboardPage DashboardPage
        {
            get { return _dashboardPage ?? (_dashboardPage = new JobPostingsDashboardPage(_driver)); }
        }

        private CreateNewPostingPage _createNewPostingPage;
        /// <summary>
        /// The 'Create New Posting' page accessed from Job Postings > Create New Posting 
        /// </summary>
        public CreateNewPostingPage CreateNewPostingPage
        {
            get { return _createNewPostingPage ?? (_createNewPostingPage = new CreateNewPostingPage(_driver)); }
        }

        private CreateNewRequisitionPage _createNewRequisitionPage;
        /// <summary>
        /// The 'Create New Requisition' page accessed from Job Postings > Create New Requisition
        /// </summary>
        public CreateNewRequisitionPage CreateNewRequisitionPage
        {
            get { return _createNewRequisitionPage ?? (_createNewRequisitionPage = new CreateNewRequisitionPage(_driver)); }
        }

        private EditCreatePostingPage _editAndCreateJobPostingPage;
        /// <summary>
        /// The 'Edit/Create' Job Posting page accessed when creating a new job posting or editing an existing one
        /// </summary>
        public EditCreatePostingPage EditAndCreateJobPostingPage
        {
            get
            {
                return _editAndCreateJobPostingPage ??
                       (_editAndCreateJobPostingPage = new EditCreatePostingPage(_driver));
            }
        }

        private EditCreateRequisitionPage _editAndCreateJobRequisitionPage;
        /// <summary>
        /// The 'Edit/Create' Job Requisition page accessed when creating a new job requisition or editing an existing one
        /// </summary>
        public EditCreateRequisitionPage EditAndCreateJobRequisitionPage
        {
            get
            {
                return _editAndCreateJobRequisitionPage ??
                       (_editAndCreateJobRequisitionPage = new EditCreateRequisitionPage(_driver));
            }
        }

        private AllPostingsByStatusPage _allPostingsByStatusPage;
        /// <summary>
        /// The 'All Postings By Status' page accessed from Job Postings > All Postings By Status
        /// </summary>
        public AllPostingsByStatusPage AllPostingsByStatusPage
        {
            get { return _allPostingsByStatusPage ?? (_allPostingsByStatusPage = new AllPostingsByStatusPage(_driver)); }
        }

        private MyDraftRequisitionsPage _myDraftRequisitionsPage;
        /// <summary>
        /// The 'My Draft Requisitions' page accessed from Job Postings > My Draft Requisitions
        /// </summary>
        public MyDraftRequisitionsPage MyDraftRequisitionsPage
        {
            get { return _myDraftRequisitionsPage ?? (_myDraftRequisitionsPage = new MyDraftRequisitionsPage(_driver)); }
        }

        private ListExistingTemplatePage _listExistingTemplatePage;
        /// <summary>
        /// The 'List Existing' page accessed from Job Postings > List Existing
        /// </summary>
        public ListExistingTemplatePage ListExistingTemplatePage
        {
            get
            {
                return _listExistingTemplatePage ?? (_listExistingTemplatePage = new ListExistingTemplatePage(_driver));
            }
        }

        private PostingsByCategoryPages _postingsByCategoryPages;
        /// <summary>
        /// The 'Open Postings By Category' and 'Active Postings By Category' pages accessed from:
        /// - Job Postings > Open Postings By Category
        /// - Job Postings > Active Postings By Category
        /// </summary>
        public PostingsByCategoryPages PostingsByCategoryPages
        {
            get { return _postingsByCategoryPages ?? (_postingsByCategoryPages = new PostingsByCategoryPages(_driver)); }
        }

        private ListAllActivePostingsPage _listAllActivePostingsPage;
        /// <summary>
        /// The 'List All Active Postings' page accessed from Job Postings > List All Active Postings
        /// </summary>
        public ListAllActivePostingsPage ListAllActivePostingsPage
        {
            get
            {
                return _listAllActivePostingsPage ?? (_listAllActivePostingsPage = new ListAllActivePostingsPage(_driver));
            }
        }

        private MyUnsubmittedRequisitionPage _myUnsubmittedRequisitionPage;
        /// <summary>
        /// The 'My Unsubmitted Requisitions' page accessed from:
        /// - Job Postings > My Unsubmitted Requisitions
        /// - After creating a new job requisition
        /// </summary>
        public MyUnsubmittedRequisitionPage MyUnsubmittedRequisitionPage
        {
            get { return _myUnsubmittedRequisitionPage ?? (_myUnsubmittedRequisitionPage = new MyUnsubmittedRequisitionPage(_driver)); }
        }

        #endregion

        #region Page Actions
        #endregion


        #region Page Objects
        #endregion
    }
}
