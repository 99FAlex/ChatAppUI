using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChatAppUI
{
    public partial class MainPage : ContentPage
    {
        public static string officalIP = "127.0.01";
        public static string port = "8001";

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var path = FileSystem.Current.AppDataDirectory;
            var fullpath = Path.Combine(path, "selectedServer.txt");
            if (!Path.Exists(fullpath))
            {
                File.WriteAllText(fullpath, "Offical Server\n" + MainPage.officalIP + "\n" + MainPage.port);
                
            }
        }
            public MainPage()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ChatPage), typeof(ChatPage));
            Routing.RegisterRoute(nameof(Serverlist), typeof(Serverlist));
            Application.Current.UserAppTheme = AppTheme.Light;



        }
        private void onServerListSwitch(object sender, EventArgs a)
        {

            GoToServerlist();
        }
        private void onChatPageSwitch(object sender, EventArgs e)
        {
            if (Username.Text != null)
            {
                TcpManager.name = Username.Text;
                GoToChatPage();
            }
            else
            {
                Error.IsVisible = true;
                Error.Text = "Error Username is required";
            }
        }

        private async void GoToServerlist()
        {
            await Shell.Current.GoToAsync(nameof(Serverlist));

        }

        private async void GoToChatPage()
        {
            await Shell.Current.GoToAsync(nameof(ChatPage));
        }



    }

}
