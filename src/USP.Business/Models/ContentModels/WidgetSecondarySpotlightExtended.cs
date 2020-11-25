using System.Collections.Generic;

namespace USP.Business.Models.ContentModels
{
    /// <summary>[Widget] Spotlight</summary>
    public partial class WidgetSecondarySpotlight
    {
        public IEnumerable<DataSpotlightItem> Items { get; set; }
    }
}
