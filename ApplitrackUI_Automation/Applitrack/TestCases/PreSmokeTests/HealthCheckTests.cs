using System;
using System.Configuration;
using System.Linq;
using System.Net;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.PreSmokeTests
{
    [TestClass]
    public class HealthCheckTests : ApplitrackUIBase
    {
        #region Setup and TearDown

        private ExtentTest test;

        /// <summary>
        /// Test Initialize Contains items to run before each [TestMethod]
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            // extent reports setup
            test = ExtentTestManager.StartTest(TestContext.Properties["TestCaseName"].ToString(),
                TestContext.Properties["TestCaseDescription"].ToString())
                .AssignCategory("Smoke");

            // No selenium needed for this at the moment...
        }

        #endregion

        #region Test Cases

        [TestMethod]
        [TestCategory("Smoke")]
        [TestProperty("TestArea", "HealthCheck")]
        [TestProperty("TestCaseName", "Health Check OK")]
        [TestProperty("TestCaseDescription", "Health Check OK")]
        [TestProperty("UsesHardcodedData", "false")]
        public void Health_Check_OK()
        {
            var environmentUrl = new Uri(ConfigurationManager.AppSettings["BaseUrl.ApplitrackLoginPage"]);
            var environment = environmentUrl.Host.Split('.').First();
           
            // Assume that the URL from the config uses a subdomain or uses www, if not this will probably break
            var healthCheckUrl = "https://" + environment + ".applitrack.com/healthcheck.aspx";

            try  //Contains Contents of Test
            {
                var healthStatus = LinkHelpers.GetLinkStatusCode(healthCheckUrl);

                Assert.IsTrue(healthStatus == HttpStatusCode.OK, 
                    "Health check returned: {0}", healthStatus);

                test.Log(LogStatus.Pass, "Health check (" + healthCheckUrl + ") returned HTTP status of OK");
            }
            catch (Exception e) //On Error Do
            {
                throw;
            }
        }

        #endregion

    }
}
