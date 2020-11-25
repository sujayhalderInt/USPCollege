using System.Web.Mvc;
using Umbraco.Web.Models;

namespace USP.Business.Controllers.Pages
{
    public class PageNewsDetailController : RenderMvcSurfaceController
    {
        public override ActionResult Index(RenderModel model)
        {
            return CurrentTemplate(model);
        }
    }
}
