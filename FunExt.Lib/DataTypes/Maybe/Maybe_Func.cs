using System;
using System.Collections;
using System.Collections.Generic;

namespace FunExt.Lib
{
    public sealed partial class Maybe<T> : IEnumerable<T>
    {
        /// <summary>
        /// Performing function depending on Union value.
        /// </summary>
        /// <param name="ifSome">Function executed when value is <see cref="Common.Some{T}"/></param>
        /// <param name="ifNone">Function executed when value is <see cref="Common.None"/></param>
        public R Match<R>(Func<T, R> ifSome, Func<R> ifNone) =>
            IsSome ? ifSome(_value) :
            ifNone();

        public IEnumerator<T> GetEnumerator()
        {
            if (IsSome) yield return _value;
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}