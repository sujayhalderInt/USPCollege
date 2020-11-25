using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
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
    public class PageBlogListingController : BaseListingController
    {
        private readonly ISearchService _searchService;

        public PageBlogListingController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        
        public override ActionResult Index(RenderModel model)
        {
            var vm = new PageBlogListing(model.Content);
            
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

            if (!string.IsNullOrEmpty(other))
            {
                var fDate = other.Split(',');
                foreach (var item in fDate)
                {
                    var _item = item.Split('-');

                    if (_item != null && _item[0] == "StartDate")
                    {
                        vm.ListingPostback.StartDate = _item[1];
                    }
                    else if (_item != null && _item[0] == "EndDate")
                    {
                        vm.ListingPostback.EndDate = _item[1];
                    }

                }
            }

            vm.ListingPostback.Page = page;
            vm.ListingPostback.SearchText = term;
            vm.ListingPostback.SortBy = sortBy;
            vm.ListingPostback.SelectedTaxonomy = taxonomy;

            BuildModel(vm);
            
            return CurrentTemplate(vm);
        }

        [HttpPost]
        public ActionResult Index(PageBlogListing model, [Bind(Prefix = "ListingPostback")]ListingPostback postback, string term = null)
        {
            if (postback == null)
            {
                postback = new ListingPostback { SearchText = term, Page = 1 };
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
                return PartialView("~/Views/Pages/PageBlogListing/BlogListing.cshtml", model);
            }

            return CurrentTemplate(model);
        }

        private void BuildModel(PageBlogListing pageBlogListing)
        {
            var setting = Umbraco.ContentQuery.TypedContentSingleAtXPath($"//{SiteConstants.AliasBlogFilterSettings}");
            var blogSettings = new SettingsBlogListingFilters(setting);

            var searchParams = new SearchParameters();
            searchParams.SearchType = SiteConstants.SearchType.BlogSearch;

            searchParams.DocTypeAliases = new[] { PageBlogDetail.ModelTypeAlias };
            searchParams.ParentId = pageBlogListing.Id.ToString();
            searchParams.PageSize = 10;
            searchParams.Page = pageBlogListing.ListingPostback.Page;
            searchParams.Taxonomy = GetTaxonomy(pageBlogListing.ListingPostback?.SelectedTaxonomy);
            searchParams.Term = pageBlogListing.ListingPostback.SearchText;
            searchParams.SortBy = pageBlogListing.ListingPostback.SortBy ?? SiteConstants.Sort.DateDesc;
            searchParams.StartDate = pageBlogListing.ListingPostback.StartDate.ToIndexDate();
            searchParams.EndDate = pageBlogListing.ListingPostback.EndDate.ToIndexDate();


            pageBlogListing.SearchResults = _searchService.Search(searchParams);
            pageBlogListing.FilterItems = blogSettings.BlogListingFilters.OfType<DataSearchFilter>().ToList();
        } 
    }
}
