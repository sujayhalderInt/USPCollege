using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Examine;
using Examine.Providers;
using Examine.SearchCriteria;
using Umbraco.Core.Logging;
using USP.Business.Constants;
using USP.Business.Extensions;
using USP.Business.Models.MappingClasses.Search;
using USP.Business.Services.Interfaces;

namespace USP.Business.Services
{
    public class SearchService : ISearchService
    {
        private readonly BaseSearchProvider _searcher = ExamineManager.Instance.SearchProviderCollection["GlobalSearchSearcher"];
        private readonly ISearchMappingService _searchMappingService;

        #region PUBLIC METHODS

        public SearchService(ISearchMappingService searchMappingService)
        {
            _searchMappingService = searchMappingService;
        }

        public SearchResultsViewModel Search(SearchParameters searchParameters)
        {
            var searchResultsModel = new SearchResultsViewModel
            {
                SearchParameters = searchParameters
            };

            var searchResults = DoSearch(searchParameters);

            IEnumerable<SearchResult> pagedItems;

            if (searchParameters.SearchType == SiteConstants.SearchType.GlobalSearch)
            {
                pagedItems = searchResults
                    .Skip(searchParameters.PageSize * (searchParameters.Page-1))
                    .Take(searchParameters.PageSize);
            }
            else
            {
                
                pagedItems = searchResults.Take(searchParameters.PageSize * searchParameters.Page);
            }

            /*-----------------SEEV 522 start---------*/
            var totalRes = searchResults.Take(searchResults.TotalItemCount);
            var item = _searchMappingService.MapSearchResults(totalRes);
            
            searchResultsModel.TotalCourseItemCount = item.Count(x => x.DocTypeAlias == "pageCourseDetail");
            searchResultsModel.TotalCareerItemCount = item.Count(x => x.DocTypeAlias == "pageCareerDetail");

            /*-----------------SEEV 522 end---------*/
            searchResultsModel.Results = _searchMappingService.MapSearchResults(pagedItems);
            searchResultsModel.TotalItemCount = searchResults.TotalItemCount;
            searchResultsModel.PageNumber = searchParameters.Page;
            

            if (searchResultsModel.TotalItemCount > 0)
            {
                searchResultsModel.PageCount = (int)Math.Ceiling((double)searchResultsModel.TotalItemCount / searchParameters.PageSize);
            }

            return searchResultsModel;
        }

        #endregion

        #region PRIVATE METHODS
        private ISearchResults DoSearch(SearchParameters searchParams)
        {
            //Fetching the PrecedentSearchSearcher 
            ISearchCriteria searchCriteria = ExamineManager.Instance.CreateSearchCriteria(BooleanOperation.And);

            string searchQuery = $"+({SiteConstants.Fields.HideFromSearch}:0)";

            if (!string.IsNullOrWhiteSpace(searchParams.Term))
            {
                searchQuery = GetTermQuery(searchParams, searchQuery);
            }

            searchQuery = GetDocTypesQuery(searchParams, searchQuery);

            if (!string.IsNullOrEmpty(searchParams.ParentId))
            {
                searchQuery += " +(" + SiteConstants.Fields.ExpandedPath + ":" + searchParams.ParentId + ")";
            }

            if (!searchParams.Taxonomy.IsNullOrEmpty())
            {
                searchQuery = FilterSearch(searchParams, searchQuery);
            }

            if (!searchParams.BasicTaxonomy.IsNullOrEmpty())
            {
                searchQuery = BasicFilter(searchParams, searchQuery);
            }

            if (!string.IsNullOrWhiteSpace(searchParams.StartDate) ||  !string.IsNullOrWhiteSpace(searchParams.EndDate))
            {
                searchQuery = FilterDates(searchParams, searchQuery);
            }

            if (!searchParams.ExcludeNodeId.IsNullOrEmpty())
            {
                searchQuery += " -(" + SiteConstants.Fields.Id + ":" + string.Join(" ", searchParams.ExcludeNodeId)  + ")";
            }

            searchCriteria = searchCriteria.RawQuery(searchQuery);
            searchCriteria = DoSort(searchParams, searchCriteria);

            LogHelper.Info<SearchService>("Global Search - Terms: Lucene: {0}", () => searchQuery);

            return _searcher.Search(searchCriteria);
        }

        private string FilterDates(SearchParameters searchParams, string searchQuery)
        {
            var startDate = searchParams.StartDate ?? DateTime.MinValue.ToString().ToIndexDate();
            var endDate = searchParams.EndDate ?? DateTime.MaxValue.ToString().ToIndexDate();


            searchQuery += " +(";

            if (!string.IsNullOrWhiteSpace(searchParams.StartDate))
            {
                searchQuery += $" filterStartDate:[{startDate}000000 TO {endDate}235959]";
            }

            if (!string.IsNullOrWhiteSpace(searchParams.EndDate))
            {
                searchQuery += $" filterEndDate:[{startDate}000000 TO {endDate}235959]";
            }

            searchQuery += ")";

            return searchQuery;
        }

        private string BasicFilter(SearchParameters searchParams, string searchQuery)
        {
            if(!string.IsNullOrEmpty(searchParams.ApplyNowCourse))
            {
                foreach (var taxonomy in searchParams.BasicTaxonomy)
                {
                    searchQuery += $" +({SiteConstants.Fields.TaxonomyId}:{taxonomy})";
                    //searchQuery += $" {SiteConstants.Fields.TaxonomyId}:{taxonomy} ";
                }
            }
            else
            {
                searchQuery += " +(";

                foreach (var taxonomy in searchParams.BasicTaxonomy)
                {
                    //searchQuery += $" +({SiteConstants.Fields.TaxonomyId}:{taxonomy})";
                    searchQuery += $" {SiteConstants.Fields.TaxonomyId}:{taxonomy} ";
                }
                
                searchQuery += ")";
            }

            
            return searchQuery;
        }

