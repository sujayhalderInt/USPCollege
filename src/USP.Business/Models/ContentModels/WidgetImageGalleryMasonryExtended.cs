using System.Collections.Generic;

namespace USP.Business.Models.ContentModels
{
    public partial class WidgetImageGalleryMasonry
    {
        public IEnumerable<DataMasonryImage> ImagesWithCaptionsAndAltTexts { get; set; }
    }
}
