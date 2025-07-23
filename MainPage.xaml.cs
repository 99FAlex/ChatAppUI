using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ChatAppUI
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ChatPage), typeof(ChatPage));

        }
        private void onSwitch(object sender, EventArgs e)
        {
            if (Username.Text != null)
            {
                TcpManager.name = Username.Text;
                GoToAnotherPage();
            }
            else
            {
                Error.IsVisible = true;
                Error.Text = "Error Username is required";
            }
        }


        private async void GoToAnotherPage()
        {
            await Shell.Current.GoToAsync(nameof(ChatPage));
        }



    }

}
