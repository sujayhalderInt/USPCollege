using System;
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
    public class FormStepTwelveController : RenderMvcSurfaceController
    {
        private readonly IDatabaseService _databaseService;

        private readonly string _nextStepUrl = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepThirteen);

        public FormStepTwelveController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public override ActionResult Index(RenderModel model)
        {
            var viewModel = model.Content.OfType<FormStepTwelve>();

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
            viewModel.PostBackModel = new FormStepTwelvePostBackModel
            {
                ReceiveMarketingEmails = userForm.ReceiveMarketingEmails,
                HowDidYouHearAboutThisCourse = userForm.HowDidYouHearAboutThisCourse,
                MostInterestedSector = userForm.MostInterestedSector
            };
            return CurrentTemplate(viewModel);
        }

        [HttpPost]
        public ActionResult HandleFormStepTwelveSubmit([Bind(Prefix = "PostBackModel")]FormStepTwelvePostBackModel submission)
        {
            if (!Members.IsLoggedIn()) return Redirect(FormHelper.GetLoginPageUrlWithReturnUrl(UmbracoContext.Current.PublishedContentRequest.PublishedContent.Url));

            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            var userGuid = Members.GetCurrentMember().GetKey();
            var applicationForm = _databaseService.GetApplicationData(userGuid) ?? new Models.Dapper.ApplicationForm();

            applicationForm.ReceiveMarketingEmails = submission.ReceiveMarketingEmails;
            applicationForm.HowDidYouHearAboutThisCourse = submission.HowDidYouHearAboutThisCourse;
            applicationForm.MostInterestedSector = submission.MostInterestedSector;
            applicationForm.IsStepTwelveSubmitted = true;
            applicationForm.DateSubmitted = DateTime.Now;
            applicationForm.IsSubmitted = true;
            _databaseService.InsertApplicationData(userGuid, applicationForm);

            if (string.IsNullOrEmpty(_nextStepUrl))
            {
                return RedirectToCurrentUmbracoPage();
            }

            return Redirect(_nextStepUrl);
        }
    }
}
