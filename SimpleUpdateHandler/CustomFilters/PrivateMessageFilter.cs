using Telegram.Bot.Types;

namespace SimpleUpdateHandler.CustomFilters
{
    public class PrivateMessageFilter : SimpleFilter<Message>
    {
        public PrivateMessageFilter()
            : base(c => c?.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Private)
        {
        }
    }
}
