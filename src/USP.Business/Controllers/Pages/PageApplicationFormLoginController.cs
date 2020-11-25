using System;
using System.Compat.Web;
using System.Web.Mvc;
using System.Web.Security;
using umbraco;
using Umbraco.Core.Logging;
using Umbraco.Web;
using Umbraco.Web.Models;
using USP.Business.Constants;
using USP.Business.Enums;
using USP.Business.Helpers;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Pages
{
    public class PageApplicationFormLoginController : RenderMvcSurfaceController
    {
        public override ActionResult Index(RenderModel model)
        {
            return CurrentTemplate(model.Content.OfType<PageApplicationFormLogin>());
        }

        public ActionResult MemberLogOut()
        {
            if (Members.IsLoggedIn())
            {
                Members.Logout();
            }

            return RedirectToCurrentUmbracoPage();
        }

        [HttpPost]
        public ActionResult HandleLogin(string emailAddress, string password)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }
            if (Members.IsLoggedIn())
            {
                Members.Logout();
            }
            var formStepTwoUrl = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepTwo);
            if (Members.Login(emailAddress, password))
            {
                FormsAuthentication.SetAuthCookie(emailAddress, true);
                var queryString = Request.QueryString;
                var returnUrl = "/";
                try
                {
                    if (queryString.HasKeys())
                    {
                        returnUrl = queryString["returnUrl"];
                        var sAbsolute = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, returnUrl);
                        returnUrl = HttpUtility.UrlDecode(sAbsolute);
                        var returnUri = new Uri(returnUrl);
                        return Redirect(returnUri.ToString());
                    }

                    return Redirect(formStepTwoUrl);
                }
                catch (Exception ex)
                {
                    LogHelper.Error<PageApplicationFormLoginController>("Login: Error redirecting to returnUrl", ex);
                    return Redirect(returnUrl);
                }
            }

            ModelState.AddModelError(FormErrorKeys.LoginFailure.ToString(), library.GetDictionaryItem(SiteConstants.UmbracoDictionaryKey.Login.Failed));
            return CurrentUmbracoPage();
        }
    }
}
