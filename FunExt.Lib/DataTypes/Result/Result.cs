using System;
using System.Collections;
using System.Collections.Generic;

using FunExt.Lib.DataTypes;

namespace FunExt.Lib
{
    /// <summary>
    /// Union Type representing result of TResult with possible error of TError
    /// </summary>
    /// <remarks>
    /// Instance can be in two states:
    ///    - Success
    ///    - Failure
    /// </remarks>
    public partial class Result<T> :
        IEnumerable<T>,
        IEquatable<Result<T>>,
        IMonadicLR<Exception, T>
    {

        internal Result(bool isSuccess, T value, Exception ex)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = ex;
        }


        public bool IsSuccess { get; }


        public bool IsFailure { get => !IsSuccess; }


        internal T Value { get; }


        internal Exception Error { get; }


        /// <summary>
        /// Performing function depending on Union value.
        /// </summary>
        /// <param name="ifSuccess">Function executed when value is Success.</param>
        /// <param name="ifFailure">Function executed when value is Failure.</param>
        public R Match<R>(Func<T, R> ifSuccess, Func<Exception, R> ifFailure) =>
            Cata(ifRight: x => ifSuccess(x),
                 ifLeft: x => ifFailure(x));


        /// <summary>
        /// Applying function into inner value of Option. If Option is None, nothing happens.
        /// Functor map.
        public Result<R> Map<R>(Func<T, R> f) =>
            (Result<R>)MonadicOperations.Map(this, f);


        /// <summary>
        /// Applying function into inner value of Option. If Option is None, nothing happens.
        /// Monadic bind.
        public Result<R> Bind<R>(Func<T, Result<R>> f) =>
            (Result<R>)MonadicOperations.Bind(this, f);


        // ---------- IMonadicLR ----------


        public R Cata<R>(Func<T, R> ifRight, Func<Exception, R> ifLeft) =>
            IsSuccess ? ifRight(Value) :
            ifLeft(Error);

        public IMonadicLR<Exception, B> Lift<B>(B rightValue) =>
            F.Success(rightValue);

        public IMonadicLR<Exception, B> GetLeftValue<B>(Exception leftValue) =>
            (Result<B>)F.Failure(leftValue);


        // ---------- Enumerables ----------


        public IEnumerator<T> GetEnumerator()
        {
            if (IsSuccess) yield return Value;
            yield break;
        }


        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();


        // ---------- Equality and operators ----------


        public override bool Equals(object obj) =>
            Equals(obj as Result<T>);


        public bool Equals(Result<T> other) =>
            ((IsFailure && other.IsFailure) && (Error.Equals(other.Error))) ||
            ((IsSuccess && other.IsSuccess) && (Value.Equals(other.Value)));


        public override int GetHashCode() =>
            IsSuccess ? Value.GetHashCode() :
            Error.GetHashCode();


        public static bool operator ==(Result<T> @this, Result<T> other) =>
            @this.Equals(other);


        public static bool operator !=(Result<T> @this, Result<T> other) =>
            !(@this == other);


        public static implicit operator Result<T>(Exception ex) =>
            new Result<T>(false, default(T), ex);

    }
}