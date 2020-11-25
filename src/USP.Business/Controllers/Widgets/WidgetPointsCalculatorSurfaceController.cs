using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetPointsCalculatorSurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetPointsCalculator()
        {
            var model = new WidgetPointsCalculator(Model);
            return CurrentPartialView(model);
        }
    }
}
