
using System.Linq;
using System.Web.Mvc;
using Umbraco.Web.Models;
using USP.Business.Constants;
using USP.Business.Helpers;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Widgets
{
    public class PageManageCookiesController : RenderMvcSurfaceController
    {
        public override ActionResult Index(RenderModel model)
        {
            HttpContext.Items["DisableCookieBanner"] = true;

            var viewmodel = new PageManageCookies(model.Content);

            viewmodel.CookieExist = Request.Cookies.AllKeys.Contains(SiteConstants.CookieName);
            viewmodel.OtherCookie = CookieHelper.GetCookieBool(SiteConstants.CookieName, "other", true);

            return CurrentTemplate(viewmodel);
        }
    }
}