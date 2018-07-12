using System;


namespace FunExt.Lib
{

    public static partial class F
    {

        /// <summary>
        /// Succesful result for use with <see cref="Result{T}"/>.
        /// </summary>
        public static Result<T> Success<T>(T value) =>
            (value == null) ? throw new ArgumentNullException() :
            new Result<T>(true, value, null);


        /// <summary>
        /// Creates Success Result if argument is not null, otherwise returns Failure Result.
        /// </summary>
        public static Result<T> SuccessIfNotNull<T>(T value) =>
            (value == null) ? new ArgumentNullException() :
            new Result<T>(true, value, null);


        /// <summary>
        /// Failure result for use with <see cref="Result{T}"/>.
        /// </summary>
        public static Exception Failure(Exception ex) =>
            ex ?? throw new ArgumentNullException();


        /// <summary>
        /// Failure result for use with <see cref="Result{T}"/>.
        /// </summary>
        /// <remarks>
        /// Failure is internally stored as <see cref="Exception"/> with given message.
        /// </remarks>
        public static Exception Failure(string failureMessage) =>
            new Exception(failureMessage);

    }

}