using Telegram.Bot.Types;

namespace SimpleUpdateHandler.CustomFilters
{
    public class FromUsersFilter<T> : SimpleFilter<T>
    {
        public FromUsersFilter(Func<T, long?> userSelector, params long[] users)
            : base(x =>
            {
                if (x is null) return false;

                var user = userSelector(x);
                if (user is null) return false;

                return users.Any(x => x == user);
            })
        {
        }
    }

    public class FromUsersMessageFilter : FromUsersFilter<Message>
    {
        public FromUsersMessageFilter(params long[] users)
            : base(x=> x.From?.Id, users)
        {
        }
    }

    public class FromUsersCallbackQueryFilter : FromUsersFilter<CallbackQuery>
    {
        public FromUsersCallbackQueryFilter(params long[] users)
            : base(x => x.From?.Id, users)
        {
        }
    }
}
