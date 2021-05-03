using MySql.Data.MySqlClient;
using Dapper;
using RailEmu.Common.Database.Modeles.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RailEmu.Common.Utils;

namespace RailEmu.Common.Database.Manager
{
    public static class AccountManager
    {
        public static MySqlConnection database;

        public static void Initialize(MySqlConnection source)
        {
            database = source;
        }

        public static Account GetAccount(string username)
        {
            var Acc = database.QueryFirstOrDefault<Account>($"SELECT * FROM Account WHERE account.Username = '{username}'", null);
            if (Acc != null) return Acc;
            return null;
        }

        public static Account GetAccountByTicket(string Ticket)
        {
            var Acc = database.QueryFirstOrDefault<Account>($"SELECT * FROM Account WHERE account.Ticket = '{Ticket}'");
            if (Acc != null)
                return Acc;
            return null;
        }
        public static Account GetAccount(int id)
        {
            var Acc = database.QueryFirstOrDefault<Account>($"SELECT * FROM Account WHERE Id = '{id}'", null);
            if (Acc != null) return Acc;
            return null;
        }
        public static List<Account> GetAccounts()
        {
            List<Account> AccountList = new List<Account>();
            IEnumerable<Account> Accounts = database.Query<Account>("SELECT * FROM Account", null);
            foreach (var ac in Accounts)
            {
                AccountList.Add(ac as Account);
            }
            return AccountList;
        }

        public static List<string> GetBlockedIps()
        {
            List<string> ips = new List<string>();
            var Ips = database.Query("SELECT distinct IpAddress FROM ipblock", null);
            foreach (var ip in Ips)
            {
                try
                {
                    ips.Add(ip.IpAddress);
                }
                catch (Exception e)
                {
                    Out.Error($"Couldn't add {ip} to BlockedIps");
                }
            }
            return ips;

        }
        public static int GetNbChar(int serverId, int account)
        {
            int nb = database.QueryFirstOrDefault<int>($"SELECT NbChar from server_characters where ServerId = {serverId} AND AccountId = {account}", 0);
            return nb;
        }

        public static bool SetAccountNickname(int account, string pseudo)
        {
            try
            {
                database.Query($"UPDATE Account SET Pseudo = '{pseudo}' WHERE Id = '{account}'");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool SetAccountTicket(int account, string Ticket)
        {
            try
            {
                database.Query($"UPDATE Account SET Ticket LIKE '{Ticket}*' WHERE Id ='{account}'");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
