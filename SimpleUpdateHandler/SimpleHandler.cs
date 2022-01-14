using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleUpdateHandler
{
    public class SimpleHandler<T> : ISimpleHandler where T : class
    {
        private readonly SimpleFilter<T>? _filter;

        public SimpleHandler(SimpleFilter<T>? filter, int priority = 0)
        {
            _filter = filter;
            Priority = priority;
        }

        public int Priority { get; }

        public bool CheckFilter(Update update)
            => _filter?.TheyShellPass(update.GetInnerUpdate<T>()) ?? false;

        public Task Handle(ITelegramBotClient telegramBotClient, Update update)
        {
            throw new NotImplementedException();
        }
    }
}
