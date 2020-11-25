using System.Web.Mvc;
using USP.Business.Services.Interfaces;

namespace USP.Business.Controllers.Partials
{
    public class SeoController : RenderMvcSurfaceController
    {
        private readonly ISeoService _seoService;

        public SeoController(ISeoService seoService)
        {
            _seoService = seoService;
        }

        public ActionResult Seo()
        {
            return PartialView("SEO", _seoService.GetSeoMetaData(CurrentPage));
        }

    }
}
