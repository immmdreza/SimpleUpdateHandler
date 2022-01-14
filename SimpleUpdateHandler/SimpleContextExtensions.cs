using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Text.RegularExpressions;
using SimpleUpdateHandler.Helpers;

namespace SimpleUpdateHandler
{
    public static class SimpleCallbackQueryContextExtensions
    {
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
        /// Edits a message
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public static async Task<Message?> Edit(this SimpleContext<CallbackQuery> simpleContext,
                                                string text,
                                                ParseMode? parseMode = default,
                                                IEnumerable<MessageEntity>? messageEntities = default,
                                                bool? disableWebpagePreview = default,
                                                InlineKeyboardMarkup? inlineKeyboardMarkup = default,
                                                CancellationToken cancellationToken = default)
        {
            if (simpleContext.Update.InlineMessageId != null)
            {
                await simpleContext.Client.EditMessageTextAsync(simpleContext.Update.InlineMessageId,
                                                                text,
                                                                parseMode,
                                                                messageEntities,
                                                                disableWebpagePreview,
                                                                inlineKeyboardMarkup,
                                                                cancellationToken);
                return null;
            }
            else if (simpleContext.Update.Message != null)
            {
                return await simpleContext.Client.EditMessageTextAsync(simpleContext.Update.Message.Chat.Id,
                                                                simpleContext.Update.Message.MessageId,
                                                                text,
                                                                parseMode,
                                                                messageEntities,
                                                                disableWebpagePreview,
                                                                inlineKeyboardMarkup,
                                                                cancellationToken);
            }

            throw new InvalidOperationException("InlineMessageId and Message are both null!");
        }

        /// <summary>
        /// Edits live location of a message
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public static async Task<Message?> Edit(this SimpleContext<CallbackQuery> simpleContext,
                                                double latitude,
                                                double longitude,
                                                float? horizontalAccuracy = default,
                                                int? heading = default,
                                                int? proximityAlertRadius = default,
                                                InlineKeyboardMarkup? inlineKeyboardMarkup = default,
                                                CancellationToken cancellationToken = default)
        {
            if (simpleContext.Update.InlineMessageId != null)
            {
                await simpleContext.Client.EditMessageLiveLocationAsync(simpleContext.Update.InlineMessageId,
                                                                        latitude,
                                                                        longitude,
                                                                        horizontalAccuracy,
                                                                        heading,
                                                                        proximityAlertRadius,
                                                                        inlineKeyboardMarkup,
                                                                        cancellationToken);
                return null;
            }
            else if (simpleContext.Update.Message != null)
            {
                return await simpleContext.Client.EditMessageLiveLocationAsync(simpleContext.Update.Message.Chat.Id,
                                                                               simpleContext.Update.Message.MessageId,
                                                                               latitude,
                                                                               longitude,
                                                                               horizontalAccuracy,
                                                                               heading,
                                                                               proximityAlertRadius,
                                                                               inlineKeyboardMarkup,
                                                                               cancellationToken);
            }

            throw new InvalidOperationException("InlineMessageId and Message are both null!");
        }

        /// <summary>
        /// Edits media of a message
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public static async Task<Message?> Edit(this SimpleContext<CallbackQuery> simpleContext,
                                                string text,
                                                InputMediaBase inputMediaBase,
                                                InlineKeyboardMarkup? inlineKeyboardMarkup = default,
                                                CancellationToken cancellationToken = default)
        {
            if (simpleContext.Update.InlineMessageId != null)
            {
                await simpleContext.Client.EditMessageMediaAsync(simpleContext.Update.InlineMessageId,
                                                                inputMediaBase,
                                                                inlineKeyboardMarkup,
                                                                cancellationToken);
                return null;
            }
            else if (simpleContext.Update.Message != null)
            {
                return await simpleContext.Client.EditMessageMediaAsync(simpleContext.Update.Message.Chat.Id,
                                                                simpleContext.Update.Message.MessageId,
                                                                inputMediaBase,
                                                                inlineKeyboardMarkup,
                                                                cancellationToken);
            }

            throw new InvalidOperationException("InlineMessageId and Message are both null!");
        }

