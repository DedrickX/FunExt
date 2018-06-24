using System;

namespace FunExt.Lib
{
    public sealed partial class ResultEx<TResult>
    {
        public override bool Equals(object obj) =>
            Equals(obj as ResultEx<TResult>);

        public override int GetHashCode() =>
            IsSuccess ? _result.GetHashCode() :
            _error.GetHashCode();

        public static bool operator ==(ResultEx<TResult> @this, ResultEx<TResult> other) =>
            @this.Equals(other);

        public static bool operator !=(ResultEx<TResult> @this, ResultEx<TResult> other) =>
            !(@this == other);

        public static implicit operator ResultEx<TResult>(Common.Some<TResult> some) =>
            new ResultEx<TResult>(true, some.Value, null);

        public static implicit operator ResultEx<TResult>(Common.Error<Exception> error) =>
             new ResultEx<TResult>(false, default(TResult), error.ErrorValue);

        public static implicit operator ResultEx<TResult>(Exception error) =>
             new ResultEx<TResult>(false, default(TResult), error);
    }
}