using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using Umbraco.Web.Models;
using USP.Business.Constants;
using USP.Business.Extensions;
using USP.Business.Models.ContentModels;
using USP.Business.Models.Data;
using USP.Business.Models.MappingClasses.Search;
using USP.Business.Services.Interfaces;

namespace USP.Business.Controllers.Pages
{    
    public class PageSearchController : RenderMvcSurfaceController
    {
        public readonly ISearchService _searchService;

        public PageSearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        //[OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public override ActionResult Index(RenderModel model)
        {

            var vm = new PageSearch(model.Content);
            
            vm.ListingPostback = new ListingPostback();
            
            var pageNo = Request.QueryString["page"];
            var term = Request.QueryString["term"];           
            var docTypes = Request.QueryString["types"];

            var page = 1;
            if (!string.IsNullOrEmpty(pageNo))
            {
                page = int.Parse(pageNo);
            }

            string[] docType = null;

            if (!string.IsNullOrEmpty(docTypes))
            {
                string[] docTypeItem = docTypes.Split(',');

                docType = string.Join("|", docTypeItem)
                    .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            }
            

            vm.ListingPostback.Page = page;
            vm.ListingPostback.SearchText = term;
            vm.ListingPostback.SelectedDocTypes = docType;

            BuildModel(vm);
           
            return CurrentTemplate(vm);
        }

        private void BuildModel(PageSearch pageListing)
        {
            var searchParams = new SearchParameters();
            searchParams.SearchType = SiteConstants.SearchType.GlobalSearch;
            searchParams.DocTypeAliases = pageListing.ListingPostback.SelectedDocTypes;
            searchParams.PageSize = 10;
            searchParams.Page = pageListing.ListingPostback.Page;
            searchParams.Term = pageListing.ListingPostback.SearchText;
            if(string.IsNullOrEmpty(pageListing.ListingPostback.SearchText))
            {
                searchParams.SortBy = pageListing.ListingPostback.SortBy ?? SiteConstants.Sort.Title;
            }
            //searchParams.SortBy = pageListing.ListingPostback.SortBy ?? SiteConstants.Sort.Title;

            pageListing.SearchResults = _searchService.Search(searchParams);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Index(PageSearch model, [Bind(Prefix = "ListingPostback")] ListingPostback postback, string term = null)
        {
            if (postback == null)
            {
                postback = new ListingPostback() { SearchText = term, Page = 1 };
            }

            if (!string.IsNullOrWhiteSpace(postback.BtnPage))
            {
                int.TryParse(postback.BtnPage, out int page);
                postback.Page = page;
            }

            if (!string.IsNullOrWhiteSpace(postback.BtnPagePrev))
            {
                postback.Page = postback.Page - 1;
            }

            if (!string.IsNullOrWhiteSpace(postback.BtnPageNext))
            {
                postback.Page = postback.Page + 1;
            }

            if (postback.SelectedDocTypes != null && postback.SelectedDocTypes.Length > 0)
            {
                postback.SelectedDocTypes = string.Join("|", postback.SelectedDocTypes)
                    .Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries).ToArray();
            }

            model.ListingPostback = postback;
            BuildModel(model);           

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Pages/PageSearch/Listing.cshtml", model);
            }
            
            return CurrentTemplate(model);
            
        }

    }
}