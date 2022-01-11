using Telegram.Bot.Types;

namespace SimpleUpdateHandler.DependencyInjection
{
    public class SimpleHandlerContainer<T>: IHandlerContainer
    {
        private T? _cachedValue;

        public SimpleHandlerContainer(
            Type handlerType,
            SimpleFilter<T>? filter = default,
            int priority = 0)
        {
            Filter = filter;
            HandlerType = handlerType;
            Priority = priority;
        }

        public SimpleFilter<T>? Filter { get; }

        public Type HandlerType { get; }

        public int Priority { get; }

        public bool CheckFilter(Update update)
            => Filter?.TheyShellPass(GetInnerUpdate(update)) ?? false;

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
