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
    public class FormStepEightController : RenderMvcSurfaceController
    {
        private readonly IDatabaseService _databaseService;

        private readonly string _nextStepUrl = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepNine);

        public FormStepEightController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public override ActionResult Index(RenderModel model)
        {
            var viewModel = model.Content.OfType<FormStepEight>();

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
            //If it's Adult Education or Apprenticeships this step is skipped
            if (userForm.IsAdultLearning || userForm.IsApprenticeships)
            {
                //clear all the data that maybe have been saved for this step
                userForm.SportsAcademiesCsv = string.Empty;
                _databaseService.InsertApplicationData(userGuid, userForm);
                return Redirect(_nextStepUrl);
            }

            viewModel.ApplicationForm = userForm;
            viewModel.PostBackModel = new FormStepEightPostBackModel
            {
                SportsAcademiesCsv = userForm.SportsAcademiesCsv
            };
            return CurrentTemplate(model);
        }


        [HttpPost]
        public ActionResult HandleFormStepEightSubmit([Bind(Prefix = "PostBackModel")]FormStepEightPostBackModel submission)
        {
            if (!Members.IsLoggedIn()) return Redirect(FormHelper.GetLoginPageUrlWithReturnUrl(UmbracoContext.Current.PublishedContentRequest.PublishedContent.Url));

            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            var userGuid = Members.GetCurrentMember().GetKey();
            var applicationForm = _databaseService.GetApplicationData(userGuid) ?? new Models.Dapper.ApplicationForm();

            applicationForm.SportsAcademiesCsv = string.Empty;
            if (submission != null)
            {
                applicationForm.SportsAcademiesCsv = submission.SportsAcademiesCsv;
            }
            applicationForm.IsStepEightSubmitted = true;
            _databaseService.InsertApplicationData(userGuid, applicationForm);

            if (string.IsNullOrEmpty(_nextStepUrl))
            {
                return RedirectToCurrentUmbracoPage();
            }

            return Redirect(_nextStepUrl);
        }
    }
}
