using System.Diagnostics;

namespace ChatAppUI
{
    public partial class MainPage : ContentPage
    {
        TcpManager tcpManager = new TcpManager();
        public MainPage()
        {
            InitializeComponent();
        }

        private void onSend(object sender, EventArgs e)
        {
            tcpManager.sendMessage(message.Text);
        }




    }

}
