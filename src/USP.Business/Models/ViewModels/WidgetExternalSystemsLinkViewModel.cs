using System.Collections.Generic;
using USP.Business.Models.ContentModels;

namespace USP.Business.Models.ViewModels
{
    public class WidgetExternalSystemsLinkViewModel
    {
        public IEnumerable<DataExternalSystem> ExternalSystems { get; set; }
    }
}
