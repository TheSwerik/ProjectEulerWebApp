using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace Initializer
{
    internal static class Initializer
    {
        internal static void Main(string[] args)
        {
            Console.WriteLine("Starting Initialization...");
            if (TestEulerAnswers()) return;
            DownloadEulerAnswers();
            InstallEulerAnswers();
            TestEulerAnswers();
        }

        private static void DownloadEulerAnswers()
        {
            Console.WriteLine("Starting Download...");
            using (var client = new WebClient())
            {
                client.Headers.Add("user-agent", "Anything");
                client.DownloadFile(
                    "https://github.com/TheSwerik/ProjectEulerAnswers/releases/download/1.0.1/ProjectEulerAnswers.exe",
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
            Process.Start(start).WaitForExit();
            Console.WriteLine("Installation Completed.");
        }

        private static bool TestEulerAnswers()
        {
            Console.WriteLine("Testing...");
            var answers = new List<string>
                          {
                              "C#: " + StartProcess("Euler.exe", "1"),
                              "C++: " + StartProcess("Euler.exe", "c 1"),
                              "Python: " + StartProcess("Euler.exe", "py 1"),
                              "Java: " + StartProcess("ProjectEulerAnswers-Java", "1")
                          };
            answers.ForEach(Console.Write);

            return answers[0].Trim().Trim('\n').Length > 0 ||
                   answers[1].Trim().Trim('\n').Length > 0 ||
                   answers[2].Trim().Trim('\n').Length > 0 ||
                   answers[3].Trim().Trim('\n').Length > 0;
        }

        private static string StartProcess(string exe, string arguments)
        {
            var start = new ProcessStartInfo
                        {
                            FileName = exe,
                            Arguments = arguments,
                            UseShellExecute = false,
                            RedirectStandardOutput = true
                        };
            using var process = Process.Start(start);
            using var reader = process.StandardOutput;
            return reader.ReadToEnd();
        }
    }
}