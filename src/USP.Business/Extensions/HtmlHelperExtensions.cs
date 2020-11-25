using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace USP.Business.Extensions
{
    public static class HtmlHelperExtensions
    {
        private const string JsHeadViewDataName = "RenderPartialViewHeadScript";
        private const string JsFootViewDataName = "RenderPartialViewFootScript";
        private const string StyleViewDataName = "RenderStyle";

        public static void AddHeadScripts(this HtmlHelper htmlHelper, string scriptUrl)
        {
            AddJavaScript(htmlHelper, scriptUrl, JsHeadViewDataName);
        }

        public static void AddFootScripts(this HtmlHelper htmlHelper, string scriptUrl)
        {
            AddJavaScript(htmlHelper, scriptUrl, JsFootViewDataName);
        }

        private static void AddJavaScript(this HtmlHelper htmlHelper, string scriptUrl, string listName)
        {
            var scriptList = htmlHelper.ViewContext.HttpContext.Items[listName] as List<string>;
            if (scriptList != null)
            {
                if (!scriptList.Contains(scriptUrl))
                {
                    scriptList.Add(scriptUrl);
                }
            }
            else
            {
                scriptList = new List<string> { scriptUrl };
                htmlHelper.ViewContext.HttpContext.Items.Add(listName, scriptList);
            }
        }

        public static MvcHtmlString RenderHeadScripts(this HtmlHelper htmlHelper)
        {
            return RenderJavaScripts(htmlHelper, JsHeadViewDataName);
        }

        public static MvcHtmlString RenderFootScripts(this HtmlHelper htmlHelper)
        {
            return RenderJavaScripts(htmlHelper, JsFootViewDataName);
        }

        private static MvcHtmlString RenderJavaScripts(this HtmlHelper htmlHelper, string listName)
        {
            var result = new StringBuilder();

            var scriptList = htmlHelper.ViewContext.HttpContext.Items[listName] as List<string>;

            if (scriptList == null) return MvcHtmlString.Create(result.ToString());

            foreach (var script in scriptList)
            {
                result.AppendLine(string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>", script));
            }

            return MvcHtmlString.Create(result.ToString());
        }

        public static void AddStyle(this HtmlHelper htmlHelper, string styleUrl)
        {
            var styleList = htmlHelper.ViewContext.HttpContext
              .Items[StyleViewDataName] as List<string>;

            if (styleList != null)
            {
                if (!styleList.Contains(styleUrl))
                {
                    styleList.Add(styleUrl);
                }
            }
            else
            {
                styleList = new List<string> { styleUrl };
                htmlHelper.ViewContext.HttpContext
                  .Items.Add(StyleViewDataName, styleList);
            }
        }

        public static MvcHtmlString RenderStyles(this HtmlHelper htmlHelper)
        {
            var result = new StringBuilder();

            var styleList = htmlHelper.ViewContext.HttpContext
              .Items[StyleViewDataName] as List<string>;

            if (styleList == null) return MvcHtmlString.Create(result.ToString());

            foreach (var script in styleList)
            {
                result.AppendLine(string.Format(
                    "<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />",
                    script));
            }

            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString LabelForRequired<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string labelText = "")
        {
            return LabelHelper(html,
                ModelMetadata.FromLambdaExpression(expression, html.ViewData),
                ExpressionHelper.GetExpressionText(expression), labelText);
        }

        private static MvcHtmlString LabelHelper(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, string labelText)
        {
            if (string.IsNullOrEmpty(labelText))
            {
                labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            }

            if (string.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }

            TagBuilder tag = new TagBuilder("label");
            tag.Attributes.Add(
                "for",
                TagBuilder.CreateSanitizedId(
                    html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)
                )
            );

            tag.SetInnerText(labelText);

            var output = tag.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(output);
        }
    }
}
