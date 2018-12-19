using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ProjectTemplate.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTemplate.Web.TagHelpers
{
    [HtmlTargetElement("select2-form-group")]
    public class Select2TagHelper : TagHelper
    {
        private string DEFAULT_CLASS_NAME = "asp-select2-";

        [HtmlAttributeName("for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("asp-selected-value")]
        public string Value { get; set; }

        [HtmlAttributeName("asp-selected-display-text")]
        public string Text { get; set; }

        [HtmlAttributeName("asp-search-url")]
        public string SearchUrl { get; set; }

        [HtmlAttributeName("asp-placeholder")]
        public string PlaceHolder { get; set; } = CommonText.Select;

        [HtmlAttributeName("asp-template-id")]
        public string TemplateId { get; set; }

        [HtmlAttributeName("label-for")]
        public ModelExpression Label { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }


        protected IHtmlGenerator Generator { get; }

        public Select2TagHelper(IHtmlGenerator htmlGenerator)
        {
            Generator = htmlGenerator;
        }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var prefixHtmlField = ViewContext.ViewData.TemplateInfo.HtmlFieldPrefix;

            if (!String.IsNullOrEmpty(prefixHtmlField))
                prefixHtmlField = $"{prefixHtmlField}_";

            DEFAULT_CLASS_NAME = $"{DEFAULT_CLASS_NAME}{prefixHtmlField}{For.Name}";

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "form-group");

            var required = "";

            var labelTag = new TagBuilder("label");
            labelTag.AddCssClass("control-label");

            if (For.Metadata.IsRequired)
                required = " <span class='required'>*</span>";

            string labelName = Label != null ? $"{Label?.Metadata?.DisplayName} ({For.Metadata?.DisplayName ?? For.Name})" + required : null;

            var displayName = labelName ?? For.Metadata?.DisplayName ?? For.Name;
            displayName += required;
            var selectId = For.Metadata?.PropertyName ?? "";

            labelTag.InnerHtml.AppendHtml(displayName);

            var input = GetInputTag();
            input.Attributes.Add("data-placeholder", PlaceHolder);

            if (!String.IsNullOrWhiteSpace(SearchUrl))
                input.Attributes.Add("data-search-url", SearchUrl);

            if (!String.IsNullOrWhiteSpace(TemplateId))
            {
                var templateId = TemplateId.StartsWith('#') ? TemplateId : TemplateId.Prepend('#').ToString();
                input.Attributes.Add("data-template-id", templateId);
            }

            var validation = GetValidationTag();

            output.PreContent.AppendHtml(labelTag);
            output.PreContent.AppendHtml(input);
            output.PreContent.AppendHtml(validation);

            base.Process(context, output);
        }

        private TagBuilder GetInputTag()
        {
            var tempSelectItemsList = new List<SelectListItem>();

            if (!String.IsNullOrWhiteSpace(Value) && !String.IsNullOrWhiteSpace(Text))
            {
                tempSelectItemsList.Add(new SelectListItem
                {
                    Value = Value,
                    Text = Text,
                    Selected = true
                });
            }

            //if you put the placeholder within GenerateSelect it will not work for select2.
            return Generator.GenerateSelect(ViewContext, For.ModelExplorer, "", For.Name, tempSelectItemsList, false, new
            {
                @class = DEFAULT_CLASS_NAME,
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
