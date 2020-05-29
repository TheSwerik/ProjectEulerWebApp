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
            //TODO call the packaged Jar and Exe files
            //TODO get from https://github.com/TheSwerik/ProjectEulerAnswers/releases
            var csharp = StartProcess("Euler", "" + id);
            if (csharp[0] != -1)
            {
                Console.WriteLine("C#: " + csharp[0] + "    " + csharp[1]);
                result.Add("C#", csharp[1]);
            }

            var java = StartProcess("ProjectEulerAnswers-Java", "" + id);
            if (java[0] != -1)
            {
                Console.WriteLine("JAVA: " + java[0] + "    " + java[1]);
                result.Add("Java", java[1]);
            }

            var cpp = GetCPlusPlus(id);
            if (cpp != -1) result.Add("C++", cpp);

            var python = GetPython(id);
            if (python != -1) result.Add("Python", python);

            return result;
        }

        private static long GetCPlusPlus(int id)
        {
            Console.WriteLine("c++: " + StartProcess("Euler.exe", "c " + id));
            return -1;
        }

        private static long GetPython(int id)
        {
            var output = StartProcess("Euler.exe", "py " + id);
            
            Console.WriteLine("PYTHON:");
            Console.WriteLine(output);
            Console.WriteLine(DateTime.Parse(output).ToLongTimeString());
            return -1;
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
            var output = StartProcess(exe,args);
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