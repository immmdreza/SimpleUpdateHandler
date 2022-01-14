using Telegram.Bot.Types;

namespace SimpleUpdateHandler.CustomFilters
{
    public class MessageRepliedFilter : SimpleFilter<Message>
    {
        public MessageRepliedFilter()
            : base(x => x?.ReplyToMessage is not null)
        {
        }
    }
}
