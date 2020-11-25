using System.Collections.Generic;
using System.Linq;
using USP.Business.Enums;

namespace USP.Business.Models.MappingClasses.Search
{
    public class SearchResultsViewModel
    {
        public SearchParameters SearchParameters { get; set; }
        public PredictiveSearchParameters PredictiveSearchParameters { get; set; }//For predictive search 

        public IEnumerable<SearchResultItem> Results { get; set; }
        public bool HasResults => Results != null && Results.Any();
        public int TotalItemCount { get; set; }
        public int? TotalCourseItemCount { get; set; }
        public int? TotalCareerItemCount { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public SearchConfigurationSettings SearchSettings { get; set; }

        public SortOrder SortOrder { get; set; }
    }

    public class SearchConfigurationSettings
    {
        public int PageSize { get; set; }
    }
}