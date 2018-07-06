using System;
using System.Collections.Generic;
using System.Text;


namespace FunExt.Lib.DataTypes
{

    public static class MonadicOperations
    {

        /// <summary>
        /// Applying function to inner value of Functor
        /// </summary>
        public static IMonadicLR<TLeft, B> Map<TLeft, A, B>(IMonadicLR<TLeft, A> a, Func<A, B> f) =>
            a.Match(ifRight: r => a.Return(f(r)),
                   ifLeft: l => a.ReturnLeft<B>(l));

        /// <summary>
        /// Applying monadic bind function to inner value of Monad
        /// </summary>
        public static IMonadicLR<TLeft, B> Bind<TLeft, A, B>(IMonadicLR<TLeft, A> a, Func<A, IMonadicLR<TLeft, B>> f) =>
            a.Match(ifRight: r => f(r),
                   ifLeft: l => a.ReturnLeft<B>(l));
    }

}
