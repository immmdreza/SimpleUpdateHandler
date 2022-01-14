using Microsoft.Extensions.DependencyInjection;
using SimpleUpdateHandler.CustomFilters;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SimpleUpdateHandler.DependencyInjection
{
    public static class DiExtensions
    {
        /// <summary>
        /// Use this to add <see cref="SimpleDiUpdateProcessor"/> to service collection
        /// </summary>
        public static void AddUpdateProcessor(this IServiceCollection serviceDescriptors,
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

        public static async Task<SimpleContext<Message>?> CarryUserResponse(
            this SimpleDiUpdateProcessor processor,
            long userId,
            int timeout = 30000,
            bool privateOnly = false,
            bool replyOnly = false,
            string? regexMatch = default,
            bool catchCaption = false,
            RegexOptions? regexOptions = default)
        {
            var filters = FilterCutify.MsgOfUsers(userId);

            if (privateOnly)
                filters &= FilterCutify.PM();

            if (replyOnly)
                filters &= FilterCutify.Replied();

            if (regexMatch != null)
                filters &= FilterCutify.TextMatchs(regexMatch, catchCaption, regexOptions);

            return await processor.RegisterCarryHandler(
                Guid.NewGuid().ToString(), filters, timeout);
        }
    }
}
