using System;
using ApplitrackUITests.DataGenerators;
using Recruit.Persistence.Models;

namespace ApplitrackUITests.DataAccess
{
    public class InterviewDataAccessor : BaseDataAccessor
    {
        private JobPostingDataAccessor jobDataAccessor;
        private UserDataAccessor userDataAccessor;

        public InterviewDataAccessor()
        {
            jobDataAccessor = new JobPostingDataAccessor();
            userDataAccessor = new UserDataAccessor();
        }

        /// <summary>
        /// Create an interview series record
        /// </summary>
        /// <param name="title">title for the interview series</param>
        /// <returns></returns>
        public InterviewSeries CreateInterviewSeries(string title)
        {
            var jobPosting = jobDataAccessor.CreateJobRequisition(string.Join(" ", Faker.Lorem.Words(3)), Faker.Company.Name());
            var interview = new InterviewSeries()
            {
                Title = title,
                JobId = jobPosting.Id,
            };
            interview.Id = (int)_readWriteData.Create(interview, true);
            return interview;
        }

        /// <summary>
        /// Create a user participant in an interview series
        /// </summary>
        /// <param name="interviewSeriesId"></param>
        /// <returns></returns>
        public InterviewUser CreateInterviewUser(int interviewSeriesId)
        {
            var targetUser = userDataAccessor.GetUser(LoginData.SuperUserName);
            var interviewUser = new InterviewUser()
            {
                InterviewSeriesId = interviewSeriesId,
                UserId = (int) targetUser.Id
            };
            interviewUser.Id = (int) _readWriteData.Create(interviewUser, true);
            return interviewUser;
        }

        /// <summary>
        /// Create an interview time slot record
        /// </summary>
        /// <param name="interviewSeriesId">The interview series to associate this record with</param>
        /// <returns></returns>
        public InterviewTimeSlot CreateInterviewTimeSlot(int interviewSeriesId)
        {
            var timeSlot = new InterviewTimeSlot()
            {
                InterviewSeriesId = interviewSeriesId,
                StartDateTime = DateTime.UtcNow,
                DurationMinutes = 60,
                MaxCapacity = 2
            };
            timeSlot.Id = (int)_readWriteData.Create(timeSlot, true);
            return timeSlot;
        }

        /// <summary>
        /// Create an applicant / interview time slot association
        /// </summary>
        /// <param name="interviewSeriesId"></param>
        /// <param name="timeSlotId"></param>
        /// <returns></returns>
        public InterviewApplicant CreateInterviewApplicant(int interviewSeriesId, int timeSlotId, int applicantId)
        {
            var interviewApplicant = new InterviewApplicant()
            {
                AppNo = applicantId,
                InterviewSeriesId = interviewSeriesId,
                InterviewTimeSlotId = timeSlotId
            };
            interviewApplicant.Id = (int)_readWriteData.Create(interviewApplicant, true);
            return interviewApplicant;
        }

        /// <summary>
        /// Remove and interview series and associated job posting requisition record from the database
        /// </summary>
        /// <param name="interviewId"></param>
        /// <param name="jobId"></param>
        public void DeleteInterviewSeries(int interviewId, int? jobId)
        {
            _readWriteData.Delete<InterviewSeries>(interviewId);
            if (jobId.HasValue && jobId > 0)
            {
                _readWriteData.Delete<JobPostingRequisition>(jobId);
            }
        }

        /// <summary>
        /// Remove the interview time slot record from the database
        /// </summary>
        /// <param name="timeSlotId"></param>
        public void DeleteInterviewTimeSlot(int timeSlotId)
        {
            _readWriteData.Delete<InterviewTimeSlot>(timeSlotId);
        }

        /// <summary>
        /// Remove the interview user record from the database
        /// </summary>
        /// <param name="interviewUserId"></param>
        public void DeleteInterviewUser(int interviewUserId)
        {
            _readWriteData.Delete<InterviewUser>(interviewUserId);
        }

        /// <summary>
        /// Remove the interview applicant record from the database
        /// </summary>
        /// <param name="interviewApplicantId"></param>
        public void DeleteInterviewApplicant(int interviewApplicantId)
        {
            _readWriteData.Delete<InterviewApplicant>(interviewApplicantId);
        }
    }
}
