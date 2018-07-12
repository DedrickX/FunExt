using System;

using FunExt.DataTypes;


namespace FunExt
{

    public static partial class F
    {

        // ---------- Actions ----------


        /// <summary>
        /// Transforming <see cref="Action"/> into <see cref="Func{Unit}"/>.
        /// </summary>
        public static Func<Unit> GetUnitFunc(Action f) =>
            () => {
                f();
                return unit;
            };


        /// <summary>
        /// Transforming <see cref="Action"/> into <see cref="Func{Unit}"/>.
        /// </summary>
        public static Func<T1, Unit> GetUnitFunc<T1>(Action<T1> f) =>
            (T1 x1) => {
                f(x1);
                return unit;
            };


        /// <summary>
        /// Transforming <see cref="Action"/> into <see cref="Func{Unit}"/>.
        /// </summary>
        public static Func<T1, T2, Unit> GetUnitFunc<T1, T2>(Action<T1, T2> f) =>
            (T1 x1, T2 x2) => {
                f(x1, x2);
                return unit;
            };


        /// <summary>
        /// Transforming <see cref="Action"/> into <see cref="Func{Unit}"/>.
        /// </summary>
        public static Func<T1, T2, T3, Unit> GetUnitFunc<T1, T2, T3>(Action<T1, T2, T3> f) =>
            (T1 x1, T2 x2, T3 x3) => {
                f(x1, x2, x3);
                return unit;
            };


        /// <summary>
        /// Transforming <see cref="Action"/> into <see cref="Func{Unit}"/>.
        /// </summary>
        public static Func<T1, T2, T3, T4, Unit> GetUnitFunc<T1, T2, T3, T4>(Action<T1, T2, T3, T4> f) =>
            (T1 x1, T2 x2, T3 x3, T4 x4) => {
                f(x1, x2, x3, x4);
                return unit;
            };


        // ---------- Functions ----------


        /// <summary>
        /// Helper for Function type inference
        /// </summary>
        public static Func<R> GetFunc<R>(Func<R> f) =>
            () => f();


        /// <summary>
        /// Helper for Function type inference
        /// </summary>
        public static Func<T1, R> GetFunc<T1, R>(Func<T1, R> f) =>
            (T1 x1) => f(x1);


        /// <summary>
        /// Helper for Function type inference
        /// </summary>
        public static Func<T1, T2, R> GetFunc<T1, T2, R>(Func<T1, T2, R> f) =>
            (T1 x1, T2 x2) => f(x1, x2);


        /// <summary>
        /// Helper for Function type inference
        /// </summary>
        public static Func<T1, T2, T3, R> GetFunc<T1, T2, T3, R>(Func<T1, T2, T3, R> f) =>
            (T1 x1, T2 x2, T3 x3) => f(x1, x2, x3);


        /// <summary>
        /// Helper for Function type inference
        /// </summary>
        public static Func<T1, T2, T3, T4, R> GetFunc<T1, T2, T3, T4, R>(Func<T1, T2, T3, T4, R> f) =>
            (T1 x1, T2 x2, T3 x3, T4 x4) => f(x1, x2, x3, x4);


        /// <summary>
        /// Running unsafe function.
        /// </summary>
        public static Result<R> TryFunc<R>(Func<R> unsafeFunc)
        {
            try
            {
                var result = unsafeFunc();
                return Success(result);
            }
            catch (Exception ex)
            {
                return Failure(ex);
            }
        }

    }

}