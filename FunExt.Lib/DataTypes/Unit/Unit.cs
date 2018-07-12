using System;

namespace FunExt.DataTypes
{

/// <summary>
/// Unit value
/// </summary>
public struct Unit :
        IEquatable<Unit>,
        IComparable<Unit>
    {

        /// <summary>
        /// Default Unit value.
        /// </summary>
        public static readonly Unit Default = new Unit();


        public override string ToString() =>
            "()";


        public override int GetHashCode() =>
            0;


        public override bool Equals(object obj) =>
            obj is Unit;


        public bool Equals(Unit other) =>
            true;


        public int CompareTo(Unit other) =>
            0;


        public static bool operator ==(Unit left, Unit right) =>
            true;


        public static bool operator !=(Unit left, Unit right) =>
            false;

    }
}