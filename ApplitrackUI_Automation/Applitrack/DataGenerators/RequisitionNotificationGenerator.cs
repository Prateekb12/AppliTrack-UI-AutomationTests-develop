using ApplitrackUITests.DataAccess;
using Recruit.Persistence.Models;

namespace ApplitrackUITests.DataGenerators
{
    public class RequisitionNotificationGenerator : INotificationGenerator
    {
        protected JobPostingDataAccessor jpData;
        protected NotificationDataAccessor NotificationDataAccessor;

        public NotificationResult ExpectedResult { get; set; }

        protected JobPostingRequisition Requisition { get; set; }
        protected JobPostingApprovalChain RequisitionApprover { get; set; }


        public RequisitionNotificationGenerator()
        {
            jpData = new JobPostingDataAccessor();
            NotificationDataAccessor = new NotificationDataAccessor();
        }

        /// <summary>
        /// Set up records needed for a requisition approval notification
        /// </summary>
        public void CreateNotificationData()
        {
            Requisition = jpData.CreateJobRequisition(Faker.Company.Name(), Faker.Company.Name());
            RequisitionApprover = jpData.CreateApprovalChainRecord(Requisition.Id, LoginData.SuperUserName);
            ExpectedResult = new NotificationResult()
            {
                Title = Requisition.AdditionalTitle + " at " + Requisition.Location,
                PopupInfo = new NotificationPopupResult
                {
                    FrameId = "MainContentsIFrame",
                    Url = $"JobPostings-Edit.aspx?id={Requisition.Id}",
                    Content = Requisition.AdditionalTitle
                }
            };
        }

        /// <summary>
        /// Delete the records used for notification
        /// </summary>
        public void DeleteNotificationData()
        {
            if (Requisition.Id > 0)
            {
                jpData.DeleteJobPosting(Requisition.Id);
            }
            if (RequisitionApprover.Id > 0)
            {
                jpData.DeleteRequisitionApprover(RequisitionApprover.Id);
                NotificationDataAccessor.DeleteNotificationStatusByObjectId(RequisitionApprover.Id);
            }
        }
    }
}
