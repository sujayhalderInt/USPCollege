using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetSectionStartSurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetSectionStart()
        {
            var model = new WidgetSectionStart(Model);
            return CurrentPartialView(model);
        }
    }
}
