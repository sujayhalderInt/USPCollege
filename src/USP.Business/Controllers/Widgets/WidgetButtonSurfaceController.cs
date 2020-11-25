using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetButtonSurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetButton()
        {
            var model = new WidgetButton(Model);
            return CurrentPartialView(model);
        }
    }
}
