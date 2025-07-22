using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ChatAppUI
{
    public partial class MainPage : ContentPage
    {
        TcpManager tcpManager = new TcpManager();
        Thread thread;

        public MainPage()
        {
            InitializeComponent();
        }

        private void onSend(object sender, EventArgs e)
        {
            thread = new Thread(sendToTcpManager);
            thread.Start();
        }

        public void changeTheTestLabel(String message)
        {
            TestLabel.Text = message;

        }

        private void sendToTcpManager()
        {
            string returnMsg = tcpManager.sendMessage(message.Text);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TestLabel.Text = returnMsg;
            });
            
            thread.Join();
        }

    }

}
