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

        /// <summary>
        /// Register a <see cref="SimpleHandlerContainer{Message, THandler}"/>
        /// for <see cref="Message"/> updates
        /// </summary>
        /// <typeparam name="THandler">The update handler</typeparam>
        public static SimpleDiUpdateProcessor RegisterMessage<THandler>(
            this SimpleDiUpdateProcessor diUpdateProcessor,
            SimpleFilter<Message>? simpleFilter = default,
            int priority = default)
            where THandler : SimpleDiHandler<Message>
        {
            var container = new SimpleHandlerContainer<Message, THandler>(simpleFilter, priority);
            diUpdateProcessor.RegisterHandler(container);
            diUpdateProcessor.ServiceDescriptors.AddScoped(container.HandlerType);
            return diUpdateProcessor;
        }

        /// <summary>
        /// Register a <see cref="SimpleHandlerContainer{CallbackQuery, THandler}"/>
        /// for <see cref="CallbackQuery"/> updates
        /// </summary>
        /// <typeparam name="THandler">The update handler</typeparam>
        public static SimpleDiUpdateProcessor RegisterCallbackQuery<THandler>(
            this SimpleDiUpdateProcessor diUpdateProcessor,
            SimpleFilter<CallbackQuery>? simpleFilter = default,
            int priority = default)
            where THandler : SimpleDiHandler<CallbackQuery>
        {
            var container = new SimpleHandlerContainer<CallbackQuery, THandler>(simpleFilter, priority);
            diUpdateProcessor.RegisterHandler(container);
            diUpdateProcessor.ServiceDescriptors.AddScoped(container.HandlerType);
            return diUpdateProcessor;
        }
    }
}
