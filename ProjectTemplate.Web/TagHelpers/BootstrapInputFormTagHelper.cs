using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTemplate.Web.TagHelpers
{
    [HtmlTargetElement("bootstrap-input-form-group")]
    public class BootstrapInputFormTagHelper : TagHelper
    {
        [HtmlAttributeName("for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator Generator { get; }

        public BootstrapInputFormTagHelper(IHtmlGenerator htmlGenerator)
        {
            Generator = htmlGenerator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "form-group");

            var labeTag = new TagBuilder("label");
            labeTag.AddCssClass("control-label");
            var displayName = For.Metadata?.DisplayName ?? For.Name;
            labeTag.InnerHtml.AppendHtml(displayName);

            var input = GetInputTag();
            var validation = GetValidationTag();

            output.PreContent.AppendHtml(labeTag);
            output.PreContent.AppendHtml(input);
            output.PreContent.AppendHtml(validation);

            base.Process(context, output);
        }

        private TagBuilder GetInputTag()
        {

            return Generator.GenerateTextBox(ViewContext, For.ModelExplorer, For.Name, For.Model, "", new
            {
                @class = "form-control",
                autocomplete = "off"
            });

        }

        private TagBuilder GetValidationTag()
        {
            return Generator.GenerateValidationMessage(ViewContext, For.ModelExplorer, For.Name, "", null, new
            {
                @class = "text-danger"
            });
        }
    }

    [HtmlTargetElement("bootstrap-password-form-group")]
    public class BootstrapPasswordFormTagHelper : TagHelper
    {
        [HtmlAttributeName("for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator Generator { get; }

        public BootstrapPasswordFormTagHelper(IHtmlGenerator htmlGenerator)
        {
            Generator = htmlGenerator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "form-group");

            var labeTag = new TagBuilder("label");
            labeTag.AddCssClass("control-label");
            var displayName = For.Metadata?.DisplayName ?? For.Name;
            labeTag.InnerHtml.AppendHtml(displayName);

            var input = GetInputTag();
            var validation = GetValidationTag();

            output.PreContent.AppendHtml(labeTag);
            output.PreContent.AppendHtml(input);
            output.PreContent.AppendHtml(validation);

            base.Process(context, output);
        }

        private TagBuilder GetInputTag()
        {

            return Generator.GeneratePassword(ViewContext, For.ModelExplorer, For.Name, For.Model, new
            {
                @class = "form-control",
                autocomplete = "off"
            });

        }

        private TagBuilder GetValidationTag()
        {
            return Generator.GenerateValidationMessage(ViewContext, For.ModelExplorer, For.Name, "", null, new
            {
                @class = "text-danger"
            });
        }
    }
}
