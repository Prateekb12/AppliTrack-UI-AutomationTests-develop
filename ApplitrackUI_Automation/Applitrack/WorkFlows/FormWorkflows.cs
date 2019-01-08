using System;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.AdminSide.Applicants;
using ApplitrackUITests.PageObjects.AdminSide.Forms;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using ApplitrackUITests.PageObjects.Menu;
using Automation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.WorkFlows
{
    public class FormWorkflows : BasePageObject
    {
        private readonly IWebDriver _driver;
        private readonly ExtentTest _test;
        public FormWorkflows(IWebDriver Driver, ExtentTest test)
        {
            _driver = Driver;
            _test = test;
        }

        /// <summary>
        /// Create a new, blank, form for testing purposes. To use, assign this function to a string.
        /// </summary>
        /// <returns>The ID of the newly created form.</returns>
        public int CreateForm()
        {
            var formData = new FormData();
            var mainMenu = new MainMenu(_driver);
            var formMenu = new SubMenuForms(_driver);
            var formPages = new FormPages(_driver);

            // navigate to Forms > Design Forms and Packets > Create New Form
            _driver.SwitchToDefaultFrame();
            mainMenu.ClickMainMenuTab();
            mainMenu.ClickForms();
            formMenu.ClickDesignFormsandPackets();
            formMenu.ClickCreateNewForm();
            _test.Log(LogStatus.Pass, "Navigate to Forms > Design Forms and Packets > Create New Form");

            // click 'A blank form'
            _driver.SwitchToFrameById("MainContentsIFrame");
            formPages.CreateNewFormPage.ClickBlankForm();
            _test.Log(LogStatus.Pass, "Click 'A blank form'");

            // enter form info
            _driver.SwitchToFrameById("tabs_Panel");
            formPages.EditAndCreateFormPage.PropertiesTab.ClickStandardFormRadioButton();
            formPages.EditAndCreateFormPage.PropertiesTab.FillOutFormTitle(formData.FormTitle);
            _test.Log(LogStatus.Pass, "Enter form information");

            // save
            _driver.SwitchToDefaultFrame();
            _driver.SwitchToFrameById("MainContentsIFrame");
            formPages.EditAndCreateFormPage.ClickSaveButton();
            _test.Log(LogStatus.Pass, "Save the form");
            return(formPages.EditAndCreateFormPage.GetFormId());
        }
    
        /// <summary>
        /// Delete a form for testing purposes. This will only work if the form to be deleted has not been used.
        /// </summary>
        /// <param name="formId">The ID of the form to be deleted.</param>
        public void DeleteForm(int formId)
        {
            Console.WriteLine("WindowHandle at Start: " + _driver.GetHashCode().ToString());

            var mainMenu = new MainMenu(_driver);
            var formMenu = new SubMenuForms(_driver);
            var formPages = new FormPages(_driver);

            Console.WriteLine("Attemping to delete form with ID {0}", formId);

            // navigate to Forms > Design Forms and Packets > Edit Forms
            _driver.SwitchToDefaultFrame();
            mainMenu.ClickMainMenuTab();
            mainMenu.ClickForms();
            formMenu.ClickDesignFormsandPackets();
            formMenu.ClickEditForms();
            _test.Log(LogStatus.Pass, "Navigate to Forms > Design Forms and Packets > Edit Forms");

            // select the form in the list
            _driver.SwitchToFrameById("MainContentsIFrame");
            formPages.EditFormsPage.SelectForm(formId);
            _test.Log(LogStatus.Pass, "Select the form in the list");

            // delete the form
            _driver.SwitchToDefaultFrame();
            formMenu.ClickDeleteForms();
            formPages.EditFormsPage.ConfirmDeletion();
            _test.Log(LogStatus.Pass, "Delete and confirm");
        }


        /// <summary>
        /// Delete a form that was sent to an applicant.
        /// </summary>
        /// <param name="formKey">A system generated string that uniquely identifies the form that was sent</param>
        public void DeleteSentForm(string formKey)
        {
            var applicantProfilePages = new ApplicantProfilePages(_driver);
            try
            {
                applicantProfilePages.ListAllForms.ClickDeleteForm(formKey);

                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));

                wait.Until(ExpectedConditions.AlertIsPresent());
                _driver.SwitchTo().Alert().Accept();

                wait.Until(ExpectedConditions.AlertIsPresent());
                _driver.SwitchTo().Alert().Accept();
            }
            // Selenium seems to have trouble with alerts when the tests are running in parallel,
            // This catches the exception and puts a warning in the log
            catch (WebDriverTimeoutException)
            {
                _test.Log(LogStatus.Warning, "Timeout while waiting for alerts to delete the sent form.");
            }
        }
    }
}
