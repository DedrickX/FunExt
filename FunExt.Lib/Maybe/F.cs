using FunExt.Lib.Common;

namespace FunExt.Lib
{
    public static partial class F
    {
        public static Some<T> Some<T>(T value) =>
            new Some<T>(value);

        public static None None =>
            new None();
    }
}