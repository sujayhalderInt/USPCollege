using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetAccordionSurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetAccordion()
        {
            var model = new WidgetAccordion(Model);
            return CurrentPartialView(model);
        }
    }
}
