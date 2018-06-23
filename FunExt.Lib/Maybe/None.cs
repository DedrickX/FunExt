namespace FunExt.Lib
{
    namespace Maybe
    {
        /// <summary>
        /// Value of None. 
        /// To be used with Union Types like <see cref="Maybe{T}"/>.
        /// </summary>
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