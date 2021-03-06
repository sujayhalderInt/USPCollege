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
	/// <summary>[Widget] Button</summary>
	[PublishedContentModel("widgetButton")]
	public partial class WidgetButton : PublishedContentModel, IBaseGoogleTrackableItem
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "widgetButton";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public WidgetButton(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<WidgetButton, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Button Link
		///</summary>
		[ImplementPropertyType("buttonLink")]
		public Umbraco.Web.Models.RelatedLinks ButtonLink
		{
			get { return this.GetPropertyValue<Umbraco.Web.Models.RelatedLinks>("buttonLink"); }
		}

		///<summary>
		/// Tracking Name: Enter a tracking name to appear in click tracking
		///</summary>
		[ImplementPropertyType("trackingName")]
		public string TrackingName
		{
			get { return USP.Business.Models.ContentModels.BaseGoogleTrackableItem.GetTrackingName(this); }
		}
	}
}
