using System;
using System.Collections;
using System.Collections.Generic;

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
        /// <param name="ifError">Function executed when value is Failure.</param>
        public R Match<R>(Func<T, R> ifSuccess, Func<Exception, R> ifError) =>
            IsSuccess ? ifSuccess(Value) :
            ifError(Error);


        #region Enumerable support


        public IEnumerator<T> GetEnumerator()
        {
            if (IsSuccess) yield return Value;
            yield break;
        }


        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();


        #endregion


        #region Operators, Equality and Comparison


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


        #endregion

    }
}