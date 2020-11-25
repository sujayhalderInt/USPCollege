using System.Collections.Generic;

namespace USP.Business.Models.ContentModels
{
    public partial class WidgetRelatedBlogs
    {
        public IEnumerable<PageBlogDetail> RelatedBlogs { get; set; }
    }
}
