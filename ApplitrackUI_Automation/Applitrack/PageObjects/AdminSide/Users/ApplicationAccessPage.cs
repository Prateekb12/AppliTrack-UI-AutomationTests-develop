using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;


namespace ApplitrackUITests.PageObjects.AdminSide.Users
{
    public class ApplicationAccessPage : BasePageObject, IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructor

        public ApplicationAccessPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        [FindsBy(How = How.CssSelector, Using = "#UserAccessTable")]
        private IWebElement UserAccessTable { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".All-Users")]
        private IWebElement AllUsersLink { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".Access-Granted")]
        private IWebElement AccessGrantedLink { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".Paused")]
        private IWebElement PausedLink { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".Invitation-Sent")]
        private IWebElement InvitationSentLink { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".No-Access")]
        private IWebElement NoAccessLink { get; set; }

        // TODO selector
        private IWebElement ActionsMenu { get; set; }

        // TODO selector
        private IWebElement OnlyShowSelectedCheckbox { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".Clear-All")]
        private IWebElement ClearAllLink { get; set; }

        // TODO selector
        private IWebElement SearchBox { get; set; }
        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        public bool IsDisplayed()
        {
            try
            {
                return UserAccessTable.WaitRetry(_driver).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Click the 'All Users' link to view a list of all users 
        /// </summary>
        public void ClickAllUsers()
        {
            AllUsersLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Access Granted' link to view a list of all users who have been granted access to the application
        /// </summary>
        public void ClickAccessGranted()
        {
            AccessGrantedLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Paused' link to view a list of all users whos acess has been paused
        /// </summary>
        public void ClickPaused()
        {
            PausedLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Invitation Sent' link to view a list of all users who have invitations sent
        /// </summary>
        public void ClickInvititationSent()
        {
            InvitationSentLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'No Access' link to view a list of users whom have no access
        /// </summary>
        public void ClickNoAccess()
        {
            NoAccessLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Send an invitiation to the specified user.
        /// </summary>
        /// <param name="userName">The user to send the invitation to</param>
        public void SendInvitation(string userName)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Use bulk actions to send invitations to a list of users
        /// </summary>
        /// <param name="userNames">The users to send invitations to</param>
        public void BulkSendInvitations(StringCollection userNames)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Set the specified users email address
        /// </summary>
        /// <param name="userName">The user to set the email address of</param>
        public void SetEmailAddress(string userName)
        {
             throw new System.NotImplementedException();           
        }

        /// <summary>
        /// Resend the invitation to the specified user
        /// </summary>
        /// <param name="userName">The user to resend the invitation to</param>
        public void ResendInvitation(string userName)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Pauce access of the specified user
        /// </summary>
        /// <param name="userName">The user to pause access of</param>
        public void PauseAccess(string userName)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Use bulk actions to pauce access to a list of users
        /// </summary>
        /// <param name="userNames">The users to pause the access of</param>
        public void BulkPauseAccess(StringCollection userNames)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Revoke the access of the specified user
        /// </summary>
        /// <param name="userName">The user to revoke access of</param>
        public void Revoke(string userName)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Resume the access of the specified user
        /// </summary>
        /// <param name="userName">The user to resume the access of</param>
        public void ResumeAccess(string userName)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Use bulk actions to resume access to a list of users
        /// </summary>
        /// <param name="userNames">The users to resume the access of</param>
        public void BulkResumeAccess(StringCollection userNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send a new invitation to the specified user
        /// </summary>
        /// <param name="userName"></param>
        public void SendNewInvitation(string userName)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// View the credentials of the specified user
        /// </summary>
        /// <param name="userName">The user to view the credentials of</param>
        public void ViewCredentials(string userName)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get the statuses of the specified users
        /// </summary>
        /// <param name="userNames">The usernames to get the statuses of</param>
        /// <returns>A dictionary containing the statuses of the indicated users</returns>
        public Dictionary<string, string> GetStatuses (StringCollection userNames)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Select the indicated users
        /// </summary>
        /// <param name="userNames">A collection of the users to collect</param>
        public void SelectUsers(StringCollection userNames)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Search for a specified user
        /// </summary>
        /// <param name="userName">The user to search for</param>
        public void FindUser(string userName)
        {
            SearchBox.Clear();
            SearchBox.SendKeys(userName);
            SearchBox.SendKeys(Keys.Enter);
        }

        #endregion
    }
}
