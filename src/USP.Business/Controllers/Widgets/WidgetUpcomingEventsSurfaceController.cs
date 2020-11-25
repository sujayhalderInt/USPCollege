using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using Umbraco.Core.Models;
using Umbraco.Web;
using USP.Business.Constants;
using USP.Business.Extensions;
using USP.Business.Models.ContentModels;
using USP.Business.Models.MappingClasses.Search;
using USP.Business.Services.Interfaces;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetUpcomingEventsSurfaceController : DocTypeGridEditorSurfaceController
    {
        private readonly ISearchService _searchService;

        public WidgetUpcomingEventsSurfaceController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public ActionResult WidgetUpcomingEvents()
        {
            var model = new WidgetUpcomingEvents(Model);
            model.EventList = GetUpcomingEvents(model);
            model.MainEventListing = GetMainEventListingPage();
            return CurrentPartialView(model);
        }

        public IEnumerable<PageEventDetail> GetUpcomingEvents(WidgetUpcomingEvents widget)
        {
            if (UmbracoContext.PublishedContentRequest.PublishedContent == null)
            {
                return null;
            }

            var currentPage = UmbracoContext.PublishedContentRequest.PublishedContent;
    
            var campuses = widget.Campuses.OfType<DataTaxonomy>().ToList();

            if (campuses.IsNullOrEmpty())
            {
                return null;
            }

            var searchParams = new SearchParameters()
            {
                Page = 1,
                PageSize = 3,
                DocTypeAliases = new[] {PageEventDetail.ModelTypeAlias},
                SortBy = SiteConstants.Sort.DateDesc,
                BasicTaxonomy = campuses.Select(d => d.GetKey().ToIndexString()).ToList(),
                ExcludeNodeId = new []{ currentPage.Id}
            };

            var results = _searchService.Search(searchParams);

            if (!results.HasResults)
            {
                return null;
            }

            var pages = new List<PageEventDetail>();

            foreach (var searchResultItem in results.Results)
            {
                var content = Umbraco.TypedContent(searchResultItem.Id);
                if (content != null)
                {
                    pages.Add(new PageEventDetail(content));
                }
            }

            return pages;
        }

        public MainEventListing GetMainEventListingPage()
        {
            var settings = Umbraco.ContentQuery.TypedContentSingleAtXPath($"//{SettingsSite.ModelTypeAlias}");
            if (settings == null) return null;

            var settingsPage = new SettingsSite(settings);
            var mainEventListing = new MainEventListing();
            mainEventListing.Page = settingsPage.MainEventListingPage?.FirstOrDefault();
            mainEventListing.CallToAction = !string.IsNullOrEmpty(settingsPage.MainEventListingPageCtaText) ? settingsPage.MainEventListingPageCtaText : SiteConstants.MainEventCtaText;
            return mainEventListing;
        }
    }
}
