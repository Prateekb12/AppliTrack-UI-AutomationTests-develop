using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.JobPostings
{
    public class PostingsByCategoryPages : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public PostingsByCategoryPages(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        [FindsBy(How = How.CssSelector, Using = "html body form div#MainRight table#maintbl.ScrollTable tbody tr#rowNbr0 td b")]
        private IWebElement TableCategoryHeader { get; set; }
        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Get the table category
        /// </summary>
        /// <returns>The text of the table's category</returns>
        public string GetTableCategoryHeader()
        {
            return TableCategoryHeader.Text;
        }

        #endregion



    }
}
