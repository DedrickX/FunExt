namespace FunExt.Lib
{
    public static partial class F
    {

        /// <summary>
        /// Some value for use with <see cref="Option{T}"/>.
        /// </summary>
        public static Option<T> Some<T>(T value) =>
            new Option<T>(true, value);


        /// <summary>
        /// None value for use with <see cref="Option{T}"/>.
        /// </summary>
        public static OptionNone None =>
            new OptionNone();

    }
}