using USP.Business.Constants;
using USP.Business.UmbracoValidationAttributes;

namespace USP.Business.Models.ContentModels
{
    public partial class PageApplicationFormLogin
    {
        [UmbracoRequired(SiteConstants.LoginValidationEmailRequiredKey)]
        [UmbracoEmail(ErrorMessageDictionaryKey = SiteConstants.LoginValidationInvalidEmailKey)]
        public virtual string EmailAddress { get; set; }

        [UmbracoRequired(SiteConstants.LoginValidationPasswordRequiredKey)]
        public virtual string Password { get; set; }
    }
}
