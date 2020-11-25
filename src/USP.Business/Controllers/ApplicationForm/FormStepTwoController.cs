using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using umbraco;
using Umbraco.Web;
using Umbraco.Web.Models;
using USP.Business.Constants;
using USP.Business.Extensions;
using USP.Business.Helpers;
using USP.Business.Models.ContentModels;
using USP.Business.Models.MappingClasses.Search;
using USP.Business.Models.PostBackModels.Application;
using USP.Business.Services.Interfaces;

namespace USP.Business.Controllers.ApplicationForm
{
    public class FormStepTwoController : RenderMvcSurfaceController
    {
        private readonly IDatabaseService _databaseService;
        private readonly ISearchService _searchService;


        public FormStepTwoController(IDatabaseService databaseService, ISearchService searchService)
        {
            _databaseService = databaseService;
            _searchService = searchService;
        }

        public override ActionResult Index(RenderModel model)
        {
            var viewModel = model.Content.OfType<FormStepTwo>();

            if (!Members.IsLoggedIn())
            {
                var loginPageUrl = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepOne);
                var stepTwwoPageUrl = viewModel.Url;
                var loginPageUrlWQueryString = loginPageUrl + "?returnUrl=" + stepTwwoPageUrl;
                return Redirect(loginPageUrlWQueryString);
            }

            var userGuid = Members.GetCurrentMember().GetKey();
            var userForm = _databaseService.GetApplicationData(userGuid);
            if (userForm != null)
            {
                if (userForm.IsSubmitted)
                {
                    return Redirect(FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepThirteen));
                }

                viewModel.ApplicationForm = userForm;
                viewModel.PostBackModel = new FormStepTwoPostBackModel
                {
                    TypeOfStudy = userForm.TypeOfStudy,
                    Campus = userForm.Campus
                };
                return CurrentTemplate(viewModel);
            }

            userForm = new Models.Dapper.ApplicationForm();
            viewModel.ApplicationForm = userForm;
            viewModel.PostBackModel = new FormStepTwoPostBackModel();

            if (userForm.IsStepTwoSubmitted) return CurrentTemplate(viewModel);

            var coursePageId = Request.QueryString["course"];
            if (string.IsNullOrEmpty(coursePageId)) return CurrentTemplate(viewModel);

            var coursePage = Umbraco.TypedContent(coursePageId)?.OfType<PageCourseDetail>();
            if (coursePage == null) return CurrentTemplate(viewModel);

            var courseTypeGuid = coursePage.CourseType.First().GetKey().ToString();
            viewModel.PostBackModel.TypeOfStudy = courseTypeGuid;
            var availableCampuses = coursePage.Campus.Count();
            if (availableCampuses == 1)
            {
                viewModel.PostBackModel.Campus = coursePage.Campus.First().GetKey().ToString();
            }
            //see if it's A-Level course or not by matching the GUIDs of the course type taxonomy 
            var aLevelCourseTypeTaxonomyGuid = viewModel.TaxonomyPickerAlevels.FirstOrDefault()?.GetKey().ToString();
            var userSelectedCourseTypeTaxonomyGuid = courseTypeGuid;
            var isAlevel = string.Equals(aLevelCourseTypeTaxonomyGuid, userSelectedCourseTypeTaxonomyGuid);
            if (isAlevel) userForm.AlevelCourseFirstChoice = coursePageId;
            else userForm.NameOfCourseOfTheTypeSelected = coursePageId;
            _databaseService.InsertApplicationData(userGuid, userForm);
            return CurrentTemplate(viewModel);
        }

        [HttpPost]
        public ActionResult HandleFormStepTwoSubmit([Bind(Prefix = "PostBackModel")]FormStepTwoPostBackModel submission)
        {
            var courseNotAvailableText =
                !string.IsNullOrEmpty(library.GetDictionaryItem(SiteConstants.UmbracoDictionaryKey.ApplicationForm
                    .Step2Validations.CourseNotAvailable))
                    ? library.GetDictionaryItem(SiteConstants.UmbracoDictionaryKey.ApplicationForm.Step2Validations
                        .CourseNotAvailable)
                    : "Course is not available in the selected campus, please select a different Campus and try again";

            if (!HasResults(submission.TypeOfStudy, submission.Campus))
            {
                ModelState.AddModelError("PostBackModel.Campus", courseNotAvailableText);
            }

            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            var userGuid = Guid.Empty;

            //get the url for the next step
            var nextStepUrl = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepThree);

            if (Members.IsLoggedIn())
            {
                userGuid = Members.GetCurrentMember().GetKey();
            }
            var stepTwoModel = new FormStepTwo(UmbracoContext.Current.PublishedContentRequest.PublishedContent);
            var applicationForm = _databaseService.GetApplicationData(userGuid) ?? new Models.Dapper.ApplicationForm();

            if (!string.Equals(submission.TypeOfStudy, applicationForm.TypeOfStudy))
            {
                applicationForm.AlevelCourseFirstChoice = string.Empty;
                applicationForm.AlevelCourseSecondChoice = string.Empty;
                applicationForm.AlevelCourseThirdChoice = string.Empty;
                applicationForm.NameOfCourseOfTheTypeSelected = string.Empty;
            }
            applicationForm.TypeOfStudy = submission.TypeOfStudy;
            applicationForm.Campus = submission.Campus;
            applicationForm.IsALevels = string.Equals(stepTwoModel.TaxonomyPickerAlevels.FirstOrDefault().GetKey().ToString(), submission.TypeOfStudy);
            applicationForm.IsAdultLearning = string.Equals(stepTwoModel.TaxonomyPickerAdultLearning.FirstOrDefault().GetKey().ToString(), submission.TypeOfStudy);
            applicationForm.IsApprenticeships = string.Equals(stepTwoModel.TaxonomyPickerApprenticeships.FirstOrDefault().GetKey().ToString(), submission.TypeOfStudy);
            applicationForm.IsProfessionalQualifications = string.Equals(stepTwoModel.TaxonomyPickerProfessionalQualifications.FirstOrDefault().GetKey().ToString(), submission.TypeOfStudy);
            applicationForm.IsHigherEducation = string.Equals(stepTwoModel.TaxonomyPickerHigherEducation.FirstOrDefault().GetKey().ToString(), submission.TypeOfStudy);
            applicationForm.IsStepTwoSubmitted = true;
            _databaseService.InsertApplicationData(userGuid, applicationForm);

            if (string.IsNullOrEmpty(nextStepUrl))
            {
                return RedirectToCurrentUmbracoPage();
            }

            return Redirect(nextStepUrl);
        }

        public bool HasResults(string typeOfStudy, string campus)
        {
            var searchParams = new SearchParameters
            {
                SearchType = SiteConstants.SearchType.CourseSearch,
                DocTypeAliases = new[] { SiteConstants.AliasCourseDetail },
                PageSize = int.MaxValue,
                Page = 1,
                ApplyNowCourse = "yes",
                BasicTaxonomy = new List<string>
                {
                    new Guid(typeOfStudy).ToIndexString(),
                    new Guid(campus).ToIndexString()
                }
            };

            var search = _searchService.Search(searchParams);

            return search.HasResults;
        }
    }
}
