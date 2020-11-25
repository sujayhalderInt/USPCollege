﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace USP.Business.UmbracoValidationAttributes
{
    public abstract class UmbracoConditionalValidationAttribute : ValidationAttribute, IClientValidatable
    {
        protected readonly ValidationAttribute InnerAttribute;
        public string DependentProperty { get; set; }
        public object TargetValue { get; set; }
        protected abstract string ValidationName { get; }

        private readonly string _errorMessageDictionaryKey;


        protected virtual IDictionary<string, object> GetExtraValidationParameters()
        {
            return new Dictionary<string, object>();
        }

        protected UmbracoConditionalValidationAttribute(ValidationAttribute innerAttribute, string dependentProperty, object targetValue, string errorMessageDictionaryKey)
        {
            InnerAttribute = innerAttribute;
            DependentProperty = dependentProperty;
            TargetValue = targetValue;
            _errorMessageDictionaryKey = errorMessageDictionaryKey;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // get a reference to the property this validation depends upon
            var containerType = validationContext.ObjectInstance.GetType();
            var field = containerType.GetProperty(this.DependentProperty);
            if (field != null)
            {
                // get the value of the dependent property
                var dependentvalue = field.GetValue(validationContext.ObjectInstance, null);

                // compare the value against the target value
                if ((dependentvalue == null && this.TargetValue == null) || (dependentvalue != null && dependentvalue.Equals(this.TargetValue)))
                {
                    // match => means we should try validating this field
                    if (!InnerAttribute.IsValid(value))
                    {
                        // validation failed - return an error
                        return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
                    }
                }
            }
            return ValidationResult.Success;
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ErrorMessage = UmbracoValidationHelper.GetDictionaryItem(_errorMessageDictionaryKey),
                ValidationType = ValidationName,
            };
            string depProp = BuildDependentPropertyId(metadata, context as ViewContext);
            // find the value on the control we depend on; if it's a bool, format it javascript style
            string targetValue = (TargetValue ?? "").ToString();
            if (this.TargetValue.GetType() == typeof(bool) || this.TargetValue is bool?)
            {
                targetValue = targetValue.ToLower();
            }
            rule.ValidationParameters.Add("dependentproperty", depProp);
            rule.ValidationParameters.Add("targetvalue", targetValue);
            // Add the extra params, if any
            foreach (var param in GetExtraValidationParameters())
            {
                rule.ValidationParameters.Add(param);
            }
            yield return rule;
        }

        private string BuildDependentPropertyId(ModelMetadata metadata, ViewContext viewContext)
        {
            string depProp = viewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(this.DependentProperty);
            // This will have the name of the current field appended to the beginning, because the TemplateInfo's context has had this fieldname appended to it.
            var thisField = metadata.PropertyName + "_";
            if (depProp.StartsWith(thisField))
            {
                depProp = depProp.Substring(thisField.Length);
            }
            return depProp;
        }
    }
}
