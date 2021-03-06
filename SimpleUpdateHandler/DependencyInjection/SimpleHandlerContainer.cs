using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SimpleUpdateHandler.DependencyInjection
{
    public class SimpleHandlerContainer<TUpdate, THandler>: IHandlerContainer
        where THandler : SimpleDiHandler<TUpdate>
    {
        public SimpleHandlerContainer(
            UpdateType updateType,
            SimpleFilter<TUpdate>? filter = default,
            int priority = 0)
        {
            UpdateType = updateType;
            Filter = filter;
            HandlerType = typeof(THandler);
            Priority = priority;
        }

        public SimpleFilter<TUpdate>? Filter { get; private set; }

        public Type HandlerType { get; }

        public int Priority { get; }

        public object? InnerUpdate { get; private set; }

        public UpdateType UpdateType { get; }

        /// <summary>
        /// Set or Reset filter.
        /// </summary>
        public void SetFilter(SimpleFilter<TUpdate> filter) => Filter = filter;

        /// <summary>
        /// Set or Reset filter.
        /// </summary>
        public void SetFilter(Func<TUpdate?, bool> filter)
            => Filter = new SimpleFilter<TUpdate>(filter);

        [MemberNotNullWhen(true, "InnerUpdate")]
        public bool CheckFilter(Update update)
        { 
            InnerUpdate = update.GetInnerUpdate();
            return Filter?.TheyShellPass((TUpdate)InnerUpdate) ?? true;
        }
    }
}
