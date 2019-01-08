using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Forms
{
    public class FormPages : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public FormPages(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        #endregion

        #region Related Pages

        private CreateNewFormPage _createNewFormPage;
        /// <summary>
        /// The 'Create New Form' page accessed from Forms > Design Forms and Packets > Create New Form
        /// </summary>
        public CreateNewFormPage CreateNewFormPage
        {
            get { return _createNewFormPage ?? (_createNewFormPage = new CreateNewFormPage(_driver)); }
        }

        private EditAndCreateFormPage _editAndCreateFormPage;
        /// <summary>
        /// The edit/create form page accessed from:
        /// - Forms > Design Forms and Packets > Create New Form
        /// - Forms > Design Forms and Packets > Edit Forms
        /// - Any other part of the system that allows you to edit a form
        /// </summary>
        public EditAndCreateFormPage EditAndCreateFormPage
        {
            get { return _editAndCreateFormPage ?? (_editAndCreateFormPage = new EditAndCreateFormPage(_driver)); }
        }

        private EditFormsPage _editFormsPage;
        /// <summary>
        /// The 'Edit Forms' page accessed from Forms > Design Forms and Packets > Edit Forms
        /// </summary>
        public EditFormsPage EditFormsPage
        {
            get { return _editFormsPage ?? (_editFormsPage = new EditFormsPage(_driver)); }
        }

        private MySentFormsPage _mySentFormsPage;

        public MySentFormsPage MySentFormsPage
        {
            get { return _mySentFormsPage ?? (_mySentFormsPage = new MySentFormsPage(_driver)); }
        }

        private SendFormPage _sendFormPage;
        /// <summary>
        /// The 'Send Form' page accessed from Forms > Send a Form
        /// </summary>
        public SendFormPage SendFormPage
        {
            get { return _sendFormPage ?? (_sendFormPage = new SendFormPage(_driver)); }
        }

        private FormsSearchPage _searchPage;
        /// <summary>
        /// The 'Forms Search' page accessed from selecting the search criteria in 
        /// Forms > View Submitted Forms By Category 
        /// </summary>
        public FormsSearchPage SearchPage
        {
            get { return _searchPage ?? (_searchPage = new FormsSearchPage(_driver)); }
        }

        #endregion

        #region Page Actions
        #endregion
    }
}