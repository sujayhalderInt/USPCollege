using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using USP.Business.Constants;
using USP.Business.Helpers;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetVideoSurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetVideo()
        {
            if (!CookieHelper.GetCookieBool(SiteConstants.CookieName, "other", false))
            {
                return Content("");
            }

            var model = new WidgetVideo(Model);
            model.ApiKey = GetApiKey();
            return CurrentPartialView(model);
        }

        private string GetApiKey()
        {
            return SettingHelper.GetXmlSetting(SettingType.Settings, "YouTube:ApiKey");
        }
    }
}
