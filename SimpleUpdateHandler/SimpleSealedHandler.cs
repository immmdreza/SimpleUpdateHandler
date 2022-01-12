using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleUpdateHandler
{
    /// <summary>
    /// Simple sealed handler for simple cases. Use <see cref="SimpleAbstractHandler{T}"/> if you need more space
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class SimpleSealedHandler<T>: ISimpleHandler where T : class
    {
        private T? _cachedValue;
        private readonly SimpleFilter<T>? _filter;
        private readonly Func<SimpleContext<T>, Task> _callback;

        public int Priority { get; }

        /// <summary>
        /// Create an handler for any update type such as
        /// <see cref="Message"/>, <see cref="CallbackQuery"/> and others.
        /// </summary>
        /// <param name="callback">A callback fuction to be executed if <paramref name="filter"/> passed.</param>
        /// <param name="priority">Process priority amoung other passed handlers for an update.</param>
        /// <param name="filter">A filter to check if this handler should be executed for an update or not.</param>
        public SimpleSealedHandler(
            Func<SimpleContext<T>, Task> callback,
            int priority = 0,
            SimpleFilter<T>? filter = default)
        {
            Priority = priority;
            _filter = filter;
            _callback = callback;
        }

        public async Task Handle(ITelegramBotClient telegramBotClient, Update update)
            => await _callback(new SimpleContext<T>(
                telegramBotClient, GetInnerUpdate(update)));

        private T GetInnerUpdate(Update update)
        {
            if (_cachedValue == null)
            {
                _cachedValue = update.GetInnerUpdate<T>();
                return _cachedValue;
            }
            else
            {
                return _cachedValue;
            }
        }

        public bool CheckFilter(Update update)
            =>_filter?.TheyShellPass(GetInnerUpdate(update)) ?? false;
    }
}