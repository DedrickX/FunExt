using System;
using Xunit;
using FluentAssertions;

using FunExt.Lib;
using System.Linq;

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
    }
}