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
	/// <summary>[Form] Application Form Step Ten</summary>
	[PublishedContentModel("formStepTen")]
	public partial class FormStepTen : PublishedContentModel, IBaseForm, IBaseHeading, IBaseMetaInformation, IBaseNavigation
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "formStepTen";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public FormStepTen(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<FormStepTen, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// Disability Checkbox List: "Prefer not to say" option will be added automatically and the list will also be sorted alphabetically
		///</summary>
		[ImplementPropertyType("checkBoxListDisability")]
		public IEnumerable<string> CheckBoxListDisability
		{
			get { return this.GetPropertyValue<IEnumerable<string>>("checkBoxListDisability"); }
		}

		///<summary>
		/// Convictions Heading
		///</summary>
		[ImplementPropertyType("convictionsHeading")]
		public string ConvictionsHeading
		{
			get { return this.GetPropertyValue<string>("convictionsHeading"); }
		}

		///<summary>
		/// Ethnicity Drop Down List: First option is the not selected
		///</summary>
		[ImplementPropertyType("dropDownListEthnicity")]
		public IEnumerable<string> DropDownListEthnicity
		{
			get { return this.GetPropertyValue<IEnumerable<string>>("dropDownListEthnicity"); }
		}

		///<summary>
		/// Equal Opportunities Heading
		///</summary>
		[ImplementPropertyType("equalOpportunitiesHeading")]
		public string EqualOpportunitiesHeading
		{
			get { return this.GetPropertyValue<string>("equalOpportunitiesHeading"); }
		}

		///<summary>
		/// Convictions Label
		///</summary>
		[ImplementPropertyType("labelConvictions")]
		public string LabelConvictions
		{
			get { return this.GetPropertyValue<string>("labelConvictions"); }
		}

		///<summary>
		/// Date From Label
		///</summary>
		[ImplementPropertyType("labelDateFrom")]
		public string LabelDateFrom
		{
			get { return this.GetPropertyValue<string>("labelDateFrom"); }
		}

		///<summary>
		/// Date To Label
		///</summary>
		[ImplementPropertyType("labelDateTo")]
		public string LabelDateTo
		{
			get { return this.GetPropertyValue<string>("labelDateTo"); }
		}

		///<summary>
		/// Disability Label
		///</summary>
		[ImplementPropertyType("labelDisability")]
		public string LabelDisability
		{
			get { return this.GetPropertyValue<string>("labelDisability"); }
		}

		///<summary>
		/// Do You Have A Disability Label
		///</summary>
		[ImplementPropertyType("labelDoYouHaveAdisability")]
		public string LabelDoYouHaveAdisability
		{
			get { return this.GetPropertyValue<string>("labelDoYouHaveAdisability"); }
		}

		///<summary>
		/// Ethnicity Label
		///</summary>
		[ImplementPropertyType("labelEthnicity")]
		public string LabelEthnicity
		{
			get { return this.GetPropertyValue<string>("labelEthnicity"); }
		}

		///<summary>
		/// First Language Label
		///</summary>
		[ImplementPropertyType("labelFirstLanguage")]
		public string LabelFirstLanguage
		{
			get { return this.GetPropertyValue<string>("labelFirstLanguage"); }
		}

		///<summary>
		/// Nationality Label
		///</summary>
		[ImplementPropertyType("labelNationality")]
		public string LabelNationality
		{
			get { return this.GetPropertyValue<string>("labelNationality"); }
		}

		///<summary>
		/// Primary Disability Label
		///</summary>
		[ImplementPropertyType("labelPrimaryDisability")]
		public string LabelPrimaryDisability
		{
			get { return this.GetPropertyValue<string>("labelPrimaryDisability"); }
		}

		///<summary>
		/// Religion Label
		///</summary>
		[ImplementPropertyType("labelReligion")]
		public string LabelReligion
		{
			get { return this.GetPropertyValue<string>("labelReligion"); }
		}

		///<summary>
		/// Resident Of UK / EU Question Label
		///</summary>
		[ImplementPropertyType("labelResidentOfUkEuQuestion")]
		public string LabelResidentOfUkEuQuestion
		{
			get { return this.GetPropertyValue<string>("labelResidentOfUkEuQuestion"); }
		}

		///<summary>
		/// Which Country Label
		///</summary>
		[ImplementPropertyType("labelWhichCountry")]
		public string LabelWhichCountry
		{
			get { return this.GetPropertyValue<string>("labelWhichCountry"); }
		}

		///<summary>
		/// Resident Heading
		///</summary>
		[ImplementPropertyType("residentHeading")]
		public string ResidentHeading
		{
			get { return this.GetPropertyValue<string>("residentHeading"); }
		}

		///<summary>
		/// Conviction Information Text Alert
		///</summary>
		[ImplementPropertyType("textAlertConvictionInformation")]
		public string TextAlertConvictionInformation
		{
			get { return this.GetPropertyValue<string>("textAlertConvictionInformation"); }
		}

		///<summary>
		/// Continue Button Text
		///</summary>
		[ImplementPropertyType("textContinueButton")]
		public string TextContinueButton
		{
			get { return this.GetPropertyValue<string>("textContinueButton"); }
		}

		///<summary>
		/// Equal Opportunities Information Text
		///</summary>
		[ImplementPropertyType("textEqualOpportunitiesInformation")]
		public string TextEqualOpportunitiesInformation
		{
			get { return this.GetPropertyValue<string>("textEqualOpportunitiesInformation"); }
		}

		///<summary>
		/// Which Dates Text
		///</summary>
		[ImplementPropertyType("textWhichDates")]
		public string TextWhichDates
		{
			get { return this.GetPropertyValue<string>("textWhichDates"); }
		}

		///<summary>
		/// Listing Name
		///</summary>
		[ImplementPropertyType("listingName")]
		public string ListingName
		{
			get { return USP.Business.Models.ContentModels.BaseForm.GetListingName(this); }
		}

		///<summary>
		/// Sub Heading
		///</summary>
		[ImplementPropertyType("subHeading")]
		public string SubHeading
		{
			get { return USP.Business.Models.ContentModels.BaseForm.GetSubHeading(this); }
		}

		///<summary>
		/// Information Text
		///</summary>
		[ImplementPropertyType("textInformation")]
		public IHtmlString TextInformation
		{
			get { return USP.Business.Models.ContentModels.BaseForm.GetTextInformation(this); }
		}

		///<summary>
		/// Progress Bar Width
		///</summary>
		[ImplementPropertyType("widthProgressBar")]
		public int WidthProgressBar
		{
			get { return USP.Business.Models.ContentModels.BaseForm.GetWidthProgressBar(this); }
		}

		///<summary>
		/// Heading
		///</summary>
		[ImplementPropertyType("heading")]
		public string Heading
		{
			get { return USP.Business.Models.ContentModels.BaseHeading.GetHeading(this); }
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
