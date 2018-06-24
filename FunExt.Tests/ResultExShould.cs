using System;
using Xunit;
using FluentAssertions;

using FunExt.Lib;
using System.Linq;

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
    }
}