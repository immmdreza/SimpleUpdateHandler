using Telegram.Bot;

namespace SimpleUpdateHandler.DependencyInjection
{
    public abstract class SimpleDiHandler<T>: ISimpleDiHandler
    {
        protected abstract Task HandleUpdate(SimpleContext<T> ctx);

        public async Task Handle(ITelegramBotClient telegramBotClient, object update)
            => await HandleUpdate(new SimpleContext<T>(
                telegramBotClient, (T)update));
    }
}
