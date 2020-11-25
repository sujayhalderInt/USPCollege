using System.Collections.Generic;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers
{
    public class BreadcrumbController : RenderMvcSurfaceController
    {
        public ActionResult RenderBreadCrumb()
        {
            var items = new List<IPublishedContent>();
            IPublishedContent currentItem = CurrentPage;
            
            while (currentItem != null)
            {
                items.Add(currentItem);

                if (currentItem.ContentType.Alias.ToLower() != "pagehome")
                {
                    currentItem = currentItem.Parent;
                }
                else
                {
                    currentItem = null;
                }
            }

            items.Reverse();


            var dic = new Dictionary<IPublishedContent, BaseNavigation>();
            foreach (var publishedContent in items)
            {
                if (publishedContent.IsComposedOf("baseNavigation"))
                {
                    var navItem = new BaseNavigation(publishedContent);
                    dic.Add(publishedContent, navItem);
                }
            }

            return PartialView("BreadCrumb", dic);
        }
    }
}
