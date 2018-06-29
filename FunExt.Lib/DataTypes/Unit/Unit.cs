using System;

namespace FunExt.Lib.DataTypes
{

/// <summary>
/// Unit value
/// </summary>
public struct Unit : IEquatable<Unit>, IComparable<Unit>
    {

        /// <summary>
        /// Default Unit value.
        /// </summary>
        public static readonly Unit Default = new Unit();


        public override string ToString() => "()";


        #region Operators, Equality and Comparison


        public override int GetHashCode() => 0;


        public override bool Equals(object obj) => obj is Unit;


        public bool Equals(Unit other) => true;


        public int CompareTo(Unit other) => 0;


        public static bool operator ==(Unit left, Unit right) =>
            true;


        public static bool operator !=(Unit left, Unit right) =>
            false;


        public static bool operator >(Unit left, Unit right) =>
            false;


        public static bool operator >=(Unit left, Unit right) =>
            true;


        public static bool operator <(Unit left, Unit right) =>
            false;


        public static bool operator <=(Unit left, Unit right) =>
            true;


        #endregion

    }
}