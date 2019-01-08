using System.Text.RegularExpressions;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.JobPostings
{
    public class AllPostingsByStatusPage : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public AllPostingsByStatusPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Get the count from the table header
        /// </summary>
        /// <returns></returns>
        public string GetJobPostingCount()
        {
            var header = _driver.FindElement(By.CssSelector("div#Header")).Text;
            // split up the words in the header
            var count = Regex.Split(header, @"\W+");
            return count[2];
        }

        #endregion


    }
}
