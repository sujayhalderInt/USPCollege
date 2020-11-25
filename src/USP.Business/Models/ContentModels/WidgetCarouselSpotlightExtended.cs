using System.Collections.Generic;

namespace USP.Business.Models.ContentModels
{
	public partial class WidgetCarouselSpotlight
	{
        public IEnumerable<DataSpotlightItem> LeftSpotlights{ get; set; }
        public IEnumerable<DataSpotlightItem> RightSpotlights { get; set; }

	}
}
