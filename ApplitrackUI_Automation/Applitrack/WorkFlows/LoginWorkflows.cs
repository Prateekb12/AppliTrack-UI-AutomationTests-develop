using System;
using System.Configuration;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.PageObjects.AdminSide;
using ApplitrackUITests.PageObjects.Jefferson;
using ApplitrackUITests.Helpers;
using Automation;
using IDMPageObjects.Pages;
using OpenQA.Selenium;

namespace ApplitrackUITests.WorkFlows
{
    public class LoginWorkflows : BaseFrameWork 
    {
        private readonly IWebDriver _driver;
        private readonly CommonWaits _commonWaits;
        private readonly bool _idmEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["IDMEnabled"]);
        private readonly bool _isJefferson = Convert.ToBoolean(ConfigurationManager.AppSettings["IsJefferson"]);

        /// <summary>
        /// Workflows for logging into Applitrack
        /// </summary>
        /// <param name="driver">Selenium webdriver</param>
        public LoginWorkflows(IWebDriver driver)
        {
            _driver = driver;
            _commonWaits = new CommonWaits(_driver);
        }

        /// <summary>
        /// Log into applitrack using the standard login page or the IDM login page
        /// </summary>
        /// <param name="userName">Applitrack username</param>
        /// <param name="password">Applitrack password</param>
        public void Login(string userName, string password)
        {
            try
            {
                if (_isJefferson)
                {
                    JeffersonLogin(userName, password);
                }
                else if (_idmEnabled)
                {
                    IDMLogin(userName, password);
                }
                else
                {
                    AptLogin(userName, password);
                }

                // sometimes an alert opens upon login
                // the following if and catch statements should handle any alerts
                if(AlertDetect(_driver))
                {
                    AlertAccept(_driver);
                }

                // wait for the dashboard page to load before continuing
                var dashboardPage = new DashboardPage(_driver);
                dashboardPage.WaitForPageToLoad();
            }
            catch (UnhandledAlertException)
            {
                AlertAccept(_driver);
            }
        }

        /// <summary>
        /// Login using the Federated login page
        /// </summary>
        private void JeffersonLogin(string userName, string password)
        {
            var loginPage = new FederatedLoginPage(_driver);
            loginPage.Login(userName, password);
        }

        /// <summary>
        /// Login using the standard Applitrack login page
        /// </summary>
        private void AptLogin(string userName, string password)
        {
            var loginPage = new LoginPage(_driver);

            WaitForLoginPageToLoad();

            loginPage.EnterUsername(userName);
            loginPage.EnterPwd(password);
            loginPage.ClickLogin();
        }

        /// <summary>
        /// Wait for the login page to load to prevent errors
        /// </summary>
        private void WaitForLoginPageToLoad()
        {
            _driver.Wait(120).Until(d => d.FindElement(By.Id("UserID")).Displayed);
        }

        /// <summary>
        /// Login using the IDM login page
        /// </summary>
        private void IDMLogin(string userName, string password)
        {
            var loginPage = new IDMLoginPage(_driver);

            loginPage.Login(userName, password);
            _commonWaits.WaitForLoadingScreen();
        }

        /// <summary>
        /// Log in as a super user
        /// </summary>
        public void LoginAsSuperUser()
        {
            Login(LoginData.SuperUserName, LoginData.SuperUserPassword);
        }

        /// <summary>
        /// Login as a standard user
        /// </summary>
        public void LoginAsStandardUser()
        {
            Login(LoginData.StdUserName, LoginData.StdUserPassword);
        }

        /// <summary>
        /// Login as a routing user
        /// </summary>
        public void LoginAsRoutingUser()
        {
            Login(LoginData.RoutingUserName, LoginData.RoutingUserPassword);
        }

    }
}
