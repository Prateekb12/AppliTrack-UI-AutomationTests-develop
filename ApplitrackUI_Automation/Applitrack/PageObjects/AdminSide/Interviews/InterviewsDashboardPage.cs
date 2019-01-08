using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Interviews
{
    public class InterviewsDashboardPage : BasePageObject, IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructor

        public InterviewsDashboardPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        [FindsBy(How = How.CssSelector, Using = "div.SkinnyTitleText")]
        private IWebElement HeaderText { get; set; }
        #endregion

        #region Related Pages
        public bool IsDisplayed()
        {
            try
            {
                return HeaderText.Text.Contains("My Interviews");
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        #endregion
    }
}
