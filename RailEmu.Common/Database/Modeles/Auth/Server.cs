using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailEmu.Common.Database.Modeles.Auth
{
    public class WorldServer
    {
        public ushort ServerId { get; set; }
        public sbyte Status { get; set; }
        public sbyte Completion { get; set; }
        public string Name { get; set; }

    }
}
