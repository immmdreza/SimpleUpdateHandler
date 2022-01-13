using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Text.RegularExpressions;
using SimpleUpdateHandler.Helpers;

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
        public static async Task<MatchContext<T>> IfMatched<T>(this SimpleContext<T> simpleContext,
                                                               Func<T, string> getText,
                                                               string pattern,
                                                               Func<SimpleContext<T>, Task> func,
                                                               RegexOptions? regexOptions = default)
        {
            var match = MatchContext<T>.Check(simpleContext, getText, pattern, regexOptions);

            if (match)
            {
                await func(simpleContext);
            }

            return match;
        }

        /// <summary>
        /// Do something when a regex matched.
        /// </summary>
        public static async Task<MatchContext<Message>> IfMatched(this SimpleContext<Message> simpleContext,
                                                                  string pattern,
                                                                  Func<SimpleContext<Message>, Task> func,
                                                                  RegexOptions? regexOptions = default)
            => await simpleContext.IfMatched(pattern, func, regexOptions);

        /// <summary>
        /// Do something when a regex matched.
        /// </summary>
        public static async Task<MatchContext<CallbackQuery>> IfMatched(this SimpleContext<CallbackQuery> simpleContext,
                                                                        string pattern,
                                                                        Func<SimpleContext<CallbackQuery>, Task> func,
                                                                        RegexOptions? regexOptions = default)
            => await simpleContext.IfMatched(pattern, func, regexOptions);

        /// <summary>
        /// Do something when a regex not matched.
        /// </summary>
        public static async Task Else<T>(this MatchContext<T> matchContext,
                                         Func<SimpleContext<T>, Task> func)
        {
            if (!matchContext.IsMatched)
            {
                await func(matchContext.SimpleContext);
            }
        }

        /// <summary>
        /// Do something when a regex not matched but something else matched.
        /// </summary>
        public static async Task<MatchContext<T>> ElseIf<T>(this MatchContext<T> matchContext,
                                                            Func<T, string?> getText,
                                                            string pattern,
                                                            Func<SimpleContext<T>, Task> func,
                                                            RegexOptions? regexOptions = default)
        {
            if (!matchContext.IsMatched)
            {
                var match = MatchContext<T>.Check(matchContext.SimpleContext, getText, pattern, regexOptions);

                if (match)
                {
                    await func(matchContext.SimpleContext);
                }

                return match;
            }

            return matchContext;
        }


        /// <summary>
        /// Do something when a regex not matched but something else matched.
        /// </summary>
        public static async Task<MatchContext<CallbackQuery>> ElseIf(this MatchContext<CallbackQuery> matchContext,
                                                                     Func<CallbackQuery, string?> getText,
                                                                     string pattern,
                                                                     Func<SimpleContext<CallbackQuery>, Task> func,
                                                                     RegexOptions? regexOptions = default)
            => await matchContext.ElseIf(getText, pattern, func, regexOptions);

        /// <summary>
        /// Do something when a regex not matched but something else matched.
        /// </summary>
        public static async Task<MatchContext<Message>> ElseIf(this MatchContext<Message> matchContext,
                                                                     Func<Message, string?> getText,
                                                                     string pattern,
                                                                     Func<SimpleContext<Message>, Task> func,
                                                                     RegexOptions? regexOptions = default)
            => await matchContext.ElseIf(getText, pattern, func, regexOptions);
    }
}
