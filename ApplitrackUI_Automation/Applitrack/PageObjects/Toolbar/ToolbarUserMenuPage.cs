using System;
using ApplitrackUITests.PageObjects.PageTypes;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ApplitrackUITests.PageObjects.Toolbar
{
    public abstract class ToolbarUserMenuPage : BasePageObject, IApplitrackPage
    {

        private readonly IWebDriver _driver;

        #region Constructors

        protected ToolbarUserMenuPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        protected abstract IWebElement SignOut { get; set; }

        #endregion


        #region Page Actions

        public virtual bool IsDisplayed()
        {
            return SignOut.Displayed;
        }

        public virtual void ClickSignOut()
        {
            SignOut.WaitAndClick(_driver);
        }

        public virtual void ClickAccountSettings()
        {            
            var message = "This function does nothing by default other than exposing it to the outside world." +
                          "If you need it to do something, please override the function."; //e.g. SideKickUserMenuPage.cs
            throw new NotImplementedException(message);
        }

        #endregion



    }
}
