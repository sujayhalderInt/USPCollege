using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using Umbraco.Core.Models;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetStatisticsSurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetStatistics()
        {
            var model = new WidgetStatistics(Model);

            model.StatisticsData = GetStatistics(model.Statistics);

            return CurrentPartialView(model);
        }

        public IEnumerable<DataStatistic> GetStatistics(IEnumerable<IPublishedContent> statisticItems)
        {
            if (statisticItems == null) return null;

            var results = new List<DataStatistic>();

            foreach (var publishedContent in statisticItems)
            {
                DataStatistic stat;

                if (publishedContent.ContentType.Alias == DataStatisticTreePicker.ModelTypeAlias)
                {
                    var picker = new DataStatisticTreePicker(publishedContent);
                    var pickedItem = picker.Statistic.FirstOrDefault();
                    stat = new DataStatistic(pickedItem);
                }
                else
                {
                    stat = new DataStatistic(publishedContent);
                }

                results.Add(stat);
            }
            return results;
        }
    }
}
