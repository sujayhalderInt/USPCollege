using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetAlertSurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetAlert()
        {
            var model = new WidgetAlert(Model);
            return CurrentPartialView(model);
        }
    }
}
