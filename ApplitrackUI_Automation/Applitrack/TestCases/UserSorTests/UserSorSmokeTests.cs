using System;
using System.Net;
using System.Threading;
using ApplitrackUITests.DataAccess;
using ApplitrackUITests.DataGenerators;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recruit.Core.Models;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.TestCases.UserSorTests
{
    [TestClass]
    public class UserSorSmokeTests : ApplitrackUIBase
    {
        #region Setup and TearDown

        private ExtentTest _test;
        private const int WaitTime = 5;

        /// <summary>
        /// Test Initialize Contains items to run before each [TestMethod]
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            // extent reports setup
            _test = ExtentTestManager.StartTest(TestContext.Properties["TestCaseName"].ToString(),
                TestContext.Properties["TestCaseDescription"].ToString())
                .AssignCategory("Smoke");
        }

        #endregion

        #region Test Cases
        [TestMethod]
        [TestCategory("UserSorSmoke")]
        [TestProperty("TestArea", "UserSor")]
        [TestProperty("TestCaseName", "User Service Create User")]
        [TestProperty("TestCaseDescription", "Verify that the service returns the correct data after creating a user in Recruit")]
        [TestProperty("UsesHardcodedData", "false")]
        public void UserSor_Create_User()
        {
            var user = new UserGenerator();

            try
            {
                user.CreateInDatabase();
                _test.Log(LogStatus.Info, $"Created user: {user.Id} - {user.UserName}");

                // wait a few seconds for the data to be sent
                Thread.Sleep(TimeSpan.FromSeconds(WaitTime));

                var userSyncData =
                    ApiHelpers.GetProductAccessUser($"{TestEnvironment.DefaultUserType}",
                    new IdCreator(TestEnvironment.ClientCode, (int) user.Id, user.UserName).ToString());

                Assert.IsTrue(String.IsNullOrWhiteSpace(userSyncData["Flid"].ToString()), "The Flid is not null");
                _test.Log(LogStatus.Pass, "The Flid is null");

                Assert.AreEqual(user.FirstName, userSyncData["FirstName"], "First name does not match");
                _test.Log(LogStatus.Pass, "The first names match");

                Assert.AreEqual(user.LastName, userSyncData["LastName"], "Last name does not match");
                _test.Log(LogStatus.Pass, "The last names match");

                Assert.AreEqual(user.Email, userSyncData["Email"], "Email does not match");
                _test.Log(LogStatus.Pass, "The emails match");

                Assert.IsTrue(String.IsNullOrWhiteSpace(userSyncData["ExternalId"].ToString()), "ExternalId is not null");
                _test.Log(LogStatus.Pass, "The external ID is null");

                user.DeleteFromDatabase();
            }
            catch (Exception e)
            {
                ReportException(e);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("UserSorSmoke")]
        [TestProperty("TestArea", "UserSor")]
        [TestProperty("TestCaseName", "User Service Update User")]
        [TestProperty("TestCaseDescription", "Verify that the service returns the correct data after updating a user")]
        [TestProperty("UsesHardcodedData", "false")]
        public void UserSor_Update_User()
        {
            var user = new UserGenerator();
            var userDataAccessor = new UserDataAccessor();

            try
            {
                user.CreateInDatabase();
                _test.Log(LogStatus.Info, $"Created user: {user.Id} - {user.UserName}");

                var createdUser = userDataAccessor.GetUser(user.Id);
                createdUser.FullName = "Newfirst Newlast";
                createdUser.Email = "updated@testing.com";
                userDataAccessor.UpdateUser(createdUser);

                // wait a few seconds for the data to be sent
                Thread.Sleep(TimeSpan.FromSeconds(WaitTime));

                var userSyncData =
                    ApiHelpers.GetProductAccessUser($"{TestEnvironment.DefaultUserType}",
                    new IdCreator(TestEnvironment.ClientCode, (int) user.Id, user.UserName).ToString());

                Assert.AreEqual(createdUser.FirstName, userSyncData["FirstName"], "First name does not match");
                _test.Log(LogStatus.Pass, "The changed first name matches");

                Assert.AreEqual(createdUser.LastName, userSyncData["LastName"], "Last name does not match");
                _test.Log(LogStatus.Pass, "The changed last name matches");

                Assert.AreEqual(createdUser.Email, userSyncData["Email"], "Email does not match");
                _test.Log(LogStatus.Pass, "The changed email matches");

                user.DeleteFromDatabase();
            }
            catch (Exception e)
            {
                ReportException(e);
                throw;
            }
        }

        [TestMethod]
        [TestCategory("UserSorSmoke")]
        [TestProperty("TestArea", "UserSor")]
        [TestProperty("TestCaseName", "User Service Delete User")]
        [TestProperty("TestCaseDescription", "Verify that the service returns the correct data after deleting a user")]
        [TestProperty("UsesHardcodedData", "false")]
        public void UserSor_Delete_User()
        {
            var user = new UserGenerator();

            try
            {
                user.CreateInDatabase();
                _test.Log(LogStatus.Info, $"Created user: {user.Id} - {user.UserName}");
                user.DeleteFromDatabase();
                _test.Log(LogStatus.Info, "Deleted user");

                // wait a few seconds for the data to be sent
                Thread.Sleep(TimeSpan.FromSeconds(WaitTime));

                var userSyncData =
                    ApiHelpers.GetProductAccessUserResponse(
                        $"{TestEnvironment.DefaultUserType}",
                        new IdCreator(TestEnvironment.ClientCode, (int) user.Id, user.UserName).ToString());

                Assert.AreEqual(userSyncData.StatusCode, HttpStatusCode.NotFound, "The API did not respond with a 404");
                _test.Log(LogStatus.Pass, "The API responded with a 404");
            }
            catch (Exception e)
            {
                ReportException(e);
                throw;
            }
        }

        #endregion
    }
}
