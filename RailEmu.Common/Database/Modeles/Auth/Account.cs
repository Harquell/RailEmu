using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailEmu.Common.Database.Modeles.Auth
{
    public class Account
    {
        public int Id { get; }
        public string Username { get; }
        public string Password { get; }
        public string Pseudo { get; }
        public bool isAdmin { get; }
        public string Question { get; }
        public string Answer { get; }
        public DateTime EndSub { get; }
        public bool Banned { get; }
        public DateTime EndBan { get; }
        public sbyte Community { get; }
        public int LastServer { get; }
        public string Ticket { get; }
    }
}
