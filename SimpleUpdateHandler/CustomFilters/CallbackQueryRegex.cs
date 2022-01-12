using System.Text.RegularExpressions;
using Telegram.Bot.Types;

namespace SimpleUpdateHandler.CustomFilters
{
    public class CallbackQueryRegex : BasicRegexFilter<CallbackQuery>
    {
        public CallbackQueryRegex(
            string pattern,
            RegexOptions? regexOptions = RegexOptions.None)
                : base(x => x?.Data, pattern, regexOptions)
        {
        }
    }
}
