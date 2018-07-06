using System;


namespace FunExt.Lib.DataTypes
{
    public interface IMonadicLR<TLeft, A>
    {
        B Cata<B>(Func<A, B> ifRight, Func<TLeft, B> ifLeft);

        IMonadicLR<TLeft, B> Lift<B>(B rightValue);

        IMonadicLR<TLeft, B> GetLeftValue<B>(TLeft leftValue);
    }

}
