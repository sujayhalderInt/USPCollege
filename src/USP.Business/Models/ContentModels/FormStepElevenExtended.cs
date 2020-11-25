using System.Collections.Generic;
using USP.Business.Models.Dapper;
using USP.Business.Models.Form;

namespace USP.Business.Models.ContentModels
{
    public partial class FormStepEleven
    {
        public Step11HeadingsAndLabels HeadingsAndLabels { get; set; }
        public ApplicationForm ApplicationForm { get; set; }
        public List<FormStepNineQualifications> Qualifications { get;set; }

        public string FormStep3Url { get; set; }
        public string FormStep4Url { get; set; }
        public string FormStep5Url { get; set; }
        public string FormStep6Url { get; set; }
        public string FormStep7Url { get; set; }
        public string FormStep8Url { get; set; }
        public string FormStep9Url { get; set; }
        public string FormStep10Url { get; set; }
    }
}
