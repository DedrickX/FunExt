using System;


namespace FunExt.Lib
{
    public static class CommonExtensions
    {

        /// <summary>
        /// Pipes value to function.
        /// </summary>
        public static R Pipe<T, R>(this T x, Func<T, R> f) =>
            f(x);


        /// <summary>
        /// Pipes IDisposable value into function. Then the value is disposed.
        /// </summary>
        /// <param name="disposable">IDisposable instance to work with.</param>
        /// <param name="f">Function applied on IDisposable instance.</param>
        /// <returns></returns>
        public static R PipeUsing<TDisp, R>(this TDisp disposable, Func<TDisp, R> f)
            where TDisp: IDisposable
        {
            using (disposable) return f(disposable);
        }

    }
}