        /// <summary>
        /// Edits caption of a message
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public static async Task<Message?> Edit(this SimpleContext<CallbackQuery> simpleContext,
                                                string caption,
                                                ParseMode? parseMode = default,
                                                IEnumerable<MessageEntity>? messageEntities = default,
                                                InlineKeyboardMarkup? inlineKeyboardMarkup = default,
                                                CancellationToken cancellationToken = default)
        {
            if (simpleContext.Update.InlineMessageId != null)
            {
                await simpleContext.Client.EditMessageCaptionAsync(simpleContext.Update.InlineMessageId,
                                                                caption,
                                                                parseMode,
                                                                messageEntities,
                                                                inlineKeyboardMarkup,
                                                                cancellationToken);
                return null;
            }
            else if (simpleContext.Update.Message != null)
            {
                return await simpleContext.Client.EditMessageCaptionAsync(simpleContext.Update.Message.Chat.Id,
                                                                simpleContext.Update.Message.MessageId,
                                                                caption,
                                                                parseMode,
                                                                messageEntities,
                                                                inlineKeyboardMarkup,
                                                                cancellationToken);
            }

            throw new InvalidOperationException("InlineMessageId and Message are both null!");
        }

        /// <summary>
        /// Edits reply markup of a message
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public static async Task<Message?> Edit(this SimpleContext<CallbackQuery> simpleContext,
                                                InlineKeyboardMarkup? inlineKeyboardMarkup = default,
                                                CancellationToken cancellationToken = default)
        {
            if (simpleContext.Update.InlineMessageId != null)
            {
                await simpleContext.Client.EditMessageReplyMarkupAsync(simpleContext.Update.InlineMessageId,
                                                                       inlineKeyboardMarkup,
                                                                       cancellationToken);
                return null;
            }
            else if (simpleContext.Update.Message != null)
            {
                return await simpleContext.Client.EditMessageReplyMarkupAsync(simpleContext.Update.Message.Chat.Id,
                                                                              simpleContext.Update.Message.MessageId,
                                                                              inlineKeyboardMarkup,
                                                                              cancellationToken);
            }

            throw new InvalidOperationException("InlineMessageId and Message are both null!");
        }
    }

