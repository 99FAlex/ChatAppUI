using ChatAppUI.Objects;
using System.Threading;

namespace ChatAppUI;

public partial class ChatPage : ContentPage
{
    TcpManager tcpManager = new TcpManager();
    CancellationTokenSource cancellationTokenSource; // For stopping the receive loop

    public ChatPage()
    {
        InitializeComponent();
        _ = ConnectAndStartReceiving(); // Start connection and listening when page is initialized
    }

    private async Task ConnectAndStartReceiving()
    {
        await tcpManager.ConnectAsync("127.0.0.1", 8001); // Connect once
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
                        onMessageReceived(message);
                    });
                }
                // Add a small delay to prevent busy-waiting if messages are infrequent
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

            message.Text = string.Empty; // Clear input after sending
        }
    }

    public void onMessageReceived(string msg)
    {
        MessageStackLayout.Add(new Label { Text = msg });
    }

    // Call this when the page is closing or no longer needed
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        cancellationTokenSource?.Cancel(); // Stop the receiving loop
        cancellationTokenSource?.Dispose();
        tcpManager.Disconnect(); // Disconnect from the server
    }
}