using System;

namespace FunExt.Lib
{
    public sealed class ResultEx<TResult> : Result<TResult, Exception>
    {
        private ResultEx(bool isSuccess, TResult result, Exception error)
            : base(isSuccess, result, error)
        {
        }

        public static implicit operator ResultEx<TResult>(Common.Some<TResult> some) =>
            new ResultEx<TResult>(true, some.Value, null);

        public static implicit operator ResultEx<TResult>(Common.Error<Exception> error) =>
             new ResultEx<TResult>(false, default(TResult), error.ErrorValue);

        public static implicit operator ResultEx<TResult>(Exception error) =>
             new ResultEx<TResult>(false, default(TResult), error);
    }
}