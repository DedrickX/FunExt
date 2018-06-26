using LanguageExt;
using static LanguageExt.Prelude;

namespace ConsoleTest
{
    public static class LanguageExt
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