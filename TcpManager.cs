using ChatAppUI.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void sendMessage(string message)
        {
            

            try
            {
                TcpClient tcpclnt = new TcpClient();
                tcpclnt.Connect("127.0.0.1", 8001);
                // use the ipaddress as in the server program


                String str = message;


                Stream stm = tcpclnt.GetStream();
                

                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);

                stm.Write(ba, 0, ba.Length);



                
            }

            catch (Exception e)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                Debug.WriteLine("Error || " + e.StackTrace);
                }); 
            }
        }


        public String messageReciever()
        {
            try
            {
                TcpClient tcpclnt = new TcpClient();
                tcpclnt.Connect("127.0.0.1", 8001);
                // use the ipaddress as in the server program
                var buffer = new byte[256];

                Stream stm = tcpclnt.GetStream();


                    Int32 bytes = stm.Read(buffer, 0, buffer.Length);
                    string result = System.Text.Encoding.ASCII.GetString(buffer, 0, bytes);
                    return result;

                

            }
            catch (Exception e)
            {
                return "Fehler";
            }
        }
        public void test()
        {
            ChatPage chatPage = new ChatPage();

            chatPage.test();
        }


    }
}
