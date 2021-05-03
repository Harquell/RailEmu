using RailEmu.Auth.Network;
using RailEmu.Protocol.Messages;
using RailEmu.Common.Database.Manager;
using RailEmu.Common.Utils;
using RailEmu.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RailEmu.Common.Database;

namespace RailEmu.Launcher
{
    class Program
    {
        static string ip = "127.0.0.1";
        static int port = 443;
        static AuthServer auth;
        static WorldServer world;

        static void Main(string[] args)
        {
            Out.Initialize();
            auth = new AuthServer();
            world = new WorldServer();

            auth.Initialize(ip, port);
            ExecuteCommand();
        }

        static void HandleCommand(string[] cmd)
        {
            switch (cmd[0])
            {
                case "rl":
                    ServersManager.Initialize(DatabaseManager.connection);
                    auth.Servers = ServersManager.GetServers();
                    foreach (AuthClient client in auth.Clients.Values)
                    {
                        foreach(Common.Database.Modeles.Auth.WorldServer s in auth.Servers.Values)
                        {
                            client.Send(new ServerStatusUpdateMessage(new Protocol.Types.GameServerInformations(s.ServerId, s.Status, s.Completion, true, 14)));
                        }
                    }
                    break;
                case "test":
                    Out.WriteLine("TEST !!!", ConsoleColor.Yellow);
                    break;
                case "exit":
                    auth.Dispose();
                    Out.WriteLine("Auth server stopped successfully", ConsoleColor.Cyan);
                    break;
                case "start":
                    if (auth.Clients != null)
                    {
                        Out.Error("Server already started");
                        break;
                    }
                    auth.Initialize(ip, port);
                    break;
                case "world":
                    if (cmd.Length >1 && cmd[1] == "start")
                        world = new WorldServer();
                    break;
                default:
                    Out.WriteLine($"Unknown command {cmd[0]}", ConsoleColor.Red);
                    break;
            }
        }

        static void ExecuteCommand()
        {
            while (true)
            {
                string[] cmd = Console.ReadLine().Split(' ');
                HandleCommand(cmd);
            }
        }
    }
}
