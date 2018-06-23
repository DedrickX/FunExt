using System;
using Xunit;
using FluentAssertions;

using FunExt.Lib;

namespace FunExt.Tests
{
    public class MaybeShould
    {
        [Fact]
        public void BeUsedWithSomeType()
        {
            Maybe<int> some = F.Some(10);

            some.IsSome.Should().Be(true);
            some.IsNone.Should().Be(false);
            some.GetValue().Should().Be(10);
        }

        [Fact]
        public void BeUsedWithSuccessType()
        {
            Maybe<int> some = F.Success(10);
            some.GetValue().Should().Be(10);
        }

        [Fact]
        public void BeUsedWithNoneType()
        {
            Maybe<int> none = F.None;

            none.IsSome.Should().Be(false);
            none.IsNone.Should().Be(true);
        }

        [Fact]
        public void ThrowExceptionWhenAccessingToNoneValue()
        {
            Maybe<int> none = F.None;

            none
                .Invoking(x => x.GetValue())
                .Should()
                .Throw<InvalidOperationException>();
        }
    }
}