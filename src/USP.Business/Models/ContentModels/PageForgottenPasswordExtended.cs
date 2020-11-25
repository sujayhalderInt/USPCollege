using USP.Business.Constants;
using USP.Business.UmbracoValidationAttributes;

namespace USP.Business.Models.ContentModels
{
    public partial class PageForgottenPassword
    {
        [UmbracoRequired(SiteConstants.ForgottenPasswordEmailRequiredKey)]
        [UmbracoEmail(ErrorMessageDictionaryKey = SiteConstants.ForgottenPasswordEmailInvalidKey)]
        public string EmailAddress { get; set; }

        public string LoginPageUrlWithReturnUrl { get; set; }
    }
}
