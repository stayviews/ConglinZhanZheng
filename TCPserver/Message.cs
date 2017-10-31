﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPserver
{
    class Message
    {
        public byte[] data = new byte[1024];
        public int startIndex = 0;
        public int RemainSize
        {
            get { return (data.Length - startIndex); }

        }

        public void AddIndex(int index)
        {
            startIndex += index;
        }
        public void ReadMessage()
        {
            while (true)
            {
                if (startIndex <= 4) return;
                int msgLength = BitConverter.ToInt32(data, 0);
                if ((startIndex - 4) >= msgLength)
                {
                    string s = Encoding.UTF8.GetString(data, 4, msgLength);
                    Console.WriteLine("解析一条消息：" + s);
                    Array.Copy(data, msgLength + 4, data, 0, startIndex - 4 - msgLength);

                }
                else
                {
                    break;
                }
            }
        }
    }
}
