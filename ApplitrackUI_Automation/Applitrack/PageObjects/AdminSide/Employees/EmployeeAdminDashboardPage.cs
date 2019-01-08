using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Employees
{
    public class EmployeeAdminDashboardPage : BasePageObject, IApplitrackPage 
    {
        private readonly IWebDriver _driver;

        #region Constructor

        public EmployeeAdminDashboardPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        [FindsBy(How = How.CssSelector, Using = ".brand")]
        private IWebElement BrandText { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions
        public bool IsDisplayed()
        {
            try
            {
                return BrandText.Text.Contains("Employees");
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        #endregion


    }
}
