namespace FunExt.Lib.Common
{
    /// <summary>
    /// Some value of T.
    /// To be used with Union Types like <see cref="Maybe{T}"/>.
    /// </summary>
    public sealed class Some<T>
    {
        internal Some(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}