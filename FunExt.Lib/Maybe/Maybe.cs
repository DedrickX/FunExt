using System;

namespace FunExt.Lib
{
    /// <summary>
    /// Union Type representing possible value of T
    /// </summary>
    /// <remarks>
    /// Instance can be in two possible states:
    ///    - <see cref="Lib.Maybe.Some{T}"/>
    ///    - <see cref="Lib.Maybe.None"/>
    /// </remarks>
    public sealed class Maybe<T>
    {
        /// <summary>
        /// None ctor
        /// </summary>
        private Maybe()
        {
            IsSome = false;
            _value = default(T);
        }

        /// <summary>
        /// Some ctor
        /// </summary>
        private Maybe(T value)
        {
            IsSome = true;
            _value = value;
        }

        /// <summary>
        /// Determines whether instance has "Some" value of its type T
        /// </summary>
        public bool IsSome { get; }

        /// <summary>
        /// Determines whether instance has "None" value
        /// </summary>
        public bool IsNone { get => !IsSome; }

        private readonly T _value;

        public T GetValue() =>
            IsSome ? _value :
            throw new InvalidOperationException("Value is None!");

        public static implicit operator Maybe<T>(Maybe.None _) =>
            new Maybe<T>();

        public static implicit operator Maybe<T>(Maybe.Some<T> some) =>
            new Maybe<T>(some.Value);
    }
}