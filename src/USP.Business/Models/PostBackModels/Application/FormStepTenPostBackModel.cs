using System;
using USP.Business.Constants;
using USP.Business.UmbracoValidationAttributes;

namespace USP.Business.Models.PostBackModels.Application
{
    public class FormStepTenPostBackModel
    {
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public bool? HasDisabilityDifficulty { get; set; }
        [UmbracoRequiredIf(SiteConstants.ApplicationFormFieldRequiredMessageKey, "HasDisabilityDifficulty", true)]
        public string DisabilitiesDifficultiesCsv { get; set; }
        //[UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public string PrimaryDisability { get; set; }//Conditional Required
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public string Ethnicity { get; set; }
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public string Nationality { get; set; }
        public string Religion { get; set; }
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public string FirstLanguage { get; set; }
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public bool? ResidentOfUkEuForThreeYears { get; set; }
        [UmbracoRequiredIf(SiteConstants.ApplicationFormFieldRequiredMessageKey, "ResidentOfUkEuForThreeYears", false)]
        public string NameOfCountryOutsideUkEu { get; set; }
        [UmbracoRequiredIf(SiteConstants.ApplicationFormFieldRequiredMessageKey, "ResidentOfUkEuForThreeYears", false)]
        public DateTime? DateOfResidenceOutsideUkEuFrom { get; set; }
        [UmbracoRequiredIf(SiteConstants.ApplicationFormFieldRequiredMessageKey, "ResidentOfUkEuForThreeYears", false)]
        public DateTime? DateOfResidenceOutsideUkEuTo { get; set; }
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public bool? AnyCriminalConvictionOrFinalWarning { get; set; }
    }
}
