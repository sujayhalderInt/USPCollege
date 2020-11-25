using System.Collections.Generic;

namespace USP.Business.Models.ContentModels
{
    public partial class WidgetContactInformation
    {
        public IEnumerable<DataContactInformation> ContactList { get; set; }
    }
}