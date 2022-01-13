using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace SimpleUpdateHandler
{
    public static class SimpleContextExtensions
    {
        /// <summary>
        /// Quickest possible way to response to a message
        /// Shortcut for <c>SendTextMessageAsync</c>
        /// </summary>
        /// <param name="text">Text to response</param>
        /// <param name="sendAsReply">To send it as a replied message if possible.</param>
        /// <returns></returns>
        public static async Task<Message> Response(this SimpleContext<Message> simpleContext, string text,
                                                   bool sendAsReply = true, ParseMode? parseMode = default,
                                                   IEnumerable<MessageEntity>? messageEntities = default,
                                                   bool? disableWebpagePreview = default,
                                                   bool? disableNotification = default,
                                                   IReplyMarkup? replyMarkup = default)
            => await simpleContext.Client.SendTextMessageAsync(
                simpleContext.Update.Chat.Id,
                text,
                parseMode, messageEntities, disableWebpagePreview, disableNotification,
                replyToMessageId: sendAsReply? simpleContext.Update.MessageId: 0,
                allowSendingWithoutReply: true,
                replyMarkup: replyMarkup);

        /// <summary>
        /// Answers a <see cref="CallbackQuery"/>.
        /// Shortcut for <c>AnswerCallbackQueryAsync</c>
        /// </summary>
        public static async Task Answer(this SimpleContext<CallbackQuery> simpleContext, string? text = default,
                                        bool? showAlert = default, string? url = default, int? cacheTime = default,
                                        CancellationToken cancellationToken = default)
            => await simpleContext.Client.AnswerCallbackQueryAsync(simpleContext.Update.Id, text, showAlert, url,
                                                                   cacheTime, cancellationToken);
    }
}
