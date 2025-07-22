using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ChatAppUI
{
    public partial class MainPage : ContentPage
    {

        ChatPage chatPage = new ChatPage();

        public MainPage()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ChatPage), typeof(ChatPage));

        }
        private void onSwitch(object sender, EventArgs e)
        {
            chatPage.startReciever();
            GoToAnotherPage();
        }


        private async void GoToAnotherPage()
        {
            await Shell.Current.GoToAsync(nameof(ChatPage));
        }



    }

}
