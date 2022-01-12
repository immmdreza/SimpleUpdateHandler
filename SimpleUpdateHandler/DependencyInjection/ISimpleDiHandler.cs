using Telegram.Bot;

namespace SimpleUpdateHandler.DependencyInjection
{
    public interface ISimpleDiHandler
    {
        public Task Handle(ITelegramBotClient telegramBotClient, object innerUpdate);
    }
}
