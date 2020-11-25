using System.Linq;
using System.Web.Mvc;
using umbraco;
using Umbraco.Web;
using Umbraco.Web.Models;
using USP.Business.Constants;
using USP.Business.Helpers;
using USP.Business.Models.ContentModels;
using USP.Business.Models.PostBackModels.Application;
using USP.Business.Services.Interfaces;

namespace USP.Business.Controllers.ApplicationForm
{
    public class FormStepTenController : RenderMvcSurfaceController
    {
        private readonly IDatabaseService _databaseService;

        private readonly string _nextStepUrl = FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepEleven);


        public FormStepTenController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public override ActionResult Index(RenderModel model)
        {
            var viewModel = model.Content.OfType<FormStepTen>();

            if (!Members.IsLoggedIn())
            {
                return Redirect(FormHelper.GetLoginPageUrlWithReturnUrlByAlias());
            }

            var userGuid = Members.GetCurrentMember().GetKey();
            var userForm = _databaseService.GetApplicationData(userGuid);
            if (userForm.IsSubmitted)
            {
                return Redirect(FormHelper.GetFormStepUrl(SiteConstants.AliasApplicationForm.StepThirteen));
            }
            viewModel.ApplicationForm = userForm;
            viewModel.PostBackModel = new FormStepTenPostBackModel
            {
                HasDisabilityDifficulty = userForm.HasDisabilityDifficulty,
                DisabilitiesDifficultiesCsv = userForm.DisabilitiesDifficultiesCsv,
                PrimaryDisability = userForm.PrimaryDisability,
                Ethnicity = userForm.StudentsEthnicity,
                Nationality = userForm.StudentsNationality,
                Religion = userForm.StudentsReligion,
                FirstLanguage = userForm.StudentsFirstLanguage,
                ResidentOfUkEuForThreeYears = userForm.ResidentOfUkEuForThreeYears,
                NameOfCountryOutsideUkEu = userForm.NameOfCountryOutsideUkEu,
                DateOfResidenceOutsideUkEuFrom = userForm.DateOfResidenceOutsideUkEuFrom,
                DateOfResidenceOutsideUkEuTo = userForm.DateOfResidenceOutsideUkEuTo,
                AnyCriminalConvictionOrFinalWarning = userForm.AnyCriminalConvictionOrFinalWarning
            };
            viewModel.SortedDisabilitiesList = viewModel.CheckBoxListDisability?.ToList().OrderBy(d => d);
            return CurrentTemplate(viewModel);
        }

        [HttpPost]
        public ActionResult Index([Bind(Prefix = "PostBackModel")]FormStepTenPostBackModel submission)
        {
            if (!Members.IsLoggedIn()) return Redirect(FormHelper.GetLoginPageUrlWithReturnUrl(UmbracoContext.Current.PublishedContentRequest.PublishedContent.Url));

            if (submission.HasDisabilityDifficulty == true)
            {
                if (string.IsNullOrEmpty(submission.DisabilitiesDifficultiesCsv))
                {
                    ModelState.AddModelError("PostBackModel.PrimaryDisability", library.GetDictionaryItem(SiteConstants.UmbracoDictionaryKey.ApplicationForm.Step10Errors.DisabilitiesDifficultiesRequired));
                }
                else
                {
                    var responseCount = submission.DisabilitiesDifficultiesCsv.Split(',').Length;
                    if (responseCount > 1 && string.IsNullOrEmpty(submission.PrimaryDisability))
                    {
                        ModelState.AddModelError("PostBackModel.PrimaryDisability", library.GetDictionaryItem(SiteConstants.UmbracoDictionaryKey.ApplicationForm.Step10Errors.PrimaryDisabilitiesDifficultiesRequired));
                    }
                }
            }
            var userGuid = Members.GetCurrentMember().GetKey();
            var applicationForm = _databaseService.GetApplicationData(userGuid) ?? new Models.Dapper.ApplicationForm();

            if (!ModelState.IsValid)
            {
                //rebuild the view model 
                var formStepTenModel =
                    new FormStepTen(UmbracoContext.Current.PublishedContentRequest.PublishedContent)
                    {
                        ApplicationForm = applicationForm,
                        PostBackModel = submission
                    };
                formStepTenModel.SortedDisabilitiesList = formStepTenModel.CheckBoxListDisability?.ToList().OrderBy(d => d);
                return CurrentTemplate(formStepTenModel);
            }

            applicationForm.HasDisabilityDifficulty = submission.HasDisabilityDifficulty;
            applicationForm.DisabilitiesDifficultiesCsv = submission.DisabilitiesDifficultiesCsv;
            applicationForm.PrimaryDisability = submission.PrimaryDisability ?? submission.DisabilitiesDifficultiesCsv;//at this point it will just be a single value
            applicationForm.StudentsEthnicity = submission.Ethnicity;
            applicationForm.StudentsNationality = submission.Nationality;
            applicationForm.StudentsReligion = submission.Religion;
            applicationForm.StudentsFirstLanguage = submission.FirstLanguage;
            applicationForm.ResidentOfUkEuForThreeYears = submission.ResidentOfUkEuForThreeYears;
            applicationForm.NameOfCountryOutsideUkEu = submission.NameOfCountryOutsideUkEu;
            applicationForm.DateOfResidenceOutsideUkEuFrom = submission.DateOfResidenceOutsideUkEuFrom;
            applicationForm.DateOfResidenceOutsideUkEuTo = submission.DateOfResidenceOutsideUkEuTo;
            applicationForm.AnyCriminalConvictionOrFinalWarning = submission.AnyCriminalConvictionOrFinalWarning;
            applicationForm.IsStepTenSubmitted = true;
            _databaseService.InsertApplicationData(userGuid, applicationForm);

            if (string.IsNullOrEmpty(_nextStepUrl))
            {
                return RedirectToCurrentUmbracoPage();
            }

            return Redirect(_nextStepUrl);
        }
    }
}
