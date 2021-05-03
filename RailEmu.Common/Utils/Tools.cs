using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RailEmu.Common.Utils
{
    public static class Tools
    {
        public static string RandomString(int nb, bool upper)
        {
            string message = "";
            Random rnd = new Random();
            for (int i = 0; i <= nb; i++)
            {
                int num = rnd.Next(0, 26);
                message += (char)('a' + num);
            }
            return upper ? message.ToUpper() : message;
        }

        public static string GetMd5(string str)
        {
            MD5 _md5 = MD5.Create();
            byte[] _inputBytes = System.Text.Encoding.ASCII.GetBytes(str);
            byte[] _hash = _md5.ComputeHash(_inputBytes);
            StringBuilder _builder = new StringBuilder();
            for (int i = 0; i < _hash.Length; i++)
            {
                _builder.Append(_hash[i].ToString("x2"));
            }
            return _builder.ToString();
        }
    }
}
