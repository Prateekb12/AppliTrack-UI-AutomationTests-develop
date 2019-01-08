using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.Helpers
{
    public class CommonWaits
    {
        private readonly IWebDriver _driver;

        [FindsBy(How = How.CssSelector, Using = "iframe#Loading")]
        private IWebElement LoadingFrame { get; set; }

        public CommonWaits(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        /// <summary>
        /// Wait for the 'Loading' screen to dissapear so we can interact with elements
        /// </summary>
        public void WaitForLoadingScreen()
        {
            _driver.Wait().Until(d => LoadingFrame.GetAttribute("class").Contains("ng-hide"));
        }
    }
}
