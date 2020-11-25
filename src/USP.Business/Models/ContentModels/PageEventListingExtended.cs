using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.ModelsBuilder;
using Umbraco.ModelsBuilder.Umbraco;
using Umbraco.Web.WebServices;
using USP.Business.Models.Data;
using USP.Business.Models.MappingClasses.Search;

namespace USP.Business.Models.ContentModels
{
	/// <summary>[Page] Course Listing</summary>
	public partial class PageEventListing
    {
	    public PageEventListing() : base(UmbracoContext.Current.PublishedContentRequest.PublishedContent)
	    {
            this.ListingPostback = new ListingPostback();
	    }

	    public SearchResultsViewModel SearchResults { get; set; }
	    public List<DataSearchFilter> FilterItems { get; set; }
        public ListingPostback ListingPostback { get; set; }

    }
}
