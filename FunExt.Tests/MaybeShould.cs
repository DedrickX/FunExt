using System;
using System.Linq;
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
            Maybe<int> someMaybe = F.Some(10);

            someMaybe.IsSome.Should().BeTrue();
            someMaybe.IsNone.Should().BeFalse();
            someMaybe.GetValue().Should().Be(10);
        }

        [Fact]
        public void BeUsedWithSuccessType()
        {
            Maybe<int> someMaybe = F.Success(10);
            someMaybe.GetValue().Should().Be(10);
        }

        [Fact]
        public void BeUsedWithNoneType()
        {
            Maybe<int> noneMaybe = F.None;

            noneMaybe.IsSome.Should().BeFalse();
            noneMaybe.IsNone.Should().BeTrue();
        }

        [Fact]
        public void ThrowExceptionWhenAccessingToNoneValue()
        {
            Maybe<int> noneMaybe = F.None;

            noneMaybe
                .Invoking(x => x.GetValue())
                .Should()
                .Throw<InvalidOperationException>();
        }

        [Fact]
        public void BeMatchedWhenSome()
        {
            Maybe<int> someMaybe = F.Some(10);

            var result = someMaybe.Match(
                ifSome: x => x,
                ifNone: () => throw new Exception("Value is none!")
            );
            result.Should().Be(10);
        }

        [Fact]
        public void BeMatchedWhenNone()
        {
            Maybe<int> noneMaybe = F.None;

            var result = noneMaybe.Match(
                ifSome: x => throw new Exception("Value is Some!"),
                ifNone: () => "Ok"
            );
            result.Should().Be("Ok");
        }

        [Fact]
        public void BeEnumerableWhenSome()
        {
            Maybe<int> someMaybe = F.Some(10);
            (from val in someMaybe select val).First().Should().Be(10);
            (from val in someMaybe select val).Count().Should().Be(1);
        }

        [Fact]
        public void BeEnumerableWhenNone()
        {
            Maybe<int> noneMaybe = F.None;
            (from val in noneMaybe select val).Count().Should().Be(0);
        }
    }
}