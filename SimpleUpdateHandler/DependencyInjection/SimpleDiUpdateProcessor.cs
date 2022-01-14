using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleUpdateHandler.DependencyInjection
{
    public class SimpleDiUpdateProcessor
    {
        private readonly List<IHandlerContainer> _handlersTypes;
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IServiceProvider _serviceProvider;

        // Handler in handler support.
        private readonly ConcurrentDictionary<string, ISimpleCarryHandler> _carryHandlers;

        /// <summary>
        /// Creates a new instance of <see cref="SimpleDiUpdateProcessor"/>, base processor for handlers.
        /// </summary>
        /// <param name="telegramBotClient">Main client resposeable for updates.</param>
        /// <param name="handlersTypes">Add your <see cref="IHandlerContainer"/>s here or <see cref="AddSimpleHandler(IHandlerContainer)"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        public SimpleDiUpdateProcessor(
            ITelegramBotClient telegramBotClient,
            IServiceProvider serviceProvider,
            IEnumerable<IHandlerContainer>? handlersTypes = default)
        {
            _handlersTypes = handlersTypes?.ToList() ?? new List<IHandlerContainer>();
            _telegramBotClient = telegramBotClient ??
                throw new ArgumentNullException(nameof(telegramBotClient));
            _serviceProvider = serviceProvider;

            _carryHandlers = new();
        }

        /// <summary>
        /// Register a carry handler ( waits for an specified update to come, inside another handler )
        /// </summary>
        /// <param name="uniqeName">A unique name - Eg: based on user id and handler name.</param>
        /// <param name="simpleFilter">Required filter to choose you're update.</param>
        /// <param name="timeout">Timeout in milliseconds</param>
        public async Task<SimpleContext<T>?> RegisterCarryHandler<T>(
            string uniqeName,
            SimpleFilter<T> simpleFilter,
            int timeout = 30000) where T : class
        {
            var updateType = SimpleExtensions.GetUpdateType<T>();
            if (updateType == null)
                throw new InvalidCastException($"{typeof(T)} is not an UpdateType");
            
            var carry = new SimpleCarryHandler<T>(
                new SimpleHandler<T>(simpleFilter), updateType.Value);

            if (_carryHandlers.TryAdd(uniqeName, carry))
            {
                try
                {
                    await Task.Delay(timeout, carry.CancellationTokenSource.Token);
                }
                catch(TaskCanceledException)
                {
                    if (carry.RealUpdate == null)
                        return null;

                    return new SimpleContext<T>(_telegramBotClient, carry.RealUpdate);
                }
            }

            return null;
        }

        /// <summary>
        /// Handles the update.
        /// </summary>
        public void Handle(Update update)
        {
            _ = ProcessSimpleHandlerAsync(update);
        }

        private async Task ProcessSimpleHandlerAsync(Update update)
        {
            // First, check for carry handler
            if (_carryHandlers.Any())
            {
                var suitableHander = _carryHandlers
                    .Where(x => x.Value.ExcpectedUpdateType == update.Type)
                    .Where(x => x.Value.SimpleHandler.CheckFilter(update));

                foreach (var handler in suitableHander)
                {
                    handler.Value.SetUpdate(update);
                    handler.Value.CancellationTokenSource.Cancel();
                    handler.Value.CancellationTokenSource.Dispose();

                    if (!_carryHandlers.TryRemove(handler.Key, out _))
                    {
                        // I hope this never happens.
                        _carryHandlers.Clear();
                    }

                    return;
                }
            }

            var appliedHandlers = _handlersTypes
                .Where(x=> x.UpdateType == update.Type)
                .Where(x => x.CheckFilter(update))
                .OrderBy(x => x.Priority);

            foreach (var handler in appliedHandlers)
            {
                using var scope = _serviceProvider.CreateAsyncScope();
                var toHandle = (ISimpleDiHandler)scope.ServiceProvider.GetRequiredService(handler.HandlerType);
                await toHandle.Handle(_telegramBotClient, handler.InnerUpdate!);
            }
        }
    }
}
