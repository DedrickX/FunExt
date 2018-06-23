namespace FunExt.Lib.Common
{
    /// <summary>
    /// Error value of T.
    /// To be used with Union Types.
    /// </summary>
    public sealed class Error<T>
    {
        internal Error(T errorValue)
        {
            ErrorValue = errorValue;
        }

        public T ErrorValue { get; }
    }
}