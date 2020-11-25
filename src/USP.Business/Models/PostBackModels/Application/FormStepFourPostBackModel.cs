using umbraco;
using USP.Business.Constants;
using USP.Business.UmbracoValidationAttributes;

namespace USP.Business.Models.PostBackModels.Application
{
    public class FormStepFourPostBackModel
    {
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public string FirstLineOfAddress { get; set; }
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public string SecondLineOfAddress { get; set;}
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public string PostCode { get; set; }
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public string TownOrCity { get; set; }
        public string MobileNumber { get; set; }
        public string HomeNumber { get; set; }
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        [UmbracoEmail(ErrorMessageDictionaryKey = SiteConstants.UmbracoDictionaryKey.ApplicationForm.InvalidEmail )]
        public string EmailAddress { get; set; }
        public bool IsAdultLearning { get; set; }


        [UmbracoRequiredIf(SiteConstants.ApplicationFormFieldRequiredMessageKey, "IsAdultLearning" , true)]
        public string EmergencyContactFirstName { get; set; }
        [UmbracoRequiredIf(SiteConstants.ApplicationFormFieldRequiredMessageKey, "IsAdultLearning", true)]
        public string EmergencyContactLastName { get; set; }
        [UmbracoRequiredIf(SiteConstants.ApplicationFormFieldRequiredMessageKey, "IsAdultLearning", true)]
        public string EmergencyContactRelationship { get; set; }
        [UmbracoRequiredIf(SiteConstants.ApplicationFormFieldRequiredMessageKey, "IsAdultLearning", true)]
        public string EmergencyContactPhoneNumber { get; set; }
        [UmbracoRequiredIf(SiteConstants.ApplicationFormFieldRequiredMessageKey, "IsAdultLearning", true)]
        [UmbracoEmail(ErrorMessageDictionaryKey = SiteConstants.UmbracoDictionaryKey.ApplicationForm.InvalidEmail)]
        public string EmergencyContactEmailAddress { get; set; }
    }
}
