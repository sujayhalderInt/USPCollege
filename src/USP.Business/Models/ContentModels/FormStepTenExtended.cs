using System.Collections.Generic;
using USP.Business.Models.Dapper;
using USP.Business.Models.PostBackModels.Application;

namespace USP.Business.Models.ContentModels
{
    public partial class FormStepTen
    {
        public FormStepTenPostBackModel PostBackModel { get; set; }
        public ApplicationForm ApplicationForm { get; set; }
        public IEnumerable<string> SortedDisabilitiesList { get; set; }
    }
}
