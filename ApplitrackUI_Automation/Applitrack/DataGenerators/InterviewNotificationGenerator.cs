using ApplitrackUITests.DataAccess;
using Recruit.Persistence.Models;

namespace ApplitrackUITests.DataGenerators
{
    public class InterviewNotificationGenerator : INotificationGenerator
    {
        protected InterviewDataAccessor InterviewDataAccessor;
        protected NotificationDataAccessor NotificationDataAccessor;

        public NotificationResult ExpectedResult { get; set; }

        protected InterviewSeries InterviewSeries { get; set; }
        protected InterviewTimeSlot InterviewTimeSlot { get; set; }
        protected InterviewApplicant InterviewApplicant { get; set; }
        protected InterviewUser InterviewUser { get; set; }

        private const int ApplicantId = 1;


        public InterviewNotificationGenerator()
        {
            InterviewDataAccessor = new InterviewDataAccessor();
            NotificationDataAccessor = new NotificationDataAccessor();
        }

        public void CreateNotificationData()
        {
            InterviewSeries = InterviewDataAccessor.CreateInterviewSeries(string.Join(" ", Faker.Lorem.Words(3)));
            InterviewTimeSlot = InterviewDataAccessor.CreateInterviewTimeSlot(InterviewSeries.Id);
            InterviewApplicant = InterviewDataAccessor.CreateInterviewApplicant(InterviewSeries.Id, InterviewTimeSlot.Id, ApplicantId);
            InterviewUser = InterviewDataAccessor.CreateInterviewUser(InterviewSeries.Id);
            ExpectedResult = new NotificationResult
            {
                Title = InterviewSeries.Title,
                PopupInfo = new NotificationPopupResult
                {
                    FrameId = $"App{ApplicantId}",
                    Url = $"AppProfile.aspx?AppNo={ApplicantId}",
                    Content = "Sample Applicant"
                }
            };
        }

        public void DeleteNotificationData()
        {
            if (InterviewApplicant.Id > 0)
            {
                InterviewDataAccessor.DeleteInterviewApplicant(InterviewApplicant.Id);
            }
            if (InterviewUser.Id > 0)
            {
                InterviewDataAccessor.DeleteInterviewUser(InterviewUser.Id);
            }
            if (InterviewTimeSlot.Id > 0)
            {
                InterviewDataAccessor.DeleteInterviewTimeSlot(InterviewTimeSlot.Id);
                NotificationDataAccessor.DeleteNotificationStatusByObjectId(InterviewTimeSlot.Id);
            }
            if (InterviewSeries.Id > 0)
            {
                InterviewDataAccessor.DeleteInterviewSeries(InterviewSeries.Id, InterviewSeries.JobId ?? 0);
            }
        }
    }
}
