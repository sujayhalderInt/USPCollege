using System.Linq;
using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetImageGalleryMasonrySurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetImageGalleryMasonry()
        {
            var model = new WidgetImageGalleryMasonry(Model);
            if (model.Images == null || !model.Images.Any())
            {
                model.ImagesWithCaptionsAndAltTexts = null;
                return CurrentPartialView(model);
            }

            model.ImagesWithCaptionsAndAltTexts = model.Images?.OfType<DataMasonryImage>();
            foreach (var item in model.ImagesWithCaptionsAndAltTexts)
            {
                var img = new Image(item.Image);
                item.AltText = !string.IsNullOrEmpty(img.AltText) ? img.AltText : img.Name;
            }
            return CurrentPartialView(model);
        }
    }
}
