using FunExt.Lib.Common;

namespace FunExt.Lib
{
    public static partial class F
    {
        public static Some<T> Success<T>(T value) =>
            F.Some(value);

        public static Error<T> Error<T>(T errorValue) =>
            new Error<T>(errorValue);
    }
}