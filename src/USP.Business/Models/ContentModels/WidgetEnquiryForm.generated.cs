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
	/// <summary>[Widget] Upcoming Events</summary>
	[PublishedContentModel("widgetEnquiryForm")]
	public partial class WidgetEnquiryForm : PublishedContentModel
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "widgetEnquiryForm";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public WidgetEnquiryForm(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<WidgetEnquiryForm, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

        ///<summary>
        /// Sub Title
        ///</summary>
        [ImplementPropertyType("subTitle")]
		public IHtmlString SubTitle
        {
			get { return this.GetPropertyValue<IHtmlString>("subTitle"); }
		}    
		
		///<summary>
		/// Title
		///</summary>
		[ImplementPropertyType("title")]
		public string Title
		{
			get { return this.GetPropertyValue<string>("title"); }
		}

        ///<summary>
		/// Select Picker
		///</summary>
		[ImplementPropertyType("selectForm")]
        public object SelectForm
        {
            get { return this.GetPropertyValue("selectForm"); }
        }
    }
}
