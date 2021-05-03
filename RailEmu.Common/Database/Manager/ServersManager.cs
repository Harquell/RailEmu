using MySql.Data.MySqlClient;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RailEmu.Common.Database.Modeles.Auth;
using RailEmu.Common.Utils;

namespace RailEmu.Common.Database.Manager
{
    public static class ServersManager
    {
        public static MySqlConnection database;

        public static void Initialize(MySqlConnection source)
        {
            database = source;
        }

        public static Dictionary<int, WorldServer> GetServers()
        {
            Dictionary<int, WorldServer> srvList = new Dictionary<int, WorldServer>();
            var request = database.Query<WorldServer>("SELECT * FROM serverlist WHERE Usable=1", null);
            foreach(var srv in request)
            {
                WorldServer serv = srv;
                srvList.Add(serv.ServerId, serv);
                Out.Debug($" +SERVER [{serv.ServerId}]\t {serv.Name} \t {serv.Status} \t {serv.Completion}");
            }
            return srvList;
        }
    }
}
