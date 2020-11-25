using USP.Business.Models.Dapper;
using USP.Business.Models.PostBackModels.Application;

namespace USP.Business.Models.ContentModels
{
    public partial class FormStepThree
    {
        public FormStepThreePostBackModel PostBackModel { get; set; }

        public ApplicationForm ApplicationForm { get; set; }
    }
}
