using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPclient
{
    class Message
    {
        public static byte[] GetBytes(string str)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(str);
            int dataLength = dataBytes.Length;
            byte[] lenBytes = BitConverter.GetBytes(dataLength);
            byte[] newBytes = lenBytes.Concat(dataBytes).ToArray();
            return newBytes;
        }
    }
}
