using System;
using Xunit;
using FluentAssertions;

using FunExt.Lib;

namespace FunExt.Tests
{
    public class ResultExShould
    {
        public void BeUsedWithSomeAndSuccessTypes()
        {
            ResultEx<int> success1 = F.Success(10);
            success1.GetSuccess().Should().Be(10);

            ResultEx<int> success2 = F.Some(11);
            success2.GetSuccess().Should().Be(11);
        }

        public void BeUsedWithError()
        {
            var ex = new Exception("error");
            ResultEx<int> error = F.Error(ex);
            error.IsError.Should().Be(true);
            error.IsSuccess.Should().Be(false);

            error.GetError().Should().BeSameAs(ex);
        }

        public void BeUsedWithException()
        {
            var ex = new Exception("error");
            ResultEx<int> error = ex;
            error.IsError.Should().Be(true);
            error.IsSuccess.Should().Be(false);

            error.GetError().Should().BeSameAs(ex);
        }
    }
}