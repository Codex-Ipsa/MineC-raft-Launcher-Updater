using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace MCLauncherUpdater
{
    class Program
    {
        public static string currentPath = Directory.GetCurrentDirectory();
        public static string updateUrl;

        static void Main(string[] args)
        {
            Console.Title = "MineC#raft Launcher updater v2.2";
            Console.WriteLine("MineC#raft Launcher updater v2.2");
            Console.WriteLine("--------------------------------");

            foreach (var process in Process.GetProcessesByName("MCLauncher"))
            {
                process.Kill();
            }

            if (args == null || args.Length == 0)
            {
                logError("No -url parameter! (Error code 3)");
                //no args
            }
            else
            {
                if (args[0] == "-url")
                {
                    updateUrl = args[1];

                    try
                    {
                        using (var client = new WebClient())
                        {
                            client.DownloadFile(updateUrl, $"{currentPath}\\launcher.zip");

                            if (File.Exists($"{currentPath}\\MCLauncher.exe"))
                                File.Delete($"{currentPath}\\MCLauncher.exe");
                            if (File.Exists($"{currentPath}\\Newtonsoft.Json.dll"))
                                File.Delete($"{currentPath}\\Newtonsoft.Json.dll");

                            string zipPath = $"{currentPath}\\launcher.zip";
                            string extractPath = currentPath;
                            ZipFile.ExtractToDirectory(zipPath, extractPath);

                            File.Delete(currentPath + "\\launcher.zip");
                        }
                        logMessage("Update successful!");
                        System.Diagnostics.Process.Start("MCLauncher.exe");
                    }
                    catch (WebException)
                    {
                        logError("A web exception error has occured! (Error code 1)\nPlease connect to the internet and try again.");
                    }
                }
            }
        }

        public static void logMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Press any key to close this window.");
            Console.ReadKey();
        }

        public static void logError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Press any key to close this window.");
            Console.ReadKey();
        }
    }
}
