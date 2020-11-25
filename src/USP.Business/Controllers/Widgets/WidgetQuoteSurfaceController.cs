using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetQuoteSurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetQuote()
        {
            var model = new WidgetQuote(Model);
            return CurrentPartialView(model);
        }
    }
}
