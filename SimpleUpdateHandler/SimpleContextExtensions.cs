using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Text.RegularExpressions;

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
        /// <summary>
        /// Do something when a regex matched.
        /// </summary>
        public static async Task OnMatch<T>(this SimpleContext<T> simpleContext,
                                            Func<T, string> getText,
                                            string pattern,
                                            Func<SimpleContext<T>, Task> func,
                                            RegexOptions? regexOptions = default)
        {
            if (Regex.IsMatch(
                getText(simpleContext.Update), pattern, regexOptions ?? RegexOptions.None))
            {
                await func(simpleContext);
            }
        }

        /// <summary>
        /// Do something when a regex matched.
        /// </summary>
        public static async Task OnMatch(this SimpleContext<Message> simpleContext,
                                         string pattern,
                                         Func<SimpleContext<Message>, Task> func,
                                         RegexOptions? regexOptions = default)
        {
            if (Regex.IsMatch(
                simpleContext.Update.Text??"", pattern, regexOptions ?? RegexOptions.None))
            {
                await func(simpleContext);
            }
        }

        /// <summary>
        /// Do something when a regex matched.
        /// </summary>
        public static async Task OnMatch(this SimpleContext<CallbackQuery> simpleContext,
                                         string pattern,
                                         Func<SimpleContext<CallbackQuery>, Task> func,
                                         RegexOptions? regexOptions = default)
        {
            if (Regex.IsMatch(
                simpleContext.Update.Data ?? "", pattern, regexOptions ?? RegexOptions.None))
            {
                await func(simpleContext);
            }
        }
    }
}
