using System;
using System.Collections.Generic;
using System.Text;

namespace FunExt.Lib
{
    public static partial class F
    {
        /// <summary>
        /// Pipe value to function
        /// </summary>
        public static R Pipe<T, R>(this T x, Func<T, R> f) =>
            f(x);

        /// <summary>
        /// Pipe IDisposable value into function. Then the value is disposed.
        /// </summary>
        /// <param name="disposable">Disposable instance to work with.</param>
        /// <param name="f">Function applied on IDisposable instance.</param>
        /// <returns></returns>
        public static R PipeUsing<TDisp, R>(this TDisp disposable, Func<TDisp, R> f)
            where TDisp: IDisposable
        {
            using (disposable) return f(disposable);
        }
    }
}
