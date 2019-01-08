using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Users
{
    public class UserPages : BasePageObject
    {
        private readonly IWebDriver _driver;

        #region Constructor

        public UserPages(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region Related Pages

        private CreateNewUserPage _createNewUserPage;
        /// <summary>
        /// The 'Create a new user' page from Users > Create a new user
        /// </summary>
        public CreateNewUserPage CreateNewUserPage => 
            _createNewUserPage ?? (_createNewUserPage = new CreateNewUserPage(_driver));

        private ListUsersPage _listUsersPage;

        /// <summary>
        /// The 'List all users' page from Users > List all users
        /// </summary>
        public ListUsersPage ListUsersPage =>
            _listUsersPage ?? (_listUsersPage = new ListUsersPage(_driver));

        #endregion
    }
}
