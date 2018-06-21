namespace FunExt.Lib
{
    namespace Maybe
    {
        public sealed class None
        {
            private None()
            {
            }

            internal None Default =>
                new None();
        }
    }
}