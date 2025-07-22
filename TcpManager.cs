using ChatAppUI.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppUI
{
    class TcpManager
    {
        private static readonly Encoding LocalEncoding = Encoding.UTF8;

        public string sendMessage(string message)
        {
            

            try
            {
                TcpClient tcpclnt = new TcpClient();
                Console.WriteLine("Connecting.....");

                tcpclnt.Connect("127.0.0.1", 8001);
                // use the ipaddress as in the server program

                Console.WriteLine("Connected");
                Console.Write("Enter the string to be transmitted : ");

                String str = message;


                Stream stm = tcpclnt.GetStream();
                

                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);
                Console.WriteLine("Transmitting.....");

                stm.Write(ba, 0, ba.Length);

                var buffer = new byte[stm.Length];
                stm.Read(buffer, 0, (int)stm.Length);
                string result = System.Text.Encoding.UTF8.GetString(buffer);
                return result;
                tcpclnt.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
                return "Error | " + e.StackTrace;
            }
        }


    }
}
