using System.Web.Mvc;
using USP.Business.Helpers;
using USP.Business.Models.Form;

namespace USP.Business.Controllers.Partials
{
    public class FormStepsListingController : RenderMvcSurfaceController
    {
        public ActionResult StepsListing(int currentFormStep = 0, Models.Dapper.ApplicationForm form = null)
        {
            if (form == null)
            {
                form = new Models.Dapper.ApplicationForm();
            }
            var model = new FormStepsListing
            {
                FormSteps = FormHelper.GetFormSteps(form),
                CurrentFormStep = currentFormStep,
                IsUserLoggedIn = Members.IsLoggedIn()
            };
            return PartialView("FormStepsListing", model);
        }


        public ActionResult ProgressBarWidth(string width = null)
        {
            if (width == null) { width = "20"; }
            var model = new FormProgressBar { ProgressBarWidth = width };
            return PartialView("ProgressBar", model);
        }
    }
}
