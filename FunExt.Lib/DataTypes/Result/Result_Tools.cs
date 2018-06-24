namespace FunExt.Lib
{
    public partial class Result<TResult, TError>
    {
        public static implicit operator Result<TResult, TError>(Common.Some<TResult> some) =>
            new Result<TResult, TError>(true, some.Value, default(TError));

        public static implicit operator Result<TResult, TError>(Common.Error<TError> error) =>
             new Result<TResult, TError>(false, default(TResult), error.ErrorValue);
    }
}