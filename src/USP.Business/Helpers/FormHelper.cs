using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using umbraco;
using Umbraco.Core.Models;
using Umbraco.Web;
using USP.Business.Constants;
using USP.Business.Models.ContentModels;
using USP.Business.Models.Dapper;
using USP.Business.Models.Form;

namespace USP.Business.Helpers
{
    public class FormHelper
    {
        public static IEnumerable<FormStep> GetFormSteps(ApplicationForm form)
        {
            var formStepsHolder = GetFormStepsHolderIPublishedContent();
            if (formStepsHolder == null) return null;
            var formStepsIpublishedContents = formStepsHolder.Children().Where(f => f.DocumentTypeAlias.Contains(SiteConstants.AliasApplicationForm.Contains)).OrderBy(f => f.SortOrder);

            return FormMappingHelper.FormStepsMapper(formStepsIpublishedContents, form);
        }

        public static string GetFormStepUrl(string pageDoctypeAlias)
        {
            var formStepsHolder = GetFormStepsHolderIPublishedContent();
            var page = formStepsHolder?.Children.FirstOrDefault(f => f.DocumentTypeAlias == pageDoctypeAlias);

            return page?.Url;
        }

        public static IPublishedContent GetFormStepsHolderIPublishedContent()
        {
            var formStepsHolder = SettingHelper.GetGlobalRoot().FirstOrDefault(f => f.DocumentTypeAlias == SiteConstants.AliasHomePage)?.Descendant(SiteConstants.AliasApplicationFormStepsHolder);
            return formStepsHolder;
        }

        public static string GetButtonTextContinueOrSave(bool isCurrentStepSubmitted, string continueText)
        {
            if (!isCurrentStepSubmitted) return continueText;
            var text = !string.IsNullOrEmpty(
                library.GetDictionaryItem(SiteConstants.UmbracoDictionaryKey.ApplicationForm.SaveButtonText))
                ? library.GetDictionaryItem(SiteConstants.UmbracoDictionaryKey.ApplicationForm.SaveButtonText)
                : "Save";
            return text;
        }

        public static string GetDictionaryItemOrSetDefaultString(string dictionaryKey, string defaultString)
        {
            return !string.IsNullOrEmpty(library.GetDictionaryItem(dictionaryKey))
                ? library.GetDictionaryItem(dictionaryKey)
                : defaultString;
        }

        public static string GetStringOrSetDefaultString(string field, string defaultString)
        {
            return !string.IsNullOrEmpty(field) ? field : defaultString;
        }

        public static string StringWithLink(string url, string errorMessage)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(errorMessage)) return string.Empty;
            return $"<a href =\"{url}\">{errorMessage}</a>";
        }

        public static string GetLoginPageUrlWithReturnUrlByAlias(string returnStepAlias = null)
        {
            var loginPageUrl = GetFormStepUrl(SiteConstants.AliasApplicationForm.StepOne);
            var returnPageUrl = !string.IsNullOrEmpty(returnStepAlias) ? GetFormStepUrl(returnStepAlias) : GetFormStepUrl(SiteConstants.AliasApplicationForm.StepTwo);
            return loginPageUrl + "?returnUrl=" + returnPageUrl;
        }

        public static string GetLoginPageUrlWithReturnUrl(string url)
        {
            var loginPageUrl = GetFormStepUrl(SiteConstants.AliasApplicationForm.StepOne);
            return loginPageUrl + "?returnUrl=" + url;
        }

        public static List<SelectListItem> GetDropDownlistFromString(string dataSource, string selectedValue = null)
        {
            if (string.IsNullOrEmpty(dataSource)) return null;
            var dropDownListArray = SplitStringByNewline(dataSource);
            var listItems = dropDownListArray.Select(value => new SelectListItem
            {
                Selected = (value == selectedValue),
                Value = value,
                Text = value
            })
                .ToList();
            listItems[0].Value = "";
            return listItems;
        }

        public static List<SelectListItem> GetDropDownlistFromRepeatableString(IEnumerable<string> dataSource, string selectedValue = null)
        {
            if (dataSource == null) return null;
            var listItems = dataSource.Select(value => new SelectListItem
            {
                Selected = (value == selectedValue),
                Value = value,
                Text = value,
            })
                    .ToList();
            listItems[0].Value = "";
            return listItems;
        }

        public static List<SelectListItem> GetDropDownList(IEnumerable<IPublishedContent> taxonomyFolder, string selectedValue = null)
        {
            if (taxonomyFolder == null || !taxonomyFolder.Any()) return null;
            var folder = taxonomyFolder.FirstOrDefault();
            if (folder == null) return null;
            var childrens = folder.Children.OfType<DataTaxonomy>();
            var listItems = childrens.Select(value => new SelectListItem
            {
                Selected = (value.GetKey().ToString() == selectedValue),
                Value = value.GetKey().ToString(),
                Text = value.TaxonomyName
            }).ToList();
            listItems.Insert(0, new SelectListItem
            {
                Text = SiteConstants.DropDownLists.NotSelectedText,
                Value = ""
            });
            return listItems;
        }

        public static List<CheckBox> GetDisabilitiesDifficultiesList(IEnumerable<string> dataSource)
        {
            if (dataSource == null || !dataSource.Any()) return null;
            var disablitiesList = dataSource.Select(data => new CheckBox
            {
                Label = data,
                Value = data
            })
                .ToList();

            var preferNotToSayLabel = !string.IsNullOrEmpty(library.GetDictionaryItem(SiteConstants.UmbracoDictionaryKey.ApplicationForm.Step10PlaceHolder.PreferNotToSay)) ? library.GetDictionaryItem(SiteConstants.UmbracoDictionaryKey.ApplicationForm.Step10PlaceHolder.PreferNotToSay) : SiteConstants.ApplicationFormGeneral.Step10.PreferNotToSayLabel;

            disablitiesList.Add(new CheckBox
            {
                Label = preferNotToSayLabel,
                Value = SiteConstants.ApplicationFormGeneral.Step10.PreferNotToSayValue
            });

            return disablitiesList;
        }

        public static string[] SplitStringByNewline(string dataSource)
        {
            return string.IsNullOrEmpty(dataSource) ? null : dataSource.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string GetField(string courseGuid, string fieldName = SiteConstants.Fields.BannerHeading)
        {
            var helper = new UmbracoHelper(UmbracoContext.Current);
            var x = (IPublishedContent)helper.Content(courseGuid);
            return x == null ? string.Empty : x.GetPropertyValue<string>(fieldName);
        }
    }
}
