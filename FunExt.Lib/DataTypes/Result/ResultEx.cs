using System;

namespace FunExt.Lib
{
    /// <summary>
    /// Union Type representing result of TResult with possible Exception
    /// </summary>
    /// <remarks>
    /// Instance can be in two states:
    ///    - Success <see cref="Lib.Common.Some{T}"/>
    ///    - Error <see cref="Exception"/>
    /// </remarks>
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