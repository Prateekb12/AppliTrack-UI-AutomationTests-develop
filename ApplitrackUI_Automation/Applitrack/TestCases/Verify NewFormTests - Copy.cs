using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using System.Threading;
namespace Automation

{
    [TestClass]
    public class CreateNewFormTests : ElementOperations
    {

        private string BT; //Pass BT as it changes between methods

        [TestInitialize]
        public void testInit()
        {
            //Browser Default 
            //Value Stored in excel

            BT = _BT;

        }

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "create New Form")]
        [TestProperty("TestCaseID", "TC1228")]
        [TestProperty("TestCaseName", "Verify Form Created Successfully")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"|DataDirectory|\DataFiles\CrossBrowser2.csv", "CrossBrowser2#csv", DataAccessMethod.Sequential)]

        public void CreateNewForms()

        {
            TestSetup(BT);

            BrowseTo(_baseURL, getwebdriver());

            login(); //login into application
            //1.click on Form menu
            ClickMenuLink("Forms");
            //2.Click on design "design forms and packets"
            ClickSubMenuLink("Design Forms and Packets");

            //3.Click on "Create new from"
            ClickSubMenuLink("Create New Form");

            switchtoFramByLocator(FileOperations.getElement("Frame1_id"));
            // 4.Click on "blank form".
            Click(FileOperations.getElement("BlankFormlink_xpath"));


            switchtoFramByLocator(FileOperations.getElement("Frame2_id"));
            ////5.Fill form details: 
            //• Select form type
            Click(FileOperations.getElement("StandardRadiobtn_xpath"));
            //• Enter title
            TypeText(FileOperations.getElement("FormTitle_xpath"), "testform");
            switchtoDefault();
            //• Click on "save " button
            switchtoFramByLocator(FileOperations.getElement("Frame1_id"));
            Click(FileOperations.getElement("FormSaveBtn_xpath"));

            Click(FileOperations.getElement("FormPermissionTab_xpath"));
            Click(FileOperations.getElement("FormSaveBtn_xpath"));

            Click(FileOperations.getElement("FormWorkflowTab_xpath"));

            Click(FileOperations.getElement("FormSaveBtn_xpath"));

            Click(FileOperations.getElement("FormAppearanceTab_xpath"));

            Click(FileOperations.getElement("FormSaveBtn_xpath"));

            VerifyText(FileOperations.getElement("SavedFormmsg_id"), "Form Saved");
           
            switchtoDefault();

            //6.Click on "Create new form"
            ClickSubMenuLink("Create New Form");
            Thread.Sleep(2000);
            switchtoFramByLocator(FileOperations.getElement("Frame1_id"));
            //7.Click on "an existing form"

            Click(FileOperations.getElement("ExistingForm_xpath"));

            //• Click on active form
            Click(FileOperations.getElement("ActiveForm_xpath"));
            //• Select first form
            Click(FileOperations.getElement("FirstExistingForm_id"));
            switchtoFramByLocator(FileOperations.getElement("Frame2_id"));
            //• Enter title name
            TypeText("//*[@id='TextboxFormTitle']", "abcd");
            switchtoFramByLocator(FileOperations.getElement("Frame1_id"));
            Click(FileOperations.getElement("FormSaveBtn_xpath"));
            Click(FileOperations.getElement("FormPermissionTab_xpath"));
            Click(FileOperations.getElement("FormSaveBtn_xpath"));

            Click(FileOperations.getElement("FormWorkflowTab_xpath"));

            Click(FileOperations.getElement("FormSaveBtn_xpath"));

            Click(FileOperations.getElement("FormAppearanceTab_xpath"));

            Click(FileOperations.getElement("FormSaveBtn_xpath"));

            VerifyText(FileOperations.getElement("SavedFormmsg_id"), "Form Saved");

            switchtoDefault();

            var Login = new Pages.LoginPage(getwebdriver());
           // Login.ClickLogOut();
           
            

        }

        [TestCleanup]
        public void teardown()
        {
            getwebdriver().Close();
        }
    }

}
