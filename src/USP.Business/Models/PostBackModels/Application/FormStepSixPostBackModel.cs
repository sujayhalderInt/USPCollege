using USP.Business.Constants;
using USP.Business.UmbracoValidationAttributes;

namespace USP.Business.Models.PostBackModels.Application
{
    public class FormStepSixPostBackModel
    {
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public string AlevelCourseFirstChoice { get; set; }
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public string AlevelCourseSecondChoice { get; set; }
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public string AlevelCourseThirdChoice { get; set; }
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public bool? IsFirstFullLevel3Qualification { get; set; }
    }
}
