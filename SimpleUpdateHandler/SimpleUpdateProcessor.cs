using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleUpdateHandler
{
    public class SimpleUpdateProcessor
    {
        private readonly List<ISimpleHandler> _simpleHandlers;
        private readonly ITelegramBotClient _telegramBotClient;

        /// <summary>
        /// Creates a new instance of <see cref="SimpleUpdateProcessor"/>, base processor for handlers.
        /// </summary>
        /// <param name="telegramBotClient">Main client resposeable for updates.</param>
        /// <param name="simpleHandlers">Add your <see cref="ISimpleHandler"/>s here or <see cref="AddSimpleHandler(ISimpleHandler)"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        public SimpleUpdateProcessor(
            ITelegramBotClient telegramBotClient,
            List<ISimpleHandler>? simpleHandlers = default)
        {
            _simpleHandlers = simpleHandlers ?? new List<ISimpleHandler>();
            _telegramBotClient = telegramBotClient ?? throw new ArgumentNullException(nameof(telegramBotClient));
        }

        /// <summary>
        /// Adds an <see cref="ISimpleHandler"/> to processor.
        /// </summary>
        /// <param name="simpleHandler">Create this using <see cref="SimpleHandler{T}"/></param>
        public void AddSimpleHandler(ISimpleHandler simpleHandler)
            => _simpleHandlers.Add(simpleHandler);

        /// <summary>
        /// Handles the update.
        /// </summary>
        public async Task ProcessSimpleHandlerAsync(Update update)
        {
            var appliedHandlers = _simpleHandlers
                .Where(x => x.CheckFilter(update))
                .OrderBy(x=> x.Priority);

            foreach (var handler in appliedHandlers)
                await handler.Handle(_telegramBotClient, update);
        }
    }
}
