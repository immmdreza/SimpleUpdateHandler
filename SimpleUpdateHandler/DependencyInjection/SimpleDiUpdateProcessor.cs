﻿using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleUpdateHandler.DependencyInjection
{
    public class SimpleDiUpdateProcessor
    {
        private readonly List<IHandlerContainer> _handlersTypes;
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Creates a new instance of <see cref="SimpleDiUpdateProcessor"/>, base processor for handlers.
        /// </summary>
        /// <param name="telegramBotClient">Main client resposeable for updates.</param>
        /// <param name="handlersTypes">Add your <see cref="IHandlerContainer"/>s here or <see cref="AddSimpleHandler(IHandlerContainer)"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        public SimpleDiUpdateProcessor(
            ITelegramBotClient telegramBotClient,
            IServiceProvider serviceProvider,
            List<IHandlerContainer>? handlersTypes = default)
        {
            _serviceProvider = serviceProvider;
            _handlersTypes = handlersTypes ?? new List<IHandlerContainer>();
            _telegramBotClient = telegramBotClient ??
                throw new ArgumentNullException(nameof(telegramBotClient));
        }

        /// <summary>
        /// Adds an <see cref="IHandlerContainer"/> to processor.
        /// </summary>
        /// <param name="handlerContainer">Create this using <see cref="SimpleHandlerContainer{T}"/></param>
        public void AddSimpleHandler(IHandlerContainer handlerContainer)
            => _handlersTypes.Add(handlerContainer);

        /// <summary>
        /// Handles the update.
        /// </summary>
        public async Task ProcessSimpleHandlerAsync(Update update)
        {
            var appliedHandlers = _handlersTypes
                .Where(x => x.CheckFilter(update))
                .OrderBy(x => x.Priority);

            foreach (var handler in appliedHandlers)
            {
                using var scope = _serviceProvider.CreateScope();
                var toHandle = (ISimpleDiHandler)scope.ServiceProvider.GetRequiredService(handler.HandlerType);
                await toHandle.Handle(_telegramBotClient, update);
            }
        }
    }
}