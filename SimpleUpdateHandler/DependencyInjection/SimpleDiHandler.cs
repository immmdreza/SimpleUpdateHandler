using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleUpdateHandler.DependencyInjection
{
    public abstract class SimpleDiHandler<T>: ISimpleDiHandler
    {
        private T? _cachedValue;

        protected abstract Task HandleUpdate(SimpleContext<T> ctx);

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
