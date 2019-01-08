using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using ApplitrackUITests.DataGenerators;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace ApplitrackUITests.Helpers
{
    public static class ApiHelpers
    {
        private static readonly string FcEmployeeApi = ConfigurationManager.AppSettings.Get("FCEmployeeApi");
        private static readonly string IdmTokenApi = ConfigurationManager.AppSettings.Get("IDMTokenApi");
        private static readonly string ProductAccessApi = ConfigurationManager.AppSettings.Get("ProductAccessApi");

        private static string _authToken;
        private static string AuthToken => _authToken ?? (_authToken = GetToken(LoginData.SuperUserName, LoginData.SuperUserPassword));

        /// <summary>
        /// Perform a GET on 'EmployeeApi/api/organizations/{id}/employees' endpoint
        /// </summary>
        /// <param name="orgId">The organization ID to search for employees in</param>
        /// <param name="parameters">Dictionary containing all parameters to use</param>
        /// <returns>A JSON object containing the response of the API</returns>
        public static JObject GetOrgEmployees(int orgId, IDictionary<string, object> parameters = null)
        {
            var client = new RestClient(FcEmployeeApi);
            var request = new RestRequest
            {
                Resource = $"organizations/{orgId}/employees"
            };
            request.AddHeader("Authorization", $"Bearer {AuthToken}");
            request.AddParameter("organizationId", orgId);

            if (parameters == null)
            {
                return JObject.Parse(client.Execute(request).Content);
            }

            foreach (var param in parameters)
            {
                request.AddParameter(param.Key, param.Value);
            }

            return ExecuteAndParseRequest(client, request);
        }

        /// <summary>
        /// Perform a DELETE on '/EmployeeApi/api/employees/{id}'
        /// </summary>
        /// <param name="empId">The ID of the employee to delete</param>
        /// <param name="version">The version of the employee to delete</param>
        /// <returns>A JSON object containing the response of the API</returns>
        public static JObject DeleteEmployee(string empId, int version)
        {
            var client = new RestClient(FcEmployeeApi);
            var request = new RestRequest(Method.DELETE)
            {
                Resource = $"employees/{empId}"
            };
            request.AddHeader("Authorization", $"Bearer {AuthToken}");
            request.AddParameter("id", empId);
            request.AddParameter("version", version);

            return ExecuteAndParseRequest(client, request);
        }

        /// <summary>
        /// Perform a POST on 'EmployeeApi/api/employees/{id}/status'
        /// </summary>
        /// <param name="empId">The ID of the employee to change the status of</param>
        /// <param name="statusObject">The new status object to add to the employee</param>
        /// <returns>A JSON object containing the response of the API</returns>
        public static JObject PostEmployeeStatus(string empId, string statusObject)
        {
            var client = new RestClient(FcEmployeeApi);
            var request = new RestRequest(Method.POST)
            {
                Resource = $"employees/{empId}/status"
            };
            request.AddHeader("Authorization", $"Bearer {AuthToken}");
            request.AddParameter("text/json", statusObject, ParameterType.RequestBody);

            return ExecuteAndParseRequest(client, request);
        }

        /// <summary>
        /// Perform a GET on 'ProductAccessService/api/users/{type}/{id}'
        /// </summary>
        /// <param name="type">The type of user as known by the external system, e.g. "RecruitQa_user"</param>
        /// <param name="id">The ID of the user as known by the external system, e.g. "clientcode|1|username"</param>
        /// <returns>A JSON object containing the response of the API</returns>
        public static JObject GetProductAccessUser(string type, string id)
        {
            var client = new RestClient(ProductAccessApi);
            var request = new RestRequest(Method.GET)
            {
                Resource=$"users/{type}/{id}"
            };

            return ExecuteAndParseRequest(client, request);
        }

        /// <summary>
        /// Perform a GET on 'ProductAccessService/api/users/{type}/{id}
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns>The response of the request</returns>
        public static IRestResponse GetProductAccessUserResponse(string type, string id)
        {
            var client = new RestClient(ProductAccessApi);
            var request = new RestRequest(Method.GET)
            {
                Resource = $"users/{type}/{id}"
            };

            return client.Execute(request);
        }

        /// <summary>
        /// Get a valid SSO token from IDM
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>The token string</returns>
        private static string GetToken(string userName, string password)
        {
            var client = new RestClient(IdmTokenApi);
            client.AddHandler("application/json", new RestSharp.Deserializers.JsonDeserializer());

            var request = new RestRequest(Method.POST)
            {
                Resource = "/connect/token",
                RequestFormat = DataFormat.Json
            };
            request.AddParameter("client_id", "fcApiTest");
            request.AddParameter("client_secret", ConfigurationManager.AppSettings.Get("IDMSecret"));
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", userName);
            request.AddParameter("password", password);
            request.AddParameter("scope", "flapi.public");

            return ExecuteAndParseRequest(client, request).GetValue("access_token").ToString();
        }

        /// <summary>
        /// Execute the request and make sure it was performed successfully
        /// </summary>
        /// <param name="client">The RestSharp client</param>
        /// <param name="request">The RestSharp request</param>
        /// <exception cref="Exception"></exception>
        /// <returns>The content of the response as a JObject</returns>
        private static JObject ExecuteAndParseRequest(IRestClient client, IRestRequest request)
        {
            var response = client.Execute(request);

            try
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                throw new AssertFailedException($"The {request.Resource} endpoint returned: {response.StatusCode}");
            }

            return JObject.Parse(response.Content);
        }
    }
}
