using System;
using System.Linq;
using Xunit;
using FluentAssertions;

using FunExt.Lib;
using System.Collections.Generic;

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

        [Fact]
        public void BeEquatableWhenSomeValue()
        {
            Maybe<int> someMaybe1 = F.Some(10);
            Maybe<int> someMaybe2 = F.Some(10);

            (someMaybe1.Equals(someMaybe2)).Should().BeTrue();
            (someMaybe1 == someMaybe2).Should().BeTrue();

            Maybe<int> someMaybe3 = F.Some(20);
            (someMaybe1 != someMaybe3).Should().BeTrue();
        }

        [Fact]
        public void BeEquatableWhenSomeObject()
        {
            var obj = new List<string>();
            Maybe<List<string>> someMaybe1 = F.Some(obj);
            Maybe<List<string>> someMaybe2 = F.Some(obj);

            (someMaybe1.Equals(someMaybe2)).Should().BeTrue();
            (someMaybe1 == someMaybe2).Should().BeTrue();

            Maybe<List<string>> someMaybe3 = F.Some(new List<string>());
            (someMaybe1 != someMaybe3).Should().BeTrue();
        }

        [Fact]
        public void BeEquatableWhenNone()
        {
            Maybe<List<string>> noneMaybe1 = F.None;
            Maybe<List<string>> noneMaybe2 = F.None;

            (noneMaybe1.Equals(noneMaybe2)).Should().BeTrue();
            (noneMaybe1 == noneMaybe2).Should().BeTrue();

            Maybe<List<string>> someMaybe3 = F.Some(new List<string>());
            (noneMaybe1 != someMaybe3).Should().BeTrue();
        }

        [Fact]
        public void BeEquatableToUnionSubtypes()
        {
            var obj = new List<string>();
            Maybe<int> someMaybeVal = F.Some(10);
            Maybe<List<string>> someMaybeObj = F.Some(obj);
            Maybe<int> noneMaybe = F.None;

            (someMaybeVal == F.Some(10)).Should().BeTrue();
            (someMaybeVal == F.Some(20)).Should().BeFalse();

            (someMaybeObj == F.Some(obj)).Should().BeTrue();
            (someMaybeObj != F.Some(new List<string>())).Should().BeTrue();

            (noneMaybe == F.None).Should().BeTrue();
            (noneMaybe != F.Some(10)).Should().BeTrue();
        }

        [Fact]
        public void BeUsedWithConditionalExpression()
        {
            var path = @"C:\";
            Maybe<string> x = !string.IsNullOrEmpty(path) ? F.Some("text") : F.None;
            x.IsSome.Should().BeTrue();
        }
    }
}