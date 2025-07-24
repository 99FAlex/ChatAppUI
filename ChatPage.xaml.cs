using ChatAppUI.Objects;
using System.Threading;

namespace ChatAppUI;

public partial class ChatPage : ContentPage
{
    TcpManager tcpManager = new TcpManager();
    CancellationTokenSource cancellationTokenSource;

    public ChatPage()
    {
        InitializeComponent();
        _ = ConnectAndStartReceiving(); 
        Routing.RegisterRoute(nameof(Serverlist), typeof(Serverlist));

    }

    private async Task ConnectAndStartReceiving()
    {
        await tcpManager.ConnectAsync();
        await tcpManager.SendMessageAsync(TcpManager.name);
        StartReceivingLoop();
    }

    private void StartReceivingLoop()
    {
        cancellationTokenSource = new CancellationTokenSource();
        Task.Run(async () =>
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                string message = await tcpManager.ReceiveMessageAsync();
                if (!string.IsNullOrEmpty(message) && message != "Fehler beim Empfangen")
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        if (message == "Error: Not connected")
                        {
                            goToMainPage();
                        }
                        else
                        {
                            onMessageReceived(message);

                        }
                    });
                }
                await Task.Delay(100, cancellationTokenSource.Token);
            }
        }, cancellationTokenSource.Token);
    }

    private async void onSend(object sender, EventArgs e)
    {
        string messageToSend = message.Text;
        if (!string.IsNullOrEmpty(messageToSend))
        {
            await tcpManager.SendMessageAsync(messageToSend);

            message.Text = string.Empty;
        }
    }

    public void onMessageReceived(string msg)
    {
        MessageStackLayout.Add(new Label { Text = msg });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        cancellationTokenSource?.Cancel();
        cancellationTokenSource?.Dispose();
        tcpManager.Disconnect();
    }
    private async void goToMainPage()
    {
        await Shell.Current.GoToAsync(nameof(Serverlist));
    }
}
