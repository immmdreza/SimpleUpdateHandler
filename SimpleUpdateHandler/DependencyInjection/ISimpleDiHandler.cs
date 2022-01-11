using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleUpdateHandler.DependencyInjection
{
    public interface ISimpleDiHandler
    {
        public Task Handle(ITelegramBotClient telegramBotClient, Update update);
    }
}
