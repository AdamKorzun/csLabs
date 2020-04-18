using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using dll;

namespace lab4
{
    class Program
    {
        static void PrintMenu()
        {
            Console.WriteLine("1 - CPU");
            Console.WriteLine("2 - GPU");
            Console.WriteLine("3 - RAM");
            Console.WriteLine("4 - General");
            Console.WriteLine("5 - Exit");
            Console.Write("Option: ");
        }
        static string GetGPUInfo(Process cmd)
        {
            StringBuilder gpuInfo = new StringBuilder();
            cmd.StartInfo.FileName = "wmic.exe";   
            cmd.StartInfo.Arguments = "PATH Win32_VideoController get  Name\n";
            
            cmd.Start();

            gpuInfo.Append(cmd.StandardOutput.ReadToEnd());
            cmd.StartInfo.Arguments = "PATH Win32_VideoController get VideoModeDescription\n";
            cmd.Start();
            gpuInfo.Append(cmd.StandardOutput.ReadToEnd());
           
            return gpuInfo.ToString(); 

        }
        static string GetRAMInfo(Process cmd)
        {
            
            StringBuilder gpuInfo = new StringBuilder();
            cmd.StartInfo.FileName = "wmic.exe";
            cmd.StartInfo.Arguments = "MemoryChip get BankLabel, Capacity, MemoryType, TypeDetail, Speed\n";

            cmd.Start();

            gpuInfo.Append(cmd.StandardOutput.ReadToEnd());
            

            return gpuInfo.ToString();
        }
        static string GetCPUInfo(Process cmd)
        {
            StringBuilder gpuInfo = new StringBuilder();
            cmd.StartInfo.FileName = "wmic.exe";
            cmd.StartInfo.Arguments = "cpu get  Caption\n";

            cmd.Start();

            gpuInfo.Append(cmd.StandardOutput.ReadToEnd());
            cmd.StartInfo.Arguments = "cpu get loadpercentage\n";
            cmd.Start();
            gpuInfo.Append(cmd.StandardOutput.ReadToEnd());

            return gpuInfo.ToString();
        }
        static void Assignment1()
        {
            Process cmd = new Process();

            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;


            while (true)
            {

                Console.Clear();

                PrintMenu();
                ConsoleKeyInfo key = Console.ReadKey();
                Console.Clear();
                if (key.Key == ConsoleKey.D1)
                {
                    Console.WriteLine(GetCPUInfo(cmd));
                }
                else if (key.Key == ConsoleKey.D2)
                {

                    Console.WriteLine(GetGPUInfo(cmd));
                }
                else if (key.Key == ConsoleKey.D3)
                {
                    Console.WriteLine(GetRAMInfo(cmd));
                }
                else if (key.Key == ConsoleKey.D4)
                {
                    cmd.StartInfo.Arguments = "/c chcp 437 && systeminfo\n";
                    cmd.Start();
                    string generalPCInfo = cmd.StandardOutput.ReadToEnd();
                    Console.WriteLine(generalPCInfo);
                }
                else if (key.Key == ConsoleKey.D5)
                {
                    return;
                }
                Console.ReadKey();
            }
        }
        [DllImport("C:\\Users\\admin\\Desktop\\C#\\lab2\\Debug\\dllcalc.dll")]
        public static extern int AddNum(int x, int y);
        static void Assignment2() {
            
           AddNum(5, 7);
        }
        static void Main(string[] args)
        {
            //Assignment1();
            Assignment2();
        }
    }
}
