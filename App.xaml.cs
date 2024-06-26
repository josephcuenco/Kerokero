using KeroKero.Pages;
using Realms;
namespace KeroKero;

public partial class App : Application
{
    public static Realms.Sync.App RealmApp;
    public const string RealmAppId = "application-0-tdxdsdz";
    public App()
    {
        InitializeComponent();
        RealmApp = Realms.Sync.App.Create(RealmAppId);

        //MainPage = new AppShell();
        MainPage = new NavigationPage(new LoginPage());
        //Shell.Current.GoToAsync("//LoginPage");

    }
}
