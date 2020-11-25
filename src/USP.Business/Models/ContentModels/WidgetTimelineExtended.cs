using System.Collections.Generic;

namespace USP.Business.Models.ContentModels
{
    public partial class WidgetTimeline
    {
        public IEnumerable<DataTimelineItem> TimelineList { get; set; }
    }
}
