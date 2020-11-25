using System.Collections.Generic;
using System.Web.Mvc;
using USP.Business.Models.Dapper;
using USP.Business.Models.PostBackModels.Application;

namespace USP.Business.Models.ContentModels
{
    public partial class FormStepSix
    {
        public FormStepSixPostBackModel PostBackModel { get; set; }
        public ApplicationForm ApplicationForm { get; set; }
        public List<SelectListItem> AlevelCoursesDropDownListFirstChoice { get; set; }
        public List<SelectListItem> AlevelCoursesDropDownListSecondChoice { get; set; }
        public List<SelectListItem> AlevelCoursesDropDownListThirdChoice { get; set; }
    }
}
