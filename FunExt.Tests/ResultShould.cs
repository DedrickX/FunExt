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
            Result<int> successResult = F.Success(10);

            successResult.IsSuccess.Should().BeTrue();
            successResult.IsFailure.Should().BeFalse();

            successResult.Match(
                ifSuccess: x => x.Should().Be(10),
                ifError: ex => throw new Exception());
        }


        [Fact]
        public void BeUsedWithErrorType()
        {
            var err = new Exception("hello");
            Result<int> errorResult = err;

            errorResult.IsSuccess.Should().BeFalse();
            errorResult.IsFailure.Should().BeTrue();

            errorResult.Match(
                ifSuccess: x => throw new Exception(),
                ifError: ex => ex.Should().BeSameAs(err));
        }


        [Fact]
        public void BeEnumerableWhenSuccess()
        {
            Result<int> successResult = F.Success(10);
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
            Result<int> successResult1 = F.Success(10);
            Result<int> successResult2 = F.Success(10);

            (successResult1.Equals(successResult2)).Should().BeTrue();
            (successResult1 == successResult2).Should().BeTrue();

            Result<int> successResult3 = F.Success(20);
            (successResult1 != successResult3).Should().BeTrue();
        }

        [Fact]
        public void BeEquatableWhenContainsSuccessObject()
        {
            var obj = new List<string>();
            Result<List<string>> successResult1 = F.Success(obj);
            Result<List<string>> successResult2 = F.Success(obj);

            (successResult1.Equals(successResult2)).Should().BeTrue();
            (successResult1 == successResult2).Should().BeTrue();

            Result<List<string>> successResult3 = F.Success(new List<string>());
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
            Result<string> x = !string.IsNullOrEmpty(path) ? F.Success("text")
                : new Exception("Invalid path!");
            x.IsSuccess.Should().BeTrue();
        }

    }
}