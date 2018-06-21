using System;

namespace FunExt.Lib
{
    public sealed class Maybe<T>
    {
        private Maybe()
        {
            IsSome = false;
            _value = default(T);
        }

        private Maybe(T value)
        {
            IsSome = true;
            _value = value;
        }

        public bool IsSome { get; }

        public bool IsNone { get => !IsSome; }

        private readonly T _value;

        internal T GetValue() =>
            IsSome ? _value :
            throw new InvalidOperationException("Value is None!");

        public static implicit operator Maybe<T>(Maybe.None _) =>
            new Maybe<T>();

        public static implicit operator Maybe<T>(Maybe.Some<T> some) =>
            new Maybe<T>(some.Value);
    }
}