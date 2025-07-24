//using static CoreFoundation.DispatchSource;

using System.Diagnostics;

namespace ChatAppUI.ServerlistPages;

public partial class AddServer : ContentPage
{
	public AddServer()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(Serverlist), typeof(Serverlist));

    }

    private void addServer(object sender, EventArgs e)
	{
        var path = FileSystem.Current.AppDataDirectory;
        var fullpath = Path.Combine(path, "serverlist.txt");
        if (Path.Exists(fullpath))
        {
            var data = File.ReadAllText(fullpath);
            File.WriteAllText(fullpath, data + "\nN" + name.Text + "\n" + adress.Text + "\n" + port.Text);
            GoToServerList();

        }
        else
        {
            File.WriteAllText(fullpath, name.Text + "\n" + adress.Text + "\n" + port.Text);

            GoToServerList();

        }
        Debug.WriteLine(fullpath);
    }


    private void back(object sender, EventArgs e)
    {
        Debug.WriteLine("test");
        GoToServerList();
    }
    private async void GoToServerList()
    {
        await Shell.Current.GoToAsync(nameof(Serverlist));
    }
}