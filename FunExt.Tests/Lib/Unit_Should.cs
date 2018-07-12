using System;
using Xunit;
using FluentAssertions;

using FunExt.Lib;
using static FunExt.Lib.F;

namespace FunExt.Tests.Lib
{
    public class Unit_Should
    {

        [Fact]
        void BeEquatable()
        {
            Unit unit1 = unit;
            Unit unit2 = unit;

            (unit1 == unit2).Should().BeTrue();
            (unit1 != unit2).Should().BeFalse();
        }


        [Fact]
        void BeComparable()
        {
            var unit1 = unit;
            var unit2 = unit;

            (unit1.CompareTo(unit2)).Should().Be(0);
        }


        [Fact]
        void HaveExactHashCode()
        {
            var unit1 = unit;
            var unit2 = unit;

            (unit1.GetHashCode() == unit2.GetHashCode())
                .Should().BeTrue();
        }

    }
}