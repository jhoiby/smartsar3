using HtmlTags;

// This code is from Jimmy Bogard's Contoso University with Razor Example
// See: https://jimmybogard.com/migrating-contoso-university-example-to-razor-pages/ 

namespace SSar.Presentation.WebUI.Infrastructure.Tags
{
    public static class HtmlTagExtensions
    {
        public static HtmlTag AddPlaceholder(this HtmlTag tag, string placeholder)
        {
            return tag.Attr("placeholder", placeholder);
        }

        public static HtmlTag AddPattern(this HtmlTag tag, string pattern)
        {
            var retVal = tag.Data("pattern", pattern);
            return retVal;
        }

        public static HtmlTag AutoCapitalize(this HtmlTag tag)
        {
            return tag.Data("autocapitalize", "true");
        }
    }
}