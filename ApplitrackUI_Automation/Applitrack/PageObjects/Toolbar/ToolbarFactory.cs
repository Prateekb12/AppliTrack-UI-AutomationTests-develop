using System;
using System.Configuration;
using OpenQA.Selenium;

namespace ApplitrackUITests.PageObjects.Toolbar
{
    /// <summary>
    /// Use the Sidekick toolbar if IDM is enabled, otherwise use the standard Applitrack toolbar
    /// </summary>
    public static class ToolbarFactory
    {
        private static readonly bool SidekickEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["SidekickEnabled"]);
        private static readonly bool IdmEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["IDMEnabled"]);

        /// <summary>
        /// Use either the Sidekick toolbar or the Applitrack toolbar if IDM or Sidekick is enabled in the App.config
        /// </summary>
        /// <param name="driver">Selenium Webdriver</param>
        /// <returns>The Sidekick or Applitrack toolbar</returns>
        public static Toolbar Get(IWebDriver driver)
        {
            return SidekickEnabled || IdmEnabled ? new SidekickToolbar(driver) as Toolbar : new ApplitrackToolbar(driver);
        }
    }
}
