using System.Collections.Generic;
using System.Web.Mvc;
using USP.Business.Models.Dapper;
using USP.Business.Models.PostBackModels.Application;

namespace USP.Business.Models.ContentModels
{
    public partial class FormStepSeven
    {
        public FormStepSevenPostBackModel PostBackModel { get; set; }
        public ApplicationForm ApplicationForm { get; set; }
        public List<SelectListItem> CourseListDropDown { get; set; }
        public List<SelectListItem> AdultLearningCourseListDropDownFirst { get; set; }
        public List<SelectListItem> AdultLearningCourseListDropDownSecond { get; set; }
        public List<SelectListItem> AdultLearningCourseListDropDownThird { get; set; }
    }
}
