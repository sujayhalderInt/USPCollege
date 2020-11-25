using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using USP.Business.Helpers;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetSinglePinMapSurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetSinglePinMap()
        {
            var model = new WidgetSinglePinMap(Model);
            model.ApiKey = GetApiKey();
            return CurrentPartialView(model);
        }

        private string GetApiKey()
        {
            return SettingHelper.GetXmlSetting(SettingType.Settings, "GoogleMap:ApiKey");
        }
    }
}
