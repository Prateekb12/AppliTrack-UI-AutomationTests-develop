using System;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using OpenQA.Selenium;

namespace ApplitrackUITests.WorkFlows
{
    class UserWorkflows
    {
        private IWebDriver _driver;
        public UserWorkflows(IWebDriver Driver)
        {
            this._driver = Driver;
        }

        public void CreateUser(UserGenerator userData)
        {
            var mainMenu = new MainMenu(_driver);
            var userPage = new SubMenuUsers(_driver);

            // create user
            Console.WriteLine("Creating user");
            mainMenu.ClickUsers();
            userPage.ClickCreateNewUser();

            // enter user info
            _driver.SwitchToFrameById("MainContentsIFrame");
            _driver.SwitchToFrameById("tabs_Panel");
            userPage.FillUserId(userData.UserName);
            userPage.ChangePassword(userData.Password);
            userPage.FillRealName(userData.RealName);
            userPage.FillEmail(userData.Email);

            // save
            _driver.SwitchToDefaultFrame();
            _driver.SwitchToFrameById("MainContentsIFrame");
            userPage.ClickSaveButton();
            userPage.ClickSaveAndCloseButton();
        }

        public void DeleteUser(UserGenerator userData)
        {
            var userPage = new SubMenuUsers(_driver);
            userPage.SelectUser(userData.UserName);
            userPage.ClickDeleteUser();
            userPage.ConfirmDeletion();
        }
    }
}
