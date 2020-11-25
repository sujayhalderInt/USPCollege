using System.Linq;
using System.Web.Mvc;
using Our.Umbraco.DocTypeGridEditor.Web.Controllers;
using USP.Business.Constants;
using USP.Business.Models.ContentModels;
using USP.Business.Models.ViewModels;

namespace USP.Business.Controllers.Widgets
{
    public class WidgetPrimarySpotlightSurfaceController : DocTypeGridEditorSurfaceController
    {
        public ActionResult WidgetPrimarySpotlight()
        {
            var model = new WidgetPrimarySpotlight(Model);
            DataSpotlightItem data;

            var item = model.SpotlightItem.FirstOrDefault();
            if (item?.ContentType.Alias == SiteConstants.AliasDataSpotlightTreePicker)
            {
                var picker = new DataSpotlightTreePicker(item);
                var pickedItem = picker.SpotlightItem.FirstOrDefault();
                data = new DataSpotlightItem(pickedItem);
            }
            else
            {
                data = new DataSpotlightItem(item);
            }

            model.Item = data;
            model.MainEventListing = GetMainEventListingPage();
            return CurrentPartialView(model);
        }

        public MainEventListing GetMainEventListingPage()
        {
            var settings = Umbraco.ContentQuery.TypedContentSingleAtXPath($"//{SettingsSite.ModelTypeAlias}");
            if (settings == null) return null;

            var settingsPage = new SettingsSite(settings);
            var mainEventListing = new MainEventListing();
            mainEventListing.Page = settingsPage.MainEventListingPage?.FirstOrDefault();
            mainEventListing.CallToAction = !string.IsNullOrEmpty(settingsPage.MainEventListingPageCtaText) ? settingsPage.MainEventListingPageCtaText : SiteConstants.MainEventCtaText;
            return mainEventListing;
        }
    }
}
