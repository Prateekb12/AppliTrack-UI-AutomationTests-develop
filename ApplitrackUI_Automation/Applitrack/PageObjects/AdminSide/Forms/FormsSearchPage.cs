using ApplitrackUITests.PageObjects.PageTypes;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Forms
{
    public class FormsSearchPage : ScrollTableType, IApplitrackPage
    {
        private IWebDriver _driver;

        #region Constructor

        public FormsSearchPage(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion


        #region Page Actions

        public bool IsDisplayed()
        {
            var header = _driver.FindElement(By.ClassName("ReportHeadTitle"));
            return header.Text.Contains("Forms Search");
        }

        #endregion
    }
}
