using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RailEmu.Common.Utils
{
    public static class Out
    {
        public static bool DebugMode = false;


        private static void Header()
        {
            #region Logo
            string Logo = $@"
                        __________        .__.__  ___________              
                        \______   \_____  |__|  | \_   _____/ _____  __ __ 
                         |       _/\__  \ |  |  |  |    __)_ /     \|  |  \
                         |    |   \ / __ \|  |  |__|        \  Y Y  \  |  /
                         |____|_  /(____  /__|____/_______  /__|_|  /____/ 
                                \/      \/                \/      \/       
                                                       
                                                        Emulator V {ConfigManager.Version}";
            #endregion
            WriteLine(Logo, ConsoleColor.Green);
        }

        public static void Initialize()
        {
            Console.Title = $"RailEmu " +
                   $"[{(DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\ hh\:mm\:ss")}]";
            Timer timer = new Timer(1000);
            Header();
            timer.Elapsed += (sender, e) =>
            {
                Console.Title = $"RailEmu " +
                    $"[{(DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\ hh\:mm\:ss")}]";
            };
            timer.Start();
        }

        public static void Debug(string msg, string prefix = "DEBUG")
        {
            if (DebugMode)
                WriteLine($"{prefix,8} > {msg}", ConsoleColor.DarkGreen);
        }
        public static void Warn(string msg) =>
            WriteLine($"{"WARN",8} > {msg}", ConsoleColor.Yellow);
        public static void Error(string msg) =>
            WriteLine($"{"ERROR",8} > {msg}", ConsoleColor.Red);
        public static void Info(string msg, ConsoleColor color = ConsoleColor.Green) =>
            WriteLine($"{"",8} > {msg}", color);


        public static void WriteLine(string msg, ConsoleColor color = ConsoleColor.Gray)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = old;
        }
        public static void Write(string msg, ConsoleColor color = ConsoleColor.Gray)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(msg);
            Console.ForegroundColor = old;
        }
    }
}
