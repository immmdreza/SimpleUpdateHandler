using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleUpdateHandler.DependencyInjection
{
    public static class DiExtensions
    {
        /// <summary>
        /// Use this to add <see cref="SimpleDiUpdateProcessor"/> to service collection
        /// </summary>
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

            var serviceProvoder = 
            serviceDescriptors.AddSingleton(
                x => new SimpleDiUpdateProcessor(
                    telegramBotClient?? x.GetRequiredService<ITelegramBotClient>(),
                    serviceDescriptors, x,
                    handlerContainers));
        }

        /// <summary>
        /// Use this to add <see cref="SimpleDiUpdateProcessor"/> to service collection
        /// </summary>
        public static SimpleDiUpdateProcessor AddUpdateProcessor(
            this IServiceProvider serviceProvider,
            IServiceCollection serviceDescriptors,
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

            var processor = new SimpleDiUpdateProcessor(
                    telegramBotClient ??
                        serviceProvider.GetRequiredService<ITelegramBotClient>(),
                    serviceDescriptors,
                    serviceProvider,
                    handlerContainers);

            serviceDescriptors.AddSingleton(processor);
            return processor;
        }
    }
}
