using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace ProjectEulerWebApp
{
    internal static class Initializer
    {
        internal static void Main(string[] args)
        {
            Console.WriteLine("Starting Initialization...");
            // if (TestEulerAnswers()) return;
            // DownloadEulerAnswers();
            // InstallEulerAnswers();
            TestEulerAnswers();
        }

        private static void DownloadEulerAnswers()
        {
            Console.WriteLine("Starting Download...");
            using (var client = new WebClient())
            {
                client.Headers.Add("user-agent", "Anything");
                client.DownloadFile(
                    "https://github.com/TheSwerik/ProjectEulerAnswers/releases/download/1.0.4/ProjectEulerAnswers.exe",
                    "ProjectEulerAnswers-Installer.exe");
            }

            Console.WriteLine("Download Completed.");
        }

        private static void InstallEulerAnswers()
        {
            Console.WriteLine("Starting Installation...");
            var start = new ProcessStartInfo
                        {
                            FileName = "ProjectEulerAnswers-Installer.exe",
                            Arguments = "/verysilent /SUPPRESSMSGBOXES",
                            UseShellExecute = false,
                            RedirectStandardOutput = true
                        };
            Process.Start(start)?.WaitForExit();
            Console.WriteLine("Installation Completed.");
        }

        private static bool TestEulerAnswers()
        {
            Console.WriteLine("Testing...");
            var answers = new List<string>
                          {
                              "C#: " + StartProcess("Euler", "1"),
                              "C++: " + StartCppProcess("1"),
                              "Python: " + StartProcess("Euler", "py 1"),
                              "Java: " + StartProcess("ProjectEulerAnswers-Java", "1")
                          };
            answers.ForEach(Console.Write);

            return answers.Any(a => a.Trim().Trim('\n').Length > 0);
        }

        private static string StartProcess(string exe, string arguments)
        {
            var start = new ProcessStartInfo
                        {
                            FileName = exe,
                            Arguments = arguments,
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            RedirectStandardOutput = true
                        };
            using var process = Process.Start(start);
            using var reader = process.StandardOutput;
            return reader.ReadToEnd();
        }

        private static string StartCppProcess(string arguments)
        {
            var start = new ProcessStartInfo
                        {
                            FileName = @"C:\Program Files\Git\git-bash.exe",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            RedirectStandardInput = true
                        };
            using var process = Process.Start(start);
            using var reader = process.StandardOutput;
            using var writer = process.StandardInput;
            writer.Write("Euler c " + arguments);
            writer.Flush();
            writer.Close();
            return reader.ReadToEnd();
        }
    }
}