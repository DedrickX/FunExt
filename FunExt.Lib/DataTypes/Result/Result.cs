using System;

namespace FunExt.Lib
{
    /// <summary>
    /// Union Type representing result of TResult with possible error of TError
    /// </summary>
    /// <remarks>
    /// Instance can be in two states:
    ///    - Success <see cref="Lib.Common.Some{T}"/>
    ///    - Error <see cref="Lib.Common.Error{T}"/>
    /// </remarks>
    public partial class Result<TResult, TError>
    {
        protected Result(bool isSuccess, TResult result, TError error)
        {
            IsSuccess = isSuccess;
            _result = result;
            _error = error;
        }

        public bool IsSuccess { get; }

        public bool IsError { get => !IsSuccess; }

        protected readonly TError _error;

        protected readonly TResult _result;

        public TResult GetSuccess() =>
            IsSuccess ? _result :
            throw new InvalidOperationException("Result is Error!");

        public TError GetError() =>
            IsError ? _error :
            throw new InvalidOperationException("Result is Success!");
    }
}