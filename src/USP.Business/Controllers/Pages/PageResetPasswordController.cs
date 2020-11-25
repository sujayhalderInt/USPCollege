using System;
using System.Collections.Specialized;
using System.Web.Mvc;
using umbraco;
using Umbraco.Web;
using Umbraco.Web.Models;
using USP.Business.Constants;
using USP.Business.Enums;
using USP.Business.Models.ContentModels;

namespace USP.Business.Controllers.Pages
{
    public class PageResetPasswordController : RenderMvcSurfaceController
    {
        public override ActionResult Index(RenderModel model)
        {
            var viewModel = model.Content.OfType<PageResetPassword>();

            try
            {
                var parent = CurrentPage.Parent;
                var login = parent.Descendant("pageApplicationFormLogin");
                
                viewModel.LoginPageUrlWithReturnUrl = (login != null ? login.Url : null);
            }
            catch(Exception e)
            {

            }
            return CurrentTemplate(model);
        }

        [HttpPost]
        public ActionResult HandleResetPassword(string emailAddress, string password, string confirmPassword)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            var memberService = Services.MemberService;
            var resetMember = memberService.GetByEmail(emailAddress);

            if (resetMember == null)
            {
                //ModelState.AddModelError(FormErrorKeys.UserNotExists.ToString(), library.GetDictionaryItem(SiteConstants.ResetPasswordUserNotExistsKey));
                ViewData["error"] = library.GetDictionaryItem(SiteConstants.ResetPasswordUserNotExistsKey);

                return CurrentUmbracoPage();
            }

            //Get the querystring
            var resetQs = Request.QueryString["token-reset"];
            if (!string.IsNullOrEmpty(resetQs))
            {
                if (resetMember.GetValue<string>("resetPasswordToken") == resetQs)
                {
                    //Got a match, now check to see if the 1 week window hasn't expired
                    var expiryTime = DateTime.ParseExact(resetQs, "ddMMyyyyHHmmssFFFF", null);

                    //Check the current time is less than the expiry time
                    var currentTime = DateTime.Now;

                    //Check if the date has NOT expired 
                    if (currentTime.CompareTo(expiryTime) < 0)
                    {
                        //Got a match, we can allow user to update password
                        memberService.SavePassword(resetMember, password);
                        resetMember.SetValue("resetPasswordToken", string.Empty);

                        memberService.Save(resetMember);
                        var queryString = new NameValueCollection
                        {
                            {"success", "1"}
                        };

                        return RedirectToCurrentUmbracoPage(queryString);
                    }

                    //ERROR: Reset token has expired
                    //ModelState.AddModelError(FormErrorKeys.TokenExpired.ToString(), library.GetDictionaryItem(SiteConstants.ResetPasswordTokenExpiredKey));

                    ViewData["error"] = library.GetDictionaryItem(SiteConstants.ResetPasswordTokenExpiredKey);

                    return CurrentUmbracoPage();
                }

                //ERROR: QS does not match what is stored on member property
                //ModelState.AddModelError(FormErrorKeys.InvalidToken.ToString(), library.GetDictionaryItem(SiteConstants.ResetPasswordInvalidTokenKey));

                ViewData["error"] = library.GetDictionaryItem(SiteConstants.ResetPasswordInvalidTokenKey);

                return CurrentUmbracoPage();
            }

            //ERROR: No QS present
            //ModelState.AddModelError(FormErrorKeys.InvalidToken.ToString(), library.GetDictionaryItem(SiteConstants.ResetPasswordInvalidTokenKey));
            ViewData["error"] = library.GetDictionaryItem(SiteConstants.ResetPasswordInvalidTokenKey);

            return CurrentUmbracoPage();
        }
    }
}
