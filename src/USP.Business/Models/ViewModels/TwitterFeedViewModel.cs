using System.Collections.Generic;
using USP.Business.Models.ContentModels;
using USP.Business.Models.Data;

namespace USP.Business.Models.ViewModels
{
    public class TwitterFeedViewModel
    {
        public SettingsTwitter Settings { get; set; }
        public WidgetTwitterFeed Parameters { get; set; }
        public IEnumerable<Tweet> Tweets { get; set; }
    }
}
