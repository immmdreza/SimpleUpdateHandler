using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace SimpleUpdateHandler.DependencyInjection
{
    public static class DiExtensions
    {
        /// <summary>
        /// Use this to add <see cref="SimpleDiUpdateProcessor"/> to service collection
        /// </summary>
        public static void AddUpdateProcessor(
            this IServiceCollection serviceDescriptors,
            Action<UpdateProcessorContext> configure,
            ITelegramBotClient? telegramBotClient = default)
        {
            var upc = new UpdateProcessorContext(telegramBotClient);

            configure(upc);

            foreach (var handler in upc.HandlerContainers)
            {
                serviceDescriptors.AddScoped(handler.HandlerType);
            }

            serviceDescriptors.AddSingleton(x => upc.Build(x));
        }
    }
}
