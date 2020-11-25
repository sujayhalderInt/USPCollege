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
	/// <summary>[Data] Campus Address</summary>
	[PublishedContentModel("dataCampusAddress")]
	public partial class DataCampusAddress : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "dataCampusAddress";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public DataCampusAddress(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<DataCampusAddress, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Address Line 1
		///</summary>
		[ImplementPropertyType("addressLine1")]
		public string AddressLine1
		{
			get { return this.GetPropertyValue<string>("addressLine1"); }
		}

		///<summary>
		/// Address Line 2
		///</summary>
		[ImplementPropertyType("addressLine2")]
		public string AddressLine2
		{
			get { return this.GetPropertyValue<string>("addressLine2"); }
		}

		///<summary>
		/// Campus Name
		///</summary>
		[ImplementPropertyType("campusName")]
		public string CampusName
		{
			get { return this.GetPropertyValue<string>("campusName"); }
		}

		///<summary>
		/// County
		///</summary>
		[ImplementPropertyType("county")]
		public string County
		{
			get { return this.GetPropertyValue<string>("county"); }
		}

		///<summary>
		/// Postcode
		///</summary>
		[ImplementPropertyType("postcode")]
		public string Postcode
		{
			get { return this.GetPropertyValue<string>("postcode"); }
		}

		///<summary>
		/// Telephone Number
		///</summary>
		[ImplementPropertyType("telephoneNumber")]
		public string TelephoneNumber
		{
			get { return this.GetPropertyValue<string>("telephoneNumber"); }
		}

		///<summary>
		/// Town/City
		///</summary>
		[ImplementPropertyType("townCity")]
		public string TownCity
		{
			get { return this.GetPropertyValue<string>("townCity"); }
		}
	}
}
