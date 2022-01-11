using Telegram.Bot.Types;

namespace SimpleUpdateHandler.CustomFilters
{
    /// <summary>
    /// A filter on <see cref="Message.Text"/>
    /// </summary>
    public class MessageTextFilter : SimpleFilter<Message>
    {
        /// <summary>
        /// A filter on <see cref="Message.Text"/>
        /// </summary>
        public MessageTextFilter(Func<string, bool> filter)
            : base((x) => x?.Text is not null && filter(x.Text))
        { }
    }
}
