using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

using FunExt.Lib;
using static FunExt.Lib.F;

namespace FunExt.Tests.Lib
{
    public class Result_Should
    {

        [Fact]
        public void BeUsedWithSuccess()
        {
            Result<int> successResult = Success(10);

            successResult.IsSuccess.Should().BeTrue();
            successResult.IsFailure.Should().BeFalse();

            successResult.Match(
                ifSuccess: x => x.Should().Be(10),
                ifFailure: ex => throw new Exception());
        }


        [Fact]
        public void BeUsedWithFailureAsException()
        {
            var err1 = new Exception("hello");
            Result<int> errorResult = Failure(err1);

            errorResult.IsSuccess.Should().BeFalse();
            errorResult.IsFailure.Should().BeTrue();

            errorResult.Match(
                ifSuccess: x => throw new Exception(),
                ifFailure: ex => ex.Should().BeSameAs(err1));
        }


        [Fact]
        public void BeUsedWithFailureAsDescription()
        {
            Result<int> errorResult = Failure("This is really bad!");

            errorResult.IsSuccess.Should().BeFalse();
            errorResult.IsFailure.Should().BeTrue();

            errorResult.Match(
                ifSuccess: x => throw new Exception(),
                ifFailure: ex => ex.Message.Should().BeEquivalentTo("This is really bad!"));
        }


        [Fact]
        public void BeUsedWithExceptionType()
        {
            var err = new Exception("hello");
            Result<int> errorResult = err;

            errorResult.IsSuccess.Should().BeFalse();
            errorResult.IsFailure.Should().BeTrue();

            errorResult.Match(
                ifSuccess: x => throw new Exception(),
                ifFailure: ex => ex.Should().BeSameAs(err));
        }


        [Fact]
        public void BeEnumerableWhenSuccess()
        {
            Result<int> successResult = Success(10);
            (from val in successResult select val).First().Should().Be(10);
            (from val in successResult select val).Count().Should().Be(1);
        }


        [Fact]
        public void BeEnumerableWhenFailure()
        {
            Result<int> errorResult = new Exception();
            (from val in errorResult select val).Count().Should().Be(0);
        }


        [Fact]
        public void BeEquatableWhenContainsSuccessValue()
        {
            Result<int> successResult1 = Success(10);
            Result<int> successResult2 = Success(10);

            (successResult1.Equals(successResult2)).Should().BeTrue();
            (successResult1 == successResult2).Should().BeTrue();

            Result<int> successResult3 = Success(20);
            (successResult1 != successResult3).Should().BeTrue();
        }

        [Fact]
        public void BeEquatableWhenContainsSuccessObject()
        {
            var obj = new List<string>();
            Result<List<string>> successResult1 = Success(obj);
            Result<List<string>> successResult2 = Success(obj);

            (successResult1.Equals(successResult2)).Should().BeTrue();
            (successResult1 == successResult2).Should().BeTrue();

            Result<List<string>> successResult3 = Success(new List<string>());
            (successResult1 != successResult3).Should().BeTrue();
        }

        [Fact]
        public void BeEquatableWhenContainsFailure()
        {
            var ex1 = new Exception("1");
            var ex2 = new Exception("2");

            Result<string> errorResult1 =  ex1;
            Result<string> errorResult2 = ex1;

            (errorResult1.Equals(errorResult2)).Should().BeTrue();
            (errorResult1 == errorResult2).Should().BeTrue();

            Result<string> errorResult3 = ex2;
            (errorResult1 != errorResult3).Should().BeTrue();
        }


        [Fact]
        public void BeUsedWithConditionalExpression()
        {
            var path = @"C:\";

            Result<string> r1 = !string.IsNullOrEmpty(path) ? Success("text")
                : new Exception("Invalid path!");
            r1.IsSuccess.Should().BeTrue();

            Result<string> r2 = !string.IsNullOrEmpty(path) ? Success("text")
                : Failure("Oops, that is bad!");
            r2.IsSuccess.Should().BeTrue();
        }


        [Fact]
        public void WorkWithHashSets()
        {
            Result<int> i1 = Success(1);
            Result<int> i2 = Success(2);
            Result<int> i3 = Success(1);

            var hs = new HashSet<Result<int>>();
            hs.Add(i1).Should().BeTrue();
            hs.Add(i2).Should().BeTrue();

            hs.Contains(Success(1)).Should().BeTrue();
            hs.Contains(Success(2)).Should().BeTrue();
            hs.Contains(Success(3)).Should().BeFalse();

            hs.Add(i3).Should().BeFalse();
        }


        [Fact]
        public void MapWhenSuccess()
        {
            Func<int, string> f = x =>
                $"Number {x}!";

            Result<int> successValue = Success(10);
            var result = successValue.Map(f);

            result.IsSuccess.Should().BeTrue();
            result.Match(
                ifSuccess: (string x) => x,
                ifFailure: (Exception _) => throw new Exception("Value is none!?"))
            .Should().Be("Number 10!");
        }


        [Fact]
        public void MapWhenFailure()
        {
            Func<int, string> f = x =>
                $"Number {x}!";

            var ex = new Exception("Hello");

            Result<int> failureValue = ex;
            var result = failureValue.Map(f);

            result.IsFailure.Should().BeTrue();
            result.Match(
                ifSuccess: (string _) => throw new Exception(),
                ifFailure: (Exception x) => x)
                .Should().BeSameAs(ex);
        }


        [Fact]
        public void BindWhenSuccess()
        {
            var ex = new Exception("Value is negative!");

            Func<int, Result<string>> f = x =>
                x >= 0 ? Success($"Number {x} is positive!") : Failure(ex);

            // positive value
            Result<int> successPositiveValue = Success(10);
            var positiveResult = successPositiveValue.Bind(f);

            positiveResult.IsSuccess.Should().BeTrue();
            positiveResult.Match(
                ifSuccess: (string x) => x,
                ifFailure: (Exception _) => throw new Exception())
                .Should().Be("Number 10 is positive!");

            // negative value
            Result<int> successNegativeValue = Success(-10);
            var negativeResult = successNegativeValue.Bind(f);

            negativeResult.IsSuccess.Should().BeFalse();
            negativeResult.Match(
                ifSuccess: (string _) => throw new Exception(),
                ifFailure: (Exception x) => x)
                .Should().BeSameAs(ex);
        }


        [Fact]
        public void BindWhenFailure()
        {
            var funcEx = new Exception("Value is negative!");

            Func<int, Result<string>> f = x =>
                x >= 0 ? Success($"Number {x} is positive!") : Failure(funcEx);

            var initialEx = new Exception("Initial value is Failure!");
            Result<int> failureValue = Failure(initialEx);
            var result = failureValue.Bind(f);

            result.IsSuccess.Should().BeFalse();
            result.Match(
                ifSuccess: (string _) => throw new Exception(),
                ifFailure: (Exception x) => x)
                .Should().BeSameAs(initialEx);
        }

    }
}