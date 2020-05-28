using System;
using System.Diagnostics;
using System.Net;

namespace Initializer
{
    internal static class Initializer
    {
        internal static void Main(string[] args)
        {
            Console.WriteLine("Starting Initialization...");
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

        private static void TestEulerAnswers()
        {
            Console.WriteLine("Testing...");
            var start = new ProcessStartInfo
                        {
                            FileName = "Euler.exe",
                            Arguments = "1",
                            UseShellExecute = false,
                            RedirectStandardOutput = true
                        };
            using var process = Process.Start(start);
            using var reader = process.StandardOutput;
            Console.WriteLine(reader.ReadToEnd());
            Console.WriteLine("Testing Completed.");
        }
    }
}