    public static class SimpleMessageContextExtensions
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
                replyToMessageId: sendAsReply ? simpleContext.Update.MessageId : 0,
                allowSendingWithoutReply: true,
                replyMarkup: replyMarkup);

        /// <summary>
        /// Message is sent to private chat.
        /// </summary>
        public static bool IsPrivate(this SimpleContext<Message> simpleContext)
            => simpleContext.Update.Chat.Type == ChatType.Private;

        /// <summary>
        /// Message is sent to group chat.
        /// </summary>
        public static bool IsGroup(this SimpleContext<Message> simpleContext)
            => simpleContext.Update.Chat.Type == ChatType.Supergroup ||
                simpleContext.Update.Chat.Type == ChatType.Group;
    }

    public static class SimpleContextConditionalExtensions
    {
        #region Sync

        #region Abstracts
        /// <summary>
        /// Do something when a regex matched.
        /// </summary>
        public static MatchContext<T> If<T>(this SimpleContext<T> simpleContext,
                                            Func<T, string?> getText,
                                            string pattern,
                                            Action<SimpleContext<T>> func,
                                            RegexOptions? regexOptions = default)
        {
            var match = MatchContext<T>.Check(simpleContext, getText, pattern, regexOptions);

            if (match)
            {
                func(simpleContext);
            }

            return match;
        }

        /// <summary>
        /// Do something when a regex matched.
        /// </summary>
        public static MatchContext<T> If<T>(this SimpleContext<T> simpleContext,
                                            Func<SimpleContext<T>, bool> predict,
                                            Action<SimpleContext<T>> func)
        {
            if (predict(simpleContext))
            {
                func(simpleContext);
                return new MatchContext<T>(simpleContext, true);
            }

            return default;
        }

        /// <summary>
        /// Do something when a regex not matched.
        /// </summary>
        public static void Else<T>(this MatchContext<T> matchContext,
                                   Action<SimpleContext<T>> func)
        {
            var match = matchContext;
            if (!match)
            {
                func(match.SimpleContext);
            }
        }

        /// <summary>
        /// Do something when a regex not matched but something else matched.
        /// </summary>
        public static MatchContext<T> ElseIf<T>(this MatchContext<T> matchContext,
                                                Func<T, string?> getText,
                                                string pattern,
                                                Action<SimpleContext<T>> func,
                                                RegexOptions? regexOptions = default)
        {
            if (!matchContext)
            {
                var match = MatchContext<T>.Check(matchContext.SimpleContext, getText, pattern, regexOptions);

                if (match)
                {
                    func(matchContext.SimpleContext);
                }

                return match;
            }

            return matchContext;
        }

        /// <summary>
        /// Do something when a regex not matched but something else matched.
        /// </summary>
        public static MatchContext<T> ElseIf<T>(this MatchContext<T> matchContext,
                                                Func<SimpleContext<T>, bool> predict,
                                                Action<SimpleContext<T>> func)
        {
            if (!matchContext)
            {
                if (predict(matchContext.SimpleContext))
                {
                    func(matchContext.SimpleContext);
                    return new(matchContext.SimpleContext, true);
                }

                return default;
            }

            return matchContext;
        }

        #endregion

        #endregion

        #region Async

        #region Abstracts
        /// <summary>
        /// Do something when a regex matched.
        /// </summary>
        public static async Task<MatchContext<T>> If<T>(this SimpleContext<T> simpleContext,
                                                               Func<T, string?> getText,
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
        public static async Task<MatchContext<T>> If<T>(this SimpleContext<T> simpleContext,
                                                               Func<SimpleContext<T>, bool> predict,
                                                               Func<SimpleContext<T>, Task> func)
        {
            if (predict(simpleContext))
            {
                await func(simpleContext);
                return new MatchContext<T>(simpleContext, true);
            }

            return default;
        }

        /// <summary>
        /// Do something when a regex not matched.
        /// </summary>
        public static async Task Else<T>(this Task<MatchContext<T>> matchContext,
                                         Func<SimpleContext<T>, Task> func)
        {
            var match = await matchContext;
            if (!match)
            {
                await func(match.SimpleContext);
            }
        }

        /// <summary>
        /// Do something when a regex not matched but something else matched.
        /// </summary>
        public static async Task<MatchContext<T>> ElseIf<T>(this Task<MatchContext<T>> matchContext,
                                                            Func<T, string?> getText,
                                                            string pattern,
                                                            Func<SimpleContext<T>, Task> func,
                                                            RegexOptions? regexOptions = default)
        {
            var prevMatch = await matchContext;
            if (!prevMatch)
            {
                var match = MatchContext<T>.Check(prevMatch.SimpleContext, getText, pattern, regexOptions);

                if (match)
                {
                    await func(prevMatch.SimpleContext);
                }

                return match;
            }

            return prevMatch;
        }

        /// <summary>
        /// Do something when a regex not matched but something else matched.
        /// </summary>
        public static async Task<MatchContext<T>> ElseIf<T>(this Task<MatchContext<T>> matchContext,
                                                            Func<SimpleContext<T>, bool> predict,
                                                            Func<SimpleContext<T>, Task> func)
        {
            var prevMatch = await matchContext;
            if (!prevMatch)
            {
                if (predict(prevMatch.SimpleContext))
                {
                    await func(prevMatch.SimpleContext);
                    return new(prevMatch.SimpleContext, true);
                }

                return default;
            }

            return prevMatch;
        }

        #endregion

        #region Sealed

        /// <summary>
        /// Do something when a regex matched.
        /// </summary>
        public static async Task<MatchContext<Message>> If(this SimpleContext<Message> simpleContext,
                                                                  string pattern,
                                                                  Func<SimpleContext<Message>, Task> func,
                                                                  RegexOptions? regexOptions = default)
            => await simpleContext.If(x => x.Text, pattern, func, regexOptions);

        /// <summary>
        /// Do something when a regex matched.
        /// </summary>
        public static async Task<MatchContext<CallbackQuery>> If(this SimpleContext<CallbackQuery> simpleContext,
                                                                        string pattern,
                                                                        Func<SimpleContext<CallbackQuery>, Task> func,
                                                                        RegexOptions? regexOptions = default)
            => await simpleContext.If(x => x.Data, pattern, func, regexOptions);

        /// <summary>
        /// Do something when a regex not matched but something else matched.
        /// </summary>
        public static async Task<MatchContext<CallbackQuery>> ElseIf(this Task<MatchContext<CallbackQuery>> matchContext,
                                                                     string pattern,
                                                                     Func<SimpleContext<CallbackQuery>, Task> func,
                                                                     RegexOptions? regexOptions = default)
            => await matchContext.ElseIf(x => x.Data, pattern, func, regexOptions);

        /// <summary>
        /// Do something when a regex not matched but something else matched.
        /// </summary>
        public static async Task<MatchContext<Message>> ElseIf(this Task<MatchContext<Message>> matchContext,
                                                               string pattern,
                                                               Func<SimpleContext<Message>, Task> func,
                                                               RegexOptions? regexOptions = default)
            => await matchContext.ElseIf(x => x.Text, pattern, func, regexOptions);

        #endregion

        #endregion
    }
}
