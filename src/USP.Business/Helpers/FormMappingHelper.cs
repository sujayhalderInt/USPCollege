using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;
using USP.Business.Constants;
using USP.Business.Models.Dapper;
using USP.Business.Models.Form;

namespace USP.Business.Helpers
{
    public class FormMappingHelper
    {
        public static IEnumerable<FormStep> FormStepsMapper(IOrderedEnumerable<IPublishedContent> formStepsIPublishedContents, ApplicationForm form)
        {
            return formStepsIPublishedContents?.Select(step => new FormStep
            {
                StepName = !string.IsNullOrEmpty(step.GetPropertyValue<string>("listingName")) ? step.GetPropertyValue<string>("listingName") : step.Name,
                StepUrl = step.Url,
                StepListingOrder = step.SortOrder,
                StepDoctypeAlias = step.DocumentTypeAlias,
                IsSubmitted = IsSubmitted(step.DocumentTypeAlias, form)
            })
                .ToList();
        }

        public static bool IsSubmitted(string stepDoctypeAlias, ApplicationForm form)
        {
            bool isSubmitted;
            switch (stepDoctypeAlias)
            {
                case SiteConstants.AliasApplicationForm.StepTwo:
                    isSubmitted = form.IsStepTwoSubmitted;
                    break;
                case SiteConstants.AliasApplicationForm.StepThree:
                    isSubmitted = form.IsStepThreeSubmitted;
                    break;
                case SiteConstants.AliasApplicationForm.StepFour:
                    isSubmitted = form.IsStepFourSubmitted;
                    break;
                case SiteConstants.AliasApplicationForm.StepFive:
                    isSubmitted = form.IsStepFiveSubmitted;
                    break;
                case SiteConstants.AliasApplicationForm.StepSix:
                    isSubmitted = form.IsStepSixSubmitted;
                    break;
                case SiteConstants.AliasApplicationForm.StepSeven:
                    isSubmitted = form.IsStepSevenSubmitted;
                    break;
                case SiteConstants.AliasApplicationForm.StepEight:
                    isSubmitted = form.IsStepEightSubmitted;
                    break;
                case SiteConstants.AliasApplicationForm.StepNine:
                    isSubmitted = form.IsStepNineSubmitted;
                    break;
                case SiteConstants.AliasApplicationForm.StepTen:
                    isSubmitted = form.IsStepTenSubmitted;
                    break;
                case SiteConstants.AliasApplicationForm.StepEleven:
                    isSubmitted = form.IsStepElevenSubmitted;
                    break;
                case SiteConstants.AliasApplicationForm.StepTwelve:
                    isSubmitted = form.IsStepTwelveSubmitted;
                    break;
                default:
                    isSubmitted = false;
                    break;
            }

            return isSubmitted;
        }
    }
}
