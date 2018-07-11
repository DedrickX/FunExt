using System;


namespace FunExt.Lib
{

    public static partial class F
    {

        /// <summary>
        /// Some value for use with <see cref="Option{T}"/>.
        /// </summary>
        public static Option<T> Some<T>(T value) =>
            (value == null) ? throw new ArgumentNullException() :
            new Option<T>(true, value);


        public static Option<T> SomeOrNone<T>(T value) =>
            typeof(T).IsValueType ? new Option<T>(true, value) :
            value != null ? new Option<T>(true, value) :
            None;


        /// <summary>
        /// None value for use with <see cref="Option{T}"/>.
        /// </summary>
        public static OptionNone None =>
            new OptionNone();

    }

}