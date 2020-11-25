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
    public class WidgetRelatedNewsSurfaceController : DocTypeGridEditorSurfaceController
    {
        private readonly ISearchService _searchService;

        public WidgetRelatedNewsSurfaceController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public ActionResult WidgetRelatedNews()
        {
            var model = new WidgetRelatedNews(Model);
            model.RelatedNews = GetRelatedNews(model);
            return CurrentPartialView(model);
        }

        public IEnumerable<PageNewsDetail> GetRelatedNews(WidgetRelatedNews widget)
        {
            if (!widget.Automatic)
            {
                return widget.News.OfType<PageNewsDetail>();
            }
            return GetResultsFromPage();
        }

        public IEnumerable<PageNewsDetail> GetResultsFromPage()
        {
            if (UmbracoContext.PublishedContentRequest.PublishedContent == null)
            {
                return null;
            }

            var currentPage = UmbracoContext.PublishedContentRequest.PublishedContent;
            if ((!currentPage.HasProperty(SiteConstants.Fields.NewsTopics) || !currentPage.HasValue(SiteConstants.Fields.NewsTopics)) &&
                (!currentPage.HasProperty(SiteConstants.Fields.Campus) || !currentPage.HasValue(SiteConstants.Fields.Campus)))
            {
                return null;
            }

            var searchParams = new SearchParameters()
            {
                Page = 1,
                PageSize = 3,
                DocTypeAliases = new[] { PageNewsDetail.ModelTypeAlias },
                SortBy = SiteConstants.Sort.DateDesc,
                Taxonomy = GetTaxonomy(currentPage),
                ExcludeNodeId = new[] { currentPage.Id }
            };

            var results = _searchService.Search(searchParams);

            if (!results.HasResults) return null;

            var pages = new List<PageNewsDetail>();

            foreach (var searchResultsItem in results.Results)
            {
                var content = Umbraco.TypedContent(searchResultsItem.Id);
                if (content != null)
                {
                    pages.Add(new PageNewsDetail(content));
                }
            }

            return pages;
        }

        private Dictionary<string, List<string>> GetTaxonomy(IPublishedContent currentPage)
        {
            const string newsTopicsAlias = SiteConstants.Fields.NewsTopics;
            const string campusAlias = SiteConstants.Fields.Campus;

            var newsTopicsTaxonomy = currentPage.GetPropertyValue<IEnumerable<IPublishedContent>>(newsTopicsAlias).Select(t => t.GetKey().ToIndexString()).ToList();
            var campusTaxonomy = currentPage.GetPropertyValue<IEnumerable<IPublishedContent>>(campusAlias).Select(t => t.GetKey().ToIndexString()).ToList();

            if (!newsTopicsTaxonomy.Any() && !campusTaxonomy.Any()) return null;

            var newsTopicsKeyValuesPair = GetKeyValuesPair(newsTopicsAlias, newsTopicsTaxonomy);
            var campusKeyValuesPair = GetKeyValuesPair(campusAlias, campusTaxonomy);

            return newsTopicsKeyValuesPair.Concat(campusKeyValuesPair).ToDictionary(x => x.Key, x => x.Value);
        }

        private Dictionary<string, List<string>> GetKeyValuesPair(string key, IEnumerable<string> values)
        {
            var result = new Dictionary<string, List<string>>();

            foreach (var value in values)
            {
                if (result.ContainsKey(key))
                {
                    result[key].Add(value);
                }
                else
                {
                    result.Add(key, new List<string> { value });
                }
            }
            return result;
        }
    }
}
