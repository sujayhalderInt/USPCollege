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
	// Mixin content Type 1130 with alias "baseGoogleTrackableItem"
	/// <summary>_Base Google Trackable Item</summary>
	public partial interface IBaseGoogleTrackableItem : IPublishedContent
	{
		/// <summary>Tracking Name</summary>
		string TrackingName { get; }
	}

	/// <summary>_Base Google Trackable Item</summary>
	[PublishedContentModel("baseGoogleTrackableItem")]
	public partial class BaseGoogleTrackableItem : PublishedContentModel, IBaseGoogleTrackableItem
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "baseGoogleTrackableItem";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public BaseGoogleTrackableItem(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<BaseGoogleTrackableItem, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Tracking Name: Enter a tracking name to appear in click tracking
		///</summary>
		[ImplementPropertyType("trackingName")]
		public string TrackingName
		{
			get { return GetTrackingName(this); }
		}

		/// <summary>Static getter for Tracking Name</summary>
		public static string GetTrackingName(IBaseGoogleTrackableItem that) { return that.GetPropertyValue<string>("trackingName"); }
	}
}
