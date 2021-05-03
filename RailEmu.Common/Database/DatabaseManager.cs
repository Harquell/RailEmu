using MySql.Data.MySqlClient;
using RailEmu.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using RailEmu.Common.Database.Modeles.Auth;

namespace RailEmu.Common.Database
{
    public static class DatabaseManager
    {
        public static MySqlConnection connection;

        public static void Initialize(string host,string user, string password,string database)
        {
            try
            {
                Out.Debug($"Connecting to:\n\t\tHOST = {host}\n\t\tUSER = {user}\n\t\tPWD = {password}\n\t\tDATABASE = {database}");
                string m_mysql = string.Format($"server={host};database={database};uid={user};pwd={password};Allow User Variables=True");
                connection = new MySqlConnection(m_mysql);
                connection.Open();
                Out.Debug($"Connection to database {database} openned");
            }
            catch (MySqlException e)
            {
                Out.Error(e.ToString());
            }
        }
    }
}
