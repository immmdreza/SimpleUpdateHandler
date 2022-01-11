namespace SimpleUpdateHandler
{
    /// <summary>
    /// A simple basic filter
    /// </summary>
    /// <typeparam name="T">Object type that filter is gonna apply to</typeparam>
    public class SimpleFilter<T>
    {
        private readonly Func<T?, bool> _filter;

        /// <summary>
        /// Creates a simple basic filter
        /// </summary>
        /// <param name="filter">A function to check the input and return a boolean</param>
        public SimpleFilter(Func<T?, bool> filter) => _filter = filter;

        public virtual bool TheyShellPass(T? input)
            => input is not null && _filter(input);

        public static implicit operator Func<T?, bool>(SimpleFilter<T> filter)
            => filter.TheyShellPass;

        public static implicit operator SimpleFilter<T>(Func<T?, bool> filter) => new(filter);

        public static SimpleFilter<T> operator &(SimpleFilter<T> a, SimpleFilter<T> b)
            => new SimpleAndFilter<T>(a, b);

        public static SimpleFilter<T> operator |(SimpleFilter<T> a, SimpleFilter<T> b)
            => new SimpleOrFilter<T>(a, b);

        public static SimpleFilter<T> operator ~(SimpleFilter<T> a)
            => new SimpleReverseFilter<T>(a);
    }

    /// <summary>
    /// Creates a simple and filter
    /// </summary>
    public class SimpleAndFilter<T> : SimpleFilter<T>
    {
        /// <summary>
        /// Creates a and filter ( Like filter1 and filter2 ), use and operator
        /// </summary>
        public SimpleAndFilter(SimpleFilter<T> filter1, SimpleFilter<T> filter2)
            : base(x => filter1.TheyShellPass(x) && filter2.TheyShellPass(x))
        { }
    }

    /// <summary>
    /// Creates a simple or filter
    /// </summary>
    public class SimpleOrFilter<T> : SimpleFilter<T>
    {
        /// <summary>
        /// Creates an or filter ( Like filter1 or filter2 ), use or operator
        /// </summary>
        public SimpleOrFilter(SimpleFilter<T> filter1, SimpleFilter<T> filter2)
            : base(x => filter1.TheyShellPass(x) || filter2.TheyShellPass(x))
        { }
    }

    /// <summary>
    /// Creates a simple reverse filter
    /// </summary>
    public class SimpleReverseFilter<T> : SimpleFilter<T>
    {
        /// <summary>
        /// Creates a reverse filter ( Like not filter ), use not operator
        /// </summary>
        public SimpleReverseFilter(SimpleFilter<T> filter1)
            : base(x => !filter1.TheyShellPass(x))
        { }
    }
}
