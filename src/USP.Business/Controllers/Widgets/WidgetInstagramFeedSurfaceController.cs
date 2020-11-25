using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using USP.Business.Models.ContentModels;
using USP.Business.Services.Interfaces;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetInstagramFeedSurfaceController : DocTypeGridEditorSurfaceController
    {
        private readonly IInstagramFeedService _instagramFeedService;

        public WidgetInstagramFeedSurfaceController(IInstagramFeedService instagramFeedService)
        {
            _instagramFeedService = instagramFeedService;
        }

        public ActionResult WidgetInstagramFeed()
        {
            var model = new WidgetInstagramFeed(Model);
            var feed = _instagramFeedService.GetLatest(model.AccessToken, model.NumberOfPosts);
            return CurrentPartialView(feed);
        }
    }
}
