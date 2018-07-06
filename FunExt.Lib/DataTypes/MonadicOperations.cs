using System;
using System.Collections.Generic;
using System.Text;


namespace FunExt.Lib.DataTypes
{

    public static class MonadicOperations
    {
        public static IMonadicLR<TLeft, B> Map<TLeft, A, B>(IMonadicLR<TLeft, A> a, Func<A, B> f) =>
            a.Cata(ifRight: r => a.Lift(f(r)),
                   ifLeft: l => a.GetLeftValue<B>(l));

        public static IMonadicLR<TLeft, B> Bind<TLeft, A, B>(IMonadicLR<TLeft, A> a, Func<A, IMonadicLR<TLeft, B>> f) =>
            a.Cata(ifRight: r => f(r),
                   ifLeft: l => a.GetLeftValue<B>(l));
    }

}
