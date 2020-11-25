using System.Collections.Generic;

namespace USP.Business.Models.ContentModels
{
    public partial class WidgetRelatedNews
    {
        public IEnumerable<PageNewsDetail> RelatedNews { get; set; }
    }
}
