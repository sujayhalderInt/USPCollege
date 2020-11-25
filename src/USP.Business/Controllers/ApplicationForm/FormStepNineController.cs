using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using Umbraco.Web;
using Umbraco.Web.Models;
using USP.Business.Constants;
using USP.Business.Extensions;
using USP.Business.Helpers;
using USP.Business.Models.ContentModels;
using USP.Business.Models.PostBackModels.Application;
using USP.Business.Services.Interfaces;

namespace USP.Business.Controllers.ApplicationForm
{
    public class FormStepNineController : RenderMvcSurfaceController
    {
        private readonly IDatabaseService _databaseService;

        private readonly string _nextStepUrl = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepTen);

        public FormStepNineController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public override ActionResult Index(RenderModel model)
        {
            var viewModel = model.Content.OfType<FormStepNine>();

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
            if (userForm.IsAdultLearning)
            {
                //If it's adult learning name of last college or school not required, so set it to empty string, if there might have been something saved
                userForm.NameOfLastCollegeOrSchool = string.Empty;
                _databaseService.InsertApplicationData(userGuid, userForm);
            }

            viewModel.ApplicationForm = userForm;
            var qualifications = new List<FormStepNineQualifications>();
            if (!string.IsNullOrEmpty(userForm.QualificationsJson))
            {
                qualifications = JsonConvert.DeserializeObject<List<FormStepNineQualifications>>(userForm.QualificationsJson);
            }
            else
            {
                qualifications.Add(new FormStepNineQualifications());
            }

            viewModel.PostBackModel = new FormStepNinePostBackModel
            {
                IsAlevelsOrProfessionlQualifications = (userForm.IsALevels || userForm.IsProfessionalQualifications),
                IsAdultLearning = userForm.IsAdultLearning,
                Qualifications = qualifications,
                LastCollegeOrSchool = userForm.NameOfLastCollegeOrSchool
            };
            return CurrentTemplate(viewModel);
        }

        [HttpPost]
        public ActionResult HandleFormStepNineSubmit([Bind(Prefix = "PostBackModel")]FormStepNinePostBackModel submission)
        {
            if (!Members.IsLoggedIn()) return Redirect(FormHelper.GetLoginPageUrlWithReturnUrl(UmbracoContext.Current.PublishedContentRequest.PublishedContent.Url));

            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            var userGuid = Members.GetCurrentMember().GetKey();
            var applicationForm = _databaseService.GetApplicationData(userGuid) ?? new Models.Dapper.ApplicationForm();


            if (!string.IsNullOrEmpty(Request.Form["addButton"]))
            {
                applicationForm.NameOfLastCollegeOrSchool = submission.LastCollegeOrSchool;
                submission.Qualifications.Add(new FormStepNineQualifications());
                applicationForm.QualificationsJson = submission.Qualifications.ToJson();
                _databaseService.InsertApplicationData(userGuid, applicationForm);
                return CurrentUmbracoPage();
            }

            if (string.IsNullOrEmpty(Request.Form["continue"])) return CurrentUmbracoPage();

            applicationForm.NameOfLastCollegeOrSchool = submission.LastCollegeOrSchool;
            applicationForm.QualificationsJson = submission.Qualifications.ToJson();
            applicationForm.IsStepNineSubmitted = true;
            _databaseService.InsertApplicationData(userGuid, applicationForm);
            if (string.IsNullOrEmpty(_nextStepUrl))
            {
                return RedirectToCurrentUmbracoPage();
            }

            return Redirect(_nextStepUrl);
        }
    }
}
