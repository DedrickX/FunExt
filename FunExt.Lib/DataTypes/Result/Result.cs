using System;
using System.Collections;
using System.Collections.Generic;

using static FunExt.F;


namespace FunExt
{

    /// <summary>
    /// Union Type representing result of TResult with possible error of TError
    /// </summary>
    /// <remarks>
    /// Instance can be in two states:
    ///    - Success
    ///    - Failure
    /// </remarks>
    public struct Result<T> :
        IEnumerable<T>,
        IEquatable<Result<T>>
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
            IsSuccess ? ifSuccess(Value) :
            ifFailure(Error);


        /// <summary>
        /// Applying inner value to provided function (functor map)
        /// </summary>
        public Result<R> Map<R>(Func<T, R> f) =>
            IsSuccess ? Success(f(Value)) :
            Failure(Error);


        /// <summary>
        /// Applying inner value to provided function (monadic bind)
        /// </summary>
        public Result<R> Bind<R>(Func<T, Result<R>> f) =>
            IsSuccess ? f(Value) :
            Failure(Error);


        public override string ToString() =>
            Match(
                ifSuccess: x => $"Result - Success({x.ToString()})",
                ifFailure: x => $"Result - Failure({x.Message})");


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
            Equals((Result<T>) obj);


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