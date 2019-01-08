using System.Collections.Generic;
using Automation;
using OpenQA.Selenium;

namespace ApplitrackUITests.Helpers
{
    class WindowHelpers : BasePageObject
    {
        private IWebDriver Driver;
        public WindowHelpers(IWebDriver Driver)
        {
            this.Driver = Driver;
        }


        public void SwitchToPopup()
        {
            var mainHandle = Driver.CurrentWindowHandle; // the main window
            var poupHandle = string.Empty; // the popup window
            IReadOnlyCollection<string> windowHandles = Driver.WindowHandles; // contains all currently open windows

            // find the popup window
            foreach (var handle in windowHandles)
            {
                if (handle == mainHandle) continue;
                poupHandle = handle;
                break;
            }

            Driver.SwitchTo().Window(poupHandle);
        }

        public void ClosePopup()
        {
            var popupHandle = Driver.CurrentWindowHandle;
            var mainHandle = string.Empty;
            IReadOnlyCollection<string> windowHandles = Driver.WindowHandles;

            foreach (var handle in windowHandles)
            {
                if (handle != popupHandle)
                {
                    mainHandle = handle;
                }
            }

            Driver.Close();
            Driver.SwitchTo().Window(mainHandle);
        }


    }
}
