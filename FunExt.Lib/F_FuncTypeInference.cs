using System;
using System.Collections.Generic;

using static FunExt.Lib.F;

namespace FunExt.Lib
{

    public static partial class F
    {

        // ---------- Actions ----------


        /// <summary>
        /// Transforming <see cref="Action"/> into <see cref="Func{Unit}"/>.
        /// </summary>
        public static Func<Unit> Fun(Action f) =>
            () => {
                f();
                return UnitValue;
            };


        /// <summary>
        /// Transforming <see cref="Action"/> into <see cref="Func{Unit}"/>.
        /// </summary>
        public static Func<T1, Unit> Fun<T1>(Action<T1> f) =>
            (T1 x1) => {
                f(x1);
                return UnitValue;
            };


        /// <summary>
        /// Transforming <see cref="Action"/> into <see cref="Func{Unit}"/>.
        /// </summary>
        public static Func<T1, T2, Unit> Fun<T1, T2>(Action<T1, T2> f) =>
            (T1 x1, T2 x2) => {
                f(x1, x2);
                return UnitValue;
            };


        /// <summary>
        /// Transforming <see cref="Action"/> into <see cref="Func{Unit}"/>.
        /// </summary>
        public static Func<T1, T2, T3, Unit> Fun<T1, T2, T3>(Action<T1, T2, T3> f) =>
            (T1 x1, T2 x2, T3 x3) => {
                f(x1, x2, x3);
                return UnitValue;
            };


        /// <summary>
        /// Transforming <see cref="Action"/> into <see cref="Func{Unit}"/>.
        /// </summary>
        public static Func<T1, T2, T3, T4, Unit> Fun<T1, T2, T3, T4>(Action<T1, T2, T3, T4> f) =>
            (T1 x1, T2 x2, T3 x3, T4 x4) => {
                f(x1, x2, x3, x4);
                return UnitValue;
            };


        // ---------- Functions ----------


        /// <summary>
        /// Helper for Function type inference
        /// </summary>
        public static Func<R> Fun<R>(Func<R> f) =>
            () => f();


        /// <summary>
        /// Helper for Function type inference
        /// </summary>
        public static Func<T1, R> Fun<T1, R>(Func<T1, R> f) =>
            (T1 x1) => f(x1);


        /// <summary>
        /// Helper for Function type inference
        /// </summary>
        public static Func<T1, T2, R> Fun<T1, T2, R>(Func<T1, T2, R> f) =>
            (T1 x1, T2 x2) => f(x1, x2);


        /// <summary>
        /// Helper for Function type inference
        /// </summary>
        public static Func<T1, T2, T3, R> Fun<T1, T2, T3, R>(Func<T1, T2, T3, R> f) =>
            (T1 x1, T2 x2, T3 x3) => f(x1, x2, x3);


        /// <summary>
        /// Helper for Function type inference
        /// </summary>
        public static Func<T1, T2, T3, T4, R> Fun<T1, T2, T3, T4, R>(Func<T1, T2, T3, T4, R> f) =>
            (T1 x1, T2 x2, T3 x3, T4 x4) => f(x1, x2, x3, x4);

    }

}