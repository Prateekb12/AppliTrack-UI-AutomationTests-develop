using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using ApplitrackUITests.Helpers;
using Automation;
using Automation.Framework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace ApplitrackUITests.PageObjects
{
    public class DefaultLandingPage : BasePageObject
    {
        private IWebDriver _driver;

        // All links on the page
        [FindsBy(How = How.TagName, Using = "a")]
        private IList<IWebElement> AllLinks { get; set; }

        // "Log in" button for external applicants
        [FindsBy(How = How.Id, Using = "ExternalLoginLink")]
        private IWebElement ExternalApplicantLoginLink { get; set; }

        // "Log in" button for internal applicants
        [FindsBy(How = How.Id, Using = "HREFInternalContinueLink")]
        private IWebElement InternalApplicantLoginLink { get; set; }

        // All Job Category Links
        [FindsBy(How = How.CssSelector, Using = "div#DivCategoryAndLocations a")]
        private IList<IWebElement> CategoryLinks { get; set; }

        // All Job Category Links
        [FindsBy(How = How.CssSelector, Using = "div#DivFeaturedJobsSection a")]
        private IList<IWebElement> FeaturedJobLinks { get; set; }

        // The header of the Job Category pages
        [FindsBy(How = How.CssSelector, Using = ".normal>b")]
        private IWebElement CategoryHeader { get; set; }

        // The header for the Featured Jobs pages
        [FindsBy(How = How.CssSelector, Using = ".title")]
        private IWebElement JobHeader { get; set; }

        // The logo, click to return to the home page
        [FindsBy(How = How.Id, Using = "HeaderLogoLeft")]
        private IWebElement Logo { get; set; }

        // The "Locations" tab
        [FindsBy(How = How.Id, Using = "LocationsTabLink")]
        private IWebElement LocationsTab { get; set; }

        // The "All Jobs" link
        [FindsBy(How = How.Id, Using = "HrefAllJob")]
        private IWebElement AllJobsLink { get; set; }

        // All Locations Links
        [FindsBy(How = How.CssSelector, Using = "div#DivLocationsSection a")]
        private IList<IWebElement> LocationLinks { get; set; }

        // List of all tables on the list of jobs pages
        [FindsBy(How = How.CssSelector, Using = "div#AppliTrackListContent table")]
        private IList<IWebElement> JobTables { get; set; }

        public DefaultLandingPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        /// <summary>
        /// Click the Locations tab
        /// </summary>
        public void ClickLocationsTab()
        {
            LocationsTab.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the "Log in" button for external applicants
        /// </summary>
        public void ClickExternalLogin()
        {
            Console.Out.WriteLineAsync("Page: Attempting to Click the Log in button for External Applicants");
            ExternalApplicantLoginLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Check all links to make sure they are valid.
        /// </summary>
        public void CheckForBrokenLinks()
        {
            //var helper = new LinkHelpers();
            int broken = 0; // counts broken links
            IList<IWebElement> links = AllLinks;
            
            // add the location links to the list
            LocationsTab.WaitAndClick(_driver);
            for (int i = 0; i < LocationLinks.Count; i++)
                links.Add(LocationLinks[i]);

            foreach (IWebElement link in links)
            {
                try
                {
                    string url = link.GetAttribute("href");

                    // Make sure to check only valid URLs
                    if (!url.Contains("javascript:"))
                    {
                        HttpStatusCode linkStatusCode = LinkHelpers.GetLinkStatusCode(url);
                        if(linkStatusCode == HttpStatusCode.OK)
                        {
                            Console.WriteLine("{0} is valid", url);
                        }
                        else
                        {
                            Console.WriteLine("{0} is broken, returns a status code of {1}", url, linkStatusCode);
                            broken++;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            Console.Out.WriteLine("There are {0} broken links", broken);
        }

        /// <summary>
        /// Click the "Log in" button for internal applicants
        /// </summary>
        public void ClickInternalLogin()
        {
            Console.Out.WriteLineAsync("Page: Attempting to Click the Log in button for Internal Applicants");
            InternalApplicantLoginLink.WaitAndClick(_driver);
        }

        /// <summary>
        /// Click the job category links on the left side of the page
        /// Verify that the correct job category page opens
        /// </summary>
        public void ClickJobCategoryLinks()
        {
            for (int i = 0; i < CategoryLinks.Count; i++)
            {
                // CategoryLinks contains several extra blank elements
                // this ensures that only actual links that appear on the page will be clicked
                if (CategoryLinks[i].Text != "")
                {
                    Console.WriteLine("Page: Clicking {0}", CategoryLinks[i].Text);
                    CategoryLinks[i].WaitAndClick(_driver);
                    Assert.IsTrue(CategoryLinks[i].Text.Contains(CategoryHeader.Text));
                }
            }
        }

        /// <summary>
        /// Click on all the featured job links and verify that the correct job opens.
        /// </summary>
        public void ClickFeaturedJobLinks()
        {
            for (int i = 0; i < FeaturedJobLinks.Count; i++)
            {
                Console.Write("Page: Clicking {0}", FeaturedJobLinks[i].Text);
                FeaturedJobLinks[i].WaitAndClick(_driver);
                Assert.IsTrue(FeaturedJobLinks[i].Text.Contains(JobHeader.Text));
            }
        }

        // TODO: Create assert statement
        /// <summary>
        /// Click the location links on the left side of the page
        /// </summary>
        public void ClickLocationsLinks()
        {
            LocationsTab.WaitAndClick(_driver);
            for (int i = 0; i < LocationLinks.Count; i++)
            {
                Console.WriteLine("Page: Clicking {0}", LocationLinks[i].Text);
                LocationLinks[i].WaitAndClick(_driver);
            }
        }

        public void ClickAllJobs()
        {
            AllJobsLink.WaitAndClick(_driver);
        }

        public bool JobIsInList(int id)
        {
            // Wait until the page loads
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.ElementExists(By.Id("AppliTrackListContent")));
            // TODO find a better way to wait for this...
            // Using Thread.Sleep because BaseWaitForPageLoad consistently times out
            Thread.Sleep(TimeSpan.FromSeconds(60));

            // Find the job in the table
            bool found = false;
            foreach (var table in JobTables)
            {
                var jobId = table.GetAttribute("id");
                jobId = jobId.Trim('p', '_');
                Console.WriteLine("Actual ID: {0} -- Expected ID: {1}", jobId, id);
                if (jobId == id.ToString())
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

    }
}
