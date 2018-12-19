using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTemplate.Web.TagHelpers
{
    [HtmlTargetElement("form", Attributes = "asp-ajax")]
    [HtmlTargetElement("button", Attributes = "asp-ajax")]
    [HtmlTargetElement("a", Attributes = "asp-ajax")]
    public class AjaxFormTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-ajax")]
        public bool EnableAjax { get; set; }

        [HtmlAttributeName("asp-ajax-block")]
        public string Block { get; set; }

        [HtmlAttributeName("asp-ajax-update")]
        public string Update { get; set; }

        [HtmlAttributeName("asp-ajax-success-method")]
        public string SuccessMethod { get; set; }

        [HtmlAttributeName("asp-ajax-complete-method")]
        public string CompleteMethod { get; set; }

        [HtmlAttributeName("asp-ajax-replace")]
        public string Replace { get; set; } = "";

        [HtmlAttributeName("asp-ajax-error")]
        public string Error { get; set; }

        [HtmlAttributeName("asp-ajax-modal")]
        public string Modal { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            /*
            | Confirm                | data-ajax-confirm           |
            | HttpMethod             | data-ajax-method            |
            | InsertionMode          | data-ajax-mode              |
            | LoadingElementDuration | data-ajax-loading-duration  |
            | LoadingElementId       | data-ajax-loading           |
            | OnBegin                | data-ajax-begin             |
            | OnComplete             | data-ajax-complete          |
            | OnFailure              | data-ajax-failure           |
            | OnSuccess              | data-ajax-success           |
            | UpdateTargetId         | data-ajax-update            |
            | Url                    | data-ajax-url 
            */

            if (EnableAjax)
            {
                var errorDiv = "";

                if (!String.IsNullOrWhiteSpace(Error))
                    errorDiv = Error;

                output.Attributes.Add("data-ajax", "true");
                output.Attributes.Add("data-ajax-begin", $"block('{Block}')");
                output.Attributes.Add("data-ajax-failure", "");

                string formId = output.Attributes["id"] != null ? "#" + output.Attributes["id"].Value : null;

                if (!String.IsNullOrWhiteSpace(CompleteMethod))
                    output.Attributes.Add("data-ajax-complete", $"onAjaxComplete(xhr, status, '{Block}', '{errorDiv}', '{Replace}', '{formId}');{CompleteMethod};");

                else
                    output.Attributes.Add("data-ajax-complete", $"onAjaxComplete(xhr, status, '{Block}', '{errorDiv}', '{Replace}', '{formId}');");

                if (!String.IsNullOrWhiteSpace(SuccessMethod))
                    output.Attributes.Add("data-ajax-success", $"onAjaxSuccess(xhr, status, '{Modal}');{SuccessMethod};");
                else
                    output.Attributes.Add("data-ajax-success", $"onAjaxSuccess(xhr, status, '{Modal}')");

            }
        }
    }
}
