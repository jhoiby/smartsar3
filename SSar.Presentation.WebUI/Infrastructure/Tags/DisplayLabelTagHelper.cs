using HtmlTags;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SSar.Presentation.WebUI.Infrastructure.Tags
{
    [HtmlTargetElement("display-label-tag", Attributes = nameof(For), TagStructure = TagStructure.WithoutEndTag)]
    public class DisplayLabelTagHelper : HtmlTagTagHelper
    {
        protected override string Category { get; } = nameof(TagConventions.DisplayLabels);
    }
}