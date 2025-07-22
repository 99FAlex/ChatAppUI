using ChatAppUI.Objects;
using System.Threading;

namespace ChatAppUI;

public partial class ChatPage : ContentPage
{
    TcpManager tcpManager = new TcpManager();
    Thread thread;
    Thread reciever;
    public ChatPage()
	{
		InitializeComponent();
	}

    public void startReciever()
    {
        reciever = new Thread(tcpManager.test);
        reciever.Start();
    }

    public void startRecieverThread()
    {
        string message = tcpManager.messageReciever();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            startReciever();
            TestLabel.Text = message;
            Console.WriteLine(message);
            reciever.Join();
        });
    }


    private void onSend(object sender, EventArgs e)
    {
        thread = new Thread(sendToTcpManager);
        thread.Start();
    }


    private void sendToTcpManager()
    {
        tcpManager.sendMessage(message.Text);

        thread.Join();
    }

    public void onMessageRecieved(string msg)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            TestLabel.Text = msg;
        });
    }
}