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
	/// <summary>[Form] Application Form Step Nine</summary>
	[PublishedContentModel("formStepNine")]
	public partial class FormStepNine : PublishedContentModel, IBaseForm, IBaseHeading, IBaseMetaInformation, IBaseNavigation
	{
#pragma warning disable 0109 // new is redundant
		public new const string ModelTypeAlias = "formStepNine";
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
#pragma warning restore 0109

		public FormStepNine(IPublishedContent content)
			: base(content)
		{ }

#pragma warning disable 0109 // new is redundant
		public new static PublishedContentType GetModelContentType()
		{
			return PublishedContentType.Get(ModelItemType, ModelTypeAlias);
		}
#pragma warning restore 0109

		public static PublishedPropertyType GetModelPropertyType<TValue>(Expression<Func<FormStepNine, TValue>> selector)
		{
			return PublishedContentModelUtility.GetModelPropertyType(GetModelContentType(), selector);
		}

		///<summary>
		/// CTA Add Another Button Text
		///</summary>
		[ImplementPropertyType("ctaAddAnotherButtonText")]
		public string CtaAddAnotherButtonText
		{
			get { return this.GetPropertyValue<string>("ctaAddAnotherButtonText"); }
		}

		///<summary>
		/// Grade Or Predicted Grade Label
		///</summary>
		[ImplementPropertyType("labelGradeOrPredictedGrade")]
		public string LabelGradeOrPredictedGrade
		{
			get { return this.GetPropertyValue<string>("labelGradeOrPredictedGrade"); }
		}

		///<summary>
		/// Previous School/College Label: This is only displayed if the student isn't doing Adult Learning, which would be selected in step 2
		///</summary>
		[ImplementPropertyType("labelPreviousSchoolCollege")]
		public string LabelPreviousSchoolCollege
		{
			get { return this.GetPropertyValue<string>("labelPreviousSchoolCollege"); }
		}

		///<summary>
		/// Subject Label
		///</summary>
		[ImplementPropertyType("labelSubject")]
		public string LabelSubject
		{
			get { return this.GetPropertyValue<string>("labelSubject"); }
		}

		///<summary>
		/// Type Label
		///</summary>
		[ImplementPropertyType("labelType")]
		public string LabelType
		{
			get { return this.GetPropertyValue<string>("labelType"); }
		}

		///<summary>
		/// Year Label: This is only displayed if it's Adult Education
		///</summary>
		[ImplementPropertyType("labelYear")]
		public string LabelYear
		{
			get { return this.GetPropertyValue<string>("labelYear"); }
		}

		///<summary>
		/// Previous School/College Heading: This is only displayed if the student isn't doing Adult Learning, which would be selected in step 2
		///</summary>
		[ImplementPropertyType("lastSchoolCollegeHeading")]
		public string LastSchoolCollegeHeading
		{
			get { return this.GetPropertyValue<string>("lastSchoolCollegeHeading"); }
		}

		///<summary>
		/// Qualifications Heading
		///</summary>
		[ImplementPropertyType("qualificationsHeading")]
		public string QualificationsHeading
		{
			get { return this.GetPropertyValue<string>("qualificationsHeading"); }
		}

		///<summary>
		/// Alert Text
		///</summary>
		[ImplementPropertyType("textAlert")]
		public string TextAlert
		{
			get { return this.GetPropertyValue<string>("textAlert"); }
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
		/// Minimum Grade Requirements Text
		///</summary>
		[ImplementPropertyType("textMinimumGradeRequirements")]
		public IHtmlString TextMinimumGradeRequirements
		{
			get { return this.GetPropertyValue<IHtmlString>("textMinimumGradeRequirements"); }
		}

		///<summary>
		/// Qualifications Information Text
		///</summary>
		[ImplementPropertyType("textQualificationsInformation")]
		public IHtmlString TextQualificationsInformation
		{
			get { return this.GetPropertyValue<IHtmlString>("textQualificationsInformation"); }
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
