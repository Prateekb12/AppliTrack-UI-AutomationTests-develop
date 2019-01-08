using System;
using Recruit.Persistence.Enums;
using Recruit.Persistence.Models;

namespace ApplitrackUITests.DataAccess
{
    public class WorkflowDataAccessor : BaseDataAccessor
    {
        /// <summary>
        /// Create a WorkflowStepInstanceAssignee record
        /// </summary>
        /// <param name="wfStepInstanceId"></param>
        /// <param name="assignee"></param>
        /// <returns></returns>
        public WorkflowStepInstanceAssignee CreateWorkflowStepInstanceAssignee(int wfStepInstanceId, RecruitUser assignee)
        {
            var workflowStepInstanceAssignee = new WorkflowStepInstanceAssignee
            {
                WorkflowStepInstanceId = wfStepInstanceId,
                AssigneeType = WorkflowStepInstanceAssigneeType.User,
                AssigneeKey = assignee.Id.ToString()
            };
            workflowStepInstanceAssignee.Id = (int)_readWriteData.Create(workflowStepInstanceAssignee, true);
            return workflowStepInstanceAssignee;
        }

        /// <summary>
        /// Create a WorkflowStepInstance record
        /// </summary>
        /// <param name="formsSentId"></param>
        /// <returns></returns>
        public WorkflowStepInstance CreateWorkflowStepInstance(int formsSentId)
        {
            var workflowStepInstance = new WorkflowStepInstance
            {
                FormsSentId = formsSentId,
                Status = WorkflowStepInstanceStatus.Active,
                DateEntered = DateTime.Today
            };
            workflowStepInstance.Id = (int) _readWriteData.Create(workflowStepInstance, true);
            return workflowStepInstance;
        }

        /// <summary>
        /// Removes the given WorkflowStepInstance record from the database by id
        /// </summary>
        /// <param name="instanceId"></param>
        public void DeleteWorkflowStepInstance(int instanceId)
        {
            _readWriteData.Delete<WorkflowStepInstance>(instanceId);
        }

        /// <summary>
        /// Removes the given WorkflowStepInstanceAssignee record from the database by id
        /// </summary>
        /// <param name="assigneeId"></param>
        public void DeleteWorkflowStepInstanceAssignee(int assigneeId)
        {
            _readWriteData.Delete<WorkflowStepInstanceAssignee>(assigneeId);
        }
    }
}
