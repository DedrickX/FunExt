using System;

namespace FunExt.Lib.Common
{
    public sealed class Unit : IEquatable<Unit>, IComparable
    {
        private Unit()
        {}

        public static Unit Instance
        {
            get
            {
                if (_instance == null) _instance = new Unit();
                return _instance;
            }
        }
        private static Unit _instance;

        public bool Equals(Unit other) => true;

        public int CompareTo(object obj) => CompareTo(obj as Unit);

        private int CompareTo(Unit obj) => 0;

        public override int GetHashCode() => 1;
    }
}