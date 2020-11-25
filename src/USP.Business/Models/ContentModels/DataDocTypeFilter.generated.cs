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
	/// <summary>[Data] DocTypeFilter</summary>
	[PublishedContentModel("dataDocTypeFilter")]
	public partial class DataDocTypeFilter : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "dataDocTypeFilter";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public DataDocTypeFilter(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<DataDocTypeFilter, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Filter Name
		///</summary>
		[ImplementPropertyType("filterName")]
		public string FilterName
		{
			get { return this.GetPropertyValue<string>("filterName"); }
		}

		///<summary>
		/// Page Content Types
		///</summary>
		[ImplementPropertyType("pageContentTypes")]
		public nuPickers.Picker PageContentTypes
		{
			get { return this.GetPropertyValue<nuPickers.Picker>("pageContentTypes"); }
		}
	}
}
