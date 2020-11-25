using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Umbraco.Web;
using Umbraco.Web.Models;
using USP.Business.Constants;
using USP.Business.Extensions;
using USP.Business.Models.ContentModels;
using USP.Business.Models.Data;
using USP.Business.Models.MappingClasses.Search;
using USP.Business.Services.Interfaces;

namespace USP.Business.Controllers.Pages
{   
    public class PageCourseCareerListingController : RenderMvcSurfaceController
    {
        private readonly ISearchService searchService;

        public PageCourseCareerListingController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        //[OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public override ActionResult Index(RenderModel model)
        {
            var vm = new PageCourseCareerListing(model.Content);
           
            vm.ListingPostback = new ListingPostback();

            var pageNo = Request.QueryString["page"];
            var term = Request.QueryString["term"];
            var sortBy = Request.QueryString["sortby"];           
            var filters = Request.QueryString["filters"];
            var types = Request.QueryString["types"];

            var page = 1;
            if (!string.IsNullOrEmpty(pageNo))
            {
                page = int.Parse(pageNo);
            }

            string[] taxonomy = (!string.IsNullOrEmpty(filters) ? filters.Split(',') : null);
            string[] docType = (!string.IsNullOrEmpty(types) ? types.Split(',') : null);

            vm.ListingPostback.Page = page;
            vm.ListingPostback.SearchText = term;
            vm.ListingPostback.SortBy = sortBy;
            vm.ListingPostback.SelectedTaxonomy = taxonomy;
            vm.ListingPostback.SelectedDocTypes = docType;
            
            BuildModel(vm);
            
            return CurrentTemplate(vm);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Index(PageCourseCareerListing model, [Bind(Prefix = "ListingPostback")] ListingPostback postback, string term = null)
        {
            if (postback == null)
            {
                postback = new ListingPostback() { SearchText = term, Page = 1};
            }

            if (!string.IsNullOrWhiteSpace(postback.BtnClearFilters))
            {
                postback.SelectedTaxonomy = null;
            }

            if (!string.IsNullOrWhiteSpace(postback.BtnLoadMore))
            {
                postback.Page = postback.Page + 1;
            }

            model.ListingPostback = postback;
            BuildModel(model);
           
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Pages/PageCourseCareerListing/Listing.cshtml", model);
            }
            
            return CurrentTemplate(model);
            
        }


        private void BuildModel(PageCourseCareerListing pageCourseListing)
        {
            var searchParams = new SearchParameters();
            searchParams.SearchType = SiteConstants.SearchType.CourseSearch;
            searchParams.DocTypeAliases = GetAliases(pageCourseListing.ListingPostback?.SelectedDocTypes);
            searchParams.PageSize = 10;
            searchParams.Page = pageCourseListing.ListingPostback.Page;
            searchParams.Taxonomy = GetTaxonomy(pageCourseListing.ListingPostback?.SelectedTaxonomy);
            searchParams.Term = pageCourseListing.ListingPostback.SearchText;
            searchParams.SortBy = SiteConstants.Sort.Title;

            pageCourseListing.SearchResults = searchService.Search(searchParams);
            pageCourseListing.FilterItems = pageCourseListing.CourseListingFilters.OfType<DataSearchFilter>().ToList();
        }

        private string[] GetAliases(string[] selectedTaxonomy)
        {
            if (selectedTaxonomy == null)
                return new[] { PageCourseDetail.ModelTypeAlias, PageCareerDetail.ModelTypeAlias };

            return selectedTaxonomy;
        }

        private Dictionary<string, List<string>> GetTaxonomy(string[] selectedTaxonomy)
        {
            var result = new Dictionary<string, List<string>>();

            if (selectedTaxonomy != null && selectedTaxonomy.Length > 0)
            {
                foreach (var taxonomy in selectedTaxonomy)
                {
                    var data = taxonomy.Split('|');
                    if (result.ContainsKey(data[0]))
                    {
                        result[data[0]].Add(data[1]);
                    }
                    else
                    {
                        result.Add(data[0], new List<string>() {data[1]});
                    }
                }
            }

            return result;
        }
    }
}
