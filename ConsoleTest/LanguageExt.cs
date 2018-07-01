using LanguageExt;
using System;
using static LanguageExt.Prelude;

namespace ConsoleTest
{
    public class LanguageExt
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

        void FunctionInference()
        {
        }


        // ---------- Actions ----------


        void Action0()
        {
            // do some side effects
            Console.WriteLine("Action0()");
        }


        void Action1(string x1)
        {
            // do some side effects
            Console.WriteLine($"Action1({x1})");
        }


        void Action2(string x1, string x2)
        {
            // do some side effects
            Console.WriteLine($"Action1({x1}, {x2})");
        }


        void Action3(string x1, string x2, string x3)
        {
            // do some side effects
            Console.WriteLine($"Action1({x1}, {x2}, {x3})");
        }


        void Action4(string x1, string x2, string x3, string x4)
        {
            // do some side effects
            Console.WriteLine($"Action1({x1}, {x2}, {x3}, {x4})");
        }


        // ---------- Functions ----------


        int Function0() =>
            0;


        int Function1(int x1) =>
            x1 + 1;


        int Function2(int x1, int x2) =>
            x1 + x2 + 2;


        int Function3(int x1, int x2, int x3) =>
            x1 + x2 + x3 + 3;


        int Function4(int x1, int x2, int x3, int x4) =>
            x1 + x2 + x3 + x4 + 4;

    }

}