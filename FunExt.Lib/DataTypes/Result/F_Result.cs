using System;

namespace FunExt.Lib
{
    public static partial class F
    {

        /// <summary>
        /// Succesful result for use with <see cref="Result{T}"/>.
        /// </summary>
        public static Result<T> Success<T>(T value) =>
            new Result<T>(true, value, null);


        /// <summary>
        /// Failure result for use with <see cref="Result{T}"/>.
        /// </summary>
        public static Exception Failure(Exception ex) =>
            ex;


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