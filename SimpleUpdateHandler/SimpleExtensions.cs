using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SimpleUpdateHandler
{
    public static class SimpleExtensions
    {
        /// <summary>
        /// Wraps an update with a <see cref="SimpleContext{T}"/>
        /// </summary>
        public static SimpleContext<T> WrapIt<T>(this T update,
                                                 ITelegramBotClient telegramBot)
            where T : class => new(telegramBot, update);

        /// <summary>
        /// Wraps an update with a <see cref="SimpleContext{T}"/>
        /// </summary>
        public static async Task<SimpleContext<T>> WrapIt<T>(this Task<T> update,
                                                             ITelegramBotClient telegramBot)
            where T : class => new SimpleContext<T>(telegramBot, await update);

        /// <summary>
        /// Wraps an update with a <see cref="SimpleContext{T}"/>
        /// </summary>
        public static SimpleContext<T> WrapIt<T>(this T update,
                                                 SimpleContext<T> ctx)
            where T : class => new(ctx.Client, update);

        /// <summary>
        /// Wraps an update with a <see cref="SimpleContext{T}"/>
        /// </summary>
        public static async Task<SimpleContext<T>> WrapIt<T>(this Task<T> update,
                                                             SimpleContext<T> ctx)
            where T : class => new SimpleContext<T>(ctx.Client, await update);

        public static object GetInnerUpdate(this Update update)
        {
            if (update.Type == UpdateType.Unknown)
                throw new ArgumentException($"Can't resolve Update of Type {update.Type}");

            return typeof(Update).GetProperty(update.Type.ToString())?.GetValue(update, null)
                ?? throw new InvalidOperationException($"Inner update is null for {update.Type}");
        }

        public static T GetInnerUpdate<T>(this Update update)
        {
            if (update.Type == UpdateType.Unknown)
                throw new ArgumentException($"Can't resolve Update of Type {update.Type}");

            return (T)(typeof(Update).GetProperty(update.Type.ToString())?.GetValue(update, null)
                ?? throw new InvalidOperationException($"Inner update is null for {update.Type}"));
        }

        public static UpdateType? GetUpdateType<T>()
        {
            if (Enum.TryParse(typeof(T).Name, out UpdateType result))
            {
                return result;
            }

            return null;
        }
    }
}
