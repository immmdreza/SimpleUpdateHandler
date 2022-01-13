using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SimpleUpdateHandler.DependencyInjection
{
    public interface IHandlerContainer
    {
        /// <summary>
        /// Type of inner handler.
        /// </summary>
        public Type HandlerType { get; }

        /// <summary>
        /// Incoming update type for the handler
        /// </summary>
        public UpdateType UpdateType { get; }

        /// <summary>
        /// Checks if this <see cref="IHandlerContainer"/> should be handled.
        /// </summary>
        public bool CheckFilter(Update update);

        /// <summary>
        /// Process priority of handler.
        /// </summary>
        public int Priority { get; }

        /// <summary>
        /// Inner update which is unknown type here.
        /// </summary>
        public object? InnerUpdate { get; }
    }
}
