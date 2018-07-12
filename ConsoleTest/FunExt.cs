using FunExt;
using static FunExt.F;

namespace ConsoleTest
{
    public static class FunExt
    {

        public static void Compare()
        {
            var x = Some("x");
            var n = None;

            var b = (x == n);
        }

        public static Option<string> ReadFile(string path) =>
            !string.IsNullOrEmpty(path) ? Some("text") :
            None;

    }
}