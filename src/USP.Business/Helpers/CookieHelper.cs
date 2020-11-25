using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using USP.Business.Constants;

namespace USP.Business.Helpers
{
    public static class CookieHelper
    {
        public static bool GetCookieBool(string cookiename, string cookievaluename, bool defaultValue)
        {
            var cookie = HttpContext.Current.Request.Cookies[SiteConstants.CookieName];
            if (cookie != null)
            {
                var other = cookie.Values["other"];
                if (bool.TryParse(other, out bool val))
                {
                    return val;
                }
            }

            return defaultValue;
        }
    }
}
