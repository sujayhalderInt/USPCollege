using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Web;
using USP.Business.Models.ContentModels;
using USP.Business.Models.ViewModels;

namespace USP.Business.Extensions
{
    public static class UmbracoHelperExtensions
    {
        public static string GetSearchUrl(this UmbracoHelper helper)
        {
            var setting = helper.TypedContentSingleAtXPath("//settingsSite");

            if (setting == null )
            {
                return string.Empty;
            }

            var settings = new SettingsSite(setting);
            if (settings.SearchPage.IsNullOrEmpty())
            {
                return string.Empty;
            }

            return settings.SearchPage.FirstOrDefault()?.Url;
        }
    }
}
