using System;
using System.Collections.Generic;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ApplitrackUITests.Helpers
{
    // TODO use this in all places with a scrolltable
    // TODO add methods to interact with other elements
    // TODO refactor for reusability
    public class ScrollTable : BasePageObject
    {
        private IWebDriver _driver;

        // The checkboxes in the Edit Forms list
        [FindsBy(How = How.CssSelector, Using = "input[name='ID']")]
        private IList<IWebElement> ItemCheckboxes { get; set; }

        // the page count
        [FindsBy(How = How.Id, Using = "BottomLeft")]
        private IWebElement NumPages { get; set; }

        public ScrollTable(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }



        /// <summary>
        /// Select the checkbox for the item
        /// </summary>
        /// <param name="itemId">The ID of the item to check</param>
        public void SelectItem(int itemId)
        {
            // Wait until the loading screen is no longer displayed
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait.Until( d => !d.FindElement(By.Id("Loading")).Displayed);

            var itemPosition = GetItemPosition(itemId);
            if (itemPosition <= -1) return; // checks to see if the item was found
            ItemCheckboxes[itemPosition].ScrollIntoView(_driver);
            ItemCheckboxes[itemPosition].WaitAndClick(_driver);
        }

        /// <summary>
        /// Find an item in a scrolltable given an ID
        /// </summary>
        /// <param name="itemId">The ID of the item to find</param>
        /// <returns>True if the given item exists in the list, false otherwise</returns>
        public bool FindItem(int itemId)
        {
            return GetItemPosition(itemId) != -1;
        }

        /// <summary>
        /// Find an item in a scrolltable given a name and a list of elements
        /// </summary>
        /// <param name="name">The name of the item to find</param>
        /// <param name="elements">A list of IWebElements to look for the item in</param>
        /// <returns>True if the given name is found in the list of elements, false otherwise</returns>
        public bool FindItem(string name, IList<IWebElement> elements)
        {
            var maxPage = GetMaxPage();
            var currentPage = GetCurrentPage();

            while (currentPage <= maxPage)
            {
                foreach (var element in elements)
                {
                    if (element.Text.Contains(name)) return true;
                }

                GoToNextPage();
                currentPage++;
            }

            return false;
        }

        /// <summary>
        /// Get the items position in the list.
        /// Can be used to test of the item is in the list or not.
        /// </summary>
        /// <param name="itemId">The ID of the item to find</param>
        /// <returns>The the index of the item, returns -1 if not found</returns>
        private int GetItemPosition(int itemId)
        {
            var itemIndex = -1;
            var found = false;
            var currentPage = 1; // assume that the list starts on page 1
            var maxPage = GetMaxPage();

            while (!found && currentPage <= maxPage)
            {
                for (var i = 0; i < ItemCheckboxes.Count; i++)
                {
                    if (ItemCheckboxes[i].GetAttribute("value") != itemId.ToString()) continue;
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

        public void ClickItemId(int itemId)
        {
            // TODO find a more elegant way of finding the itemLink element
            GetItemPosition(itemId);
            var itemLink = _driver.FindElement(By.PartialLinkText(itemId.ToString()));
            itemLink.ScrollIntoView(_driver);
            itemLink.WaitAndClick(_driver);
        }

        public void GoToNextPage()
        {
             ((IJavaScriptExecutor)_driver).ExecuteScript("page('Down');");
        }

        /// <summary>
        /// Get the last page of the list
        /// </summary>
        /// <returns>The last page of the list</returns>
        public int GetMaxPage()
        {
            // Get the max page by returning the number after the '/'
            // This assumes that there is only one '/' in the string and that the max page is the number after it
            var split = NumPages.WaitRetry(_driver).Text.Split('/');
            return Int32.Parse(split[1].Trim());
        }

        /// <summary>
        /// Get the current page of the list
        /// </summary>
        /// <returns>The current page of the list</returns>
        public int GetCurrentPage()
        {
            // Get the max page by returning the number before the '/'
            // This assumes that there is only one '/' in the string and that the current page is the number before it
            var split = NumPages.WaitRetry(_driver).Text.Split('/');
            return Int32.Parse(split[0].Replace("Pg. ", "").Trim());;
        }

    }
}
