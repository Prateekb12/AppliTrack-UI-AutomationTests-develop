using System;
using System.Linq;
using ApplitrackUITests.DataAccess;
using Recruit.Persistence.Models;

namespace ApplitrackUITests.DataGenerators
{
    public class FormInboxNotificationGenerator : INotificationGenerator
    {
        protected NotificationDataAccessor NotificationDataAccessor;
        protected FormDataAccessor FormDataAccessor;
        protected WorkflowDataAccessor WorkflowDataAccessor;
        protected UserDataAccessor UserDataAccessor;

        public NotificationResult ExpectedResult { get; set; }

        protected Form _form;
        protected FormSent _formSent;
        protected WorkflowStepInstance _wfStepInstance;
        protected WorkflowStepInstanceAssignee _wfStepInstanceAssignee;
        protected RecruitUser TargetUser;


        public FormInboxNotificationGenerator()
        {
            NotificationDataAccessor = new NotificationDataAccessor();
            FormDataAccessor = new FormDataAccessor();
            WorkflowDataAccessor = new WorkflowDataAccessor();
            UserDataAccessor = new UserDataAccessor();
            TargetUser = UserDataAccessor.GetUser(LoginData.SuperUserName);
        }

        /// <summary>
        /// Set up records needed for a formInbox notification
        /// </summary>
        public void CreateNotificationData()
        {
            _form = FormDataAccessor.CreateForm(string.Join(" ", Faker.Lorem.Words(3)));
            _formSent = FormDataAccessor.CreateFormSent(_form.Id);
            _wfStepInstance = WorkflowDataAccessor.CreateWorkflowStepInstance(_formSent.Id);
            _wfStepInstanceAssignee = WorkflowDataAccessor.CreateWorkflowStepInstanceAssignee(_wfStepInstance.Id, TargetUser);

            ExpectedResult = new NotificationResult()
            {
                Title = _form.Title,
                PopupInfo = new NotificationPopupResult
                {
                    FrameId = "MainContentsIFrame",
                    Url = $"EForm.aspx?src=admin&NoInstructions=1&ID={_formSent.Guid}",
                    Content = string.Empty //TODO: Resolve data setup gap that is generating server error
                }
            };

            ValidateNotificationExistsForFormSentId();
        }

        /// <summary>
        /// Delete records used for notification
        /// </summary>
        public void DeleteNotificationData()
        {
            if (_wfStepInstanceAssignee.Id > 0)
            {
                WorkflowDataAccessor.DeleteWorkflowStepInstanceAssignee(_wfStepInstanceAssignee.Id);
            }
            if (_wfStepInstance.Id > 0)
            {
                WorkflowDataAccessor.DeleteWorkflowStepInstance(_wfStepInstance.Id);
            }
            if (_formSent.Id > 0)
            {
                NotificationDataAccessor.DeleteNotificationStatusByObjectId(_formSent.Id);
                FormDataAccessor.DeleteFormSent(_formSent.Id);
            }
            if (_form.Id > 0)
            {
                FormDataAccessor.DeleteForm(_form.Id);
            }
        }

        private void ValidateNotificationExistsForFormSentId()
        {
            if (NotificationDataAccessor.GetNotificationsForUser(TargetUser.UserName).All(nu => nu.ObjectId != _formSent.Id))
            {
                throw new Exception("Data setup for forminbox notification failed");
            }
        }
    }
}
