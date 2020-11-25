using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetRTESurfaceController: DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetRTE()
        {
            var model = new WidgetRte(Model);
            return CurrentPartialView(model);
        }
    }
}
