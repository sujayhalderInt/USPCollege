using USP.Business.Constants;
using USP.Business.UmbracoValidationAttributes;

namespace USP.Business.Models.PostBackModels.Application
{
    public class FormStepSevenPostBackModel
    {
        public bool IsAdultLearning { get; set; }
        [UmbracoRequiredIf(SiteConstants.ApplicationFormFieldRequiredMessageKey, "IsAdultLearning", false)]
        public string NameOfCourse { get; set; }
       [UmbracoRequiredIf(SiteConstants.ApplicationFormFieldRequiredMessageKey, "IsAdultLearning", true)]
        public string AdultLearningCourseOne { get; set; }
        public string AdultLearningCourseTwo { get; set; }
        public string AdultLearningCourseThree { get; set; }
    }
}
