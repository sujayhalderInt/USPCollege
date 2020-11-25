using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DevTrends.MvcDonutCaching;

namespace USP.Business.Mvc
{
    public class UmbracoDonutOutputCacheAttribute : DonutOutputCacheAttribute
    {
        public UmbracoDonutOutputCacheAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool preview = false;

            var cookie = filterContext.HttpContext.Request.Cookies["UMB_PREVIEW"];
            if (!string.IsNullOrWhiteSpace(cookie?.Value))
            {
                preview = true;
            }

            if (filterContext.HttpContext.Request.QueryString?["dtgePreview"] != null)
            {
                preview = true;
            }

            if (!preview)
            {
                base.OnActionExecuting(filterContext);
            }
        }
    }
}
