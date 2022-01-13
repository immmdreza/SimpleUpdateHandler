using System.Text.RegularExpressions;

namespace SimpleUpdateHandler.Helpers
{
    /// <summary>
    /// Provide information of a regex match
    /// </summary>
    public readonly struct MatchContext<TUpdate>
    {
        public MatchContext(SimpleContext<TUpdate> simpleContext,
                            bool isMatched = default,
                            MatchCollection? matchCollection = default)
        {
            SimpleContext = simpleContext;
            IsMatched = isMatched;
            MatchCollection = matchCollection;
        }

        public bool IsMatched { get; } = default;

        public MatchCollection? MatchCollection { get; } = default;

        public SimpleContext<TUpdate> SimpleContext { get; }

        public static MatchContext<T> Check<T>(
            SimpleContext<T> simpleContext,
            Func<T, string?> getText,
            string pattern,
            RegexOptions? regexOptions = default)
        {
            var text = getText(simpleContext.Update);

            if (string.IsNullOrEmpty(text)) return new(simpleContext);

            var matches = Regex.Matches(
                text, pattern, regexOptions?? RegexOptions.None, TimeSpan.FromSeconds(3));

            if (matches.Count > 0)
            {
                return new(simpleContext, true, matches);
            }

            return new(simpleContext);
        }

        public static implicit operator bool(MatchContext<TUpdate> matchContext)
            => matchContext.IsMatched;
    }
}
