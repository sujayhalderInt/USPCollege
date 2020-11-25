using USP.Business.Models.Data;
using USP.Business.Models.MappingClasses.Search;

namespace USP.Business.Models.ContentModels
{
    public partial class PageSearch
    {
        public SearchResultsViewModel SearchResults { get; set; }
        public ListingPostback ListingPostback { get; set; }
    }
}
