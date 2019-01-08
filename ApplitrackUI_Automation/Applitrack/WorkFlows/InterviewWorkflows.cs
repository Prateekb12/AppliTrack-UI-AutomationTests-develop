using System;
using ApplitrackUITests.PageObjects;
using ApplitrackUITests.PageObjects.AdminSide.Menu;
using OpenQA.Selenium;

namespace ApplitrackUITests.WorkFlows
{
    public class InterviewWorkflows : ApplitrackUIBase
    {
        private IWebDriver Driver;
        public InterviewWorkflows(IWebDriver Driver)
        {
            this.Driver = Driver;
        }

        public void CreateInterview()
        {
            var mainMenu = new MainMenu(Driver);

            try  //Contains Contents of Test
            {

            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }
    
        /// <summary>
        /// Delete a form for testing purposes. This will only work if the form to be deleted has not been used.
        /// </summary>
        /// <param name="newFormId">The ID of the form to be deleted.</param>
        public void DeleteInterview(string formId)
        {
            var mainMenu = new MainMenu(Driver);

            try  //Contains Contents of Test
            {

            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }
    }
}
