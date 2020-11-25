using System.Web.Mvc;
using Umbraco.Web.Models;

namespace USP.Business.Controllers.ApplicationForm
{
    public class FormStepThirteenController : RenderMvcSurfaceController
    {
        public override ActionResult Index(RenderModel model)
        {
            return CurrentTemplate(model);
        }

        [HttpPost]
        public ActionResult HandleFormStepThirteenSubmit()
        {
            return null;
        }
    }
}
