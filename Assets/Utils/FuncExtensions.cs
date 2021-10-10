using System;

namespace Utils
{
    public static class FuncExtensions
    {
        public static Func<T1, R> AndThen<T1, U, R>(this Func<T1, U> func, Func<U, R> andThen)
            => (x) => andThen(func(x));
    }
}