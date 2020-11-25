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
    public class WidgetRelatedBlogsSurfaceController : DocTypeGridEditorSurfaceController
    {
        private readonly ISearchService _searchService;

        public WidgetRelatedBlogsSurfaceController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public ActionResult WidgetRelatedBlogs()
        {
            var model = new WidgetRelatedBlogs(Model);
            model.RelatedBlogs = GetRelatedBlogs(model);
            return CurrentPartialView(model);
        }

        private IEnumerable<PageBlogDetail> GetRelatedBlogs(WidgetRelatedBlogs widget)
        {
            if (!widget.Automatic)
            {
                return widget.Blogs.OfType<PageBlogDetail>();
            }
            return GetResultsFromPage();
        }

        private IEnumerable<PageBlogDetail> GetResultsFromPage()
        {
            if (UmbracoContext.PublishedContentRequest.PublishedContent == null)
            {
                return null;
            }

            var currentPage = UmbracoContext.PublishedContentRequest.PublishedContent;
            if (!currentPage.HasProperty(SiteConstants.Fields.BlogAuthor) ||
                !currentPage.HasValue(SiteConstants.Fields.BlogAuthor))
            {
                return null;
            }

            var searchParams = new SearchParameters()
            {
                Page = 1,
                PageSize = 3,
                DocTypeAliases = new[] { PageBlogDetail.ModelTypeAlias },
                SortBy = SiteConstants.Sort.DateDesc,
                BasicTaxonomy = GetAuthorGuid(currentPage),
                ExcludeNodeId = new[] { currentPage.Id }
            };

            var results = _searchService.Search(searchParams);

            if (!results.HasResults) return null;

            var pages = new List<PageBlogDetail>();

            foreach (var searchResultsItem in results.Results)
            {
                var content = Umbraco.TypedContent(searchResultsItem.Id);
                if (content != null)
                {
                    pages.Add(new PageBlogDetail(content));
                }
            }

            return pages;
        }

        public List<string> GetAuthorGuid(IPublishedContent currentPage)
        {
            var authors = new List<string>();
            var author = currentPage.GetPropertyValue<IEnumerable<IPublishedContent>>(SiteConstants.Fields.BlogAuthor)?.FirstOrDefault().GetKey().ToIndexString();
            authors.Add(author);
            return authors;
        }
    }
}
