using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleUpdateHandler
{
    /// <summary>
    /// Base interface for <see cref="SimpleHandler{T}"/> to get raid of generic stuff.
    /// </summary>
    public interface ISimpleHandler
    {
        /// <summary>
        /// Handle the current update.
        /// </summary>
        public Task Handle(ITelegramBotClient telegramBotClient, Update update);

        /// <summary>
        /// Checks if this <see cref="ISimpleHandler"/> should be handled.
        /// </summary>
        public bool CheckFilter(Update update);

        /// <summary>
        /// Process priority of handler.
        /// </summary>
        public int Priority { get; }
    }
}
