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
    public class FormStepSevenController : RenderMvcSurfaceController
    {
        private readonly IDatabaseService _databaseService;

        private readonly ISearchService _searchService;

        private readonly string _nextStepUrl = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepEight);


        public FormStepSevenController(IDatabaseService databaseService, ISearchService searchService)
        {
            _databaseService = databaseService;
            _searchService = searchService;
        }

        public override ActionResult Index(RenderModel model)
        {
            var viewModel = model.Content.OfType<FormStepSeven>();

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
            //If it's A-Levels this step is skipped 
            if (userForm.IsALevels)
            {
                //clear all the data that maybe have been saved for this step
                userForm.NameOfCourseOfTheTypeSelected = string.Empty;
                _databaseService.InsertApplicationData(userGuid, userForm);
                return Redirect(_nextStepUrl);
            }

            viewModel.ApplicationForm = userForm;
            viewModel.CourseListDropDown = GetCoursesDropDownList(userForm, SiteConstants.UmbracoDictionaryKey.ApplicationForm.PickOne);
            viewModel.AdultLearningCourseListDropDownFirst = GetCoursesDropDownList(userForm, SiteConstants.UmbracoDictionaryKey.ApplicationForm.PickOne);
            viewModel.AdultLearningCourseListDropDownSecond = GetCoursesDropDownList(userForm, SiteConstants.UmbracoDictionaryKey.ApplicationForm.PickOne);
            viewModel.AdultLearningCourseListDropDownThird = GetCoursesDropDownList(userForm, SiteConstants.UmbracoDictionaryKey.ApplicationForm.PickOne);
            viewModel.PostBackModel = new FormStepSevenPostBackModel
            {
                NameOfCourse = userForm.NameOfCourseOfTheTypeSelected,
                AdultLearningCourseOne = userForm.AlevelCourseFirstChoice,
                AdultLearningCourseTwo = userForm.AlevelCourseSecondChoice,
                AdultLearningCourseThree = userForm.AlevelCourseThirdChoice,
                IsAdultLearning = userForm.IsAdultLearning
            };
            return CurrentTemplate(model);
        }

        [HttpPost]
        public ActionResult HandleFormStepSevenSubmit(
            [Bind(Prefix = "PostBackModel")] FormStepSevenPostBackModel submission)
        {
            if (!Members.IsLoggedIn())
                return Redirect(FormHelper.GetLoginPageUrlWithReturnUrl(UmbracoContext.Current.PublishedContentRequest
                    .PublishedContent.Url));

            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }


            var userGuid = Members.GetCurrentMember().GetKey();
            var applicationForm = _databaseService.GetApplicationData(userGuid) ?? new Models.Dapper.ApplicationForm();


            if (applicationForm.IsAdultLearning)
            {
                //since we need three courses for adult learning, I have decided to use the a-levels which also has three selections 
                applicationForm.AlevelCourseFirstChoice = submission.AdultLearningCourseOne;
                applicationForm.AlevelCourseSecondChoice = submission.AdultLearningCourseTwo;
                applicationForm.AlevelCourseThirdChoice = submission.AdultLearningCourseThree;
            }
            else
            {
                applicationForm.NameOfCourseOfTheTypeSelected = submission.NameOfCourse;
            }
            applicationForm.IsStepSevenSubmitted = true;
            _databaseService.InsertApplicationData(userGuid, applicationForm);

            if (string.IsNullOrEmpty(_nextStepUrl))
            {
                return RedirectToCurrentUmbracoPage();
            }

            return Redirect(_nextStepUrl);
        }

        private List<SelectListItem> GetCoursesDropDownList(Models.Dapper.ApplicationForm form, string placeholderDictionaryKey)
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
                Text = !string.IsNullOrEmpty(library.GetDictionaryItem(placeholderDictionaryKey)) ? library.GetDictionaryItem(placeholderDictionaryKey) : SiteConstants.DropDownLists.NotSelectedText,
                Value = ""
            });
            return listItems;
        }
    }
}
