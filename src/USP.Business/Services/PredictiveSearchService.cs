using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Examine;
using Examine.Providers;
using Examine.SearchCriteria;
using Umbraco.Core.Logging;
using USP.Business.Constants;
using USP.Business.Models.MappingClasses.Search;
using USP.Business.Services.Interfaces;

namespace USP.Business.Services
{
    public class PredictiveSearchService : IPredictiveSearchService
    {
        private readonly BaseSearchProvider _searcher = ExamineManager.Instance.SearchProviderCollection["GlobalSearchSearcher"];
        private readonly ISearchMappingService _searchMappingService;

        public PredictiveSearchService(ISearchMappingService searchMappingService)
        {
            _searchMappingService = searchMappingService;
        }

        public SearchResultsViewModel PredictiveSearch(PredictiveSearchParameters predictiveSearchParameters)
        {
            var searchResultsModel = new SearchResultsViewModel
            {
                PredictiveSearchParameters = predictiveSearchParameters
            };

            var searchResults = DoSearch(predictiveSearchParameters);

            IEnumerable<SearchResult> pagedItems;

            if (predictiveSearchParameters.SearchType == SiteConstants.SearchType.GlobalSearch)
            {
                pagedItems = searchResults
                    .Skip(predictiveSearchParameters.PageSize * (predictiveSearchParameters.Page - 1))
                    .Take(predictiveSearchParameters.PageSize);
            }
            else
            {
                pagedItems = searchResults.Take(predictiveSearchParameters.PageSize * predictiveSearchParameters.Page);
            }

            searchResultsModel.Results = _searchMappingService.MapSearchResults(pagedItems);
            searchResultsModel.TotalItemCount = searchResults.TotalItemCount;
            searchResultsModel.PageNumber = predictiveSearchParameters.Page;

            if (searchResultsModel.TotalItemCount > 0)
            {
                searchResultsModel.PageCount = (int)Math.Ceiling((double)searchResultsModel.TotalItemCount / predictiveSearchParameters.PageSize);
            }

            return searchResultsModel;
        }

        private ISearchResults DoSearch(PredictiveSearchParameters predictiveSearchParams)
        {
            if (string.IsNullOrEmpty(predictiveSearchParams.Term))
            {
                return null;
            }

            ISearchCriteria searchCriteria = ExamineManager.Instance.CreateSearchCriteria(BooleanOperation.And);

            var searchQuery = $"+({SiteConstants.Fields.HideFromSearch}:0)";

            if (!string.IsNullOrWhiteSpace(predictiveSearchParams.Term))
            {
                searchQuery = GetTermQuery(predictiveSearchParams, searchQuery);
            }

            searchQuery = GetDocTypesQuery(predictiveSearchParams, searchQuery);
            searchCriteria = searchCriteria.RawQuery(searchQuery);

            LogHelper.Info<SearchService>("Predictive search - Terms: Lucene: {0}", () => searchQuery);

            return _searcher.Search(searchCriteria);
        }

        private string GetTermQuery(PredictiveSearchParameters predictiveSearchParams, string searchQuery)
        {
            //var term = predictiveSearchParams.Term.Escape().Value;
            var term = predictiveSearchParams.Term;

            term = term.Replace("[", string.Empty).Replace("]", string.Empty);
            Regex reg = new Regex("[*`'\",_&#^@?()$%~!+-/{}:]");

            if (reg.IsMatch(term))
            {
                term = reg.Replace(term, string.Empty);
            }

            if (!string.IsNullOrEmpty(term))
            {
                searchQuery += $" +({SiteConstants.Fields.BannerHeading}:{term.Trim()}^100.0 {SiteConstants.Fields.BannerHeading}:{term.Trim()}*^80.0)";

            }

            return searchQuery;
        }

        /// <summary>
        /// Returns a Lucene query that filters by nodeTypeAlias
        /// </summary>
        private string GetDocTypesQuery(PredictiveSearchParameters predictiveSearchParams, string query)
        {
            if (predictiveSearchParams.DocTypeAliases != null && predictiveSearchParams.DocTypeAliases.Any())
            {
                query = query + " +(";

                foreach (var docType in predictiveSearchParams.DocTypeAliases)
                {
                    query = query + " nodeTypeAlias:" + docType;
                }

                query = query + ")";
            }
            return query;
        }
    }
}
