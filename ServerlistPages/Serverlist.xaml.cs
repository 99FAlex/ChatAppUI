using ChatAppUI.ServerlistPages;
using Microsoft.Maui.Controls;
using System.Diagnostics;

namespace ChatAppUI;

public partial class Serverlist : ContentPage
{

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var pathSelected = FileSystem.Current.AppDataDirectory;
        var fullpathSelected = Path.Combine(pathSelected, "selectedServer.txt");
        var selectedData = File.ReadAllText(fullpathSelected).Split("\n");

        currently.Text = "Currently selected: " + selectedData[0];


        var path = FileSystem.Current.AppDataDirectory;
        var fullpath = Path.Combine(path, "serverlist.txt");
        if (Path.Exists(fullpath))
        {
            var data = File.ReadAllText(fullpath).Split("\nN");

            foreach (var item in data)
            {

                string[] serverdata = item.Split("\n");


                    Grid grid = new Grid();
                    Label name = new Label() { Text = serverdata[0], HorizontalOptions = LayoutOptions.Start };
                    Label adress = new Label() { Text = serverdata[1], HorizontalOptions = LayoutOptions.Center };
                    Label port = new Label() { Text = serverdata[2], HorizontalOptions = LayoutOptions.End };
                    grid.Add(name);
                    grid.Add(adress);
                    grid.Add(port);
                    Frame frame = new Frame() { Content = grid, Margin=new Thickness(0,6,0,0)};
                    frame.BorderColor = Color.FromRgb(0, 0, 0);

                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer() { NumberOfTapsRequired = 1 };
                    tapGestureRecognizer.Tapped += (sender, e) =>
                    {
                        var path = FileSystem.Current.AppDataDirectory;
                        var fullpath = Path.Combine(path, "selectedServer.txt");
                        currently.Text = "Currently selected: " + serverdata[0];

                        File.WriteAllText(fullpath, serverdata[0] + "\n" + serverdata[1] + "\n" + serverdata[2]);
                        Debug.WriteLine(name.Text + " Clicked");
                    };
                    //gestureRecognizer.AddLogicalChild(gestureRecognizer);
                    frame.GestureRecognizers.Add(tapGestureRecognizer);
                    ServerStackLayout.Add(frame);



            }
        }
            

    }


    private void removeServer(object sender, EventArgs e)
    {
        Debug.WriteLine("Coming Soon");
        //OnAppearing();
    }
    public Serverlist()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(AddServer), typeof(AddServer));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));

    }


	private void switchAddServer(object sender, EventArgs e)
	{
		goToAddServer();
	}
    private void backToMainPage(object sender, EventArgs e)
    {
        GoToMainPage();
    }

    private async void goToAddServer()
    {
        await Shell.Current.GoToAsync(nameof(AddServer));
    }

    private async void GoToMainPage()
    {
        await Shell.Current.GoToAsync("///MainPage");
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var path = FileSystem.Current.AppDataDirectory;
        var fullpath = Path.Combine(path, "selectedServer.txt");
        currently.Text = "Currently selected: Offical Server";

        File.WriteAllText(fullpath, "Offical Server\n" + MainPage.officalIP + "\n" + MainPage.port);
    }
}