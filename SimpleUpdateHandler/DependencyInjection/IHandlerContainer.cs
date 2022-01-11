using Telegram.Bot.Types;

namespace SimpleUpdateHandler.DependencyInjection
{
    public interface IHandlerContainer
    {
        /// <summary>
        /// Type of inner handler.
        /// </summary>
        public Type HandlerType { get; }

        /// <summary>
        /// Checks if this <see cref="IHandlerContainer"/> should be handled.
        /// </summary>
        public bool CheckFilter(Update update);

        /// <summary>
        /// Process priority of handler.
        /// </summary>
        public int Priority { get; }
    }
}
