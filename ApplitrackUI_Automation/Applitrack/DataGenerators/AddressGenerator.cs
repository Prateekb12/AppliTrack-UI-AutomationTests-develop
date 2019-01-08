using ApplitrackUITests.DataModels;

namespace ApplitrackUITests.DataGenerators
{
    /// <summary>
    /// Assign or randomly generate data for an address
    /// </summary>
    public class AddressGenerator : IAddress
    {
        private string _numberAndStreet;
        public string NumberAndStreet
        {
            get { return _numberAndStreet ?? (_numberAndStreet = Faker.Address.StreetAddress()); }
            set { _numberAndStreet = value; }
        }

        private string _aptNumber;
        public string AptNumber
        {
            get { return _aptNumber ?? (_aptNumber = Faker.Address.BuildingNumber()); }
            set { _aptNumber = value; }
        }

        private string _city;
        public string City
        {
            get { return _city ?? (_city = Faker.Address.City()); }
            set { _city = value; }
        }

        private string _state;
        public string State
        {
            get { return _state ?? (_state = "IL"); }
            set { _state = value; }
        }

        private string _zip;
        public string Zip
        {
            get { return _zip ?? (_zip = Faker.Address.ZipCode()); }
            set { _zip = value; }
        }

        private string _country;
        public string Country
        {
            get { return _country ?? (_country = "United States of America"); }
            set { _country = value; }
        }

        private string _daytimePhone;
        public string DaytimePhone
        {
            // Remove the last character because the faker generates an 11 digit phone number. 
            // This can cause issues if a field requires the number be 11 digits
            get { return _daytimePhone ?? (_daytimePhone = Faker.PhoneFaker.Phone().Remove(12).Replace("-", "")); }
            set { _daytimePhone = value; }
        }

        private string _cellPhone;
        public string CellPhone
        {
            // Remove the last character because the faker generates an 11 digit phone number. 
            // This can cause issues if a field requires the number be 11 digits
            get { return _cellPhone ?? (_cellPhone = Faker.PhoneFaker.Phone().Remove(12).Replace("-", "")); }
            set { _cellPhone = value; }
        }
    }
}
