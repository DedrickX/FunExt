using System;
using Xunit;
using FluentAssertions;

using FunExt.Lib;

namespace FunExt.Tests
{
    public class UnitShould
    {

        [Fact]
        void BeEquatable()
        {
            var unit1 = F.Unit;
            var unit2 = F.Unit;

            (unit1 == unit2).Should().BeTrue();
            (unit1 != unit2).Should().BeFalse();
        }


        [Fact]
        void BeComparable()
        {
            var unit1 = F.Unit;
            var unit2 = F.Unit;

            (unit1.CompareTo(unit2)).Should().Be(0);
        }


        [Fact]
        void HaveExactHashCode()
        {
            var unit1 = F.Unit;
            var unit2 = F.Unit;

            (unit1.GetHashCode() == unit2.GetHashCode())
                .Should().BeTrue();
        }

    }
}