using System.Collections.Generic;
using Umbraco.Web;
using USP.Business.Models.Data;
using USP.Business.Models.MappingClasses.Search;

namespace USP.Business.Models.ContentModels
{
    public partial class PageBlogListing
    {
        public PageBlogListing() : base(UmbracoContext.Current.PublishedContentRequest.PublishedContent)
        {
            this.ListingPostback = new ListingPostback(); 
        }
  
        public SearchResultsViewModel SearchResults { get; set; }
        public List<DataSearchFilter> FilterItems { get; set; }
        public ListingPostback ListingPostback { get; set; }
    }
}
