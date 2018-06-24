using System;
using System.Collections;
using System.Collections.Generic;

namespace FunExt.Lib
{
    public partial class Result<TResult, TError> : IEnumerable<TResult>
    {
        /// <summary>
        /// Performing function depending on Union value.
        /// </summary>
        /// <param name="ifSuccess">Function executed when value is <see cref="Common.Some{T}"/></param>
        /// <param name="ifError">Function executed when value is <see cref="Common.Error{T}"/></param>
        public R Match<R>(Func<TResult, R> ifSuccess, Func<TError, R> ifError) =>
            IsSuccess ? ifSuccess(_result) :
            ifError(_error);

        public IEnumerator<TResult> GetEnumerator()
        {
            if (IsSuccess) yield return _result;
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}