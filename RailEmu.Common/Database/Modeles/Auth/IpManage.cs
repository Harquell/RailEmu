using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailEmu.Common.Database.Modeles.Auth
{
    public class IpBlock
    {
        public int Id;
        public string IpAddress;
    }
    public class IpAllow
    {
        public int Id;
        public int IdAccount;
        public string IpAddress;
    }
}
