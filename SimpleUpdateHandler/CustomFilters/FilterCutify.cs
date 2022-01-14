using System.Text.RegularExpressions;
using Telegram.Bot.Types;

namespace SimpleUpdateHandler.CustomFilters
{
    /// <summary>
    /// A collection of cutified filters for kids.
    /// </summary>
    public static class FilterCutify
    {
        /// <summary>
        /// The handler will always be triggered on specified update type of <typeparamref name="T"/>
        /// </summary>
        public static SimpleFilter<T> Always<T>() => new((_) => true);

        /// <summary>
        /// The handler will be triggered when <paramref name="func"/> returns true
        /// on specified update type of <typeparamref name="T"/>
        /// </summary>
        public static SimpleFilter<T> When<T>(Func<T?, bool> func) => new(func);

        /// <summary>
        /// The handler will be triggered when <paramref name="func"/> passes
        /// on specified update type of <typeparamref name="T"/>
        /// </summary>
        public static SimpleFilter<T>? When<T>(SimpleFilter<T>? func) => func;

        /// <summary>
        /// The handler will be triggered when a message is a command specified in <paramref name="commands"/>
        /// </summary>
        public static SimpleFilter<Message> OnCommand(params string[] commands)
            => new CommandFilter(commands);

        /// <summary>
        /// The handler will be triggered when a message is a command specified in <paramref name="commands"/>
        /// </summary>
        public static SimpleFilter<Message> OnCommand(char prefix = '/', params string[] commands)
            => new CommandFilter(prefix, commands);

        /// <summary>
        /// The handler will be triggered when a regex matchs its text.
        /// </summary>
        public static SimpleFilter<Message> TextMatchs(
            string pattern, bool catchCaption = false, RegexOptions? regexOptions = default)
                => new MessageTextRegex(pattern, catchCaption, regexOptions);

        /// <summary>
        /// The handler will be triggered when a regex matchs its data.
        /// </summary>
        public static SimpleFilter<CallbackQuery> DataMatches(
            string pattern, RegexOptions? regexOptions = default)
            => new CallbackQueryRegex(pattern, regexOptions);

        /// <summary>
        /// The handler will be triggered when a message sent in private chat
        /// </summary>
        public static SimpleFilter<Message> PM() => new PrivateMessageFilter();

        /// <summary>
        /// A message comes only from specified user(s) is <paramref name="users"/>
        /// </summary>
        public static SimpleFilter<Message> MsgOfUsers(params long[] users)
            => new FromUsersMessageFilter(users);

        /// <summary>
        /// A callback query comes only from specified user(s) is <paramref name="users"/>
        /// </summary>
        public static SimpleFilter<CallbackQuery> CbqOfUsers(params long[] users)
            => new FromUsersCallbackQueryFilter(users);
    }
}
