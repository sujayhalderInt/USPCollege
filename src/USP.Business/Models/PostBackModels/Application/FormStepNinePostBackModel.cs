using System.Collections.Generic;
using USP.Business.Constants;
using USP.Business.Models.ContentModels;
using USP.Business.UmbracoValidationAttributes;

namespace USP.Business.Models.PostBackModels.Application
{
    public class FormStepNinePostBackModel
    {
        public bool IsAdultLearning { get; set; }
        [UmbracoRequiredIf(SiteConstants.ApplicationFormFieldRequiredMessageKey, "IsAdultLearning", false)]
        public string LastCollegeOrSchool { get; set; }
        public bool IsAlevelsOrProfessionlQualifications { get; set; }
        public List<FormStepNineQualifications> Qualifications { get; set; }
    }
}
