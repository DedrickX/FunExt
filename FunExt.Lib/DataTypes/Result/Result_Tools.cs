using System;

namespace FunExt.Lib
{
    public partial class Result<TResult, TError> :
        IEquatable<Result<TResult, TError>>
    {
        public override bool Equals(object obj) =>
            Equals(obj as Result<TResult, TError>);

        public bool Equals(Result<TResult, TError> other) =>
            ((IsError && other.IsError) && (_error.Equals(other._error))) ||
            ((IsSuccess && other.IsSuccess) && (_result.Equals(other._result)));

        public override int GetHashCode() =>
            IsSuccess ? _result.GetHashCode() :
            _error.GetHashCode();

        public static bool operator ==(Result<TResult, TError> @this, Result<TResult, TError> other) =>
            @this.Equals(other);

        public static bool operator !=(Result<TResult, TError> @this, Result<TResult, TError> other) =>
            !(@this == other);

        public static implicit operator Result<TResult, TError>(Common.Some<TResult> some) =>
            new Result<TResult, TError>(true, some.Value, default(TError));

        public static implicit operator Result<TResult, TError>(Common.Error<TError> error) =>
             new Result<TResult, TError>(false, default(TResult), error.ErrorValue);
    }
}