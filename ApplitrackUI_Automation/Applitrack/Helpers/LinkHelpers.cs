using System;
using System.Net;

namespace ApplitrackUITests.Helpers
{
    public static class LinkHelpers
    {
        /// <summary>
        /// Get the HTTP status code of an URL.
        /// Can be used to test if the link is valid or not.
        /// </summary>
        /// <param name="url">The URL to be checked</param>
        /// <returns>The HTTP status code of an URL</returns>
        public static HttpStatusCode GetLinkStatusCode(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Close();
                return response.StatusCode;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}