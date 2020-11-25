using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.Models;
using USP.Business.Constants;
using USP.Business.Helpers;
using USP.Business.Models.ContentModels;
using USP.Business.Models.PostBackModels.Application;
using USP.Business.Services.Interfaces;

namespace USP.Business.Controllers.ApplicationForm
{
    public class FormStepFiveController : RenderMvcSurfaceController
    {
        private readonly IDatabaseService _databaseService;

        private readonly string _nextStepUrl = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepSix);

        public FormStepFiveController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public override ActionResult Index(RenderModel model)
        {
            var viewModel = model.Content.OfType<FormStepFive>();

            if (!Members.IsLoggedIn())
            {
                return Redirect(FormHelper.GetLoginPageUrlWithReturnUrlByAlias());
            }

            var userGuid = Members.GetCurrentMember().GetKey();
            var userForm = _databaseService.GetApplicationData(userGuid);
            if (userForm.IsSubmitted)
            {
                return Redirect(FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepThirteen));
            }
            //If it's adult learning, this step is skipped to 6
            if (userForm.IsAdultLearning)
            {
                //clear all the data that maybe have been saved for this step
                userForm.ParentOrCarerFirstName = string.Empty;
                userForm.ParentOrCarerLastName = string.Empty;
                userForm.ParentOrCarerRelationshipToStudent = string.Empty;
                userForm.ParentOrCarerPhoneNumber = string.Empty;
                userForm.ParentOrCarerEmailAddress = string.Empty;
                userForm.IsParentOrCarerPrimaryContact = null;
                userForm.PrimaryContactFirstName = string.Empty;
                userForm.PrimaryContactLastName = string.Empty;
                userForm.PrimaryContactRelationshipToStudent = string.Empty;
                userForm.PrimaryContactPhoneNumber = string.Empty;
                userForm.PrimaryContactEmailAddress = string.Empty;
                userForm.IsStepFiveSubmitted = false;
                _databaseService.InsertApplicationData(userGuid, userForm);
                return Redirect(_nextStepUrl);
            }

            viewModel.ApplicationForm = userForm;
            viewModel.PostBackModel = new FormStepFivePostBackModel
            {
                ParentOrCarerFirstName = userForm.ParentOrCarerFirstName,
                ParentOrCarerLastName = userForm.ParentOrCarerLastName,
                ParentOrCarerRelationshipToStudent = userForm.ParentOrCarerRelationshipToStudent,
                ParentOrCarerPhoneNumber = userForm.ParentOrCarerPhoneNumber,
                ParentOrCarerEmailAddress = userForm.ParentOrCarerEmailAddress,
                IsParentOrCarerPrimaryContact = userForm.IsParentOrCarerPrimaryContact,
                PrimaryContactFirstName = userForm.PrimaryContactFirstName,
                PrimaryContactLastName = userForm.PrimaryContactLastName,
                PrimaryContactRelationshipToStudent = userForm.PrimaryContactRelationshipToStudent,
                PrimaryContactPhoneNumber = userForm.PrimaryContactPhoneNumber,
                PrimaryContactEmailAddress = userForm.PrimaryContactEmailAddress
            };
            return CurrentTemplate(model);
        }

        [HttpPost]
        public ActionResult HandleFormStepFiveSubmit([Bind(Prefix = "PostBackModel")]FormStepFivePostBackModel submission)
        {
            if (!Members.IsLoggedIn()) return Redirect(FormHelper.GetLoginPageUrlWithReturnUrl(UmbracoContext.Current.PublishedContentRequest.PublishedContent.Url));

            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            var userGuid = Members.GetCurrentMember().GetKey();
            var applicationForm = _databaseService.GetApplicationData(userGuid) ?? new Models.Dapper.ApplicationForm();

            applicationForm.ParentOrCarerFirstName = submission.ParentOrCarerFirstName;
            applicationForm.ParentOrCarerLastName = submission.ParentOrCarerLastName;
            applicationForm.ParentOrCarerRelationshipToStudent = submission.ParentOrCarerRelationshipToStudent;
            applicationForm.ParentOrCarerPhoneNumber = submission.ParentOrCarerPhoneNumber;
            applicationForm.ParentOrCarerEmailAddress = submission.ParentOrCarerEmailAddress;
            applicationForm.IsParentOrCarerPrimaryContact = submission.IsParentOrCarerPrimaryContact;

            var isParentOrCarerPrimaryContact = submission.IsParentOrCarerPrimaryContact ?? false;
            applicationForm.PrimaryContactFirstName = !isParentOrCarerPrimaryContact ? submission.PrimaryContactFirstName : string.Empty;
            applicationForm.PrimaryContactLastName = !isParentOrCarerPrimaryContact ? submission.PrimaryContactLastName : string.Empty;
            applicationForm.PrimaryContactRelationshipToStudent = !isParentOrCarerPrimaryContact ? submission.PrimaryContactRelationshipToStudent : string.Empty;
            applicationForm.PrimaryContactPhoneNumber = !isParentOrCarerPrimaryContact ? submission.PrimaryContactPhoneNumber : string.Empty;
            applicationForm.PrimaryContactEmailAddress = !isParentOrCarerPrimaryContact ? submission.PrimaryContactEmailAddress : string.Empty;
            applicationForm.IsStepFiveSubmitted = true;
            _databaseService.InsertApplicationData(userGuid, applicationForm);

            if (string.IsNullOrEmpty(_nextStepUrl))
            {
                return RedirectToCurrentUmbracoPage();
            }

            return Redirect(_nextStepUrl);
        }
    }
}
