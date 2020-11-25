using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace USP.Business.UmbracoValidationAttributes
{
    public class UmbracoRegularExpression : RegularExpressionAttribute, IClientValidatable
    {
        private readonly string _errorMessageDictionaryKey;
        


        public UmbracoRegularExpression(string errorMessageDictionaryKey,string patternKey): base(UmbracoValidationHelper.GetDictionaryItem(patternKey))
        {
            _errorMessageDictionaryKey = errorMessageDictionaryKey;
        }


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage = UmbracoValidationHelper.GetDictionaryItem(_errorMessageDictionaryKey);

            var error           = FormatErrorMessage(metadata.DisplayName);
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = error,
                ValidationType = "regex"
            };

            rule.ValidationParameters.Add("pattern", this.Pattern);

            yield return rule;
        }
    }
}