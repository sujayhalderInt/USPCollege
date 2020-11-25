using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace USP.Business.Models.ContentModels
{
    public partial class WidgetUpcomingEvents
    {
        public IEnumerable<PageEventDetail> EventList { get; set; }
        public MainEventListing MainEventListing { get; set; }
    }

    public class MainEventListing
    {
        public IPublishedContent Page { get; set; }
        public string CallToAction { get; set; }
    }
}
