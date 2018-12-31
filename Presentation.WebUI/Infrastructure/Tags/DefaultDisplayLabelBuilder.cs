using System.Linq;
using System.Text.RegularExpressions;
using HtmlTags;
using HtmlTags.Conventions;
using HtmlTags.Conventions.Elements;

// This code is from Jimmy Bogard's Contoso University with Razor Example
// See: https://jimmybogard.com/migrating-contoso-university-example-to-razor-pages/ 

namespace SSar.Presentation.WebUI.Infrastructure.Tags
{
    public class DefaultDisplayLabelBuilder : IElementBuilder
    {
        public bool Matches(ElementRequest subject)
        {
            return true;
        }

        public HtmlTag Build(ElementRequest request)
        {
            return new HtmlTag("").NoTag().Text(BreakUpCamelCase(request.Accessor.Name));
        }

        public static string BreakUpCamelCase(string fieldName)
        {
            var patterns = new[]
            {
                "([a-z])([A-Z])",
                "([0-9])([a-zA-Z])",
                "([a-zA-Z])([0-9])"
            };
            var output = patterns.Aggregate(fieldName,
                (current, pattern) => Regex.Replace(current, pattern, "$1 $2", RegexOptions.IgnorePatternWhitespace));
            return output.Replace('_', ' ');
        }
    }
}
