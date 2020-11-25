using System.Web.Mvc;
using umbraco;
using Umbraco.Web;
using Umbraco.Web.Models;
using USP.Business.Constants;
using USP.Business.Enums;
using USP.Business.Models.ContentModels;
using USP.Business.Models.PostBackModels;
using USP.Business.Services.Interfaces;
using System.Linq;
using USP.Business.Helpers;

namespace USP.Business.Controllers.Pages
{
    public class PageRegistrationController : RenderMvcSurfaceController
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IEmailService _emailService;
        private readonly IRecaptchaService _recaptchaService;

        public PageRegistrationController(IAuthorizationService authorizationService, IEmailService emailService, IRecaptchaService recaptchaService)
        {
            _authorizationService = authorizationService;
            _emailService = emailService;
            _recaptchaService = recaptchaService;
        }

        public override ActionResult Index(RenderModel model)
        {
            var viewModel = model.Content.OfType<PageRegistration>();
            var returnUrl = Request.QueryString["returnUrl"];

            var loginPageUrl = viewModel.LoginPage?.First()?.Url();
            if (string.IsNullOrEmpty(loginPageUrl))
            {
                loginPageUrl = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepOne);
                if (string.IsNullOrEmpty(loginPageUrl))
                    return CurrentTemplate(model);
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "?returnUrl=" + returnUrl;
            }

            var fullLoginPageUrlWithReturnUrl = loginPageUrl + returnUrl;
            viewModel.LoginPageUrlWithReturnUrl = fullLoginPageUrlWithReturnUrl;
            return CurrentTemplate(viewModel);
        }

        [HttpPost]
        public ActionResult HandleRegistration(RegistrationPostBackModel postmodel)
        {
            var captcha = Request["g-recaptcha-response"];

            if (string.IsNullOrEmpty(captcha) || !_recaptchaService.VerifyRecaptcha(captcha, library.GetDictionaryItem(SiteConstants.RecaptchaSecretKeyKey)))
            {
                ModelState.AddModelError("Recaptcha", library.GetDictionaryItem(SiteConstants.RegistrationValidationRecaptchaInvalidKey));
            }

            var model = new PageRegistration(UmbracoContext.Current.PublishedContentRequest.PublishedContent)
            {
                EmailAddress = postmodel.EmailAddress,
                Password = postmodel.Password
            };

            var loginPageUrl = model.LoginPage.First().Url();
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }
            if (Services.MemberService.GetByEmail(model.EmailAddress) != null)
            {
                ModelState.AddModelError(FormErrorKeys.UserExists.ToString(), library.GetDictionaryItem(SiteConstants.RegistrationFailedUserExistsKey));
                return CurrentUmbracoPage();
            }
            var createdUser = _authorizationService.RegisterUmbracoUser(model, true);
            if (createdUser == null)
            {
                ModelState.AddModelError(FormErrorKeys.RegistrationError.ToString(), library.GetDictionaryItem(SiteConstants.RegistrationFailedErrorKey));
                return CurrentUmbracoPage();
            }
            var emailSent = _emailService.SendEmail(
                model.EmailAddress,
                string.Empty,
                CurrentPage.GetPropertyValue<string>("emailSubject"),
                CurrentPage.GetPropertyValue<string>("emailBody"),
                CurrentPage.GetPropertyValue<string>("emailFrom"),
                null
            );
            Members.Login(model.EmailAddress, model.Password);

            var returnUrl = Request.QueryString["returnUrl"];
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            var formStepTwoUrl = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepTwo);
            return Redirect(formStepTwoUrl);

        }
    }
}
