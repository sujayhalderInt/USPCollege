using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace USP.Business.UmbracoValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class UmbracoDataType : DataTypeAttribute, IClientValidatable
    {
        private readonly string _errorMessageDictionaryKey;

        public UmbracoDataType(DataType type, string errorMessageDictionaryKey) : base(type)
        {
            _errorMessageDictionaryKey = errorMessageDictionaryKey;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(System.Web.Mvc.ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage = UmbracoValidationHelper.GetDictionaryItem(_errorMessageDictionaryKey);

            yield return new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.DisplayName),
                ValidationType = this.DataType.ToString().ToLower()
            };
        }
    }
}