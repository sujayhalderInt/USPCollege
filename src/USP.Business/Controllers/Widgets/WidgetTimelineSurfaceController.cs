using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using Umbraco.Core.Models;
using USP.Business.Constants;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetTimelineSurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetTimeline()
        {
            var model = new WidgetTimeline(Model);
            model.TimelineList = GetTimelines(model.Items);
            return CurrentPartialView(model);
        }

        public IEnumerable<DataTimelineItem> GetTimelines(IEnumerable<IPublishedContent> contacts)
        {
            var results = new List<DataTimelineItem>();

            if (contacts != null)
            {
                foreach (var publishedContent in contacts)
                {
                    DataTimelineItem data;

                    if (publishedContent?.ContentType.Alias == SiteConstants.AliasDataExistingTimelinePicker)
                    {
                        var picker = new DataExistingTimelineItem(publishedContent);
                        var pickedItem = picker.TimelineItem.FirstOrDefault();
                        data = new DataTimelineItem(pickedItem);
                    }
                    else
                    {
                        data = new DataTimelineItem(publishedContent);
                    }

                    results.Add(data);
                }
            }
            return results;
        }
    }
}

