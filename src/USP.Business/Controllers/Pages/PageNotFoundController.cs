using System.Web.Mvc;
using Umbraco.Web.Models;

namespace USP.Business.Controllers.Pages
{
    public class PageNotFoundController : RenderMvcSurfaceController
    {
        public override ActionResult Index(RenderModel model)
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 404;
            Response.StatusDescription = "404 Page Not Found";
            return CurrentTemplate(model);
        }
    }
}
