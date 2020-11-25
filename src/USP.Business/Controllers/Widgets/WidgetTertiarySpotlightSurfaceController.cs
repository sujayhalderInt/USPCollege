using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using Umbraco.Core.Models;
using USP.Business.Constants;
using USP.Business.Models.ContentModels;
using USP.Business.Models.ViewModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetTertiarySpotlightSurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetTertiarySpotlight()
        {
            var model = new WidgetTertiarySpotlight(Model);

            model.Items = GetSpotlights(model.SpotlightItems);
            
            return CurrentPartialView(model);
        }

        public IEnumerable<DataSpotlightItem> GetSpotlights(IEnumerable<IPublishedContent> spotlightItems)
        {
            var results = new List<DataSpotlightItem>();

            if (spotlightItems != null)
            {
                foreach (var publishedContent in spotlightItems)
                {
                    DataSpotlightItem data;

                    if (publishedContent?.ContentType.Alias == SiteConstants.AliasDataSpotlightTreePicker)
                    {
                        var picker = new DataSpotlightTreePicker(publishedContent);
                        var pickedItem = picker.SpotlightItem.FirstOrDefault();
                        data = new DataSpotlightItem(pickedItem);
                    }
                    else
                    {
                        data = new DataSpotlightItem(publishedContent);
                    }

                    results.Add(data);
                }
            }
            return results;
        }
    }
 }

