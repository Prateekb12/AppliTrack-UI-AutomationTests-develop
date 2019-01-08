using ApplitrackUITests.DataModels;

namespace ApplitrackUITests.DataGenerators
{
    public abstract class PersonGenerator : IPerson
    {
        private string _firstName;

        public virtual string FirstName
        {
            get { return _firstName ?? (_firstName = Faker.Name.First()); }
            set { _firstName = value; }
        }

        private string _lastName;

        public virtual string LastName
        {
            get { return _lastName ?? (_lastName = Faker.Name.Last()); }
            set { _firstName = value; }
        }

        private string _email;

        public virtual string Email
        {
            // remove ' chars as it causes errors in email addresses
            get
            {
                return _email ?? (_email = FirstName.Replace("'", "") + "." + LastName.Replace("'", "") + "@" +
                                           Faker.Internet.DomainName());
            }
            set { _email = value; }
        }

        private string _password;

        public virtual string Password
        {
            get { return _password ?? (_password = Faker.Internet.Password(5, 10)); }
            set { _password = value; }
        }

        private string _realName;

        public virtual string RealName
        {
            get { return _realName ?? FirstName + " " + LastName; }
            set { _realName = value; }
        }
    }
}
