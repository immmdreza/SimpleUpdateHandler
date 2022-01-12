using System.Text.RegularExpressions;

namespace SimpleUpdateHandler.CustomFilters
{
    public class BasicRegexFilter<T> : SimpleFilter<T>
    {
        public BasicRegexFilter(
            Func<T?, string?> getText,
            string pattern,
            RegexOptions? regexOptions = default)
                : base(x =>
                {
                    var text = getText(x);

                    if (string.IsNullOrEmpty(text)) return false;

                    var matches = Regex.Matches(
                        text, pattern, regexOptions?? RegexOptions.None, TimeSpan.FromSeconds(3));

                    if (matches.Count > 0)
                    {
                        return true;
                    }

                    return false;
                })
        { }
    }
}
