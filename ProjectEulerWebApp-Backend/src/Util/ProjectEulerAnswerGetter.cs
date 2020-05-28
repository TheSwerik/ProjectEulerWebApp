using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProjectEulerWebApp.Util
{
    public static class ProjectEulerAnswerGetter
    {
        public static Dictionary<string, long> Solve(int id)
        {
            var result = new Dictionary<string, long>();
            //TODO call the packaged Jar and Exe files
            //TODO get from https://github.com/TheSwerik/ProjectEulerAnswers/releases
            var java = GetJava(id);
            if (java != -1) result.Add("Java", java);

            var csharp = GetCSharp(id);
            if (csharp != -1) result.Add("C#", csharp);

            var cpp = GetCPlusPlus(id);
            if (cpp != -1) result.Add("C++", cpp);

            var python = GetPython(id);
            if (python != -1) result.Add("Python", python);

            return result;
        }

        private static long GetJava(int id)
        {
            var start = new ProcessStartInfo
                        {
                            FileName = "ProjectEulerAnswers-Java.exe",
                            Arguments = id + "",
                            UseShellExecute = false,
                            RedirectStandardOutput = true
                        };
            using var process = Process.Start(start);
            using var reader = process.StandardOutput;
            Console.WriteLine(reader.ReadToEnd());
            return -1;
        }

        private static long GetCSharp(int id)
        {
            var start = new ProcessStartInfo
                        {
                            FileName = "Euler.exe",
                            Arguments = id + "",
                            UseShellExecute = false,
                            RedirectStandardOutput = true
                        };
            using var process = Process.Start(start);
            using var reader = process.StandardOutput;
            Console.WriteLine(reader.ReadToEnd());
            return -1;
        }

        private static long GetCPlusPlus(int id)
        {
            var start = new ProcessStartInfo
                        {
                            FileName = "Euler.exe",
                            Arguments = "cpp " + id,
                            UseShellExecute = false,
                            RedirectStandardOutput = true
                        };
            using var process = Process.Start(start);
            using var reader = process.StandardOutput;
            Console.WriteLine(reader.ReadToEnd());
            return -1;
        }

        private static long GetPython(int id)
        {
            var start = new ProcessStartInfo
                        {
                            FileName = "Euler.exe",
                            Arguments = "py " + id,
                            UseShellExecute = false,
                            RedirectStandardOutput = true
                        };
            using var process = Process.Start(start);
            using var reader = process.StandardOutput;
            Console.WriteLine(reader.ReadToEnd());
            return -1;
        }
    }
}