using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleUpdateHandler
{
    /// <summary>
    /// Abstract handler if you need more space. Use <see cref="SimpleSealedHandler{T}"/> for simple cases.  
    /// </summary>
    /// <typeparam name="T">Update type to handle</typeparam>
    public abstract class SimpleAbstractHandler<T> : ISimpleHandler
    {
        private T? _cachedValue;
        private readonly SimpleFilter<T>? _filter;

        protected SimpleAbstractHandler(SimpleFilter<T>? filter = default, int priority = 0)
            => (_filter, Priority) = (filter, priority);

        protected abstract Task HandleUpdate(SimpleContext<T> ctx);

        public int Priority { get; }

        public bool CheckFilter(Update update)
            => _filter?.TheyShellPass(GetInnerUpdate(update)) ?? false;

        public async Task Handle(ITelegramBotClient telegramBotClient, Update update)
            => await HandleUpdate(new SimpleContext<T>(telegramBotClient, GetInnerUpdate(update)));

        private T GetInnerUpdate(Update update)
        {
            if (_cachedValue == null)
            {
                _cachedValue = (T)(typeof(Update).GetProperties()
                        .Where(x => x.PropertyType is Type type &&
                            !type.IsEnum &&
                            type == typeof(T))
                        .Select(x => x.GetValue(update))
                        .Where(x => x is not null).Single()
                    ?? throw new InvalidOperationException("Update can't be null"));
                return _cachedValue;
            }
            else
            {
                return _cachedValue;
            }
        }
    }
}
