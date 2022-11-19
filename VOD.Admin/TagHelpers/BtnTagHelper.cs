using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using System;
using System.Drawing;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Policy;
using Microsoft.AspNetCore.Razor;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace VOD.Admin.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("btn")]
    public class BtnTagHelper : AnchorTagHelper
    {
        #region properties
        public string Icon { get; set; } = string.Empty;
        const string btnPrimary = "btn-primary";
        const string btnDanger = "btn-danger";
        const string btnDefault = "btn-default";
        const string btnInfo = "btn-info";
        const string btnSucess = "btn-success";
        const string btnWarning = "btn-warning";
        // Google's Material Icons provider name
        const string iconProvider = "material-icons";
        #endregion

        #region This constructor is needed for AnchorTagHelper inheritance
        public BtnTagHelper(IHtmlGenerator generator) : base(generator)
        {

        }
        #endregion

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));
            // Change <btn> tag to <a> tag
            output.TagName = "a";

            #region Bootstrap Button
            
            var aspPageAttribute = context.AllAttributes.SingleOrDefault(p =>p.Name.ToLower().Equals("asp-page"));
            var classAttribute = context.AllAttributes.SingleOrDefault(p =>p.Name.ToLower().Equals("class"));
            var buttonStyle = btnDefault;
            if (aspPageAttribute != null) { 
                var pageValue = aspPageAttribute.Value.ToString().ToLower();
                buttonStyle =
                pageValue.Equals("create") ? btnPrimary :
                pageValue.Equals("delete") ? btnDanger :
                pageValue.Equals("edit") ? btnSucess :
                pageValue.Equals("index") ? btnPrimary :
                pageValue.Equals("details") ? btnInfo :
                pageValue.Equals("/index") ? btnWarning :
                pageValue.Equals("error") ? btnDanger :
                btnDefault;
            }
            var bootstrapClasses = $"btn-sm {buttonStyle}";

            if (classAttribute != null)
            {
                var css = classAttribute.Value.ToString();
                if (!css.ToLower().Contains("btn-"))
                {
                    output.Attributes.Remove(classAttribute);
                    classAttribute = new TagHelperAttribute("class", $"{css} {bootstrapClasses}");
                    output.Attributes.Add(classAttribute);
                }
            }
            else
            {
                output.Attributes.Add("class", bootstrapClasses);
            }
            #endregion

            #region Icon
            if (!Icon.Equals(string.Empty))
            {
                var childContext = output.GetChildContentAsync().Result;
                var content = childContext.GetContent().Trim();
                if (content.Length > 0) content = $"&nbsp{content}";
                output.Content.SetHtmlContent($"<i class='{iconProvider}'style='display: inline-flex; vertical-align: top; line-height: inherit;font-size: medium;'>{Icon}</i> " +
                    $"<span style='font-size:"+ $"medium;'>{content}</span>");
            }
            #endregion
            #region Style Attribute
            var style = context.AllAttributes.SingleOrDefault(s =>s.Name.ToLower().Equals("style"));
            var styleValue = style == null ? "" : style.Value;
            var newStyle = new TagHelperAttribute("style", $"{styleValue} display:inline-flex;border-radius:0px;text-decoration: none;");
            if (style != null) output.Attributes.Remove(style);
            output.Attributes.Add(newStyle);
           
            #endregion
            base.Process(context, output);
            
        }
    }
}
