using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

using FunExt.Lib;
using static FunExt.Lib.F;

namespace FunExt.Tests.Lib
{
    public class FuncTypeInference_Should
    {

        [Fact]
        public void ConvertActionToFunction()
        {
            var f0 = Fun(() => Console.WriteLine("ConvertActionToFunction"));
            var f1 = Fun((int x1) => Console.WriteLine("ConvertActionToFunction"));
            var f2 = Fun((int x1, int x2) => Console.WriteLine("ConvertActionToFunction"));
            var f3 = Fun((int x1, int x2, int x3) => Console.WriteLine("ConvertActionToFunction"));
            var f4 = Fun((int x1, int x2, int x3, int x4) => Console.WriteLine("ConvertActionToFunction"));

            f0.Should().BeOfType<Func<Unit>>();
            f1.Should().BeOfType<Func<int, Unit>>();
            f2.Should().BeOfType<Func<int, int, Unit>>();
            f3.Should().BeOfType<Func<int, int, int, Unit>>();
            f4.Should().BeOfType<Func<int, int, int, int, Unit>>();
        }

        [Fact]
        public void InferFunctionTypeForVariable()
        {
            // This won't compile...
            // var fAdd = (int x1, int x2) => x1 + x1;
            // "Cannot assign lambda expression to an implicitly-typed variable"

            // So this is (bit ugly) solution:
            var f0 = Fun(() => 1);
            var f1 = Fun((int x1) => x1);
            var f2 = Fun((int x1, int x2) => x1 + x2);
            var f3 = Fun((int x1, int x2, int x3) => x1 + x2 + x3);
            var f4 = Fun((int x1, int x2, int x3, int x4) => x1 + x2 + x3 + x4);

            f0.Should().BeOfType<Func<int>>();
            f1.Should().BeOfType<Func<int, int>>();
            f2.Should().BeOfType<Func<int, int, int>>();
            f3.Should().BeOfType<Func<int, int, int, int>>();
            f4.Should().BeOfType<Func<int, int, int, int, int>>();

            var result = f0() + f1(4) + f2(2, 3) + f3(2, 3, 5) + f4(1, 2, 3, 4);
            result.Should().Be(30);
        }

    }
}
