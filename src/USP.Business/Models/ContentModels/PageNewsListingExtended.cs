using System.Collections.Generic;
using Umbraco.Web;
using USP.Business.Models.Data;
using USP.Business.Models.MappingClasses.Search;

namespace USP.Business.Models.ContentModels
{
    public partial class PageNewsListing
    {
        public PageNewsListing() : base(UmbracoContext.Current.PublishedContentRequest.PublishedContent)
        {
            ListingPostback = new ListingPostback();
        }

        public SearchResultsViewModel SearchResults { get; set; }
        public List<DataSearchFilter> FilterItems { get; set; }
        public ListingPostback ListingPostback { get; set; }
    }
}
