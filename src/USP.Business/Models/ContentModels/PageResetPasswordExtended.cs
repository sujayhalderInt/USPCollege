using USP.Business.Constants;
using USP.Business.UmbracoValidationAttributes;

namespace USP.Business.Models.ContentModels
{
    public partial class PageResetPassword
    {
        [UmbracoRequired(SiteConstants.ResetPasswordEmailRequiredKey)]
        public string EmailAddress { get; set; }

        //[UmbracoRequired(SiteConstants.ResetPasswordPasswordRequiredKey)]
        [UmbracoRequired(SiteConstants.RegistrationValidationPasswordRequiredKey)]
        [UmbracoRegularExpression(SiteConstants.RegistrationValidationPasswordInvalidKey, SiteConstants.RegistrationValidationPasswordRegexKey)]
        public string Password { get; set; }
        [UmbracoRequired(SiteConstants.ResetPasswordConfirmPasswordRequiredKey)]
        [UmbracoCompare(SiteConstants.ResetPasswordPasswordsNoMatchKey, "Password")]
        public string ConfirmPassword { get; set; }

        public string LoginPageUrlWithReturnUrl { get; set; }
    }
}
