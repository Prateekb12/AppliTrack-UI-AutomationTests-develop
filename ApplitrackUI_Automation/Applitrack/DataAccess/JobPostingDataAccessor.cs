using System;
using Recruit.Persistence.Enums;
using Recruit.Persistence.Models;

namespace ApplitrackUITests.DataAccess
{
    public class JobPostingDataAccessor : BaseDataAccessor
    {
        /// <summary>
        /// Create a job posting requisition with dummy data
        /// </summary>
        /// <param name="title"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public JobPostingRequisition CreateJobRequisition(string title, string location)
        {
            var posting = new JobPostingRequisition()
            {
                AdditionalTitle = title,
                Location = location,
                ReqStatus = "Submitted",
                DatePosted = DateTime.UtcNow
            };
            posting.Id = (int)_readWriteData.Create(posting, true);
            return posting;
        }


        public JobPostingRequisition CreateJobPosting(string title, string location)
        {
            var posting = new JobPostingRequisition()
            {
                AdditionalTitle = title,
                Location = location,
                DatePosted = DateTime.UtcNow
            };
            posting.Id = (int)_readWriteData.Create(posting, true);
            return posting;
        }

        /// <summary>
        /// Create an approval chain record associated with the given requision / user combination
        /// </summary>
        /// <param name="requisitionId"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public JobPostingApprovalChain CreateApprovalChainRecord(int requisitionId, string username)
        {
            var approver = new JobPostingApprovalChain
            {
                Approver = username,
                JobPostingRequisitionId = requisitionId,
                Status = JobPostingApprovalChainStatus.Pending,
                SortOrder = 1,
                IsFinalApprover = true
            };
            approver.Id = (int) _readWriteData.Create(approver, true);
            return approver;
        }

        /// <summary>
        /// Remove the Requisition record from the database
        /// </summary>
        /// <param name="requisitionId"></param>
        public void DeleteJobPosting(int requisitionId)
        {
            _readWriteData.Delete<JobPostingRequisition>(requisitionId);
        }

        /// <summary>
        /// Remove the requisition approver record from the database
        /// </summary>
        /// <param name="approverRecordId"></param>
        public void DeleteRequisitionApprover(int approverRecordId)
        {
            _readWriteData.Delete<JobPostingApprovalChain>(approverRecordId);
        }
    }
}
