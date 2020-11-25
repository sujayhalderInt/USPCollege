using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Umbraco.Web.WebApi;
using USP.Business.Models.ContentModels;
using USP.Business.Models.MappingClasses.Search;
using USP.Business.Models.PredictiveSearch;
using USP.Business.Services.Interfaces;

namespace USP.Business.Controllers.API
{
    public class PredictiveSearchController : UmbracoApiController
    {
        private readonly IPredictiveSearchService _searchService;

        public PredictiveSearchController(IPredictiveSearchService searchService)
        {
            _searchService = searchService;
        }

        
        public IHttpActionResult GetResults(string term)
        {
            if (string.IsNullOrEmpty(term)) return Json(new PredictiveSearch[0]);

            var searchTerm = term.Split(' ')[0];

            var parameters = new PredictiveSearchParameters { Term = searchTerm };
            var searchResults = GetSearchResults(parameters);

            if (searchResults == null) return Json(new PredictiveSearch[0]);

            var predictiveSearch = new List<PredictiveSearch>();

            foreach (var sortedItem in GetSortedResults(searchResults, searchTerm).Take(5))
            {
                var item = new PredictiveSearch();
                item.ItemUrl = sortedItem.ResultItem.Url;
                item.Heading = sortedItem.ResultItem.Heading;
                item.ResultType = sortedItem.ResultItem.DocTypeAlias == PageCareerDetail.ModelTypeAlias ? "Career" : "Course";
                predictiveSearch.Add(item);
            }

            return Json(predictiveSearch.ToArray());
        }

        public IHttpActionResult GetNewsResults(string term)
        {
            if (string.IsNullOrEmpty(term)) return Json(new PredictiveSearch[0]);

            var searchTerm = term.Split(' ')[0];

            var parameters = new PredictiveSearchParameters { Term = searchTerm };
            var searchResults = GetNewsSearchResults(parameters);

            if (searchResults == null) return Json(new PredictiveSearch[0]);

            var predictiveSearch = new List<PredictiveSearch>();

            foreach (var sortedItem in GetSortedNewsResults(searchResults, searchTerm, PageNewsDetail.ModelTypeAlias).Take(5))
            {
                var item = new PredictiveSearch();
                item.ItemUrl = sortedItem.ResultItem.Url;
                item.Heading = sortedItem.ResultItem.Heading;
                item.ResultType = "News";
                predictiveSearch.Add(item);
            }

            return Json(predictiveSearch.ToArray());
        }

        public IHttpActionResult GetBlogResults(string term)
        {
            if (string.IsNullOrEmpty(term)) return Json(new PredictiveSearch[0]);

            var searchTerm = term.Split(' ')[0];

            var parameters = new PredictiveSearchParameters { Term = searchTerm };
            var searchResults = GetBlogSearchResults(parameters);

            if (searchResults == null) return Json(new PredictiveSearch[0]);

            var predictiveSearch = new List<PredictiveSearch>();

            foreach (var sortedItem in GetSortedNewsResults(searchResults, searchTerm, PageBlogDetail.ModelTypeAlias).Take(5))
            {
                var item = new PredictiveSearch();
                item.ItemUrl = sortedItem.ResultItem.Url;
                item.Heading = sortedItem.ResultItem.Heading;
                item.ResultType = "Blog";
                predictiveSearch.Add(item);
            }

            return Json(predictiveSearch.ToArray());
        }

        public IHttpActionResult GetEventResults(string term)
        {
            if (string.IsNullOrEmpty(term)) return Json(new PredictiveSearch[0]);

            var searchTerm = term.Split(' ')[0];

            var parameters = new PredictiveSearchParameters { Term = searchTerm };
            var searchResults = GetEventSearchResults(parameters);

            if (searchResults == null) return Json(new PredictiveSearch[0]);

            var predictiveSearch = new List<PredictiveSearch>();

            foreach (var sortedItem in GetSortedNewsResults(searchResults, searchTerm, PageEventDetail.ModelTypeAlias).Take(5))
            {
                var item = new PredictiveSearch();
                item.ItemUrl = sortedItem.ResultItem.Url;
                item.Heading = sortedItem.ResultItem.Heading;
                item.ResultType = "Event";
                predictiveSearch.Add(item);
            }

            return Json(predictiveSearch.ToArray());
        }


        public IHttpActionResult GetCourseResults(string term)
        {
            if (string.IsNullOrEmpty(term)) return Json(new PredictiveSearch[0]);

            var searchTerm = term.Split(' ')[0];

            var parameters = new PredictiveSearchParameters { Term = searchTerm };
            var searchResults = GetCourseSearchResults(parameters);

            if (searchResults == null) return Json(new PredictiveSearch[0]);

            var predictiveSearch = new List<PredictiveSearch>();

            foreach (var sortedItem in GetSortedNewsResults(searchResults, searchTerm, PageCourseDetail.ModelTypeAlias).Take(5))
            {
                var item = new PredictiveSearch();
                item.ItemUrl = sortedItem.ResultItem.Url;
                item.Heading = sortedItem.ResultItem.Heading;
                item.ResultType = "Course";
                predictiveSearch.Add(item);
            }

            return Json(predictiveSearch.ToArray());
        }

