using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace USP.Business.Extensions
{
    public static class MvcHtmlStringExtensions
    {
        const string InputFilledClass = "input--filled";
        const string GroupValidationErrorClass = "group-validation-error";

        public static PropertyInError AddClassesOnPropertyValidation<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var expressionText = ExpressionHelper.GetExpressionText(expression);
            var fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expressionText);
            var state = htmlHelper.ViewData.ModelState[fullHtmlFieldName];
            var propertyInError = new PropertyInError();

            string propertyClass = string.Empty;

            if (state != null)
            {
                var modelState = htmlHelper.ViewData.ModelState[expressionText].Value;

                if (!string.IsNullOrEmpty(modelState?.AttemptedValue))
                {
                    propertyClass = InputFilledClass;
                }
            }

            if (state == null)
            {
                propertyInError.ErrorClass = new MvcHtmlString(propertyClass);
                return propertyInError;
            }

            if (state.Errors.Count == 0)
            {
                propertyInError.ErrorClass = new MvcHtmlString(propertyClass);
                return propertyInError;
            }

            propertyInError.ErrorState = state.Errors[0].ErrorMessage;
            propertyInError.ErrorClass = new MvcHtmlString(string.Format("{0} {1}", GroupValidationErrorClass, propertyClass));

            return propertyInError;
        }

        public static MvcHtmlString AddClassStringOnPropertyValidation<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return AddClassesOnPropertyValidation(htmlHelper, expression).ErrorClass;
        }

    }

    public class PropertyInError
    {
        public string ErrorState { get; set; }
        public MvcHtmlString ErrorClass { get; set; }
    }
}
