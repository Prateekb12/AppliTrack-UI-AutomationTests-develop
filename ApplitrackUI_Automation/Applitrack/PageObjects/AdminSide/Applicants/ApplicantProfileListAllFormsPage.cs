using System;
using System.Collections.Generic;
using System.Linq;
using ApplitrackUITests.Helpers;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ApplitrackUITests.PageObjects.AdminSide.Applicants
{
    public class ApplicantProfileListAllFormsPage : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public ApplicantProfileListAllFormsPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // The 'Delete' button for the list of forms
        [FindsBy(How = How.ClassName, Using = "btn-danger")]
        private IList<IWebElement> DeleteButtons { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions
        /// <summary>
        /// Check to see that the form was sent by seeing if it exists on the page
        /// </summary>
        /// <param name="formGuid">Unique, system generated key, used to find the form in the list</param>
        /// <returns>True if the form exists in the list, false otherwise</returns>
        public bool FormWasSent(string formGuid)
        {
            return FindDeleteButton(formGuid).Displayed;
        }

        /// <summary>
        /// Check to see that the form was approved
        /// </summary>
        /// <param name="formGuid">Unique, system generated key, used to find the form in the list</param>
        /// <returns>True if the form was approved, false otherwise</returns>
        public bool FormIsApproved(string formGuid)
        {
            var progressBar = GetFormDiv(formGuid).FindElement(By.ClassName("bar"));
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait.Until(d => progressBar.Displayed);
            return progressBar.GetAttribute("class").Contains("bar-success");
        }

        /// <summary>
        /// Check to see that the form was denied 
        /// </summary>
        /// <param name="formGuid">Unique, system generated key, used to find the form in the list</param>
        /// <returns>True if the form was approved, false otherwise</returns>
        public bool FormIsDenied(string formGuid)
        {
            var progressBar = GetFormDiv(formGuid).FindElement(By.ClassName("bar"));
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait.Until(d => progressBar.Displayed);
            return progressBar.GetAttribute("class").Contains("bar-danger");
        }

        private IWebElement GetFormDiv(string formGuid)
        {
            // Locate the correct form by finding the delete key and getting its parent element
            // The delete key contains the formGuid in its link
            // This is the easiest way I've discovered to do this, but it could probably be improved in the future..
            var parent = FindDeleteButton(formGuid);

            for (var i = 0; i < 2; i++)
            {
                parent = parent.GetParentElement();
            }

            return parent;
        }

        /// <summary>
        /// Delete the form that was sent
        /// </summary>
        /// <param name="formGuid">Unique, system generated key, used to find the form in the list</param>
        public void ClickDeleteForm(string formGuid)
        {
            var btn = FindDeleteButton(formGuid);
            btn.ScrollIntoView(_driver);
            btn.WaitAndClick(_driver);
        }

        private IWebElement FindDeleteButton(string formGuid)
        {
            return DeleteButtons.FirstOrDefault(btn => btn.GetAttribute("href").Contains(formGuid));
        }

        // TODO improve this
        /// <summary>
        /// Click the Edit Form link
        /// </summary>
        /// <param name="formGuid"></param>
        public void EditForm(string formGuid)
        {
            var formDiv = GetFormDiv(formGuid).FindElements(By.ClassName("btn-default"));
            foreach (var e in formDiv)
            {
                if (e.Text == "Edit")
                {
                    e.WaitAndClick(_driver);
                }
            }
        }


        #endregion
    }
}
