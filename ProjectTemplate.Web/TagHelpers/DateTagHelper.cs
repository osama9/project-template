using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTemplate.Web.TagHelpers
{
    [HtmlTargetElement("datetime-picker", TagStructure = TagStructure.WithoutEndTag)]
    public class DateTagHelper : TagHelper
    {
        public enum SelectMode
        {
            Date = 0,
            Month = 1,
            Year = 2,
            DateTime = 3
        }

        public enum ViewMode
        {
            Days = 0,
            Months = 1,
            Years = 2
        }


        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("asp-switch-for-other-element")]
        public ModelExpression OtherFor { get; set; }

        [HtmlAttributeName("asp-switch-hijri")]
        public bool AllowSwitchToHijri { get; set; } = true;

        [HtmlAttributeName("asp-month-only")]
        public bool MonthOnly { get; set; } = false;

        [HtmlAttributeName("asp-min-date")]
        public DateTime? MinDate { get; set; }

        [HtmlAttributeName("asp-max-date")]
        public DateTime? MaxDate { get; set; }

        [HtmlAttributeName("asp-view-mode")]
        public ViewMode ViewModeType { get; set; } = ViewMode.Days;

        [HtmlAttributeName("asp-id")]
        public string Id { get; set; }

        [HtmlAttributeName("asp-select-mode")]
        public SelectMode SelectModeType { get; set; } = SelectMode.Date;

        [HtmlAttributeName("asp-is-side-by-side")]
        public bool IsSideBySide { get; set; } = false;

        public string ViewModeValue
        {
            get
            {
                switch (ViewModeType)
                {
                    case ViewMode.Months:
                        return "months";
                    case ViewMode.Years:
                        return "years";
                    default:
                        return "days";
                }
            }
        }

        public string SelectModeValue
        {
            get
            {
                switch (SelectModeType)
                {
                    case SelectMode.Month:
                        return "month";
                    case SelectMode.Year:
                        return "year";
                    case SelectMode.DateTime:
                        return "date_time";
                    default:
                        return "day";
                }
            }
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator Generator { get; }

        public DateTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "form-group date-picker");

            if (!String.IsNullOrWhiteSpace(Id))
                output.Attributes.Add("id", Id + "-div");

            SetAttributes(output);

            var required = "";
            TagBuilder label = new TagBuilder("label");
            label.AddCssClass("control-label");
            label.Attributes.Add("for", For.Name);

            if (For.Metadata.IsRequired)
                required = " <span class='required'>*</span>";

            label.InnerHtml.Append(For.Metadata?.DisplayName);
            label.InnerHtml.AppendHtml(required);

            TagBuilder dateDiv = GetDateDiveTag();
            TagBuilder validation = GetValidationTag();

            output.PreContent.AppendHtml(label);
            output.PostContent.AppendHtml(dateDiv);
            output.PostContent.AppendHtml(validation);
        }

        private void SetAttributes(TagHelperOutput output)
        {
            if (MinDate.HasValue)
                output.Attributes.Add("data-min-date", MinDate.Value.ToString("dd-MM-yyyy"));

            if (MaxDate.HasValue)
                output.Attributes.Add("data-max-date", MaxDate.Value.ToString("dd-MM-yyyy"));

            if (MonthOnly)
                output.Attributes.Add("data-month-only", MonthOnly);

            output.Attributes.Add("data-select-mode", SelectModeValue);

            output.Attributes.Add("data-view-mode", ViewModeValue);

            if (IsSideBySide)
                output.Attributes.Add("data-side-by-side", IsSideBySide);

        }

        private TagBuilder GetDateDiveTag()
        {
            TagBuilder dateDiv = new TagBuilder("div");
            dateDiv.AddCssClass("input-group");
            dateDiv.TagRenderMode = TagRenderMode.Normal;

            var input = GetInputTag();
            var icon = GetIconTag();
            var switchButton = GetSwitchButton();

            dateDiv.InnerHtml.AppendHtml(input);
            dateDiv.InnerHtml.AppendHtml(icon);

            if (AllowSwitchToHijri)
                dateDiv.InnerHtml.AppendHtml(switchButton);

            return dateDiv;
        }

        private TagBuilder GetIconTag()
        {
            TagBuilder icon = new TagBuilder("div");
            icon.TagRenderMode = TagRenderMode.Normal;
            icon.AddCssClass("input-group-addon");

            TagBuilder span = new TagBuilder("span");
            span.AddCssClass("fa fa-calendar");
            span.TagRenderMode = TagRenderMode.SelfClosing;

            icon.InnerHtml.AppendHtml(span);

            return icon;
        }

        private TagBuilder GetSwitchButton()
        {
            TagBuilder div = new TagBuilder("div");
            div.TagRenderMode = TagRenderMode.Normal;
            div.AddCssClass("input-group-btn");

            TagBuilder button = new TagBuilder("button");
            button.AddCssClass("btn green");
            button.Attributes.Add("type", "button");
            if (OtherFor != null)
                button.Attributes.Add("onclick", $"switchDate(this, '#{OtherFor.Name}')");
            else
                button.Attributes.Add("onclick", $"switchDate(this)");
            button.TagRenderMode = TagRenderMode.Normal;

            TagBuilder span = new TagBuilder("span");
            span.AddCssClass("fa fa-refresh");
            span.TagRenderMode = TagRenderMode.SelfClosing;

            button.InnerHtml.AppendHtml(span);

            div.InnerHtml.AppendHtml(button);

            return div;
        }

        private TagBuilder GetInputTag()
        {

            var format = "{0:dd-MM-yyyy}";

            if (SelectModeType == SelectMode.DateTime)
                format = "{0:dd-MM-yyyy HH:mm tt}";

            return Generator.GenerateTextBox(ViewContext, For.ModelExplorer, For.Name, For.ModelExplorer.Model, format, new
            {
                @class = "form-control",
                @id = !String.IsNullOrWhiteSpace(Id) ? Id : For.Name,
                autocomplete = "off"
            });

        }

        private TagBuilder GetValidationTag()
        {
            return Generator.GenerateValidationMessage(ViewContext, For.ModelExplorer, For.Name, null, null, new
            {
                @class = "text-danger"
            });
        }
    }
}
