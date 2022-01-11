using Telegram.Bot;

namespace SimpleUpdateHandler
{
    /// <summary>
    /// An <see cref="TelegramBotClient"/> <see cref="Telegram.Bot.Types.Update"/> context
    /// Which has required information to handle an incoming update.
    /// </summary>
    public class SimpleContext<T>
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly T _update;

        /// <summary>
        /// The responseable client for update.
        /// </summary>
        public ITelegramBotClient Client => _telegramBotClient;

        /// <summary>
        /// The update.
        /// </summary>
        public T Update => _update;

        public SimpleContext(ITelegramBotClient telegramBotClient, T update)
        {
            _telegramBotClient = telegramBotClient ?? throw new ArgumentNullException(nameof(telegramBotClient));
            _update = update;
        }
    }
}