        public IEnumerable<SearchResultItem> GetSearchResults(PredictiveSearchParameters parameters)
        {
            var predictiveSearchParams = new PredictiveSearchParameters();
            predictiveSearchParams.Term = parameters.Term;
            predictiveSearchParams.DocTypeAliases = new[] { PageCareerDetail.ModelTypeAlias, PageCourseDetail.ModelTypeAlias };
            predictiveSearchParams.PageSize = int.MaxValue;
            predictiveSearchParams.Page = 1;

            var search = _searchService.PredictiveSearch(predictiveSearchParams);
            return !search.HasResults ? null : search.Results;
        }

        public IEnumerable<SearchResultItem> GetBlogSearchResults(PredictiveSearchParameters parameters)
        {
            var predictiveSearchParams = new PredictiveSearchParameters();
            predictiveSearchParams.Term = parameters.Term;
            predictiveSearchParams.DocTypeAliases = new[] { PageBlogDetail.ModelTypeAlias};
            predictiveSearchParams.PageSize = int.MaxValue;
            predictiveSearchParams.Page = 1;

            var search = _searchService.PredictiveSearch(predictiveSearchParams);
            return !search.HasResults ? null : search.Results;
        }

        public IEnumerable<SearchResultItem> GetCourseSearchResults(PredictiveSearchParameters parameters)
        {
            var predictiveSearchParams = new PredictiveSearchParameters();
            predictiveSearchParams.Term = parameters.Term;
            predictiveSearchParams.DocTypeAliases = new[] { PageCourseDetail.ModelTypeAlias };
            predictiveSearchParams.PageSize = int.MaxValue;
            predictiveSearchParams.Page = 1;

            var search = _searchService.PredictiveSearch(predictiveSearchParams);
            return !search.HasResults ? null : search.Results;
        }


        public IEnumerable<SearchResultItem> GetNewsSearchResults(PredictiveSearchParameters parameters)
        {
            var predictiveSearchParams = new PredictiveSearchParameters();
            predictiveSearchParams.Term = parameters.Term;
            predictiveSearchParams.DocTypeAliases = new[] { PageNewsDetail.ModelTypeAlias};
            predictiveSearchParams.PageSize = int.MaxValue;
            predictiveSearchParams.Page = 1;

            var search = _searchService.PredictiveSearch(predictiveSearchParams);
            return !search.HasResults ? null : search.Results;
        }

        public IEnumerable<SearchResultItem> GetEventSearchResults(PredictiveSearchParameters parameters)
        {
            var predictiveSearchParams = new PredictiveSearchParameters();
            predictiveSearchParams.Term = parameters.Term;
            predictiveSearchParams.DocTypeAliases = new[] { PageEventDetail.ModelTypeAlias};
            predictiveSearchParams.PageSize = int.MaxValue;
            predictiveSearchParams.Page = 1;

            var search = _searchService.PredictiveSearch(predictiveSearchParams);
            return !search.HasResults ? null : search.Results;
        }

        public IEnumerable<PredictiveSearchItem> GetSortedResults(IEnumerable<SearchResultItem> searchResultItem, string term)
        {
            var predictiveSearchList = new List<PredictiveSearchItem>();

            foreach (var item in searchResultItem)
            {
                PredictiveSearchItem predictiveSearchItem = new PredictiveSearchItem();

                if (item.DocTypeAlias == PageCareerDetail.ModelTypeAlias)
                {
                    predictiveSearchItem.ResultItem = item;
                    predictiveSearchItem.Precedence = 1;
                    predictiveSearchItem.AdjacentFactor = GetAdjacentFactor(term, item.Heading);
                }
                if (item.DocTypeAlias == PageCourseDetail.ModelTypeAlias)
                {
                    predictiveSearchItem.Precedence = 2;
                    predictiveSearchItem.ResultItem = item;
                    predictiveSearchItem.AdjacentFactor = GetAdjacentFactor(term, item.Heading);
                }

                predictiveSearchList.Add(predictiveSearchItem);
            }

            return predictiveSearchList.OrderBy(x => x.Precedence).ThenBy(x => x.AdjacentFactor).ThenBy(x => x.ResultItem.Heading);
        }

        public IEnumerable<PredictiveSearchItem> GetSortedNewsResults(IEnumerable<SearchResultItem> searchResultItem, string term, string ModelTypeAlias)
        {
            var predictiveSearchList = new List<PredictiveSearchItem>();

            foreach (var item in searchResultItem)
            {
                PredictiveSearchItem predictiveSearchItem = new PredictiveSearchItem();

                if (item.DocTypeAlias == ModelTypeAlias)
                {
                    predictiveSearchItem.ResultItem = item;
                    predictiveSearchItem.Precedence = 1;
                    predictiveSearchItem.AdjacentFactor = GetAdjacentFactor(term, item.Heading);
                }
                

                predictiveSearchList.Add(predictiveSearchItem);
            }

            return predictiveSearchList.OrderBy(x => x.Precedence).ThenBy(x => x.AdjacentFactor).ThenBy(x => x.ResultItem.Heading);
        }

        public int GetAdjacentFactor(string term, string heading)
        {
            return heading.IndexOf(term, StringComparison.OrdinalIgnoreCase);
        }
    }
}
