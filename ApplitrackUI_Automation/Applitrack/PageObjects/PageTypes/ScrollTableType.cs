using System;
using System.Collections.Generic;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using ApplitrackUITests.Helpers;
using System.Threading;

namespace ApplitrackUITests.PageObjects.PageTypes
{
    // TODO use this in all places with a scrolltable
    // TODO add methods to interact with other elements
    // TODO refactor for reusability
    public abstract class ScrollTableType : BasePageObject
    {
        private readonly IWebDriver _driver;

        #region Constructors

        protected ScrollTableType(IWebDriver driver)
        {
            _driver = driver;
        }

        #endregion

        #region UI Selectors

        // The checkboxes in the Edit Forms list
        [FindsBy(How = How.CssSelector, Using = "input[name='ID']")]
        internal IList<IWebElement> ItemCheckboxes { get; set; }

        // The page count
        [FindsBy(How = How.Id, Using = "BottomLeft")]
        internal IWebElement NumPages { get; set; }

        // The yes button on the "Delete" window
        [FindsBy(How = How.Name, Using = "resp")]
        private IWebElement YesButton { get; set; }

        #endregion

        #region Page Actions

        /// <summary>
        /// Mark the checkbox for the item with the given ID
        /// </summary>
        /// <param name="itemId">The ID of the item to check</param>
        public virtual void MarkItem(string itemId)
        {
            // Wait until the table to load
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Header")));

            var itemPosition = GetItemPosition(itemId);
            if (itemPosition <= -1) return; // checks to see if the item was found
            ItemCheckboxes[itemPosition].ScrollIntoView(_driver);
            ItemCheckboxes[itemPosition].WaitRetry(_driver).Click();
        }

        public virtual bool ItemFound(string itemId)
        {
            return GetItemPosition(itemId) != -1;
        }

        /// <summary>
        /// Get the items position in the list.
        /// Can be used to test of the item is in the list or not.
        /// </summary>
        /// <param name="itemId">The ID of the item to find</param>
        /// <returns>The the index of the item, returns -1 if not found</returns>
        private int GetItemPosition(string itemId)
        {
            var itemIndex = -1;
            var found = false;
            var currentPage = 1; // assume that the list starts on page 1
            var maxPage = GetMaxPage();

            while (!found && currentPage <= maxPage)
            {
                for (var i = 0; i < ItemCheckboxes.Count; i++)
                {
                    if (ItemCheckboxes[i].GetAttribute("value") != itemId) continue;
                    itemIndex = i;
                    found = true;
                    break;
                }

                if (found) continue;
                GoToNextPage();

                currentPage++;
            }

            return itemIndex;
        }

        public virtual void ClickItemId(string itemId)
        {
            // TODO find a more elegant way of finding the itemLink element
            GetItemPosition(itemId);
            var itemLink = _driver.FindElement(By.PartialLinkText(itemId));
            itemLink.ScrollIntoView(_driver);
            itemLink.WaitAndClick(_driver);               
        }

        private void GoToNextPage()
        {
             ((IJavaScriptExecutor)_driver).ExecuteScript("page('Down');");
            Thread.Sleep(TimeSpan.FromSeconds(0.25)); // because going too fast can break things
        }

        /// <summary>
        /// Get the last page of the list
        /// </summary>
        /// <returns>The last page of the list</returns>
        private int GetMaxPage()
        {
            // Get the max page by returning the number after the '/'
            // This assumes that there is only one '/' in the string and that the max page is the number after it
            var split = NumPages.WaitRetry(_driver).Text.Split('/');
            return Int32.Parse(split[1].Trim());;
        }

        /// <summary>
        /// Click the Yes button to delete the record
        /// </summary>
        public void ConfirmDeletion()
        {
            _driver.SwitchToPopup();
            YesButton.WaitRetry(_driver).Click();
            _driver.ClosePopup();
        }

        #endregion
    }
}
