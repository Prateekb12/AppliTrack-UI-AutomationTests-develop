using System;
using ApplitrackUITests.DataGenerators;
using Recruit.Persistence.Models;

namespace ApplitrackUITests.DataAccess
{
    public class FormDataAccessor : BaseDataAccessor
    {
        /// <summary>
        /// Create a form record with some default dummy values
        /// </summary>
        /// <param name="title">Title for form</param>
        /// <returns></returns>
        public Form CreateForm(string title)
        {
            var form = new Form
            {
                Title = title,
                FormType = "Standard Form",
                Category = "New Hire Forms",
                IsGlobal = true,
                RuntimeContextApplicant = 1,
                FormOwner = LoginData.SuperUserName
            };
            form.Id = (int)_readWriteData.Create(form, true);
            CreateCachedForm(form);
            return form;
        }

        /// <summary>
        /// Directly create a FormCached record for a given form to mimic automatic cacheing from the application
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public FormCached CreateCachedForm(Form form)
        {
            //TODO: This is very, very bad...
            //Implement the Cache_Tables SPROC in persistence instead
            var formCached = new FormCached
            {
                Id = form.Id,
                Title = form.Title,
                FormType = form.FormType,
                Category = form.Category,
                RuntimeContextApplicant = form.RuntimeContextApplicant,
                FormOwner = form.FormOwner
            };
            _readWriteData.Create(formCached);
            return formCached;
        }

        /// <summary>
        /// Create a form instance (FormSent record)
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        public FormSent CreateFormSent(int formId)
        {
            var formSent = new FormSent
            {
                FormId = formId,
                CreatorUserName = LoginData.SuperUserName,
                CreationDate = DateTime.UtcNow,
                ForAppNo = 1,
                IsExpired = false,
                IsDeleted = false
            };
            formSent.Id = (int) _readWriteData.Create(formSent, true);
            return formSent;
        }

        /// <summary>
        /// Remove Form and FormCached records from the database
        /// </summary>
        /// <param name="formId"></param>
        public void DeleteForm(int formId)
        {
            _readWriteData.Delete<Form>(formId);
            _readWriteData.Delete<FormCached>(formId);
        }

        /// <summary>
        /// Remove FormSent record from the database
        /// </summary>
        /// <param name="formSentId"></param>
        public void DeleteFormSent(int formSentId)
        {
            _readWriteData.Delete<FormSent>(formSentId);
        }
    }
}
