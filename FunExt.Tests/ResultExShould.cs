using System;
using Xunit;
using FluentAssertions;

using FunExt.Lib;
using System.Linq;
using System.Collections.Generic;

namespace FunExt.Tests
{
    public class ResultExShould
    {
        [Fact]
        public void BeUsedWithSomeAndSuccessTypes()
        {
            ResultEx<int> successResult = F.Success(10);
            successResult.GetSuccess().Should().Be(10);

            ResultEx<int> someResult = F.Some(11);
            someResult.GetSuccess().Should().Be(11);
        }

        [Fact]
        public void BeUsedWithError()
        {
            var ex = new Exception("error");
            ResultEx<int> errorResult = F.Error(ex);
            errorResult.IsError.Should().BeTrue();
            errorResult.IsSuccess.Should().BeFalse();

            errorResult.GetError().Should().BeSameAs(ex);
        }

        [Fact]
        public void BeUsedWithException()
        {
            var ex = new Exception("error");
            ResultEx<int> errorResult = ex;
            errorResult.IsError.Should().BeTrue();
            errorResult.IsSuccess.Should().BeFalse();

            errorResult.GetError().Should().BeSameAs(ex);
        }

        [Fact]
        public void BeMatchedWhenSuccess()
        {
            ResultEx<int> successResult = F.Success(10);

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
            ResultEx<int> errorResult = ex;

            var result = errorResult.Match(
                ifSuccess: x => throw new Exception("Value is Success!"),
                ifError: x => x
            );
            result.Should().BeSameAs(ex);
        }

        [Fact]
        public void BeEnumerableWhenSuccess()
        {
            ResultEx<int> successResult = F.Success(10);
            (from val in successResult select val).First().Should().Be(10);
            (from val in successResult select val).Count().Should().Be(1);
        }

        [Fact]
        public void BeEnumerableWhenError()
        {
            ResultEx<int> errorResult = F.Error(new Exception());
            (from val in errorResult select val).Count().Should().Be(0);
        }

        [Fact]
        public void BeEquatableWhenContainsSuccessValue()
        {
            ResultEx<int> successResult1 = F.Success(10);
            ResultEx<int> successResult2 = F.Success(10);

            (successResult1.Equals(successResult2)).Should().BeTrue();
            (successResult1 == successResult2).Should().BeTrue();

            ResultEx<int> successResult3 = F.Success(20);
            (successResult1 != successResult3).Should().BeTrue();
        }

        [Fact]
        public void BeEquatableWhenContainsSuccessObject()
        {
            var obj = new List<string>();
            ResultEx<List<string>> successResult1 = F.Success(obj);
            ResultEx<List<string>> successResult2 = F.Success(obj);

            (successResult1.Equals(successResult2)).Should().BeTrue();
            (successResult1 == successResult2).Should().BeTrue();

            ResultEx<List<string>> successResult3 = F.Success(new List<string>());
            (successResult1 != successResult3).Should().BeTrue();
        }

        [Fact]
        public void BeEquatableWhenContainsError()
        {
            var obj = new Exception("hello");
            ResultEx<int> errorResult1 = F.Error(obj);
            ResultEx<int> errorResult2 = F.Error(obj);

            (errorResult1.Equals(errorResult2)).Should().BeTrue();
            (errorResult1 == errorResult2).Should().BeTrue();

            ResultEx<int> errorResult3 = F.Error(new Exception("hi"));
            (errorResult1 != errorResult3).Should().BeTrue();
        }
    }
}