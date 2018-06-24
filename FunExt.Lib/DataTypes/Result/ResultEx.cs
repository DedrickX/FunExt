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
    public sealed partial class ResultEx<TResult> : Result<TResult, Exception>
    {
        private ResultEx(bool isSuccess, TResult result, Exception error)
            : base(isSuccess, result, error)
        {
        }
    }
}