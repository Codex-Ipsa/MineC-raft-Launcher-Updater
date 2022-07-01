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
        static void Main(string[] args)
        {
            Console.Title = "MineC#raft Launcher Dev updater v1.2";
            Console.WriteLine("MineC#raft Launcher Dev updater v1.2");
            Console.WriteLine("------------------------");
            try
            {
                using (var client = new WebClient())
                {
                    string currentPath = Directory.GetCurrentDirectory();
                    client.DownloadFile("http://codex-ipsa.dejvoss.cz/MCL-Data/launcher/launcherDev.zip", currentPath + "\\launcherDev.zip");

                    if (File.Exists(currentPath + "\\MCLauncher.exe"))
                        File.Delete(currentPath + "\\MCLauncher.exe");
                    if (File.Exists(currentPath + "\\Newtonsoft.Json.dll"))
                        File.Delete(currentPath + "\\Newtonsoft.Json.dll");

                    string zipPath = currentPath + "\\launcherDev.zip";
                    string extractPath = currentPath;
                    ZipFile.ExtractToDirectory(zipPath, extractPath);

                    File.Delete(currentPath + "\\launcherDev.zip");

                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Update successful!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Press enter to close this window and run the launcher.");
                Console.ReadKey();
                System.Diagnostics.Process.Start("CMD.exe", $"/C MCLauncher.exe");
            }
            catch(WebException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("A web exception error has occured! (Error code 1)");
                Console.WriteLine("Please connect to the internet and try again.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Press enter to close this window.");
                Console.ReadKey();

            }
        }
    }
}
