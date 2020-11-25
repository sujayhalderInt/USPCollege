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
using USP.Business.Extensions;
using USP.Business.Helpers;
using USP.Business.Models.ContentModels;
using USP.Business.Models.MappingClasses.Search;
using USP.Business.Models.PostBackModels.Application;
using USP.Business.Services.Interfaces;

namespace USP.Business.Controllers.ApplicationForm
{
    public class FormStepSixController : RenderMvcSurfaceController
    {
        private readonly IDatabaseService _databaseService;

        private readonly ISearchService _searchService;
        
        private readonly string _nextStepUrl = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepSeven);

        public FormStepSixController(IDatabaseService databaseService , ISearchService searchService)
        {
            _databaseService = databaseService;
            _searchService = searchService;
        }

        public override ActionResult Index(RenderModel model)
        {
            var viewModel = model.Content.OfType<FormStepSix>();

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
            //If it's not A-Levels this step is skipped 
            if (!userForm.IsALevels)
            {
                //clear all the data that maybe have been saved for this step
                userForm.AlevelCourseFirstChoice = string.Empty;
                userForm.AlevelCourseSecondChoice = string.Empty;
                userForm.AlevelCourseThirdChoice = string.Empty;
                userForm.IsFirstFullLevel3Qualification = null;
                _databaseService.InsertApplicationData(userGuid, userForm);
                return Redirect(_nextStepUrl);
            }

            viewModel.ApplicationForm = userForm;
            viewModel.AlevelCoursesDropDownListFirstChoice = GetALevelCoursesDropDownList(userForm, SiteConstants.UmbracoDictionaryKey.ApplicationForm.Step6PlaceHolder.FirstChoice);
            viewModel.AlevelCoursesDropDownListSecondChoice = GetALevelCoursesDropDownList(userForm, SiteConstants.UmbracoDictionaryKey.ApplicationForm.Step6PlaceHolder.SecondChoice);
            viewModel.AlevelCoursesDropDownListThirdChoice = GetALevelCoursesDropDownList(userForm, SiteConstants.UmbracoDictionaryKey.ApplicationForm.Step6PlaceHolder.ThirdChoice);
            viewModel.PostBackModel = new FormStepSixPostBackModel
            {
                AlevelCourseFirstChoice = userForm.AlevelCourseFirstChoice,
                AlevelCourseSecondChoice = userForm.AlevelCourseSecondChoice,
                AlevelCourseThirdChoice = userForm.AlevelCourseThirdChoice,
                IsFirstFullLevel3Qualification = userForm.IsFirstFullLevel3Qualification
            };
            return CurrentTemplate(model);
        }

        [HttpPost]
        public ActionResult HandleFormStepSixSubmit([Bind(Prefix = "PostBackModel")]FormStepSixPostBackModel submission)
        {
            if (!Members.IsLoggedIn()) return Redirect(FormHelper.GetLoginPageUrlWithReturnUrl(UmbracoContext.Current.PublishedContentRequest.PublishedContent.Url));

            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            var userGuid = Members.GetCurrentMember().GetKey();
            var applicationForm = _databaseService.GetApplicationData(userGuid) ?? new Models.Dapper.ApplicationForm();

            applicationForm.AlevelCourseFirstChoice = submission.AlevelCourseFirstChoice;
            applicationForm.AlevelCourseSecondChoice = submission.AlevelCourseSecondChoice;
            applicationForm.AlevelCourseThirdChoice = submission.AlevelCourseThirdChoice;
            applicationForm.IsFirstFullLevel3Qualification = submission.IsFirstFullLevel3Qualification;
            applicationForm.IsStepSixSubmitted = true;
            _databaseService.InsertApplicationData(userGuid, applicationForm);

            if (string.IsNullOrEmpty(_nextStepUrl))
            {
                return RedirectToCurrentUmbracoPage();
            }

            return Redirect(_nextStepUrl);
        }

        private List<SelectListItem> GetALevelCoursesDropDownList(Models.Dapper.ApplicationForm form, string placeholderDictionaryKey)
        {
            var searchParams = new SearchParameters
            {
                SearchType = SiteConstants.SearchType.CourseSearch,
                DocTypeAliases = new[] {SiteConstants.AliasCourseDetail},
                PageSize = int.MaxValue,
                Page = 1,
                ApplyNowCourse = "yes",
                BasicTaxonomy = new List<string>
                {
                    new Guid(form.TypeOfStudy).ToIndexString(),
                    new Guid(form.Campus).ToIndexString()
                }
            };

            var search = _searchService.Search(searchParams);

            var listItems = search.Results.Select(course => new SelectListItem
                {
                    Value = course.Key.ToString(),
                    Text = course.Heading
                })
                .ToList();
            listItems.Insert(0, new SelectListItem
            {
                Text = !string.IsNullOrEmpty(library.GetDictionaryItem(placeholderDictionaryKey)) ? library.GetDictionaryItem(placeholderDictionaryKey)  : SiteConstants.DropDownLists.NotSelectedText,
                Value = ""
            });
            return listItems;
        }
    }
}
