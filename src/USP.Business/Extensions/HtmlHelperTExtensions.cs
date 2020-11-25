using System;
using System.Dynamic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core.Models;
using Umbraco.Web;
using USP.Business.Constants;
using USP.Business.Models.ContentModels;

namespace USP.Business.Extensions
{
    public static class HtmlHelperTExtensions
    {
        private static string _lazyClass = "lazy";

        public static HtmlString GoogleCTAEvent<T>(this HtmlHelper<T> helper, string eventName, string eventCategory, string eventAction, string eventLabel, string eventValue)
        {
            if (string.IsNullOrWhiteSpace(eventName) || string.IsNullOrWhiteSpace(eventCategory) || string.IsNullOrWhiteSpace(eventAction) || string.IsNullOrWhiteSpace(eventLabel))
            {
                return new HtmlString("");
            }

            return new HtmlString($"onclick=\"kennedys.google.fireCTAEvent('{eventName}','{eventCategory}','{eventAction}','{eventLabel}','{(!string.IsNullOrWhiteSpace(eventValue) ? eventValue : "-")}')\"");
        }

        public static HtmlString GoogleCTAEvent<T>(this HtmlHelper<T> helper, string eventName, string eventCategory, string eventAction, IBaseGoogleTrackableItem item)
        {
            if (item == null || string.IsNullOrWhiteSpace(eventName) || string.IsNullOrWhiteSpace(eventCategory) || string.IsNullOrWhiteSpace(eventAction) || string.IsNullOrWhiteSpace(item.TrackingName))
            {
                return new HtmlString("");
            }

            return new HtmlString($"onclick=\"kennedys.google.fireCTAEvent('{eventName}','{eventCategory}','{eventAction}','{item.TrackingName}','')\"");
        }

        public static HtmlString LazyImage<T>(this HtmlHelper<T> helper, IPublishedContent mediaContent, string crop = null, object htmlAttributes = null, string aspect = SiteConstants.Crops.AspectRatio.Square)
        {
            var media = new Image(mediaContent);
            var initialData = new RouteValueDictionary(htmlAttributes);
            var d = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (!string.IsNullOrEmpty(media.AltText))
            {
                d["alt"] = initialData["alt"] = media.AltText;
            }

            if (!d.ContainsKey("alt"))
            {
                d["alt"] = initialData["alt"] = media.Name;
            }

            var desktopUrl = media.Url;
            if (!string.IsNullOrWhiteSpace(crop))
            {
                desktopUrl = media.GetCropUrl(crop+"-Desktop");
            }

            var mobileUrl = media.Url;
            if (!string.IsNullOrWhiteSpace(crop))
            {
                mobileUrl = media.GetCropUrl(crop + "-Mobile");
            }

            var loaderUrl = media.GetCropUrl("Loader-" + aspect);


            if (d.ContainsKey("class"))
            {
                d["class"] += " " + _lazyClass;
            }
            else
            {
                d.Add("class", _lazyClass);
            }

            var sb = new StringBuilder("<picture>");
            sb.AppendFormat("<source media=\"(min-width: 768px)\" srcset=\"" + loaderUrl + "\" data-srcset=\"{0}\" />", desktopUrl);
            sb.AppendFormat("<source media=\"(max-width: 768.01px)\" srcset=\"" + loaderUrl + "\"  data-srcset=\"{0}\" />", mobileUrl);
            sb.AppendFormat("<img src=\"{0}\" data-src=\"{0}\"", loaderUrl);
            foreach (var attribute in d)
            {
                sb.Append(" " + attribute.Key + "=\"" + attribute.Value + "\"");
            }
            sb.Append(" />");
            sb.Append("</picture>");

            d = initialData;
            sb.Append("<noscript><img src=\"" + desktopUrl + "\"");
            foreach (var attribute in d)
            {
                sb.Append(" " + attribute.Key + "=\"" + attribute.Value + "\"");
            }
            sb.Append(" /></noscript>");

            return new HtmlString(sb.ToString());
        }

        public static HtmlString LazyImage<T>(this HtmlHelper<T> helper, string url, object htmlAttributes, string loader = null)
        {
            var d = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            return LazyImage(helper, url, d, loader);
        }

        private static HtmlString LazyImage<T>(this HtmlHelper<T> helper, string url, RouteValueDictionary htmlAttributes, string loader = null)
        {
            var initialData = new RouteValueDictionary(htmlAttributes);
            if (htmlAttributes.ContainsKey("class"))
            {
                htmlAttributes["class"] += " " + _lazyClass;
            }
            else
            {
                htmlAttributes.Add("class", _lazyClass);
            }

            var sb = new StringBuilder();
            sb.Append("<img src=\"" + loader + "\" data-src=\"" + url + "\"");
            foreach (var attribute in htmlAttributes)
            {
                sb.Append(" " + attribute.Key + "=\"" + attribute.Value + "\"");
            }
            sb.Append(" />");

            htmlAttributes = initialData;
            sb.Append("<noscript><img src=\"" + url + "\"");
            foreach (var attribute in htmlAttributes)
            {
                sb.Append(" " + attribute.Key + "=\"" + attribute.Value + "\"");
            }
            sb.Append(" /></noscript>");

            return new HtmlString(sb.ToString());
        }

        public static HtmlString NewWindow<T>(this HtmlHelper<T> helper, bool openInNewWindow)
        {
            return new HtmlString(openInNewWindow ? "target=\"blank\" rel=\"noopener noreferrer\"" : "");
        }

        public static string ImagePosition<T>(this HtmlHelper<T> helper, string position)
        {
            if (string.IsNullOrEmpty(position) || position == "Left")
            {
                return "left";
            }
            return "right";
        }

        public static string EnsureSchema<T>(this HtmlHelper<T> helper, string url)
        {
            if (!url.StartsWith("https://") && !url.StartsWith("http://"))
            {
                return "https://" + url;
            }
            return url;
        }
    }
}
