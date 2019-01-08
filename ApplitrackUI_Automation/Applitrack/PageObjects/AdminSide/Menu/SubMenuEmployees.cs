using System;
using System.Collections.Generic;
using ApplitrackUITests.Helpers;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ApplitrackUITests.PageObjects.AdminSide.Menu
{
    public class SubMenuEmployees : BasePageObject
    {
        private readonly IWebDriver _driver;
        #region Constructor

        public SubMenuEmployees(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        [FindsBy(How = How.CssSelector, Using = "li.Header")]
        public IWebElement MenuHeader { get; set; }

        [FindsBy(How = How.LinkText, Using = "Employee Dashboard")]
        public IWebElement EmployeeDashboard { get; set; }

        [FindsBy(How = How.LinkText, Using = "Create New Employee")]
        public IWebElement CreateNewEmployee { get; set; }

        [FindsBy(How = How.LinkText, Using = "Employee List")]
        public IWebElement EmployeeList { get; set; }

        [FindsBy(How = How.LinkText, Using = "Alpha Groups")]
        public IWebElement AlphaGroups { get; set; }

        [FindsBy(How = How.LinkText, Using = "Location")]
        public IWebElement Location { get; set; }

        [FindsBy(How = How.LinkText, Using = "Position")]
        public IWebElement Position { get; set; }

        //Create new employee Page

        [FindsBy(How = How.LinkText, Using = "A blank form")]
        public IWebElement BlankForm { get; set; }

        [FindsBy(How = How.Id, Using = "EmpTitle")]
        public IWebElement EmployeeTitle { get; set; }

        [FindsBy(How = How.Id, Using = "EmpFirstName")]
        public IWebElement EmployeeFirstName { get; set; }

        [FindsBy(How = How.Id, Using = "EmpLastName")]
        public IWebElement EmployeeLastName { get; set; }

        [FindsBy(How = How.Id, Using = "EmpAddress1")]
        public IWebElement EmployeeAddress1 { get; set; }

        [FindsBy(How = How.Id, Using = "EmpCity")]
        public IWebElement EmployeeCity { get; set; }

        [FindsBy(How = How.LinkText, Using = "Delete their file")]
        public IWebElement Delete { get; set; }

        [FindsBy(How = How.LinkText, Using = "Delete Checked Employees")]
        public IWebElement DeleteEmployee { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='Save']")]
        public IWebElement SaveButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name='resp']")]
        public IWebElement ConfirmDelete { get; set; }

        #endregion

        #region Page Actions

        public void ClickCreateNewEmployee()
        {
            CreateNewEmployee.WaitRetry(_driver).Click();
        }

        public void ClickEmployeeList()
        {
            EmployeeList.WaitAndClick(_driver);
        }

        public void ClickBlankForm()
        {
            BlankForm.WaitRetry(_driver).Click();
        }


        public void SelectEmpTitleItem(string matchItem)
        {
            EmployeeTitle.WaitRetry(_driver).Click();
            SelectElement se = new SelectElement(EmployeeTitle);
            se.SelectByText(matchItem);
        }

        public void FillEmployeeFirstName(string str)
        {
            EmployeeFirstName.WaitAndClick(_driver);
            EmployeeFirstName.Clear();
            EmployeeFirstName.SendKeys(str);
        }

        public void FillEmployeeLastName(string str)
        {
            EmployeeLastName.WaitAndClick(_driver);
            EmployeeLastName.Clear();
            EmployeeLastName.SendKeys(str);
        }

        public void FillEmployeeAddress1(string str)
        {
            EmployeeAddress1.WaitAndClick(_driver);
            EmployeeAddress1.Clear();
            EmployeeAddress1.SendKeys(str);
        }

        public void FillEmployeeCity(string str)
        {
            EmployeeCity.WaitAndClick(_driver);
            EmployeeCity.Clear();
            EmployeeCity.SendKeys(str);
        }

        public void ClickSaveButton()
        {
            SaveButton.WaitAndClick(_driver);
        }

        public void DeleteEmployeeFromList(string employeeName)
        {
            for (int i = 1; i < 50; i++)
            {
                String str = _driver.FindElement(By.XPath("//html/body/form[2]/div[6]/table[1]/tbody/tr[" + i + "]/td[1]")).Text;

                if (str.Equals(employeeName))
                {

                    IWebElement chkbx = _driver.FindElement(By.XPath("//html/body/form[2]/div[4]/table/tbody/tr[" + i + "]/td[1]/input"));
                    chkbx.WaitAndClick(_driver);
                    break;
                }
                else
                {
                    Console.Out.WriteLineAsync("Form not found");
                }
            }
        }

        public void ClickDelete()
        {
            Delete.WaitAndClick(_driver);
        }

        public void ClickDeleteEmployee()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementToBeClickable(DeleteEmployee));
            DeleteEmployee.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the Yes button to delete the Employee
        /// </summary>
        public void ConfirmDeleteEmployee()
        {
            string parentHandle = _driver.CurrentWindowHandle; // the main window
            string popupHandle = string.Empty; // the popup window
            IReadOnlyCollection<string> windowHandles = _driver.WindowHandles; // contains all currently open windows

            // find the popup window
            foreach (string handle in windowHandles)
            {
                if (handle != parentHandle)
                {
                    popupHandle = handle;
                    break;
                }
            }

            // switch to the popup window, click <Yes>, and switch back to the main window
            _driver.SwitchTo().Window(popupHandle);
            ConfirmDelete.WaitAndClick(_driver);
            _driver.SwitchTo().Window(parentHandle);
        }

        #endregion
    }
}
