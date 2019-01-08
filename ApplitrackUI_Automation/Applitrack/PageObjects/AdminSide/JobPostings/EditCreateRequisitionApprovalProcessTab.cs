using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ApplitrackUITests.PageObjects.AdminSide.JobPostings
{
    public class EditCreateRequisitionApprovalProcessTab : BasePageObject
    {
        private IWebDriver _driver;

        #region Constructor

        public EditCreateRequisitionApprovalProcessTab(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        #endregion

        #region UI Selectors

        // The Final Approver dropdown
        [FindsBy(How = How.Id, Using = "FinalApprover")]
        private IWebElement FinalApproverDropDown { get; set; }

        #endregion

        #region Related Pages
        #endregion

        #region Page Actions

        /// <summary>
        /// Select the Final Approver from the drop-down on the Approval Process tab
        /// </summary>
        /// <param name="name">The name to select, must be an exact match</param>
        public void SelectFinalApprover(string name)
        {
            SelectElement finalApproverDropDown = new SelectElement(FinalApproverDropDown);

            finalApproverDropDown.SelectByText(name);
        }


        #endregion


    }
}
