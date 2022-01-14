using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SimpleUpdateHandler
{
    public class SimpleCarryHandler<T> : ISimpleCarryHandler
        where T : class
    {
        public SimpleCarryHandler(
            ISimpleHandler simpleHandler,
            UpdateType excpectedUpdateType)
        {
            CancellationTokenSource = new CancellationTokenSource();
            SimpleHandler = simpleHandler;
            ExcpectedUpdateType = excpectedUpdateType;
        }

        public T? RealUpdate => MatchedUpdate?.GetInnerUpdate<T>();

        public ISimpleHandler SimpleHandler { get; }

        public Update? MatchedUpdate { get; private set; }

        public UpdateType ExcpectedUpdateType { get; }

        public CancellationTokenSource CancellationTokenSource { get; }

        public void SetUpdate(Update update) => MatchedUpdate = update;
    }
}
