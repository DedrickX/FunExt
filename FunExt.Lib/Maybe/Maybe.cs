using System;

namespace FunExt.Lib
{
    /// <summary>
    /// Union Type representing possible value of T
    /// </summary>
    /// <remarks>
    /// Instance can be in two states:
    ///    - <see cref="Lib.Common.Some{T}"/>
    ///    - <see cref="Lib.Common.None"/>
    /// </remarks>
    public sealed class Maybe<T>
    {
        private Maybe(bool isSome, T value)
        {
            IsSome = isSome;
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

        public static implicit operator Maybe<T>(Common.None _) =>
            new Maybe<T>(false, default(T));

        public static implicit operator Maybe<T>(Common.Some<T> some) =>
            new Maybe<T>(true, some.Value);
    }
}