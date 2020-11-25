using USP.Business.Constants;
using USP.Business.UmbracoValidationAttributes;

namespace USP.Business.Models.ContentModels
{
    public partial class PageRegistration
    {
        [UmbracoRequired(SiteConstants.RegistrationValidationEmailRequiredKey)]
        [UmbracoEmail]
        public virtual string EmailAddress { get; set; }
        [UmbracoRequired(SiteConstants.RegistrationValidationPasswordRequiredKey)]
        [UmbracoRegularExpression(SiteConstants.RegistrationValidationPasswordInvalidKey, SiteConstants.RegistrationValidationPasswordRegexKey)]
        public virtual string Password { get; set; }
        [UmbracoRequired(SiteConstants.RegistrationValidationRepeatPasswordRequiredKey)]
        [UmbracoCompare(SiteConstants.RegistrationValidationPasswordsNoMatchKey, "Password")]
        public virtual string RepeatPassword { get; set; }
        [UmbracoMustBeTrue(ErrorMessageDictionaryKey = SiteConstants.RegistrationValidationAgreeToPrivacyStatementKey)]
        public virtual bool AgreeToPrivacyStatement { get; set; }

        public string LoginPageUrlWithReturnUrl { get; set; }
    }
}
