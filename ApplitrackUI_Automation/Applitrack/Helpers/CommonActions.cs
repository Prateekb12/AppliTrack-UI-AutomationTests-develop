using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace ApplitrackUITests.Helpers
{
    public class CommonActions
    {
        private readonly IWebDriver _driver;
        private readonly CommonWaits _commonWaits;

        public CommonActions(IWebDriver driver)
        {
            _driver = driver;
            _commonWaits = new CommonWaits(_driver);
        }

        public void SwitchToMainContentsIFrame()
        {
            _commonWaits.WaitForLoadingScreen();
            _driver.SwitchToFrameById("MainContentsIFrame");
        }
    }
}
