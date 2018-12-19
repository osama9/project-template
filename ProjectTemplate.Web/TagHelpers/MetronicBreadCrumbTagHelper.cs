using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ProjectTemplate.Web.TagHelpers.Breadcrumb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProjectTemplate.Web.TagHelpers
{
    public class MetronicBreadCrumbTagHelper : TagHelper
    {
        /// <summary>
        /// Such as 'Home' or 'الرئيسية'
        /// </summary>
        [HtmlAttributeName("asp-homepage-title")]
        public string HomePageTitle { set; get; }

        /// <summary>
        /// Such as @Url.Action("Index", "Home")
        /// </summary>
        [HtmlAttributeName("asp-homepage-url")]
        public string HomePageUrl { set; get; }

        /// <summary>
        /// such as `glyphicon glyphicon-home`
        /// </summary>
        [HtmlAttributeName("asp-homepage-glyphicon")]
        public string HomePageGlyphIcon { set; get; }

        /// <summary>
        ///
        /// </summary>
        protected HttpRequest Request => ViewContext.HttpContext.Request;

        /// <summary>
        ///
        /// </summary>
        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            var breadCrumbs = ViewContext.HttpContext.Items[BreadCrumbExtentions.CurrentBreadCrumbKey] as List<BreadCrumb>;
            if (breadCrumbs == null || !breadCrumbs.Any())
            {
                return;
            }

            var currentFullUrl = Request.GetEncodedUrl();
            var currentRouteUrl = new UrlHelper(ViewContext).Action(ViewContext.ActionDescriptor.RouteValues["action"]);
            var isCurrentPageHomeUrl = HomePageUrl.Equals(currentFullUrl, StringComparison.OrdinalIgnoreCase) ||
                                       HomePageUrl.Equals(currentRouteUrl, StringComparison.OrdinalIgnoreCase);


            output.TagName = "ul";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "page-breadcrumb");

            if (isCurrentPageHomeUrl)
            {
                var itemBuilder = new TagBuilder("li");
                itemBuilder.AddCssClass("active");

                itemBuilder.InnerHtml.AppendHtml(
                    $"<span class='{HomePageGlyphIcon}' aria-hidden='true'></span> {HomePageTitle}");

                output.Content.AppendHtml(itemBuilder);
            }
            else
            {
                var itemBuilder = new TagBuilder("li");
                itemBuilder.InnerHtml.AppendHtml(
                    $"<a href='{HomePageUrl}'><span class='{HomePageGlyphIcon}' aria-hidden='true'></span> {HomePageTitle}</a>");

                output.Content.AppendHtml(itemBuilder);
            }

            foreach (var node in breadCrumbs.OrderBy(x => x.Order))
            {
                if (node.Url.Equals(HomePageUrl, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (node.Url.Equals(currentFullUrl, StringComparison.OrdinalIgnoreCase) ||
                    node.Url.Equals(currentRouteUrl, StringComparison.OrdinalIgnoreCase))
                {
                    var itemBuilder = new TagBuilder("li");
                    itemBuilder.AddCssClass("active");

                    itemBuilder.InnerHtml.AppendHtml(
                    $"<i class='fa fa-circle'></i>");


                    if (!string.IsNullOrWhiteSpace(node.Icon))
                    {
                        itemBuilder.InnerHtml.AppendHtml(
                            $"<i class='{node.Icon}'></i> ");
                    }
                    itemBuilder.InnerHtml.AppendHtml($"{node.Title}");

                    output.Content.AppendHtml(itemBuilder);
                }
                else
                {
                    var itemBuilder = new TagBuilder("li");

                    itemBuilder.InnerHtml.AppendHtml(
                    $"<i class='fa fa-circle'></i>");


                    itemBuilder.InnerHtml.AppendHtml($@"<a href='{WebUtility.HtmlEncode(node.Url)}'>");

                    if (!string.IsNullOrWhiteSpace(node.Icon))
                    {
                        itemBuilder.InnerHtml.AppendHtml(
                            $"<i class='{node.Icon}'></i> ");
                    }
                    itemBuilder.InnerHtml.AppendHtml($"{node.Title}");
                    itemBuilder.InnerHtml.AppendHtml("</a>");
                    output.Content.AppendHtml(itemBuilder);
                }
            }
        }
    }
}
