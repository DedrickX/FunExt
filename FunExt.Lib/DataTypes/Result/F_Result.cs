namespace FunExt.Lib
{
    public static partial class F
    {

        public static Result<T> Success<T>(T value) =>
            new Result<T>(true, value, null);

    }
}