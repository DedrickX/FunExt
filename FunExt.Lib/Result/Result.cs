using System;

namespace FunExt.Lib
{
    /// <summary>
    /// Union Type representing result of TResult with possible error of TError
    /// </summary>
    /// <remarks>
    /// Instance can be in two states:
    ///    - <see cref="Lib.Common.Some{T}"/>
    ///    - <see cref="Lib.Common.Error{T}"/>
    /// </remarks>
    public class Result<TResult, TError>
    {
        protected Result(bool isSuccess, TResult result, TError error)
        {
            IsSuccess = isSuccess;
            _result = result;
            _error = error;
        }

        public bool IsSuccess { get; }

        public bool IsError { get => !IsSuccess; }

        private readonly TError _error;

        private readonly TResult _result;

        public TResult GetSuccess() =>
            IsSuccess ? _result :
            throw new InvalidOperationException("Result is Error!");

        public TError GetError() =>
            IsError ? _error :
            throw new InvalidOperationException("Result is Success!");

        public static implicit operator Result<TResult, TError>(Common.Some<TResult> some) =>
            new Result<TResult, TError>(true, some.Value, default(TError));

        public static implicit operator Result<TResult, TError>(Common.Error<TError> error) =>
             new Result<TResult, TError>(false, default(TResult), error.ErrorValue);
    }
}