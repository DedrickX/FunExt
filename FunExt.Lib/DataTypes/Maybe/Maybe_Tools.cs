using System;
using FunExt.Lib.Common;

namespace FunExt.Lib
{
    public sealed partial class Maybe<T> :
        IEquatable<Maybe<T>>
    {
        public override bool Equals(object obj) =>
            Equals(obj as Maybe<T>);

        public bool Equals(Maybe<T> other) =>
            (IsNone && other.IsNone) ||
            ((IsSome && other.IsSome) && (_value.Equals(other._value)));

        public override int GetHashCode() =>
            IsSome ? _value.GetHashCode() :
            0;

        public static bool operator ==(Maybe<T> @this, Maybe<T> other) =>
            @this.Equals(other);

        public static bool operator !=(Maybe<T> @this, Maybe<T> other) =>
            !(@this == other);

        public static implicit operator Maybe<T>(Common.None _) =>
            new Maybe<T>(false, default(T));

        public static implicit operator Maybe<T>(Common.Some<T> some) =>
            new Maybe<T>(true, some.Value);
    }
}