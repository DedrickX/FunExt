using System;
using Xunit;
using FluentAssertions;

using FunExt.Lib;

namespace FunExt.Tests
{
    public class ResultShould
    {
        [Fact]
        public void BeUsedWithSuccessType()
        {
            Result<int, Exception> success = F.Success(10);

            success.IsSuccess.Should().Be(true);
            success.IsError.Should().Be(false);
            success.GetSuccess().Should().Be(10);
        }

        [Fact]
        public void BeUsedWithSomeType()
        {
            Result<int, Exception> success = F.Some(10);
            success.GetSuccess().Should().Be(10);
        }

        [Fact]
        public void BeUsedWithErrorType()
        {
            var err = new Exception("hello");
            Result<int, Exception> error = F.Error(err);

            error.IsSuccess.Should().Be(false);
            error.IsError.Should().Be(true);
            error.GetError().Should().BeSameAs(err);
        }

        [Fact]
        public void ThrowExceptionWhenAccessingErrorOnSuccess()
        {
            Result<int, Exception> result = F.Success(10);
            result
                .Invoking(x => x.GetError())
                .Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void ThrowExceptionWhenAccessingSuccessOnError()
        {
            Result<int, Exception> result = F.Error(new Exception("hello"));
            result
                .Invoking(x => x.GetSuccess())
                .Should().Throw<InvalidOperationException>();
        }
    }
}