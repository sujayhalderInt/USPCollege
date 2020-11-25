using System.Collections.Generic;

namespace USP.Business.Models.ContentModels
{
    /// <summary>[Widget] Spotlight</summary>
    public partial class WidgetTertiarySpotlight
    {
        public IEnumerable<DataSpotlightItem> Items { get; set; }
    }
}
