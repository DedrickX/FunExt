using System;
using System.Linq;
using Xunit;
using FluentAssertions;

using FunExt.Lib;
using System.Collections.Generic;

namespace FunExt.Tests
{
    public class OptionShould
    {

        [Fact]
        public void BeUsedWithSomeType()
        {
            Option<int> someOption = F.Some(10);

            someOption.IsSome.Should().BeTrue();
            someOption.IsNone.Should().BeFalse();
            someOption.Match(
                ifSome: x => x.Should().Be(10),
                ifNone: () => throw new Exception("Invalid value!")
            );
        }


        [Fact]
        public void BeUsedWithNoneType()
        {
            Option<int> noneOption = F.None;

            noneOption.IsSome.Should().BeFalse();
            noneOption.IsNone.Should().BeTrue();
        }


        [Fact]
        public void BeMatchedWhenSome()
        {
            Option<int> someOption = F.Some(10);

            var result = someOption.Match(
                ifSome: x => x,
                ifNone: () => throw new Exception("Value is none!")
            );
            result.Should().Be(10);
        }


        [Fact]
        public void BeMatchedWhenNone()
        {
            Option<int> noneOption = F.None;

            var result = noneOption.Match(
                ifSome: x => throw new Exception("Value is Some!"),
                ifNone: () => "Ok"
            );
            result.Should().Be("Ok");
        }


        [Fact]
        public void BeEnumerableWhenSome()
        {
            Option<int> someOption = F.Some(10);
            (from val in someOption select val).First().Should().Be(10);
            (from val in someOption select val).Count().Should().Be(1);
        }


        [Fact]
        public void BeEnumerableWhenNone()
        {
            Option<int> noneOption = F.None;
            (from val in noneOption select val).Count().Should().Be(0);
        }


        [Fact]
        public void BeEquatableWhenSomeValue()
        {
            Option<int> someOption1 = F.Some(10);
            Option<int> someOption2 = F.Some(10);

            (someOption1.Equals(someOption2)).Should().BeTrue();
            (someOption1 == someOption2).Should().BeTrue();

            Option<int> someOption3 = F.Some(20);
            (someOption1 != someOption3).Should().BeTrue();
        }


        [Fact]
        public void BeEquatableWhenSomeObject()
        {
            var obj = new List<string>();
            Option<List<string>> someOption1 = F.Some(obj);
            Option<List<string>> someOption2 = F.Some(obj);

            (someOption1.Equals(someOption2)).Should().BeTrue();
            (someOption1 == someOption2).Should().BeTrue();

            Option<List<string>> someOption3 = F.Some(new List<string>());
            (someOption1 != someOption3).Should().BeTrue();
        }


        [Fact]
        public void BeEquatableWhenNone()
        {
            Option<List<string>> noneOption1 = F.None;
            Option<List<string>> noneOption2 = F.None;

            (noneOption1.Equals(noneOption2)).Should().BeTrue();
            (noneOption1 == noneOption2).Should().BeTrue();

            Option<List<string>> someOption3 = F.Some(new List<string>());
            (noneOption1 != someOption3).Should().BeTrue();
        }


        [Fact]
        public void BeEquatableToUnionSubtypes()
        {
            var obj = new List<string>();
            Option<int> someOptionVal = F.Some(10);
            Option<List<string>> someOptionObj = F.Some(obj);
            Option<int> noneOption = F.None;

            (someOptionVal == F.Some(10)).Should().BeTrue();
            (someOptionVal == F.Some(20)).Should().BeFalse();

            (someOptionObj == F.Some(obj)).Should().BeTrue();
            (someOptionObj != F.Some(new List<string>())).Should().BeTrue();

            (noneOption == F.None).Should().BeTrue();
            (noneOption != F.Some(10)).Should().BeTrue();
        }


        [Fact]
        public void BeUsedWithConditionalExpression()
        {
            var path = @"C:\";
            Option<string> x = !string.IsNullOrEmpty(path) ? F.Some("text") : F.None;
            x.IsSome.Should().BeTrue();
        }

    }
}