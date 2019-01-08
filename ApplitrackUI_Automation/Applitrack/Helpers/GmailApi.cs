using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Thread = Google.Apis.Gmail.v1.Data.Thread;

namespace ApplitrackUITests.Helpers
{
    // This class is a modified version of:
    // https://developers.google.com/gmail/api/quickstart/dotnet

    /// <summary>
    /// Use the Gmail API to find emails
    /// </summary>
    public static class GmailApi
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ../Applitrack-UI-AutomationTests/ApplitrackUI_Automation/.credentials
        private static readonly string[] Scopes = { GmailService.Scope.GmailReadonly };
        private const string ApplicationName = "applitrack-email-test";

        /// <summary>
        /// Use the Gmail API to find an email body containing the specified text
        /// </summary>
        /// <param name="uniqueText">The text to find</param>
        /// <returns>True if the email contains the specified text, false otherwise</returns>
        public static bool FindEmail(string uniqueText)
        {
            var credential = GetUserCredentials();
            var service = CreateGmailApiService(credential);
            var threads = GetThreads(service);

            // Make sure emails are found
            if (threads == null || threads.Count <= 0) return false;

            // Return true if the email contains the uniqueText
            return threads.Any(thread => thread.Snippet.Contains(uniqueText));
        }

        /// <summary>
        /// Obtain the gmail credentials.
        /// This assumes that the 'client_secret.json' file is in the following folder:
        /// ../Applitrack-UI-Automation/ApplitrackUI_Automation/.credentials/
        /// </summary>
        /// <returns>The user credentials</returns>
        private static UserCredential GetUserCredentials()
        {
            UserCredential credential;

            using (var stream =
                new FileStream(
                    Path.Combine(System.Environment.CurrentDirectory, @"../../../.credentials/client_secret.json"),
                    FileMode.Open, FileAccess.Read))
            {
                // credpath is at: ../Applitrack-UI-AutomationTests/ApplitrackUI_Automation/.credentials
                string credPath = Path.Combine(System.Environment.CurrentDirectory, @"../../../");
                credPath = Path.Combine(credPath, ".credentials/gmail-dotnet-creds.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            return credential;
        }

        /// <summary>
        /// Create the Gmail API service using the specified credentials
        /// </summary>
        /// <param name="credential">The user credentials for the gmail account</param>
        /// <returns>The service to create the API request</returns>
        private static GmailService CreateGmailApiService(UserCredential credential)
        {
            var service = new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

            return service;
        }

        /// <summary>
        /// Execute the request to get a list of threads
        /// </summary>
        /// <param name="service">The GmailService to use</param>
        /// <returns>A list of threads from the users inbox</returns>
        private static IList<Thread> GetThreads(GmailService service)
        {
            UsersResource.ThreadsResource.ListRequest request = service.Users.Threads.List("me");
            var threads = request.Execute().Threads;
            return threads;
        }

    }
}