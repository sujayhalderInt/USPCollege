using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using USP.Business.Constants;
using USP.Business.Helpers;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers
{
    public class ScriptController : RenderMvcSurfaceController
    {
        public ActionResult RenderAnalyticsScript()
        {
            if (!CookieHelper.GetCookieBool(SiteConstants.CookieName, "other", false))
            {
                return Content("");
            }

            var setting = Umbraco.ContentQuery.TypedContentSingleAtXPath("//settingsSite");
            var model = new SettingsSite(setting);

            return Content(model.AnalyticsScript);
        }
    }
}
