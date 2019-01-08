using ApplitrackUITests.DataModels;

namespace ApplitrackUITests.DataGenerators
{
    /// <summary>
    /// Assign or randomly generate data for an applicant
    /// </summary>
    public class ApplicantGenerator : PersonGenerator, IApplicant
    {
        public int AppNo { get; set; }

        public string Title { get; set; }

        private string _middleInitial;
        public string MiddleInitial
        {
            get { return _middleInitial ?? (_middleInitial = Faker.Lorem.Characters(1)); }
            set { _middleInitial = value; }
        }

        public string Suffix { get; set; }

        private string _socialSecurityNumber;
        public string SocialSecurityNumber
        {
            get
            {
                return _socialSecurityNumber ??
                       (_socialSecurityNumber =
                           Faker.NumberFaker.Number(100, 665) + "-" + Faker.NumberFaker.Number(10, 99) + "-" +
                           Faker.NumberFaker.Number(1000, 9999));
            }
            set { _socialSecurityNumber = value; }
        }

        private string _secretAnswer;
        public string SecretAnswer
        {
            get { return _secretAnswer ?? (_secretAnswer = Faker.Lorem.Characters(5)); }
            set { _secretAnswer = value; }
        }

        private IAddress _address;
        public IAddress Address
        {
            get { return _address ?? (_address = new AddressGenerator()); }
            set { _address = value; }
        }
    }
}
