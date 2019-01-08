using ApplitrackUITests.PageObjects.PageTypes;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.AdminSide.Applicants
{
    public class ApplicantSearchResultsPage : ScrollTableType, IApplitrackPage
    {
        private readonly IWebDriver _driver;

        #region Constructors

        public ApplicantSearchResultsPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        #endregion

        #region Page Actions

        public bool IsDisplayed()
        {
            try
            {
                return NumPages.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        #endregion

    }
}
