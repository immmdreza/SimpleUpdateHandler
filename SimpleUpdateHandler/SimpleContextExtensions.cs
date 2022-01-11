using Telegram.Bot.Types;
using Telegram.Bot;

namespace SimpleUpdateHandler
{
    public static class SimpleContextExtensions
    {
        /// <summary>
        /// Quickest possible way to response to a message
        /// </summary>
        /// <param name="text">Text to response</param>
        /// <param name="sendAsReply">To send it as a replied message if possible.</param>
        /// <returns></returns>
        public static async Task<Message> QuickResponse(
            this SimpleContext<Message> simpleContext,
            string text,
            bool sendAsReply = true) => await simpleContext.Client.SendTextMessageAsync(
                simpleContext.Update.Chat.Id,
                text,
                replyToMessageId: sendAsReply? simpleContext.Update.MessageId: 0,
                allowSendingWithoutReply: true);
    }
}
