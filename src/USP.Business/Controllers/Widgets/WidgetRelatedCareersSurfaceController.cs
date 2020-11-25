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
    public class WidgetRelatedCareersSurfaceController : DocTypeGridEditorSurfaceController
    {
        private readonly ISearchService _searchService;

        public WidgetRelatedCareersSurfaceController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public ActionResult WidgetRelatedCareers()
        {
            var model = new WidgetRelatedCareers(Model);
            model.RelatedCareers = GetRelatedCourses(model);
            return CurrentPartialView(model);
        }

        public IEnumerable<PageCareerDetail> GetRelatedCourses(WidgetRelatedCareers widget)
        {
            if (!widget.Automatic)
            {
                return widget.Careers.OfType<PageCareerDetail>();
            }

            return GetResultsFromPage();
        }

        private IEnumerable<PageCareerDetail> GetResultsFromPage()
        {
            if (UmbracoContext.PublishedContentRequest.PublishedContent == null)
            {
                return null;
            }

            var currentPage = UmbracoContext.PublishedContentRequest.PublishedContent;
            if (!currentPage.HasProperty("careerSector") || !currentPage.HasValue("careerSector"))
            {
                return null;
            }

            var searchParams = new SearchParameters()
            {
                Page = 1,
                PageSize = 3,
                DocTypeAliases = new[] {PageCareerDetail.ModelTypeAlias},
                SortBy = SiteConstants.Sort.Date,
                BasicTaxonomy = GetTaxonomy(currentPage),
                ExcludeNodeId = new[]{currentPage.Id}
            };

            var results = _searchService.Search(searchParams);

            if (!results.HasResults)
            {
                return null;
            }

            var pages = new List<PageCareerDetail>();

            foreach (var searchResultItem in results.Results)
            {
                var content = Umbraco.TypedContent(searchResultItem.Id);
                if (content != null)
                {
                    pages.Add(new PageCareerDetail(content));
                }
            }

            return pages;
        }

        private List<string> GetTaxonomy(IPublishedContent currentPage)
        {
            return currentPage.GetPropertyValue<IEnumerable<IPublishedContent>>("careerSector")
                .Select(t => t.GetKey().ToIndexString())
                .ToList();
        }
    }
}
