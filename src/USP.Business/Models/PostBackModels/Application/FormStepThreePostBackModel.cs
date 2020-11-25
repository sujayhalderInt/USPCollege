using System;
using USP.Business.Constants;
using USP.Business.UmbracoValidationAttributes;

namespace USP.Business.Models.PostBackModels.Application
{
    public class FormStepThreePostBackModel
    {
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public string LastName { get; set; }
        public string AnotherName { get; set; }
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public DateTime? DateOfBirth { get; set; }
        [UmbracoRequired(SiteConstants.ApplicationFormFieldRequiredMessageKey)]
        public string Gender { get; set; }

        public bool IsNIRequired { get; set; }

        [UmbracoRequiredIf(SiteConstants.ApplicationFormFieldRequiredMessageKey, "IsNIRequired", true)]
        public string NationalInsuranceNumber { get; set; }
    }
}
