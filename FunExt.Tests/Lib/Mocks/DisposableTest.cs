using System;

namespace FunExt.Tests.Lib
{
    public class DisposableTest : IDisposable
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
