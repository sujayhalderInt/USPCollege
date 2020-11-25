using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class FormStepFourController : RenderMvcSurfaceController
    {
        private readonly IDatabaseService _databaseService;

        public FormStepFourController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        public override ActionResult Index(RenderModel model)
        {
            var viewModel = model.Content.OfType<FormStepFour>();

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

            if (string.IsNullOrEmpty(userForm.StudentsEmailAddress))
            {
                userForm.StudentsEmailAddress = Umbraco.MembershipHelper
                    ?.GetCurrentMember()
                    ?.GetPropertyValue<string>("Email");
            }

            viewModel.PostBackModel = new FormStepFourPostBackModel
            {
                IsAdultLearning = userForm.IsAdultLearning,
                FirstLineOfAddress = userForm.StudentsHomeAddressLine1,
                SecondLineOfAddress = userForm.StudentsHomeAddressLine2,
                PostCode = userForm.StudentsHomePostCode,
                TownOrCity = userForm.StudentsTownOrCity,
                MobileNumber = userForm.StudentsMobileNumber,
                HomeNumber = userForm.StudentsHomeNumber,
                EmailAddress = userForm.StudentsEmailAddress,
                EmergencyContactFirstName = userForm.EmergencyContactFirstName,
                EmergencyContactLastName = userForm.EmergencyContactLastName,
                EmergencyContactRelationship = userForm.EmergencyContactRelationship,
                EmergencyContactPhoneNumber = userForm.EmergencyContactPhoneNumber,
                EmergencyContactEmailAddress = userForm.EmergencyContactEmailAddress
            };
            return CurrentTemplate(viewModel);
        }

        [HttpPost]
        public ActionResult HandleFormStepFourSubmit([Bind(Prefix = "PostBackModel")]FormStepFourPostBackModel submission)
        {
            if (!Members.IsLoggedIn()) return Redirect(FormHelper.GetLoginPageUrlWithReturnUrl(UmbracoContext.Current.PublishedContentRequest.PublishedContent.Url));

            var stepFourModel = new FormStepFour(UmbracoContext.Current.PublishedContentRequest.PublishedContent);

            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            //get the url for the next step
            var nextStepUrl = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepFive);

            var userGuid = Members.GetCurrentMember().GetKey();
            var applicationForm = _databaseService.GetApplicationData(userGuid) ?? new Models.Dapper.ApplicationForm();


            applicationForm.StudentsHomeAddressLine1 = submission.FirstLineOfAddress;
            applicationForm.StudentsHomeAddressLine2 = submission.SecondLineOfAddress;
            applicationForm.StudentsHomePostCode = submission.PostCode;
            applicationForm.StudentsTownOrCity = submission.TownOrCity;
            applicationForm.StudentsMobileNumber = submission.MobileNumber;
            applicationForm.StudentsHomeNumber = submission.HomeNumber;
            applicationForm.StudentsEmailAddress = submission.EmailAddress;

            var isAdultLearning = applicationForm.IsAdultLearning;

            applicationForm.EmergencyContactFirstName = isAdultLearning ? submission.EmergencyContactFirstName : string.Empty;
            applicationForm.EmergencyContactLastName = isAdultLearning ? submission.EmergencyContactLastName : string.Empty;
            applicationForm.EmergencyContactRelationship = isAdultLearning ? submission.EmergencyContactRelationship : string.Empty;
            applicationForm.EmergencyContactPhoneNumber = isAdultLearning ? submission.EmergencyContactPhoneNumber : string.Empty;
            applicationForm.EmergencyContactEmailAddress = isAdultLearning ? submission.EmergencyContactEmailAddress : string.Empty;
            applicationForm.IsStepFourSubmitted = true;
            _databaseService.InsertApplicationData(userGuid, applicationForm);


            if (string.IsNullOrEmpty(nextStepUrl))
            {
                return RedirectToCurrentUmbracoPage();
            }

            return Redirect(nextStepUrl);
        }
    }
}
