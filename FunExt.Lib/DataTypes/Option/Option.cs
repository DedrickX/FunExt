using System;
using System.Collections;
using System.Collections.Generic;

using FunExt.Lib.DataTypes;

namespace FunExt.Lib
{

    /// <summary>
    /// Union Type representing possible value of T
    /// </summary>
    /// <remarks>
    /// Instance can be in two states:
    ///    - Some of T
    ///    - None
    /// </remarks>
    public struct Option<T> :
        IEnumerable<T>
    {

        internal Option(bool isSome, T value)
        {
            IsSome = isSome;
            Value = value;
        }


        /// <summary>
        /// Determines whether instance has "Some" value of its type T
        /// </summary>
        public bool IsSome { get; }


        /// <summary>
        /// Determines whether instance has "None" value
        /// </summary>
        public bool IsNone { get => !IsSome; }


        internal T Value { get; }


        /// <summary>
        /// Performing function depending on Union value.
        /// </summary>
        /// <param name="ifSome">Function executed when value is <see cref="Common.Some{T}"/></param>
        /// <param name="ifNone">Function executed when value is <see cref="Common.Option_None"/></param>
        public R Match<R>(Func<T, R> ifSome, Func<OptionNone, R> ifNone) =>
            IsSome ? ifSome(Value) :
            ifNone(F.None);


        // ---------- Enumerables ----------


        public IEnumerator<T> GetEnumerator()
        {
            if (IsSome) yield return Value;
            yield break;
        }


        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();


        // ---------- Equality and operators ----------


        public override bool Equals(object obj) =>
            Equals((Option<T>) obj);


        public bool Equals(Option<T> other) =>
            (IsNone && other.IsNone) ||
            ((IsSome && other.IsSome) && (Value.Equals(other.Value)));


        public override int GetHashCode() =>
            IsSome ? Value.GetHashCode() :
            0;


        public static bool operator ==(Option<T> @this, Option<T> other) =>
            @this.Equals(other);


        public static bool operator !=(Option<T> @this, Option<T> other) =>
            !(@this == other);


        public static implicit operator Option<T>(T value) =>
            typeof(T).IsValueType ? new Option<T>(true, value) :
            new Option<T>(value != null, value);


        public static implicit operator Option<T>(OptionNone _) =>
            new Option<T>(false, default(T));

    }

}