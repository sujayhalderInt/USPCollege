//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder v3.0.10.102
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.ModelsBuilder;
using Umbraco.ModelsBuilder.Umbraco;

namespace USP.Business.Models.ContentModels
{
	/// <summary>[Page] Course Listing</summary>
	[PublishedContentModel("pageCourseListing")]
	public partial class PageCourseListing : PublishedContentModel, IBaseContentBanner, IBaseMetaInformation, IBaseNavigation
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "pageCourseListing";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public PageCourseListing(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<PageCourseListing, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Course Type: Prefilter the courses by Course Type taxonomy
		///</summary>
		[ImplementPropertyType("courseType")]
		public IEnumerable<IPublishedContent> CourseType
		{
			get { return this.GetPropertyValue<IEnumerable<IPublishedContent>>("courseType"); }
		}

		///<summary>
		/// Banner Heading: H1 heading
		///</summary>
		[ImplementPropertyType("bannerHeading")]
		public string BannerHeading
		{
			get { return USP.Business.Models.ContentModels.BaseContentBanner.GetBannerHeading(this); }
		}

		///<summary>
		/// Banner Image
		///</summary>
		[ImplementPropertyType("bannerImage")]
		public IPublishedContent BannerImage
		{
			get { return USP.Business.Models.ContentModels.BaseContentBanner.GetBannerImage(this); }
		}

		///<summary>
		/// Banner Summary: 80 characters max
		///</summary>
		[ImplementPropertyType("bannerSummary")]
		public string BannerSummary
		{
			get { return USP.Business.Models.ContentModels.BaseContentBanner.GetBannerSummary(this); }
		}

		///<summary>
		/// Overlay
		///</summary>
		[ImplementPropertyType("overlay")]
		public object Overlay
		{
			get { return USP.Business.Models.ContentModels.BaseContentBanner.GetOverlay(this); }
		}

		///<summary>
		/// Hide From Sitemap
		///</summary>
		[ImplementPropertyType("hideFromSitemap")]
		public bool HideFromSitemap
		{
			get { return USP.Business.Models.ContentModels.BaseMetaInformation.GetHideFromSitemap(this); }
		}

		///<summary>
		/// Metadata Description
		///</summary>
		[ImplementPropertyType("metadataDescription")]
		public string MetadataDescription
		{
			get { return USP.Business.Models.ContentModels.BaseMetaInformation.GetMetadataDescription(this); }
		}

		///<summary>
		/// Metadata Keywords
		///</summary>
		[ImplementPropertyType("metadataKeywords")]
		public IEnumerable<string> MetadataKeywords
		{
			get { return USP.Business.Models.ContentModels.BaseMetaInformation.GetMetadataKeywords(this); }
		}

		///<summary>
		/// Metadata Title
		///</summary>
		[ImplementPropertyType("metadataTitle")]
		public string MetadataTitle
		{
			get { return USP.Business.Models.ContentModels.BaseMetaInformation.GetMetadataTitle(this); }
		}

		///<summary>
		/// Hide Children From Navigation: Hides all child pages from navigation menus
		///</summary>
		[ImplementPropertyType("hideChildrenFromNavigation")]
		public bool HideChildrenFromNavigation
		{
			get { return USP.Business.Models.ContentModels.BaseNavigation.GetHideChildrenFromNavigation(this); }
		}

		///<summary>
		/// Hide From Navigation: Hides both this page and it's children from navigation menus
		///</summary>
		[ImplementPropertyType("hideFromNavigation")]
		public bool HideFromNavigation
		{
			get { return USP.Business.Models.ContentModels.BaseNavigation.GetHideFromNavigation(this); }
		}

		///<summary>
		/// Hide From Search: Do not show this page in search result or listing pages
		///</summary>
		[ImplementPropertyType("hideFromSearch")]
		public bool HideFromSearch
		{
			get { return USP.Business.Models.ContentModels.BaseNavigation.GetHideFromSearch(this); }
		}

		///<summary>
		/// Navigation Title: Name of page shown in breadcrumb and navigation menus
		///</summary>
		[ImplementPropertyType("navigationTitle")]
		public string NavigationTitle
		{
			get { return USP.Business.Models.ContentModels.BaseNavigation.GetNavigationTitle(this); }
		}
	}
}
