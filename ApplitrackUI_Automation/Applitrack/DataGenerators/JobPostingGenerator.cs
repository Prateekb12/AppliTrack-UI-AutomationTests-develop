using System;
using ApplitrackUITests.DataAccess;
using Recruit.Persistence.Models;

namespace ApplitrackUITests.DataGenerators
{
    public class JobPostingGenerator : JobPostingRequisition
    {
        private readonly JobPostingDataAccessor _accessor;

        public JobPostingGenerator()
        {
            _accessor = new JobPostingDataAccessor();
        }

        public int Id { get; set; }

        private string _additionalTitle;
        public string AdditionalTitle
        {
            get => _additionalTitle ?? (_additionalTitle = Faker.Beer.Name());
            set => _additionalTitle = value;
        }

        private string _location;
        public string Location
        {
            get => _location ?? (_location = Faker.Company.Name());
            set => _location = value;
        }

        private DateTime _datePosted;
        public DateTime DatePosted
        {
            get => _datePosted != null ? _datePosted : (_datePosted = DateTime.UtcNow);
            set => _datePosted = value;
        }

        /// <summary>
        /// Create a job posting record in the database
        /// </summary>
        public void CreateInDatabase()
        {
            var posting = _accessor.CreateJobPosting(this.AdditionalTitle, this.Location);
            this.Id = posting.Id;
        }

        /// <summary>
        /// Delete a job posting record from the database
        /// </summary>
        public void DeleteFromDatabase()
        {
            _accessor.DeleteJobPosting(this.Id);
        }
    }
}
