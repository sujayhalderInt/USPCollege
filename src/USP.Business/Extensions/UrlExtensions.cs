using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace USP.Business.Extensions
{
    public static class UrlExtensions
    {
        public static string AbsoluteUrl(this UrlHelper helper, string relativeUrl)
        {
            if (string.IsNullOrWhiteSpace(relativeUrl)) return null;

            var url = new StringBuilder();
            url.Append(HttpContext.Current.Request.Url.Scheme);
            url.Append("://");
            url.Append(HttpContext.Current.Request.Url.Host);
            url.Append(relativeUrl);

            return url.ToString();
        }

        public static Uri AddQuery(this Uri uri, string name, string value)
        {
            var ub = new UriBuilder(uri);

            // decodes urlencoded pairs from uri.Query to HttpValueCollection
            var httpValueCollection = HttpUtility.ParseQueryString(uri.Query);

            httpValueCollection.Add(name, value);

            // urlencodes the whole HttpValueCollection
            ub.Query = httpValueCollection.ToString();

            return ub.Uri;
        }

        public static string ToQueryString(this Uri uri, NameValueCollection nvc)
        {
            if (nvc == null)
            {
                return uri.ToString();
            }

            var queryString = new StringBuilder("?");

            bool first = true;

            foreach (string key in nvc.AllKeys)
            {
                var values = nvc.GetValues(key);
                if (values != null)
                    foreach (string value in values)
                    {
                        if (!first)
                        {
                            queryString.Append("&");
                        }

                        queryString.AppendFormat("{0}={1}", Uri.EscapeDataString(key), Uri.EscapeDataString(value));

                        first = false;
                    }
            }
            //return HttpUtility.UrlDecode(uri.ToString() + queryString);
            return uri.ToString() + queryString;
        }

    }
}
