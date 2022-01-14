using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SimpleUpdateHandler
{
    public interface ISimpleCarryHandler
    {
        public ISimpleHandler SimpleHandler { get; }

        public UpdateType ExcpectedUpdateType { get; }

        /// <summary>
        /// The very first matched update for this <see cref="ISimpleHandler"/> and <see cref="UpdateType"/>
        /// </summary>
        public Update? MatchedUpdate { get; }

        /// <summary>
        /// Token to cancell waitting...
        /// </summary>
        public CancellationTokenSource CancellationTokenSource { get; }

        public void SetUpdate(Update update);
    }
}
