namespace ApplitrackUITests.DataGenerators
{
    class ApplicationPageData
    {
        private string _sectionTitle;

        public string SectionTitle
        {
            get { return _sectionTitle ?? (_sectionTitle = Faker.Lorem.Sentence(3)); }
            set { _sectionTitle = value; }
        }
    }
}
