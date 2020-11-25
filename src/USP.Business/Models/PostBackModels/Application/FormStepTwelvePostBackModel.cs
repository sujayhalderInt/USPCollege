using USP.Business.Constants;
using USP.Business.UmbracoValidationAttributes;

namespace USP.Business.Models.PostBackModels.Application
{
    public class FormStepTwelvePostBackModel
    {
        public bool ReceiveMarketingEmails { get; set; }

        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public string HowDidYouHearAboutThisCourse { get; set; }

        public string MostInterestedSector { get; set; }

    }
}
