using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

using FunExt.Lib;
using static FunExt.Lib.F;


namespace FunExt.Tests.Lib
{
    public class Option_Should
    {

        [Fact]
        public void BeUsedWithSomeType()
        {
            Option<int> someOption = Some(10);

            someOption.IsSome.Should().BeTrue();
            someOption.IsNone.Should().BeFalse();
            someOption.Match(
                ifSome: x => x.Should().Be(10),
                ifNone: _ => throw new Exception("Value is none!?")
            );
        }


        [Fact]
        public void ThrowExceptionIfSomeIsNull()
        {           
            this.Invoking((x) => { Option<int?> s = Some<int?>(null); })
                .Should()
                .Throw<ArgumentNullException>();

            this.Invoking((x) => { Option<string> s = Some<string>(null); })
                .Should()
                .Throw<ArgumentNullException>();

            this.Invoking((x) => { Option<Exception> s = Some<Exception>(null); })
                .Should()
                .Throw<ArgumentNullException>();
        }

        
        [Fact]
        public void BeUsedWithNoneType()
        {
            Option<int> noneOption = None;

            noneOption.IsSome.Should().BeFalse();
            noneOption.IsNone.Should().BeTrue();
        }


        [Fact]
        public void BeMatchedWhenSome()
        {
            Option<int> someOption = Some(10);

            var result = someOption.Match(
                ifSome: x => x,
                ifNone: _ => throw new Exception("Value is none!?")
            );
            result.Should().Be(10);
        }


        [Fact]
        public void BeMatchedWhenNone()
        {
            Option<int> noneOption = None;

            var result = noneOption.Match(
                ifSome: x => throw new Exception("Value is Some!?"),
                ifNone: _ => "Ok"
            );
            result.Should().Be("Ok");
        }


        [Fact]
        public void BeEnumerableWhenSome()
        {
            Option<int> someOption = Some(10);
            (from val in someOption select val).First().Should().Be(10);
            (from val in someOption select val).Count().Should().Be(1);
        }


        [Fact]
        public void BeEnumerableWhenNone()
        {
            Option<int> noneOption = None;
            (from val in noneOption select val).Count().Should().Be(0);
        }


        [Fact]
        public void BeEquatableWhenSomeValue()
        {
            Option<int> someOption1 = Some(10);
            Option<int> someOption2 = Some(10);

            (someOption1.Equals(someOption2)).Should().BeTrue();
            (someOption1 == someOption2).Should().BeTrue();

            Option<int> someOption3 = Some(20);
            (someOption1 != someOption3).Should().BeTrue();
        }


        [Fact]
        public void BeEquatableWhenSomeObject()
        {
            var obj = new List<string>();
            Option<List<string>> someOption1 = Some(obj);
            Option<List<string>> someOption2 = Some(obj);

            (someOption1.Equals(someOption2)).Should().BeTrue();
            (someOption1 == someOption2).Should().BeTrue();

            Option<List<string>> someOption3 =  Some(new List<string>());
            (someOption1 != someOption3).Should().BeTrue();
        }


        [Fact]
        public void BeEquatableWhenNone()
        {
            Option<List<string>> noneOption1 = None;
            Option<List<string>> noneOption2 = None;

            (noneOption1.Equals(noneOption2)).Should().BeTrue();
            (noneOption1 == noneOption2).Should().BeTrue();

            Option<List<string>> someOption3 = Some(new List<string>());
            (noneOption1 != someOption3).Should().BeTrue();
        }


        [Fact]
        public void BeEquatableToUnionSubtypes()
        {
            var obj = new List<string>();
            Option<int> someOptionVal =  Some(10);
            Option<List<string>> someOptionObj = Some(obj);
            Option<int> noneOption =  None;

            (someOptionVal == Some(10)).Should().BeTrue();
            (someOptionVal == Some(20)).Should().BeFalse();

            (someOptionObj == Some(obj)).Should().BeTrue();
            (someOptionObj != Some(new List<string>())).Should().BeTrue();

            (noneOption == None).Should().BeTrue();
            (noneOption != Some(10)).Should().BeTrue();
        }


        [Fact]
        public void BeUsedWithConditionalExpression()
        {
            var path = @"C:\";
            Option<string> x = !string.IsNullOrEmpty(path) ? Some("text") : None;
            x.IsSome.Should().BeTrue();
        }


        [Fact]
        public void WorkWithHashSets()
        {
            Option<int> i1 = Some(1);
            Option<int> i2 = Some(2);
            Option<int> i3 = Some(1);

            var hs = new HashSet<Option<int>>();
            hs.Add(i1).Should().BeTrue();
            hs.Add(i2).Should().BeTrue();

            hs.Contains(Some(1)).Should().BeTrue();
            hs.Contains(Some(2)).Should().BeTrue();
            hs.Contains(Some(3)).Should().BeFalse();

            hs.Add(i3).Should().BeFalse();
        }


        //[Fact]
        //public void MapWhenSome()
        //{
        //    string f(int x) =>
        //        $"Number {x}!";

        //    Option<int> someValue = Some(10);
        //    var result = someValue.Map(f);

        //    result.IsSome.Should().BeTrue();
        //    var resultValue = result.Match(
        //        ifSome: (string x) => x,
        //        ifNone: _ => throw new Exception("Value is none!?")
        //    );
        //    resultValue.Should().Be("Number 10!");
        //}


        //[Fact]
        //public void MapWhenNone()
        //{
        //    string f(int x) =>
        //        $"Number {x}!";

        //    Option<int> noneValue = None;
        //    var result = noneValue.Map(f);

        //    result.IsNone.Should().BeTrue();
        //}


        //[Fact]
        //public void BindWhenSome()
        //{
        //    Option<string> f(int x) =>
        //        x >= 0 ? Some($"Number {x} is positive!") : None;

        //    // positive value
        //    Option<int> somePositiveValue = Some(10);
        //    var positiveResult = somePositiveValue.Bind(f);

        //    positiveResult.IsSome.Should().BeTrue();
        //    var resultValue = positiveResult.Match(
        //        ifSome: (string x) => x,
        //        ifNone: _ => throw new Exception("Value is none!?")
        //    );
        //    resultValue.Should().Be("Number 10 is positive!");

        //    // negative value
        //    Option<int> someNegativeValue = Some(-10);
        //    var negativeResult = someNegativeValue.Bind(f);

        //    negativeResult.IsSome.Should().BeFalse();
        //}


        //[Fact]
        //public void BindWhenNone()
        //{
        //    Option<string> f(int x) =>
        //        x >= 0 ? Some($"Number {x} is positive!") : None;

        //    // positive value
        //    Option<int> noneValue = None;
        //    var result = noneValue.Bind(f);

        //    result.IsSome.Should().BeFalse();
        //}

    }
}