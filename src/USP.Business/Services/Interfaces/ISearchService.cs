using System.Collections.Generic;
using Examine;
using Umbraco.Core.Models;
using USP.Business.Models.ContentModels;
using USP.Business.Models.MappingClasses.Search;

namespace USP.Business.Services.Interfaces
{
    public interface ISearchService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        SearchResultsViewModel Search(SearchParameters searchParameters);
    }
}