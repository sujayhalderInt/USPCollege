using System;
using System.Web.Mvc;
using umbraco;
using Umbraco.Web;
using Umbraco.Web.Models;
using USP.Business.Constants;
using USP.Business.Helpers;
using USP.Business.Models.ContentModels;
using USP.Business.Models.PostBackModels.Application;
using USP.Business.Services.Interfaces;

namespace USP.Business.Controllers.ApplicationForm
{
    public class FormStepThreeController : RenderMvcSurfaceController
    {
        private readonly IDatabaseService _databaseService;

        public FormStepThreeController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public override ActionResult Index(RenderModel model)
        {
            var viewModel = model.Content.OfType<FormStepThree>();

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
            viewModel.ApplicationForm = userForm;
            viewModel.PostBackModel = new FormStepThreePostBackModel
            {
                FirstName = userForm.StudentsFirstName,
                MiddleName = userForm.StudentsMiddleName,
                LastName = userForm.StudentsLastName,
                AnotherName = userForm.StudentsPreferedName,
                DateOfBirth = userForm.StudentsDateOfBirth,
                Gender = userForm.StudentsGender,
                NationalInsuranceNumber = userForm.NationalInsuranceNumber,
                IsNIRequired = userForm.IsHigherEducation || userForm.IsAdultLearning
            };

            return CurrentTemplate(model);
        }

        [HttpPost]
        public ActionResult HandleFormStepThreeSubmit([Bind(Prefix = "PostBackModel")]FormStepThreePostBackModel submission)
        {
            if (!Members.IsLoggedIn()) return Redirect(FormHelper.GetLoginPageUrlWithReturnUrl(UmbracoContext.Current.PublishedContentRequest.PublishedContent.Url));

            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            //get the url for the next step
            var nextStepUrl = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepFour);

            var userGuid = Members.GetCurrentMember().GetKey();
            var applicationForm = _databaseService.GetApplicationData(userGuid) ?? new Models.Dapper.ApplicationForm();

            applicationForm.StudentsFirstName = submission.FirstName;
            applicationForm.StudentsMiddleName = submission.MiddleName;
            applicationForm.StudentsLastName = submission.LastName;
            applicationForm.StudentsPreferedName = submission.AnotherName;
            applicationForm.StudentsDateOfBirth = submission.DateOfBirth;
            applicationForm.StudentsGender = submission.Gender;
            applicationForm.NationalInsuranceNumber = submission.NationalInsuranceNumber;
            applicationForm.IsStepThreeSubmitted = true;
            _databaseService.InsertApplicationData(userGuid, applicationForm);


            if (string.IsNullOrEmpty(nextStepUrl))
            {
                return RedirectToCurrentUmbracoPage();
            }

            return Redirect(nextStepUrl);
        }
    }
}
