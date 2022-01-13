using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SimpleUpdateHandler.DependencyInjection
{
    public class UpdateProcessorContext
    {
        public UpdateProcessorContext(ITelegramBotClient? telegramBotClient)
        {
            HandlerContainers = new();
            TelegramBotClient = telegramBotClient;
        }

        public List<IHandlerContainer> HandlerContainers { get; }

        ITelegramBotClient? TelegramBotClient { get; }

        /// <summary>
        /// Build this.
        /// </summary>
        public SimpleDiUpdateProcessor Build(IServiceProvider serviceProvider)
        {
            return new(
                TelegramBotClient ?? serviceProvider.GetRequiredService<ITelegramBotClient>(),
                serviceProvider,
                HandlerContainers);
        }

        /// <summary>
        /// Register a <see cref="SimpleHandlerContainer{Message, THandler}"/>
        /// for <see cref="Message"/> updates
        /// </summary>
        /// <typeparam name="THandler">The update handler</typeparam>
        public UpdateProcessorContext RegisterMessage<THandler>(
            SimpleFilter<Message>? simpleFilter = default,
            int priority = default)
            where THandler : SimpleDiHandler<Message>
        {
            var container = new SimpleHandlerContainer<Message, THandler>(
                UpdateType.Message, simpleFilter, priority);
            HandlerContainers.Add(container);
            return this;
        }

        /// <summary>
        /// Register a <see cref="SimpleHandlerContainer{CallbackQuery, THandler}"/>
        /// for <see cref="CallbackQuery"/> updates
        /// </summary>
        /// <typeparam name="THandler">The update handler</typeparam>
        public UpdateProcessorContext RegisterCallbackQuery<THandler>(
            SimpleFilter<CallbackQuery>? simpleFilter = default,
            int priority = default)
            where THandler : SimpleDiHandler<CallbackQuery>
        {
            var container = new SimpleHandlerContainer<CallbackQuery, THandler>(
                UpdateType.CallbackQuery, simpleFilter, priority);
            HandlerContainers.Add(container);
            return this;
        }
    }
}
