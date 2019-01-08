using Automation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApplitrackUITests.TestCases
{
    [TestClass]
    public class ProjectManagerMenuTest : BaseFrameWork
    {
        /*

        //***********************************************
        //This Section Contains the needed SetUp Activites
        //for all the tests to execute below. 
        //***********************************************

        //Class Vars
        private string BT; //Pass BT as it changes between methods
        private IWebDriver Driver;
        //Load Test Helpers

        /// <summary>
        ///Test Initialize Contains items to 
        ///run before each [TestMethod]
        /// </summary>
        [TestInitialize]
        public void testInit()
        {
            //Browser Default 
            //Value Stored in App.Config 
            BT = _BT;
            //To override the Default BrowserType
            // Un-comment and Set Value below 
            //string BT = "firefox"; 
        }

        /// <summary>
        /// Build From Page Objects: builds a test directly from Page Object Actions
        /// </summary>
        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "Menu")]
        [TestProperty("TestCaseID", "TC1382")]
        [TestProperty("TestCaseName", "Validate ProjectManager Menu")]

        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", @"|DataDirectory|\DataFiles\CrossBrowser2.csv", "CrossBrowser2#csv", DataAccessMethod.Sequential)]

        public void ValidateProjectManagerMenu()
        {
            //Override Default BrowserType with DataSource BrowserType
            //BT = Convert.ToString(testContextInstance.DataRow["BrowserType"]);

            Driver = SetUp(BT); //Stand up Driver and Log Test
            Console.WriteLine("WindowHandle at Start: " + Driver.GetHashCode().ToString());

            var LoginData = new Data.LoginData();
            var LogingPage = new Pages.LoginPage(Driver);
            var MainMenu = new Pages.MainMenu(Driver);

            try  //Contains Contents of Test
            {
                BrowseTo(BaseUrls["ApplitrackLoginPage"], Driver);
                LogingPage.EnterUsername(LoginData.UserName);
                LogingPage.EnterPwd(LoginData.Password);
                LogingPage.ClickLogin();
                Driver.Manage().Window.Maximize();
                MainMenu.VerifyProjectManagerMenu();
                //Driver.Quit();
            }
            catch (Exception e) //On Error Do
            {
                HandleException(e, Driver);
                throw;
            }
        }      
         */
    }
}

