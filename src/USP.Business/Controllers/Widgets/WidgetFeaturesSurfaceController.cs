using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using Umbraco.Core.Models;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetFeaturesSurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetFeatures()
        {
            var model = new WidgetFeatures(Model);
            model.FeaturesData = GetFeatures(model.Features);
            return CurrentPartialView(model);
        }

        public IEnumerable<DataFeature> GetFeatures(IEnumerable<IPublishedContent> featuresItem)
        {
            if (featuresItem == null) return null;
            var featuresList = new List<DataFeature>();
            foreach (var item in featuresItem)
            {
                DataFeature feature;
                if (item.ContentType.Alias == DataFeatureTreePicker.ModelTypeAlias)
                {
                    var picker = new DataFeatureTreePicker(item);
                    var pickedItem = picker.ExistingFeature.FirstOrDefault();
                    feature = new DataFeature(pickedItem);
                }
                else
                {
                    feature = new DataFeature(item);
                }

                featuresList.Add(feature);
            }
            return featuresList;
        }
    }
}
