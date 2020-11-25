using System.Linq;
using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using USP.Business.Models.ContentModels;
using USP.Business.Models.ViewModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetExternalSystemsLinkSurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetExternalSystemsLink()
        {
            var model = new WidgetExternalSystemsLinkViewModel();
            var widgetExternalSystemsLinkModel = new WidgetExternalSystemsLink(Model);

            model.ExternalSystems = widgetExternalSystemsLinkModel.ExternalSystems.OfType<DataExternalSystem>();
            return CurrentPartialView(model);
        }
    }
}
