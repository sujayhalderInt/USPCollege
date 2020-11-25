using System.Linq;
using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using Umbraco.Web;
using USP.Business.Models.ContentModels;
using USP.Business.Models.ViewModels;
using USP.Business.Services.Interfaces;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetTwitterFeedSurfaceController : DocTypeGridEditorSurfaceController
    {
        private readonly ITwitterManagerService _twitterManagerService;

        public WidgetTwitterFeedSurfaceController(ITwitterManagerService twitterManagerService)
        {
            _twitterManagerService = twitterManagerService;
        }

        public ActionResult WidgetTwitterFeed()
        {
            var model = new TwitterFeedViewModel();
            var parameters = new WidgetTwitterFeed(Model);
         
            if (parameters.TwitterSettingsPicker != null)
            {
                var twitterSettings = parameters.TwitterSettingsPicker.FirstOrDefault().OfType<SettingsTwitter>();
                model.Parameters = parameters;
                model.Tweets = _twitterManagerService.GetTweets(twitterSettings).ToList();
                model.Settings = twitterSettings;
            }

            return CurrentPartialView(model);
        }

    }
}
