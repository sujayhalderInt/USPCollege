using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Umbraco.Web;
using Umbraco.Web.Models;
using USP.Business.Constants;
using USP.Business.Controllers.Base;
using USP.Business.Extensions;
using USP.Business.Models.ContentModels;
using USP.Business.Models.Data;
using USP.Business.Models.MappingClasses.Search;
using USP.Business.Services.Interfaces;

namespace USP.Business.Controllers.Pages
{
    public class PageCourseListingController : BaseListingController
    {
        private readonly ISearchService searchService;

        public PageCourseListingController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        public override ActionResult Index(RenderModel model)
        {
            var vm = new PageCourseListing(model.Content);
            vm.ListingPostback = new ListingPostback();
            var pageNo = Request.QueryString["page"];
            var term = Request.QueryString["term"];
            var sortBy = Request.QueryString["sortby"];
            var other = Request.QueryString["other"];
            var filters = Request.QueryString["filters"];

            var page = 1;
            if (!string.IsNullOrEmpty(pageNo))
            {
                page = int.Parse(pageNo);
            }
            
            string[] taxonomy = (!string.IsNullOrEmpty(filters) ? filters.Split(',') : null);

            vm.ListingPostback.Page = page;
            vm.ListingPostback.SearchText = term;
            vm.ListingPostback.SortBy = sortBy;
            vm.ListingPostback.SelectedTaxonomy = taxonomy;
            
            BuildModel(vm);

            return CurrentTemplate(vm);
        }

        [HttpPost]
        public ActionResult Index(PageCourseListing model, [Bind(Prefix = "ListingPostback")] ListingPostback postback, string term = null)
        {
            if (postback == null)
            {
                postback = new ListingPostback() { SearchText = term, Page = 1 };
            }

            if (!string.IsNullOrWhiteSpace(postback.BtnClearFilters))
            {
                postback.SelectedTaxonomy = null;
            }

            if (!string.IsNullOrWhiteSpace(postback.BtnLoadMore))
            {
                postback.Page = postback.Page;
            }


            model.ListingPostback = postback;
            BuildModel(model);

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Pages/PageCourseListing/CourseListing.cshtml", model);
            }

            return CurrentTemplate(model);
        }


        private void BuildModel(PageCourseListing pageCourseListing)
        {
            var setting = Umbraco.ContentQuery.TypedContentSingleAtXPath($"//{SiteConstants.AliasCourseFilterSettings}");
            var courseSettings = new SettingsCourseListingFilters(setting);

            if (!pageCourseListing.CourseType.IsNullOrEmpty())
            {
                var courseType = pageCourseListing.CourseType.First();
                var taxonomies = new List<string>(pageCourseListing.ListingPostback?.SelectedTaxonomy ?? new string[0]);
                taxonomies.Add($"{courseType.Parent.Id}|{courseType.GetKey().ToIndexString()}");
                pageCourseListing.ListingPostback.SelectedTaxonomy = taxonomies.ToArray();
            }

            var searchParams = new SearchParameters();
            searchParams.SearchType = SiteConstants.SearchType.CourseSearch;
            searchParams.DocTypeAliases = new[] { SiteConstants.AliasCourseDetail };
            searchParams.PageSize = 10;
            searchParams.Page = pageCourseListing.ListingPostback.Page;
            searchParams.Taxonomy = GetTaxonomy(pageCourseListing.ListingPostback?.SelectedTaxonomy);
            searchParams.Term = pageCourseListing.ListingPostback.SearchText;
            searchParams.SortBy = pageCourseListing.ListingPostback.SortBy ?? SiteConstants.Sort.Title;

            pageCourseListing.SearchResults = searchService.Search(searchParams);
            pageCourseListing.FilterItems = courseSettings.CourseListingFilters.OfType<DataSearchFilter>().ToList();

            if (!pageCourseListing.CourseType.IsNullOrEmpty())
            {
                var courseType = pageCourseListing.CourseType.First();
                pageCourseListing.FilterItems.RemoveAll(p => p.Taxonomies.FirstOrDefault()?.Id == courseType.Parent.Id);
            }
        }
    }
}
