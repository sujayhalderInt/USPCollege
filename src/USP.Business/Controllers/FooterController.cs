using System.Web.Mvc;
using USP.Business.Models.ContentModels;
using USP.Business.Mvc;

namespace USP.Business.Controllers
{
    public class FooterController : RenderMvcSurfaceController
    {
        [UmbracoDonutOutputCache(CacheProfile = "Global")]
        public ActionResult RenderFooter()
        {
            // TODO: Setting Service / Helper
            var setting = Umbraco.ContentQuery.TypedContentSingleAtXPath("//settingsSite");
            var model = new SettingsSite(setting);
            return PartialView("Footer", model);
        }
    }
}
