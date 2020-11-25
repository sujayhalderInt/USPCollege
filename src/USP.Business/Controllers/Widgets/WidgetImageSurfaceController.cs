using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetImageSurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetImage()
        {
            var model = new WidgetImage(Model);
            return CurrentPartialView(model);
        }
    }
}
