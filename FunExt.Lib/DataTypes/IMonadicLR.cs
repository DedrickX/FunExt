using System;


namespace FunExt.Lib.DataTypes
{
    public interface IMonadicLR<TLeft, A> // is this really necessary? :)
    {
        B Match<B>(Func<A, B> ifRight, Func<TLeft, B> ifLeft);

        IMonadicLR<TLeft, B> Return<B>(B rightValue);

        IMonadicLR<TLeft, B> ReturnLeft<B>(TLeft leftValue);
    }

}
