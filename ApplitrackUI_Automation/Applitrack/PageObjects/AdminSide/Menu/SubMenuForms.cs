using System;
using ApplitrackUITests.Helpers;
using Automation;
using Automation.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ApplitrackUITests.PageObjects.AdminSide.Menu
{
    public class SubMenuForms : BasePageObject
    {
        private readonly IWebDriver _driver;

        #region Constructors

        public SubMenuForms(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors
        // "My Send Forms" link
        [FindsBy(How = How.LinkText, Using = "My Sent Forms")]
        private IWebElement MySentForms { get; set; }

        // "Send a Form" link
        [FindsBy(How = How.LinkText, Using = "Send a Form")]
        private IWebElement SendForm { get; set; }

        // "View Submitted Forms By Category" link
        [FindsBy(How = How.LinkText, Using = "View Submitted Forms By Category")]
        private IWebElement ViewSubmittedFormsByCategory { get; set; }

        // "Design Forms and Packets" link
        [FindsBy(How = How.LinkText, Using = "Design Forms and Packets")]
        private IWebElement DesignFormsandPackets { get; set; }

        /************************
         * Main Menu > Forms > Design Forms and Packets
         ***********************/
        // "Edit Forms" link
        [FindsBy(How = How.LinkText, Using = "Edit Forms")]
        private IWebElement EditForms { get; set; }

        // "Create New Form" link
        [FindsBy(How = How.LinkText, Using = "Create New Form")]
        private IWebElement CreateNewForm { get; set; }

        /************************
         * "Edit Forms" page menu items
         ***********************/
        // Delete Form link that appears after selecting forms on the Edit Form page
        [FindsBy(How = How.LinkText, Using = "Delete Forms")]
        private IWebElement DeleteForms { get; set; }

        #endregion

        #region Page actions

        /// <summary>
        /// Click the "My Sent Forms" link from the "Forms" submenu
        /// </summary>
        public void ClickMySentForms()
        {
            MySentForms.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the 'Send a Form' link from the 'Forms' submenu
        /// </summary>
        public void ClickSendForm()
        {
            SendForm.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the 'View Submitted Forms By Category' link from the 'Forms' submenu
        /// </summary>
        public void ClickViewSubmittedFormsByCategory()
        {
            ViewSubmittedFormsByCategory.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the 'Design Forms and Packets' from the 'Forms' submenu
        /// </summary>
        public void ClickDesignFormsandPackets()
        {
            DesignFormsandPackets.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the "Edit Forms" link
        /// </summary>
        public void ClickEditForms()
        {
            EditForms.WaitRetry(_driver).Click();
        }

        /// <summary>
        /// Click the "Create New Form" link in the "Design Forms and Packets" Menu
        /// </summary>
        public void ClickCreateNewForm()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(120));
            wait.Until(d => CreateNewForm.Displayed);
            CreateNewForm.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the "Delete" link on the "Edit Forms" page
        /// </summary>
        public void ClickDeleteForms()
        {
            DeleteForms.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click a menu item with the given word
        /// </summary>
        /// <param name="category">The menu item to click</param>
        public void ClickCategory(string category)
        {
            // Wait until the link is displayed until attempting to click on it
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            var categoryLink = _driver.FindElement(By.PartialLinkText(category));
            wait.Until(ExpectedConditions.ElementToBeClickable(categoryLink));

            categoryLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Get the header of the 'By Status' submenu in Forms > View Submitted Forms By Category
        /// </summary>
        /// <returns>The header text of the submenu</returns>
        public string GetByStatusHeaderText()
        {
            var header = _driver.FindElement(By.Id("ListViewFormByStatus_Li1"));
            return header.Text;
        }

        #endregion
    }
}