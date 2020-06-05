using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace ProjectEulerWebApp
{
    internal static class Initializer
    {
        internal static void Main()
        {
            Console.WriteLine("Starting Initialization...");
            try
            {
                TestEulerAnswers();
            }
            catch (Win32Exception)
            {
                Console.WriteLine("ProjectEulerAnswers is not installed.");
                DownloadEulerAnswers();
                InstallEulerAnswers();
                TestEulerAnswers();
            }
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
            File.Delete("ProjectEulerAnswers-Installer.exe");
            Console.WriteLine("Installation Completed.");
        }

        private static void TestEulerAnswers()
        {
            Console.WriteLine("Testing...");
            var answers = new List<string>
                          {
                              "C#: " + StartProcess("Euler", "1"),
                              "C++: " + StartProcess("Euler", "c 1"),
                              "Python: " + StartProcess("Euler", "py 1"),
                              "Java: " + StartProcess("ProjectEulerAnswers-Java", "1")
                          };
            answers.ForEach(Console.Write);
        }

        private static string StartProcess(string exe, string arguments)
        {
            var start = new ProcessStartInfo
                        {
                            FileName = exe,
                            Arguments = arguments,
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            WorkingDirectory = @"C:\Program Files\ProjectEulerAnswers\bin",
                            RedirectStandardOutput = true
                        };
            using var process = Process.Start(start);
            using var reader = process?.StandardOutput;
            return reader?.ReadToEnd();
        }
    }
}