using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Web;
using Umbraco.Web.Models;
using USP.Business.Constants;
using USP.Business.Extensions;
using USP.Business.Helpers;
using USP.Business.Models.ContentModels;
using USP.Business.Models.ViewModels;
using USP.Business.Mvc;
using USP.Business.Services.Interfaces;

namespace USP.Business.Controllers
{
    public class NavigationController : RenderMvcSurfaceController
    {
        private readonly IDatabaseService _databaseService;

        public NavigationController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [UmbracoDonutOutputCache(CacheProfile = "Global")]
        public ActionResult Navigation()
        {
            var model = new NavigationViewModel();
            var primaryItems = SettingHelper.GetCurrentSiteRoot()
                .Children
                .OfType<IBaseNavigation>()
                .Where(n => !n.HideFromNavigation)
                .Take(6)
                .ToList();

            foreach (var primaryItem in primaryItems)
            {
                var menuItem = GetMenuItem(primaryItem);
                model.NavigationItems.Add(menuItem);
            }

            var setting = Umbraco.ContentQuery.TypedContentSingleAtXPath("//settingsSite");
            model.Settings = new SettingsSite(setting);

            return PartialView("Navigation", model);
        }

        private NavigationMenuItem GetMenuItem(IBaseNavigation primaryItem)
        {
            var menu = new NavigationMenuItem();
            var buckets = new List<NavigationBucketItem>();
            var columns = new NavigationBucket[4] { new NavigationBucket(), new NavigationBucket(), new NavigationBucket(), new NavigationBucket() };

            if (!primaryItem.HideChildrenFromNavigation)
            {
                foreach (var secondaryItem in primaryItem
                    .Children
                    .OfType<IBaseNavigation>()
                    .Where(n => !n.HideFromNavigation))
                {
                    var bucketItem = new NavigationBucketItem();

                    bucketItem.SecondaryItem = secondaryItem;

                    if (!secondaryItem.HideChildrenFromNavigation)
                    {
                        bucketItem.TertiaryItem = secondaryItem
                            .Children
                            .OfType<IBaseNavigation>()
                            .Where(n => !n.HideFromNavigation)
                            .ToList();
                    }

                    buckets.Add(bucketItem);
                }
            }
            // Order Bucket Items
            foreach (var bucket in buckets.OrderByDescending(b => b.BucketCount).ThenBy(b => b.SecondaryItem.SortOrder))
            {
                var min = columns.Min(c => c.InclusiveBucketCount);
                var column = columns.FindIndex(b => b.InclusiveBucketCount == min);
                columns[column].Buckets.Add(bucket);
            }

            foreach (var column in columns)
            {
                column.Buckets = column.Buckets.OrderBy(b => b.BucketCount).ToList();
            }

            menu.Buckets = columns;
            menu.PrimaryNavigationItem = primaryItem;
            return menu;
        }

        public ActionResult SignUp()
        {
            var setting = Umbraco.ContentQuery.TypedContentSingleAtXPath("//settingsSite");
            var model = new SignupViewModel();
            model.Settings = new SettingsSite(setting);

            model.ApplicationUrl = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepOne);

            return PartialView("SignUp", model);
        }

        public ActionResult RenderApply()
        {
            var formStartUrl = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepTwo);
            var formUrl = formStartUrl;

            var navigationApplyButtonModel = new NavigationApplyButtonModel();

            if (Umbraco.MemberIsLoggedOn())
            {
                var userGuid = Members.GetCurrentMember().GetKey();
                var userForm = _databaseService.GetApplicationData(userGuid) ?? new Models.Dapper.ApplicationForm();

                navigationApplyButtonModel.ApplicationForm = userForm;
                navigationApplyButtonModel.FormUrl = formUrl;
                return PartialView("ApplyButton", navigationApplyButtonModel);
            }

            navigationApplyButtonModel.ApplicationForm = null;
            navigationApplyButtonModel.FormUrl = formUrl;
            var holder = FormHelper.GetFormStepsHolderIPublishedContent();
            if (holder == null)
            {
                return PartialView("ApplyButton", navigationApplyButtonModel);
            }

            var login = holder.Descendant(SiteConstants.AliasApplicationForm.StepOne);
            if (login == null)
            {
                return PartialView("ApplyButton", navigationApplyButtonModel);
            }

            var loginPage = new PageApplicationFormLogin(login);
            if (loginPage.AccountCreationPage.IsNullOrEmpty())
            {
                return PartialView("ApplyButton", navigationApplyButtonModel);
            }

            formUrl = loginPage.AccountCreationPage.First().Url;
            navigationApplyButtonModel.FormUrl = formUrl;
            var page = Umbraco.UmbracoContext.PublishedContentRequest.PublishedContent;
            if (page?.DocumentTypeAlias == SiteConstants.AliasCourseDetail)
            {
                formStartUrl = formStartUrl + "?course=" + page.GetKey();
                navigationApplyButtonModel.FormUrl = formUrl + "?returnUrl=" + Url.Encode(formStartUrl);
            }
            else if (page?.DocumentTypeAlias == SiteConstants.AliasCareerDetail)
            {
                formStartUrl = formStartUrl + "?career=" + page.GetKey();
                navigationApplyButtonModel.FormUrl = formUrl + "?returnUrl=" + Url.Encode(formStartUrl);
            }

            return PartialView("ApplyButton", navigationApplyButtonModel);
        }

        public ActionResult RenderGlobalSearch()
        {
            var setting = Umbraco.ContentQuery.TypedContentSingleAtXPath("//settingsSite");
            var home = Umbraco.ContentQuery.TypedContentSingleAtXPath("//pageHome");

            if (setting == null || home == null)
            {
                return Content("");
            }

            var settings = new SettingsSite(setting);
            var homepage = new PageHome(home);
            if (settings.SearchPage.IsNullOrEmpty() || homepage.CourseCareerSearchPage.IsNullOrEmpty())
            {
                return Content("");
            }

            var model = new GlobalSearchViewModel();
            model.SearchUrl = settings.SearchPage.FirstOrDefault()?.Url;
            model.CourseCareerUrl = homepage.CourseCareerSearchPage.FirstOrDefault()?.Url;

            return PartialView("GlobalSearchBar", model);
        }
    }
}