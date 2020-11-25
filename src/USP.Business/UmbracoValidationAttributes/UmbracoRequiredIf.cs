using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace USP.Business.UmbracoValidationAttributes
{
    public class UmbracoRequiredIf :UmbracoConditionalValidationAttribute
    {
        public UmbracoRequiredIf(string errorMessageDictionaryKey , string dependentProperty, object targetValue) : base(new RequiredAttribute(), dependentProperty, targetValue, errorMessageDictionaryKey)
        {
        }

        protected override string ValidationName => "requiredif";

        protected override IDictionary<string, object> GetExtraValidationParameters()
        {
            return new Dictionary<string, object>
            {
                { "rule", "required" }
            };
        }
    }
}
