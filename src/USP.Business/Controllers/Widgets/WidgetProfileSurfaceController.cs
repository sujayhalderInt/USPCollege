using System.Linq;
using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using USP.Business.Constants;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetProfileSurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetProfile()
        {
            var model = new WidgetProfile(Model);
            DataProfileItem data;

            var item = model.ProfileItem.First();
            if (item?.ContentType.Alias == SiteConstants.AliasDataProfileTreePicker)
            {
                var picker = new DataProfileTreePicker(item);
                var pickedItem = picker.ProfileTreePicker.FirstOrDefault();
                data = new DataProfileItem(pickedItem);
            }
            else
            {
                data = new DataProfileItem(item);
            }

            model.Item = data;
            return CurrentPartialView(model);
        }
    }
}
