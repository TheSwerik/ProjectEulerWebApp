using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Threading;

namespace ProjectEulerWebApp.Util
{
    public static class ProjectEulerAnswerGetter
    {
        public static Dictionary<string, long> Solve(int id)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var result = new Dictionary<string, long>();

            var csharp = ParseResult("Euler", "" + id);
            if (csharp[0] != -1)
            {
                Console.WriteLine("C#: " + csharp[0] + "    " + csharp[1]);
                result.Add("C#", csharp[1]);
            }

            var java = ParseResult("ProjectEulerAnswers-Java", "" + id);
            if (java[0] != -1)
            {
                Console.WriteLine("JAVA: " + java[0] + "    " + java[1]);
                result.Add("Java", java[1]);
            }

            var cpp = ParseResult("Euler", "c " + id);
            if (cpp[0] != -1)
            {
                Console.WriteLine("C++: " + cpp[0] + "    " + cpp[1]);
                result.Add("C++", cpp[1]);
            }

            var python = GetPython(id);
            if (python != -1) result.Add("Python", python);
            Console.WriteLine("PYTHON: " + python);

            return result;
        }

        private static long GetPython(int id)
        {
            var output = StartProcess("Euler.exe", "py " + id);
            if (output == null) return -1;
            return DateTime.Parse(output).Millisecond * 1000;
        }

        private static string StartProcess(string exe, string args)
        {
            var start = new ProcessStartInfo
                        {
                            FileName = exe,
                            Arguments = args,
                            UseShellExecute = false,
                            RedirectStandardOutput = true
                        };
            using var process = Process.Start(start);
            using var reader = process?.StandardOutput;
            return reader?.ReadToEnd();
        }

        private static long[] ParseResult(string exe, string args)
        {
            var output = StartProcess(exe, args);
            if (output == null) return new[] {-1L};
            var ms = output.Contains("ms");
            var result = output.Replace("Result:", "")
                               .Replace("ms", "")
                               .Replace("s", "")
                               .Split("Time:");
            return new[] {long.Parse(result[0]), (long) (double.Parse(result[1]) * (ms ? 1000 : 1000000))};
        }
    }
}