        private ISearchCriteria DoSort(SearchParameters searchParams, ISearchCriteria searchQuery)
        {
            if (searchParams.SortBy == SiteConstants.Sort.Level)
            {
                searchQuery.OrderBy(new[] { SiteConstants.Fields.SortCourseLevel, SiteConstants.Fields.BannerHeading });
            }
            else if (searchParams.SortBy == SiteConstants.Sort.SubjectArea)
            {
                searchQuery.OrderBy(new[] { SiteConstants.Fields.SortSubjectArea, SiteConstants.Fields.BannerHeading });
            }
            else if (searchParams.SortBy == SiteConstants.Sort.Date)
            {
                searchQuery.OrderBy(new[] { SiteConstants.Fields.SortDate, SiteConstants.Fields.BannerHeading });
            }
            else if (searchParams.SortBy == SiteConstants.Sort.DateDesc)
            {
                searchQuery.OrderByDescending(new[] { SiteConstants.Fields.SortDate, SiteConstants.Fields.SortBannerHeadingDesc });
            }
            else if (searchParams.SortBy == SiteConstants.Sort.Campus)
            {
                searchQuery.OrderBy(new[] { SiteConstants.Fields.SortCampus, SiteConstants.Fields.BannerHeading });
            }
            else if (searchParams.SortBy == SiteConstants.Sort.CampusDesc)
            {
                searchQuery.OrderByDescending(new[] { SiteConstants.Fields.SortCampus, SiteConstants.Fields.SortBannerHeadingDesc });
            }
            else if (searchParams.SortBy == SiteConstants.Sort.EventType)
            {
                searchQuery.OrderBy(new[] { SiteConstants.Fields.SortEventType, SiteConstants.Fields.BannerHeading });
            }
            else if (searchParams.SortBy == SiteConstants.Sort.BlogAuthor)
            {
                searchQuery.OrderBy(new [] {SiteConstants.Fields.SortBlogAuthorLastName, SiteConstants.Fields.BannerHeading });
            }
            else if (searchParams.SortBy == SiteConstants.Sort.BlogAuthorDesc)
            {
                searchQuery.OrderByDescending(new [] {SiteConstants.Fields.SortBlogAuthorLastName, SiteConstants.Fields.SortBannerHeadingDesc });
            }            
            //else
            //{
            //    searchQuery.OrderBy(new[] { SiteConstants.Fields.BannerHeading });
            //}
            else if (searchParams.SortBy == SiteConstants.Sort.Title)
            {
                searchQuery.OrderBy(new[] { SiteConstants.Fields.BannerHeading });
            }
            return searchQuery;
        }

        private string FilterSearch(SearchParameters searchParams, string searchQuery)
        {
            if (searchParams.Taxonomy == null || searchParams.Taxonomy.Count == 0)
            {
                return searchQuery;
            }


            foreach (var taxonomy in searchParams.Taxonomy)
            {
                // AND Filter Groups
                searchQuery += " +(";

                foreach (var id in taxonomy.Value)
                {
                    searchQuery += $" {SiteConstants.Fields.TaxonomyId}:{id} ";
                }

                searchQuery += ")";

            }


            return searchQuery;
        }

        private string GetTermQuery(SearchParameters searchParams, string searchQuery)
        {
            var term = searchParams.Term.Trim();
            //var term = searchParams.Term.Escape().Value;
            //var term = searchParams.Term;
            term = term.Replace("[", string.Empty).Replace("]",string.Empty);
            Regex reg = new Regex("[*`'\",_&#^@?()$%~!+-/{}:]");

            if (reg.IsMatch(term))
            {
                term = reg.Replace(term, string.Empty);
            }
            
            if (!string.IsNullOrEmpty(term))
            {
                searchQuery += " +(" +
                           $" {SiteConstants.Fields.BannerHeading}:{term}^100.0 {SiteConstants.Fields.BannerHeading}:{term}*^80.0" +
                           $" {SiteConstants.Fields.BannerSummary}: {term}^80.0 {SiteConstants.Fields.BannerSummary}: {term}*^60.0";

                if (searchParams.SearchType == SiteConstants.SearchType.GlobalSearch)
                {
                    searchQuery +=
                        $" {SiteConstants.Fields.MainContent}:{term}^60.0 {SiteConstants.Fields.MainContent}:{term}*^40.0";
                }

                if (searchParams.DocTypeAliases != null
                    && searchParams.DocTypeAliases.Length > 0
                    && searchParams.DocTypeAliases.Contains(SiteConstants.AliasCourseDetail))
                {
                    searchQuery += $"{SiteConstants.Fields.Qualification}:{term}^60.0";
                }

                searchQuery += ")";
            }
            
            return searchQuery;
        }

       /// <summary>
        /// Returns a Lucene query that filters by nodeTypeAlias
        /// </summary>
        private string GetDocTypesQuery(SearchParameters searchParams, string query)
        {
            if (searchParams.DocTypeAliases != null && searchParams.DocTypeAliases.Any())
            {
                query = query + "+(";

                foreach (var docType in searchParams.DocTypeAliases)
                {
                    query = query + " nodeTypeAlias:" + docType;
                }

                query = query + ")";
            }

            return query;
        }
        #endregion

    }
}