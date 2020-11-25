using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;
using USP.Business.Constants;

namespace USP.Business.Controllers.Sitemap
{
    public class SitemapController : RenderMvcSurfaceController
    {
        public override ActionResult Index(RenderModel model)
        {
            IEnumerable<Models.Data.Sitemap> sitemap = model.Content.DescendantsOrSelf()
                .Where(x => x.ContentType.Alias != SiteConstants.AliasNotFound)
                .Select(Map)
                .Where(w => !w.HideFromSitemap)
                .ToList();

            Response.ContentType = "text/xml";
            return View(sitemap);
        }

        private Models.Data.Sitemap Map(IPublishedContent publishedContent)
        {
            var s = new Models.Data.Sitemap();

            if (publishedContent.HasProperty("hideFromSitemap"))
            {
                s.HideFromSitemap = publishedContent.GetPropertyValue<bool>("hideFromSitemap");
            }

            s.LastModified = (publishedContent.UpdateDate != DateTime.MinValue) ? publishedContent.UpdateDate : publishedContent.CreateDate;
            s.Url = publishedContent.Url;

            return s;
        }
    }
}
