using RailEmu.Common.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using RailEmu.Protocol.Messages;
using System.Net;
using RailEmu.Common.Utils;
using RailEmu.Common.Database;
using RailEmu.Common.Database.Manager;
using System.Timers;
using RailEmu.Common.Database.Modeles.Auth;
using System.IO;

namespace RailEmu.Auth.Network
{
    public class AuthServer : IDisposable
    {
        public static bool onMaintenance = false;
        public static int ProtocolVersion;

        public Dictionary<string, string> Config;
        public Socket socket { get; private set; }
        public Dictionary<Socket, IBaseClient> Clients;
        public Dictionary<int, WorldServer> Servers { get; set; }
        public List<string> BlockedIp { get; private set; }
        public string Ip { get; private set; }
        public int Port { get; private set; }
        public Timer Timer { get; private set; }

        public AuthServer() { }



        public void Initialize(string ip, int port)
        {
            DateTime startTime = DateTime.Now;

            Out.Info("Initializing server...");
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
                socket.Listen(100);
                Ip = ip;
                Port = port;
            }
            catch (SocketException e)
            {
                Out.Error(e.Message);
                Dispose();
                return;
            }
            Clients = new Dictionary<Socket, IBaseClient>();
            Servers = new Dictionary<int, WorldServer>();
            Out.Info("Loading Config...");
            if (!File.Exists("config.txt"))
            {
                Out.Error("Can't load 'config.txt' !");
                Dispose();
                return;
            }
            Config = ConfigManager.GetConfig();
            if (Config.ContainsKey("Debug") && Config["Debug"] == "true") Out.DebugMode = true;
            if (Config.ContainsKey("Maintenance") && Config["Maintenance"] == "true") onMaintenance = true;
            ProtocolVersion = int.Parse(Config["ProtocolVersion"]);
            if (!ConfigManager.CheckConfig(Config))
            {
                Dispose();
                return;
            }
            Out.Info("Initializing database...");
            DatabaseManager.Initialize(Config["Database_Host"], Config["Database_User"], Config["Database_Password"], Config["Database_Auth"]);
            if (Config.ContainsKey("AuthServer_Name")) Console.Title = Config["AuthServer_Name"];
            AccountManager.Initialize(DatabaseManager.connection);
            ServersManager.Initialize(DatabaseManager.connection);
            LoadData();
            
            Out.Info($"Server started successfuly (Elapsed time : {DateTime.Now.Subtract(startTime).TotalMilliseconds.ToString("### ###")} ms)");
            socket.BeginAccept(AcceptCallBack, null);
            Out.Debug("Waiting for clients...");
        }

        public void LoadData()
        {
            Out.Info("Loading ServerList...");
            Servers = ServersManager.GetServers();
            Out.Info("Loading BlockedIp");
            BlockedIp = AccountManager.GetBlockedIps(); Out.Debug($"{BlockedIp.Count} ips blocked");

        }

        public void AcceptCallBack(IAsyncResult result)
        {
            try
            {
                AuthClient _newClient = new AuthClient(socket.EndAccept(result), this)
                {
                    Ticket = Tools.RandomString(32, false)
                };
                Out.Debug($"New socket [{_newClient.socket.RemoteEndPoint}]");
                //_newClient.Initialize();
                Clients.Add(_newClient.socket, _newClient);

                socket.BeginAccept(AcceptCallBack, null);
            }catch(Exception e)
            {
                if(Clients != null)
                    Out.Error(e.Message);
            }
        }

        private void InitializeTimer()
        {
            string[] config = System.IO.File.ReadAllLines(".\\Config.xml");
            double interval = int.Parse(config[12].Remove(0, 19));
            Timer = new Timer(interval);
            //Timer.Elapsed += Save;
            Timer.Start();
        }

        public void Dispose()
        {
            foreach (KeyValuePair<Socket,IBaseClient> pair in Clients)
            {
                pair.Key.Dispose();
            }
            socket.Dispose();
            Config = null;
            Clients = null;
            Servers = null;
        }

    }
}
