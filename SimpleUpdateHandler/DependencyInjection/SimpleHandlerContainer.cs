using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types;

namespace SimpleUpdateHandler.DependencyInjection
{
    public class SimpleHandlerContainer<TUpdate, THandler>: IHandlerContainer
        where THandler : SimpleDiHandler<TUpdate>
    {
        public SimpleHandlerContainer(
            SimpleFilter<TUpdate>? filter = default,
            int priority = 0)
        {
            Filter = filter;
            HandlerType = typeof(THandler);
            Priority = priority;
        }

        public SimpleFilter<TUpdate>? Filter { get; }

        public Type HandlerType { get; }

        public int Priority { get; }

        public object? InnerUpdate { get; private set; }

        [MemberNotNullWhen(true, "InnerUpdate")]
        public bool CheckFilter(Update update)
        { 
            InnerUpdate = update.GetInnerUpdate();
            return Filter?.TheyShellPass((TUpdate)InnerUpdate) ?? false;
        }
    }
}
