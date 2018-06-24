using System;
using Xunit;
using FluentAssertions;

using FunExt.Lib;
using System.Linq;
using System.Collections.Generic;

namespace FunExt.Tests
{
    public class ResultShould
    {
        [Fact]
        public void BeUsedWithSuccessType()
        {
            Result<int, Exception> successResult = F.Success(10);

            successResult.IsSuccess.Should().BeTrue();
            successResult.IsError.Should().BeFalse();
            successResult.GetSuccess().Should().Be(10);
        }

        [Fact]
        public void BeUsedWithSomeType()
        {
            Result<int, Exception> successResult = F.Some(10);
            successResult.GetSuccess().Should().Be(10);
        }

        [Fact]
        public void BeUsedWithErrorType()
        {
            var err = new Exception("hello");
            Result<int, Exception> errorResult = F.Error(err);

            errorResult.IsSuccess.Should().BeFalse();
            errorResult.IsError.Should().BeTrue();
            errorResult.GetError().Should().BeSameAs(err);
        }

        [Fact]
        public void ThrowExceptionWhenAccessingErrorOnSuccess()
        {
            Result<int, Exception> successResult = F.Success(10);
            successResult
                .Invoking(x => x.GetError())
                .Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void ThrowExceptionWhenAccessingSuccessOnError()
        {
            Result<int, Exception> errorResult = F.Error(new Exception("hello"));
            errorResult
                .Invoking(x => x.GetSuccess())
                .Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void BeMatchedWhenSuccess()
        {
            Result<int, Exception> successResult = F.Success(10);

            var result = successResult.Match(
                ifSuccess: x => x,
                ifError: x => throw new Exception("Value is Error!")
            );
            result.Should().Be(10);
        }

        [Fact]
        public void BeMatchedWhenError()
        {
            var ex = new Exception("hello");
            Result<int, Exception> errorResult = F.Error(ex);

            var result = errorResult.Match(
                ifSuccess: x => throw new Exception("Value is Success!"),
                ifError: x => x
            );
            result.Should().BeSameAs(ex);
        }

        [Fact]
        public void BeEnumerableWhenSuccess()
        {
            Result<int, Exception> successResult = F.Success(10);
            (from val in successResult select val).First().Should().Be(10);
            (from val in successResult select val).Count().Should().Be(1);
        }

        [Fact]
        public void BeEnumerableWhenError()
        {
            Result<int, Exception> errorResult = F.Error(new Exception());
            (from val in errorResult select val).Count().Should().Be(0);
        }

        [Fact]
        public void BeEquatableWhenContainsSuccessValue()
        {
            Result<int, Exception> successResult1 = F.Success(10);
            Result<int, Exception> successResult2 = F.Success(10);

            (successResult1.Equals(successResult2)).Should().BeTrue();
            (successResult1 == successResult2).Should().BeTrue();

            Result<int, Exception> successResult3 = F.Success(20);
            (successResult1 != successResult3).Should().BeTrue();
        }

        [Fact]
        public void BeEquatableWhenContainsSuccessObject()
        {
            var obj = new List<string>();
            Result<List<string>, Exception> successResult1 = F.Success(obj);
            Result<List<string>, Exception> successResult2 = F.Success(obj);

            (successResult1.Equals(successResult2)).Should().BeTrue();
            (successResult1 == successResult2).Should().BeTrue();

            Result<List<string>, Exception> successResult3 = F.Success(new List<string>());
            (successResult1 != successResult3).Should().BeTrue();
        }

        [Fact]
        public void BeEquatableWhenContainsErrorValue()
        {
            Result<string, int> errorResult1 = F.Error(10);
            Result<string, int> errorResult2 = F.Error(10);

            (errorResult1.Equals(errorResult2)).Should().BeTrue();
            (errorResult1 == errorResult2).Should().BeTrue();

            Result<string, int> errorResult3 = F.Error(20);
            (errorResult1 != errorResult3).Should().BeTrue();
        }

        [Fact]
        public void BeEquatableWhenContainsErrorObject()
        {
            var obj = new List<string>();
            Result<int, List<string>> errorResult1 = F.Error(obj);
            Result<int, List<string>> errorResult2 = F.Error(obj);

            (errorResult1.Equals(errorResult2)).Should().BeTrue();
            (errorResult1 == errorResult2).Should().BeTrue();

            Result<int, List<string>> errorResult3 = F.Error(new List<string>());
            (errorResult1 != errorResult3).Should().BeTrue();
        }

        [Fact]
        public void BeEquatableToUnionSubtypes()
        {
            var obj = new List<string>();
            Result<int, Exception> successResultVal = F.Success(10);
            Result<List<string>, Exception> successResultObj = F.Some(obj);
            Result<string, int> errorResultVal = F.Error(10);
            Result<int, List<string>> errorResultObj = F.Error(obj);

            (successResultVal == F.Success(10)).Should().BeTrue();
            (successResultVal == F.Success(20)).Should().BeFalse();
            (successResultVal != F.Error(new Exception())).Should().BeTrue();

            (successResultObj == F.Success(obj)).Should().BeTrue();
            (successResultObj == F.Success(new List<string>())).Should().BeFalse();
            (successResultObj != F.Error(new Exception())).Should().BeTrue();

            (errorResultVal == F.Error(10)).Should().BeTrue();
            (errorResultVal == F.Error(20)).Should().BeFalse();
            (errorResultVal != F.Success("hello")).Should().BeTrue();

            (errorResultObj == F.Error(obj)).Should().BeTrue();
            (errorResultObj == F.Error(new List<string>())).Should().BeFalse();
            (errorResultObj != F.Success(42)).Should().BeTrue();
        }
    }
}