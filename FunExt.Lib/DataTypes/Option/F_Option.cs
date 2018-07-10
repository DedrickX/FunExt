using System;

namespace FunExt.Lib
{
    public static partial class F
    {

        /// <summary>
        /// Some value for use with <see cref="Option{T}"/>.
        /// </summary>
        public static Option<T> Some<T>(T value) =>
            typeof(T).IsValueType ? new Option<T>(true, value) :
            value != null ? new Option<T>(true, value) :
            throw new ArgumentNullException();


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