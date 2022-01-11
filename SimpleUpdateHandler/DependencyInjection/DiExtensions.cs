using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace SimpleUpdateHandler.DependencyInjection
{
    public static class DiExtensions
    {
        /// <summary>
        /// Use this to add <see cref="SimpleDiUpdateProcessor"/> to service collection
        /// </summary>
        /// <param name="serviceDescriptors"></param>
        /// <param name="handlerContainers"></param>
        public static void AddUpdateProcessor(
            this IServiceCollection serviceDescriptors,
            ITelegramBotClient? telegramBotClient = default,
            params IHandlerContainer[]? handlerContainers)
        {
            if (handlerContainers != null)
            {
                foreach (var handlerContainer in handlerContainers)
                {
                    serviceDescriptors.AddScoped(handlerContainer.HandlerType);
                }
            }

            serviceDescriptors.AddSingleton(
                x => new SimpleDiUpdateProcessor(
                    telegramBotClient?? x.GetRequiredService<ITelegramBotClient>(),
                    x.GetRequiredService<IServiceProvider>(),
                    handlerContainers));
        }
    }
}
