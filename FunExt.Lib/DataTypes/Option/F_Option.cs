using System;

using FunExt.DataTypes;


namespace FunExt
{

    public static partial class F
    {

        /// <summary>
        /// Some value for use with <see cref="Option{T}"/>.
        /// </summary>
        public static Option<T> Some<T>(T value) =>
            (value == null) ? throw new ArgumentNullException() :
            new Option<T>(true, value);


        /// <summary>
        /// Creates Some Option if argument is not null, otherwise returns None Option.
        /// </summary>
        public static Option<T> SomeIfNotNull<T>(T value) =>
            (value == null) ? new OptionNone() :
            new Option<T>(true, value);


        /// <summary>
        /// None value for use with <see cref="Option{T}"/>.
        /// </summary>
        public static OptionNone None =>
            new OptionNone();

    }

}