using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace MCLauncherUpdater
{
    class Program
    {
        public static string currentPath = Directory.GetCurrentDirectory();
        public static string updatePath = $"{currentPath}\\.codexipsa\\update.cfg";

        public static string updateUrl;

        static void Main(string[] args)
        {
            Console.Title = "MineC#raft Launcher updater v2.0";
            Console.WriteLine("MineC#raft Launcher updater v2.0");
            Console.WriteLine("--------------------------------");
            
            if(File.Exists(updatePath))
            {
                updateUrl = File.ReadAllText(updatePath);

                try
                {
                    using (var client = new WebClient())
                    {
                        //Console.WriteLine($"[Updater] Url: {updateUrl}");
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
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Update successful!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Press enter to close this window and run the launcher.");
                    Console.ReadKey();
                    System.Diagnostics.Process.Start("MCLauncher.exe");
                }
                catch (WebException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("A web exception error has occured! (Error code 1)");
                    Console.WriteLine("Please connect to the internet and try again.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Press enter to close this window.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[Updater] Update.cfg not found! (Error code: 2)");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Press enter to close this window.");
                Console.ReadKey();
            }
        }
    }
}
