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
	/// <summary>[Widget] Instagram Feed</summary>
	[PublishedContentModel("widgetInstagramFeed")]
	public partial class WidgetInstagramFeed : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "widgetInstagramFeed";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public WidgetInstagramFeed(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<WidgetInstagramFeed, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Access Token: Access token for Instagram account to display
		///</summary>
		[ImplementPropertyType("accessToken")]
		public string AccessToken
		{
			get { return this.GetPropertyValue<string>("accessToken"); }
		}

		///<summary>
		/// Number of posts
		///</summary>
		[ImplementPropertyType("numberOfPosts")]
		public int NumberOfPosts
		{
			get { return this.GetPropertyValue<int>("numberOfPosts"); }
		}
	}
}
