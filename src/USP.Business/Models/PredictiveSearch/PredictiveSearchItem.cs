using USP.Business.Models.MappingClasses.Search;

namespace USP.Business.Models.PredictiveSearch
{
    public class PredictiveSearchItem
    {
        public SearchResultItem ResultItem { get; set; }
        public int Precedence { get; set; }
        public int AdjacentFactor { get; set; }
    }
}
