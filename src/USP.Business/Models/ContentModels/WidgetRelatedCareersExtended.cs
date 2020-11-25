using System.Collections.Generic;

namespace USP.Business.Models.ContentModels
{
    public partial class WidgetRelatedCareers
    {
        public IEnumerable<PageCareerDetail> RelatedCareers { get; set; }
    }
}
