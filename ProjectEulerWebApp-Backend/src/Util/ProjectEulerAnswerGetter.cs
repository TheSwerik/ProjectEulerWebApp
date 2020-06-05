using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
            if (csharp[0] != -1) result.Add("C#", csharp[1]);

            var java = ParseResult("ProjectEulerAnswers-Java", "" + id);
            if (java[0] != -1) result.Add("Java", java[1]);

            var cpp = ParseResult("Euler", "c " + id);
            if (cpp[0] != -1) result.Add("C++", cpp[1]);

            var python = ParseResult("Euler", "py " + id);
            if (python[0] != -1) result.Add("Python", python[1]);
            // Console.WriteLine("C#: " + csharp[0] + "    " + csharp[1] + "ms");
            // Console.WriteLine("JAVA: " + java[0] + "    " + java[1] + "ms");
            // Console.WriteLine("C++: " + cpp[0] + "    " + cpp[1] + "ms");
            // Console.WriteLine("PYTHON: " + python + "ms");

            return result;
        }

        private static string StartProcess(string exe, string args)
        {
            var start = new ProcessStartInfo
                        {
                            FileName = exe,
                            Arguments = args,
                            UseShellExecute = false,
                            WorkingDirectory = @"C:\Program Files\ProjectEulerAnswers\bin",
                            RedirectStandardOutput = true
                        };
            using var process = Process.Start(start);
            using var reader = process?.StandardOutput;
            return reader?.ReadToEnd();
        }

        private static long[] ParseResult(string exe, string args)
        {
            var output = StartProcess(exe, args);
            if (string.IsNullOrWhiteSpace(output) || output.Contains("Cannot") || output.Contains("not valid"))
                return new[] {-1L};
            var ms = output.Contains("ms");
            var result = output.Replace("Result:", "")
                               .Replace("ms", "")
                               .Replace("s", "")
                               .Split("Time:");
            Console.WriteLine(output);
            return new[] {long.Parse(result[0]), (long) (double.Parse(result[1]) * (ms ? 1000 : 1000000))};
        }
    }
}