using System.Collections.Generic;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.JobPostings
{
    public class MyDraftRequisitionsPage : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public MyDraftRequisitionsPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // The checkbox in the list
        [FindsBy(How = How.CssSelector, Using = "input[name='ID']")]
        private IList<IWebElement> ListingCheckBox { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        public bool RequisitionInList(int jobId)
        {
            var ndx = -1; // the index of the checkbox containing the ID
            var found = false;

            while (ndx == -1)
            {
                for (var i = 0; i < ListingCheckBox.Count; i++)
                {
                    if (ListingCheckBox[i].GetAttribute("value") != jobId.ToString()) continue;
                    ndx = i;
                    found = true;
                    break;
                }
                if (ndx == -1)
                {
                    ((IJavaScriptExecutor) _driver).ExecuteScript("page('Down')");
                }
            }
            return found;
        }

        #endregion


    }
}
