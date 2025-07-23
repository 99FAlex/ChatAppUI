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
        public static string name;
        private static readonly Encoding LocalEncoding = Encoding.UTF8;
        private TcpClient tcpClient = new TcpClient(); // Single instance
        private NetworkStream stream; // Single instance



        public async Task ConnectAsync(string ipAddress, int port)
        {
            try
            {
                await tcpClient.ConnectAsync(ipAddress, port);
                stream = tcpClient.GetStream();
                Debug.WriteLine("Connected to server.");
            }
            catch (Exception e)
            {

            }
        }

        public async Task SendMessageAsync(string message)
        {
            if (stream == null || !tcpClient.Connected)
            {
                Debug.WriteLine("Not connected. Cannot send message.");
                return;
            }

            try
            {
                byte[] data = LocalEncoding.GetBytes(message);
                await stream.WriteAsync(data, 0, data.Length);
                Debug.WriteLine("Message sent: " + message);
            }
            catch (Exception e)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Debug.WriteLine("Send Error || " + e.Message);
                });
            }
        }

        public async Task<string> ReceiveMessageAsync()
        {
            if (stream == null || !tcpClient.Connected)
            {
                Debug.WriteLine("Not connected. Cannot receive message.");
                return "Error: Not connected";
            }

            try
            {
                byte[] buffer = new byte[256]; // Or a larger buffer as needed
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    return LocalEncoding.GetString(buffer, 0, bytesRead);
                }
                return string.Empty; // No bytes read
            }
            catch (Exception e)
            {
                /*MainThread.BeginInvokeOnMainThread(() =>
                {
                    Debug.WriteLine("Receive Error || " + e.Message);
                });*/
                return "Fehler beim Empfangen";
            }
        }

        public void Disconnect()
        {
            stream?.Dispose();
            tcpClient?.Close();
            Debug.WriteLine("Disconnected.");
        }
    }
}

