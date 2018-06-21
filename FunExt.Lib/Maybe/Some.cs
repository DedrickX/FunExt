namespace FunExt.Lib
{
    namespace Maybe
    {
        public sealed class Some<T>
        {
            internal Some(T value)
            {
                Value = value;
            }

            public T Value { get; }
        }
    }
}