using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web;
using USP.Business.Constants;
using USP.Business.Helpers;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers
{
    public class CookiePolicyController : RenderMvcSurfaceController
    {
        public ActionResult Cookies()

        {
            if (HttpContext.Items["DisableCookieBanner"] != null)
            {
                return Content("");
            }
            //USP-2_13/11/2020 Begin
            TempData["CurrentPageId"] = CurrentPage.Id;
            //USP-2_13/11/2020 End
            var cookiesModel = SettingHelper.GetSettingsByDocType<SettingsCookieMessage>();

            cookiesModel.CookieExist = Request.Cookies.AllKeys.Contains(SiteConstants.CookieName);
            cookiesModel.OtherCookie = CookieHelper.GetCookieBool(SiteConstants.CookieName, "other", true);

            return PartialView("Cookies" , cookiesModel);
        }

        [HttpGet]
        public ActionResult SaveCookies(bool otherCookies = false)
        {
            //USP-2_13/11/2020 Begin
            //var cookie = new HttpCookie(SiteConstants.CookieName);
            //cookie.Values.Add("other", otherCookies.ToString().ToLower());
            //Response.Cookies.Set(cookie);
            //return Redirect(UmbracoContext.Current.PublishedContentRequest.PublishedContent.Url);
            int CurrentPageId = Convert.ToInt32(TempData["CurrentPageId"]);
            return RedirectToUmbracoPage(CurrentPageId);
            //USP-2_13/11/2020 End
        }

    }
}
