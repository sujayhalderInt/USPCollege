using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USP.Business.Models.ContentModels;
using USP.Business.Models.Dapper;
using USP.Business.Models.Form;

namespace USP.Business.Models.ViewModels
{
    public class FormApplicationItem
    {
        public Step11HeadingsAndLabels HeadingsAndLabels { get; set; }
        public ApplicationForm ApplicationForm { get; set; }
        public List<FormStepNineQualifications> Qualifications { get; set; }
    }
}
