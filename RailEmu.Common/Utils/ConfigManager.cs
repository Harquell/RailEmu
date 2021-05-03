using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailEmu.Common.Utils
{
    public static class ConfigManager
    {
        public const string Version = "0.1 (2.3.7)";


        public static Dictionary<string, string> GetConfig(string config = ".\\config.txt")
        {
            Dictionary<string, string> output = new Dictionary<string, string>();
            if (File.Exists(config))
            {
                string[] lines = File.ReadAllLines(config);
                foreach (string line in lines)
                {
                    if (line.Length > 0)
                    {
                        if (line.StartsWith("#")) continue;
                        int delimiterPos = line.IndexOf(':');
                        if (delimiterPos == -1) continue;
                        int valueLength = line.Length - 1 - delimiterPos;

                        string key = line.Substring(0, delimiterPos).Trim();
                        string value = line.Substring(delimiterPos+1, valueLength).Trim();
                        output.Add(key, value);
                    }
                }
            }
            return output;
        }

        public static string GetConfigValue(string key, string config = ".\\config.txt")
        {
            Dictionary<string, string> Config = GetConfig(config);

            if (!Config.ContainsKey(key)) return null;

            Config.TryGetValue(key, out string value);
            return value;
        }

        public static bool CheckConfig(Dictionary<string, string> config)
        {
            bool success = true;
            string[] keys = new string[]
            {
                "ProtocolVersion",
                "Maintenance",
                "Database_Host",
                "Database_User",
                "Database_Password",
                "Database_Auth",
                "Database_World"
            };

            foreach(string key in keys)
            {
                if (config.ContainsKey(key))
                {
                    Out.Debug($"CONFIG - {key} = {config[key]}");
                    continue;
                }
                Out.Error($"Error while loading config.\n" +
                    $"Can't found key [{key}]");
                success = false;
            }
            Out.Debug("Config checking ended");
            return success;
        }
    }
}
