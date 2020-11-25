using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetStreetViewEmbedSurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetStreetViewEmbed()
        {
            var model = new WidgetStreetViewEmbed(Model);
            return CurrentPartialView(model);
        }
    }
}
