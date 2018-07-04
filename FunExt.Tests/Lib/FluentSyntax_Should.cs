using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

using FunExt.Lib;
using static FunExt.Lib.F;

namespace FunExt.Tests.Lib
{
    public class FluentSyntax_Should
    {

        [Fact]
        void WorkWithIDisposable()
        {
            var test = new DisposableTest(100);
            test.IsDisposed.Should().BeFalse();

            var result = test
                .PipeAndDispose(x => x.Value + 10);

            result.Should().Be(110);
            test.IsDisposed.Should().BeTrue();
        }


        [Fact]
        void PipeValueToFunction()
        {
            var result = (10)
                .Pipe(x => x + 2)
                .Pipe(x => x * 12);

            result.Should().Be(144);
        }


        [Fact]
        void PipeObjectToFunction()
        {
            var result = new List<string>() { "Ahoj", "Dodo" }
                .Pipe(l => l.First())
                .Pipe(s => s.ToLower());

            result.Should().Be("ahoj");
        }

        // ---------- Test class ----------

        private class DisposableTest : IDisposable
        {
            public DisposableTest(int value)
            {
                IsDisposed = false;
                Value = value;
            }

            public readonly int Value;

            public bool IsDisposed { get; private set; }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }

    }
}
