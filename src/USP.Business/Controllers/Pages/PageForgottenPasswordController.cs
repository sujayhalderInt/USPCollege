using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using umbraco;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;
using USP.Business.Constants;
using USP.Business.Enums;
using USP.Business.Services.Interfaces;

namespace USP.Business.Controllers.Pages
{
    public class PageForgottenPasswordController : RenderMvcSurfaceController
    {
        private readonly IEmailService _emailService;
        private readonly IAuthorizationService _authorizationService;

        public PageForgottenPasswordController(IEmailService emailService, IAuthorizationService authorizationService)
        {
            _emailService = emailService;
            _authorizationService = authorizationService;
        }

        public override ActionResult Index(RenderModel model)
        {
            return CurrentTemplate(model);
        }

        [HttpPost]
        public ActionResult HandleForgottenPassword(string emailAddress)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            var memberService = Services.MemberService;
            var member = memberService.GetByEmail(emailAddress);

            if (member == null)
            {
                //ModelState.AddModelError("EmailAddress", library.GetDictionaryItem(SiteConstants.ForgottenPasswordUserNotExistsKey));
                ViewData["error"] = library.GetDictionaryItem(SiteConstants.ForgottenPasswordUserNotExistsKey);
                return CurrentUmbracoPage();
            }

            //Set expiry date to now  + 7 days
            var expiryTime = DateTime.Now.AddDays(7);
            var expiryToken = _authorizationService.SetMemberResetPasswordToken(member, expiryTime);

            var emailFrom = CurrentPage.GetPropertyValue<string>("from");
            var emailSubject = CurrentPage.GetPropertyValue<string>("subject");
            var emailBody = CurrentPage.GetPropertyValue<string>("body");

            var resetPasswordPage = CurrentPage.GetPropertyValue<IEnumerable<IPublishedContent>>("resetPasswordPage").FirstOrDefault();
            var resetPasswordPageUrl = resetPasswordPage.UrlAbsolute();

            //Reset password link generation
            var resetPasswordUrl = $"{resetPasswordPageUrl}?token-reset={expiryToken}&emailAddress={emailAddress}";

            var token = new Dictionary<EmailTokens, string>
            {
                {EmailTokens.ForgotPasswordLink, resetPasswordUrl}
            };

            //Send user an email to reset password with token in URL
            if (_emailService.SendEmail(emailAddress, string.Empty, emailSubject, emailBody, emailFrom, token))
            {
                //ModelState.AddModelError("EmailAddress", library.GetDictionaryItem(SiteConstants.ForgottenPasswordEmailSentKey));
                ViewData["success"] = library.GetDictionaryItem(SiteConstants.ForgottenPasswordEmailSentKey);
                ModelState.Clear();
                return CurrentUmbracoPage();
            }

            //ModelState.AddModelError("EmailAddress", library.GetDictionaryItem(SiteConstants.ForgottenPasswordErrorKey));
            ViewData["error"] = library.GetDictionaryItem(SiteConstants.ForgottenPasswordErrorKey);
            return CurrentUmbracoPage();
        }
    }
}
