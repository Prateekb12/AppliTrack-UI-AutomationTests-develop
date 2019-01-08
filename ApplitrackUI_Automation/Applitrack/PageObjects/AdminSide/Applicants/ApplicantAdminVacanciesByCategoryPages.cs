using System.Text.RegularExpressions;
using ApplitrackUITests.PageObjects.PageTypes;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Applicants
{
    public class ApplicantAdminVacanciesByCategoryPages : ScrollTableType 
    {
        private IWebDriver _driver;

        #region Constructor

        public ApplicantAdminVacanciesByCategoryPages(IWebDriver driver) : base(driver)
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
        /// Get the Job ID from the table header
        /// </summary>
        /// <returns>The Job ID from the table header</returns>
        public string GetTableJobId()
        {
            var jobHeader = _driver.FindElement(By.CssSelector("html body form div#Header div table tbody tr td font")).Text;
            var jobId = Regex.Split(jobHeader, @"\W+");
            return jobId[1];
        }

        /// <summary>
        /// Get the applicant count from the table header
        /// </summary>
        /// <returns>The applicant count from the header</returns>
        public string GetTableApplicantCount()
        {
            var tableCount = _driver.FindElement(By.XPath("/html/body/form[2]/div[1]/div/table/tbody/tr[2]/td/font")).Text;
            var count = Regex.Split(tableCount, @"\W+");
            return count[0];
        }

        #endregion


    }
}
