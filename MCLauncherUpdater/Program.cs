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
                if (args[2] == "-updInst")
                {
                    updateInstances();


                    Console.ReadLine(); //remove this
                }

                /*if (args[0] == "-url")
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
                }*/
            }
        }

        public static void updateInstances()
        {
            string[] dirs = Directory.GetDirectories(".codexipsa\\instance\\");
            foreach (string dir in dirs)
            {
                Console.WriteLine(dir);
                int index = dir.LastIndexOf("\\") + 1;
                string name = dir.Substring(index, dir.Length - index);

                if (File.Exists($"{dir}\\instance.cfg"))
                {
                    string text = File.ReadAllText($"{dir}\\instance.cfg");
                    Console.WriteLine(text);
                    if(text.Contains("classroom"))
                    {
                        text = text.Replace($"[\n{{", $"[\n{{\n\"name\":\"{name}\",\n\"edition\":\"MinecraftEdu\",");
                    }
                    else
                    {
                        text = text.Replace($"[\n{{", $"[\n{{\n\"name\":\"{name}\",\n\"edition\":\"Java Edition\",");
                    }
                    text = text.Replace("instVer", "version");
                    text = text.Replace("instType", "type");
                    text = text.Replace("instUrl", "url");
                    text = text.Replace("instDir", "directory");
                    text = text.Replace("instResWidth", "resolutionX");
                    text = text.Replace("instResHeight", "resolutionY");
                    text = text.Replace("instRamMin", "ramMin");
                    text = text.Replace("instRamMax", "ramMax");
                    text = text.Replace("useCustJava", "useCustomJava");
                    text = text.Replace("instCustJava", "customJava");
                    text = text.Replace("useCustJvm", "useJvmArgs");
                    text = text.Replace("instCustJvm", "jvmArgs");
                    text = text.Replace("useCustMethod", "useLaunchMethod");
                    text = text.Replace("instCustMethod", "launchMethod");
                    text = text.Replace("\"useCustJar\":\"False\",", String.Empty);
                    text = text.Replace("\"instCustJar\":\"\",", String.Empty);
                    text = text.Replace("useOfflineMode", "offlineMode");
                    Console.WriteLine(text);
                    Console.WriteLine();
                    //File.WriteAllText("test.txt", text);
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
