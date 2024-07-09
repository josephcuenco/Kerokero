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

        MainPage = new AppShell();

    }

    protected override void OnStart()
    {
        // Set the initial route to login
        Shell.Current.GoToAsync("//WelcomePage");
    }

    protected override void OnSleep() { }

    protected override void OnResume() { }


}

