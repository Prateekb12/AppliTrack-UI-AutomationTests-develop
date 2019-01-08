using ApplitrackUITests.DataModels;
using ApplitrackUITests.Helpers;
using ApplitrackUITests.PageObjects.ApplicantSide;
using ApplitrackUITests.PageObjects.ApplicantSide.EmploymentApplication;
using Automation;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ApplitrackUITests.WorkFlows
{
    public class ApplicationWorkflows : BaseFrameWork
    {
        private readonly IWebDriver _driver;
        private readonly ExtentTest _test;

        public ApplicationWorkflows(IWebDriver driver, ExtentTest test)
        {
            this._driver = driver;
            _test = test;
        }

        public void CompleteFitAssessment()
        {
            while (true)
            {
                var applicantPages = new ApplicantPages(_driver);

                if (applicantPages.EmploymentApplicationPages.FitPages.QuestionPageDisplayed())
                {
                    applicantPages.EmploymentApplicationPages.FitPages.SelectRandomOptions();

                    _driver.SwitchToDefaultFrame();

                    applicantPages.EmploymentApplicationPages.ClickNextPage();

                    continue;
                }
                else if (applicantPages.EmploymentApplicationPages.FitPages.MultipleChoicePageDisplayed())
                {
                    applicantPages.EmploymentApplicationPages.FitPages.SelectRandomMultipleChoice();

                    _driver.SwitchToDefaultFrame();

                    applicantPages.EmploymentApplicationPages.ClickNextPage();

                    continue;
                }
                else if (applicantPages.EmploymentApplicationPages.FitPages.OkNextDisplayed())
                {
                    applicantPages.EmploymentApplicationPages.FitPages.ClickOkNext();

                    continue;
                }
                break;
            }
        }

        public void FillOutPersonalInfo(IApplicant applicantData)
        {
            var applicantPages = new ApplicantPages(_driver);

            _driver.SwitchToDefaultFrame();
            _driver.SwitchToFrameById("AppDataPage");

            applicantPages.EmploymentApplicationPages.PersonalInfoPage.EnterFirstName(applicantData.FirstName);
            _test.Log(LogStatus.Info, "Enter into the 'First' name field: " + applicantData.FirstName);

            applicantPages.EmploymentApplicationPages.PersonalInfoPage.EnterLastName(applicantData.LastName);
            _test.Log(LogStatus.Info, "Enter into the 'Last' name field: " + applicantData.LastName);

            applicantPages.EmploymentApplicationPages.PersonalInfoPage.EnterEmailAddress(applicantData.Email);
            _test.Log(LogStatus.Info, "Enter into the 'Email Address' field: " + applicantData.Email);

            applicantPages.EmploymentApplicationPages.PersonalInfoPage.EnterConfirmEmail(applicantData.Email);
            _test.Log(LogStatus.Info, "Enter into the 'Confirm Email' field: " + applicantData.Email);

            if (FeatureFlags.PersonalInfo.CollectSSN)
            {
                 applicantPages.EmploymentApplicationPages.PersonalInfoPage.EnterSocialSecurityNumber(applicantData.SocialSecurityNumber);
                _test.Log(LogStatus.Info, "Enter into the 'Social Security Number' field: " + applicantData.SocialSecurityNumber);

                applicantPages.EmploymentApplicationPages.PersonalInfoPage.EnterConfirmSsn(applicantData.SocialSecurityNumber);
                _test.Log(LogStatus.Info, "Enter into the 'Confirm SSN' field: " + applicantData.SocialSecurityNumber);
            }

            applicantPages.EmploymentApplicationPages.PersonalInfoPage.EnterPassword(applicantData.Password);
            _test.Log(LogStatus.Info, "Enter into the 'Password' field: " + applicantData.Password);

            applicantPages.EmploymentApplicationPages.PersonalInfoPage.EnterConfirmPassword(applicantData.Password);
            _test.Log(LogStatus.Info, "Enter into the 'Confirm Password' field: " + applicantData.Password);

            applicantPages.EmploymentApplicationPages.PersonalInfoPage.SelectSecretQuestion();
            _test.Log(LogStatus.Info, "Select a secret question");

            applicantPages.EmploymentApplicationPages.PersonalInfoPage.EnterSecretAnswer(applicantData.SecretAnswer);
            _test.Log(LogStatus.Info, "Enter into the 'Secret Answer' field: " + applicantData.SecretAnswer);

            _driver.SwitchToDefaultFrame();
        }

        /// <summary>
        /// Fill out all fields for the 'Permanent Address' of the applicant
        /// </summary>
        /// <param name="applicantAddress">The applicants address</param>
        public void FillOutPermanentAddress(IAddress applicantAddress)
        {
            var postalAddressPage = new PostalAddressPage(_driver);

            _driver.SwitchToDefaultFrame();
            _driver.SwitchToFrameById("AppDataPage");

            postalAddressPage.EnterPermanentNumberAndStreet(applicantAddress.NumberAndStreet);
            _test.Log(LogStatus.Info, "Enter into the 'Number & Street field: " + applicantAddress.NumberAndStreet);

            postalAddressPage.EnterPermanentAptNumber(applicantAddress.AptNumber);
            _test.Log(LogStatus.Info, "Enter into the 'Apt. Number' field: " + applicantAddress.AptNumber);

            postalAddressPage.EnterPermanentCity(applicantAddress.City);
            _test.Log(LogStatus.Info, "Enter into the 'City' field: " + applicantAddress.City);

            postalAddressPage.SelectPermanentStateProvince(applicantAddress.State);
            _test.Log(LogStatus.Info, "Select from the 'State/Province' field: " + applicantAddress.State);

            postalAddressPage.EnterPermanentZipPostalCode(applicantAddress.Zip);
            _test.Log(LogStatus.Info, "Enter into the 'Zip/Postal Code' field: " + applicantAddress.Zip);

            postalAddressPage.SelectPermanentCountry(applicantAddress.Country);
            _test.Log(LogStatus.Info, "Select from the 'Country' field: " + applicantAddress.Country);

            postalAddressPage.EnterPermanentDayPhone(applicantAddress.DaytimePhone);
            _test.Log(LogStatus.Info, "Enter into the 'Daytime Phone' field: " + applicantAddress.DaytimePhone);

            postalAddressPage.EnterPermanentCellPhone(applicantAddress.CellPhone);
            _test.Log(LogStatus.Info, "Enter into the 'Home/Cell Phone' field: " + applicantAddress.CellPhone);

            _driver.SwitchToDefaultFrame();
        }
    }
}
