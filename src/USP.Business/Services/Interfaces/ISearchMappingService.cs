using System.Collections.Generic;
using Examine;
using USP.Business.Models.MappingClasses.Search;

namespace USP.Business.Services.Interfaces
{
    public interface ISearchMappingService
    {
        IEnumerable<SearchResultItem> MapSearchResults(IEnumerable<SearchResult> searchResults);
    }
}