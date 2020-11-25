using System.Collections.Generic;

namespace USP.Business.Models.ContentModels
{
    public partial class WidgetRelatedCourses
    {
        public IEnumerable<DataRelatedCourses> RelatedCourse { get; set; }
    }
}
