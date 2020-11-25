using USP.Business.Constants;
using USP.Business.UmbracoValidationAttributes;

namespace USP.Business.Models.PostBackModels.Application
{
    public class FormStepFivePostBackModel
    {
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public string ParentOrCarerFirstName { get; set; }
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public string ParentOrCarerLastName { get; set; }
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public string ParentOrCarerRelationshipToStudent { get; set; }
        public string ParentOrCarerPhoneNumber { get; set; }
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        [UmbracoEmail(ErrorMessageDictionaryKey = SiteConstants.UmbracoDictionaryKey.ApplicationForm.InvalidEmail)]
        public string ParentOrCarerEmailAddress { get; set; }
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public bool? IsParentOrCarerPrimaryContact { get; set; }


        [UmbracoRequiredIf(SiteConstants.ApplicationFormFieldRequiredMessageKey, "IsParentOrCarerPrimaryContact", false)]
        public string PrimaryContactFirstName { get; set; }
        [UmbracoRequiredIf(SiteConstants.ApplicationFormFieldRequiredMessageKey, "IsParentOrCarerPrimaryContact", false)]
        public string PrimaryContactLastName { get; set; }
        [UmbracoRequiredIf(SiteConstants.ApplicationFormFieldRequiredMessageKey, "IsParentOrCarerPrimaryContact", false)]
        public string PrimaryContactRelationshipToStudent { get; set; }
        [UmbracoRequiredIf(SiteConstants.ApplicationFormFieldRequiredMessageKey, "IsParentOrCarerPrimaryContact", false)]
        public string PrimaryContactPhoneNumber { get; set; }
        [UmbracoRequiredIf(SiteConstants.ApplicationFormFieldRequiredMessageKey, "IsParentOrCarerPrimaryContact", false)]
        [UmbracoEmail(ErrorMessageDictionaryKey = SiteConstants.UmbracoDictionaryKey.ApplicationForm.InvalidEmail)]
        public string PrimaryContactEmailAddress { get; set; }
    }
}
