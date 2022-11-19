using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using System;
using System.Drawing;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc.TagHelpers;
namespace VOD.Admin.TagHelpers
{
    
    [HtmlTargetElement("alert")]
    public class AlertTagHelper : TagHelper
    {
        public string AlertType { get; set; } = "success";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            throw new ArgumentNullException(nameof(context));
            if (output == null)
            throw new ArgumentNullException(nameof(output));
            var content = output.GetChildContentAsync().Result.GetContent();
            // Don't render the <alert> Tag Helper
            if (content.Trim().Equals(string.Empty)) return;
            // Create the close button inside the alert
            var close = $"<button type='button' class='close' " +
            $"data-dismiss='alert' aria-label='Close'>" +
            $"<span aria-hidden='true'>&times;</span></button>";
            var html = $"{content}{close}";

            output.TagName = "div";
            output.Attributes.Add("class",
            $"alert alert-{AlertType} alert-dismissible fade show");
            output.Attributes.Add("role", "alert");
            output.Attributes.Add("style",
            "border-radius: 0;margin-bottom: 0;");
            output.Content.SetHtmlContent(html);
            output.Content.AppendHtml("</div>");
            // Create the alert <div> element
            base.Process(context, output);
        }
    }
}
