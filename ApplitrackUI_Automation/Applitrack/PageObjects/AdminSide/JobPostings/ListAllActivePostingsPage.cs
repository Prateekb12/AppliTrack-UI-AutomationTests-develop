using System;
using System.Collections.Generic;
using System.Linq;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.JobPostings
{
    public class ListAllActivePostingsPage : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public ListAllActivePostingsPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // The checkbox in the list
        [FindsBy(How = How.CssSelector, Using = "input[name='ID']")]
        private IList<IWebElement> ListingCheckBox { get; set; }

        // The 'Inactivate Postings' link that appears in the menu after marking a checkbox
        [FindsBy(How = How.LinkText, Using = "Inactivate Postings")]
        private IWebElement InactivatePostingsLink { get; set; }

        // the yes button on the "Inactivate Form(s)" window
        [FindsBy(How = How.Name, Using = "resp")]
        private IWebElement YesButton { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions
        /// <summary>
        /// Mark the checkbox associated to a Job ID.
        /// </summary>
        /// <param name="jobId">The Job ID to be found and marked.</param>
        public void MarkListingCheckbox(int jobId)
        {
            try
            {
                int ndx = -1; // the index of the checkbox containing the ID

                while (ndx == -1)
                {
                    for (int i = 0; i < ListingCheckBox.Count(); i++)
                    {
                        if (ListingCheckBox[i].GetAttribute("value") == jobId.ToString())
                        {
                            ndx = i;
                            break;
                        }
                    }

                    // Mark the checkbox if the Job ID is found
                    if (ndx > -1)
                    {
                        ListingCheckBox[ndx].ScrollIntoView(_driver);
                        ListingCheckBox[ndx].WaitAndClick(_driver);
                    }
                    // If its not found, go to the next page
                    else
                    {
                        ((IJavaScriptExecutor)_driver).ExecuteScript("page('Down');");
                    }
                }
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Click the 'Inactivate Postings' menu link when a record is marked in the 'List All Active Job Postings' page
        /// </summary>
        public void ClickInactivatePostings()
        {
            InactivatePostingsLink.WaitAndClick(_driver);
        }

        // TODO use window helpers
        /// <summary>
        /// Click the Yes button to inactivate a form
        /// </summary>
        public void ConfirmInactivation()
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
                YesButton.WaitAndClick(_driver);
                _driver.SwitchTo().Window(parentHandle);
        }


        #endregion
    }
}
