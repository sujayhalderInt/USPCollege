using USP.Business.Constants;
using USP.Business.UmbracoValidationAttributes;

namespace USP.Business.Models.PostBackModels.Application
{
    public class FormStepTwoPostBackModel
    {
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public string TypeOfStudy { get; set; }
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public string Campus { get; set; }
    }
}
