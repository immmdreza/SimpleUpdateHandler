using System.Text.RegularExpressions;
using Telegram.Bot.Types;

namespace SimpleUpdateHandler.CustomFilters
{
    public class MessageTextRegex : BasicRegexFilter<Message>
    {
        public MessageTextRegex(
            string pattern,
            bool catchCaption = false,
            RegexOptions? regexOptions = RegexOptions.None)
                : base(x =>
                {
                    return x switch
                    {
                        { Text: { } text } => text,
                        { Caption: { } caption } when catchCaption => caption,
                        _ => null
                    };
                }, pattern, regexOptions)
        { }
    }
}
