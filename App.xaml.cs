using KeroKero.Pages;

namespace KeroKero;

public partial class App : Application
{

    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();

        Routing.RegisterRoute("MainPage", typeof(KeroKero.Pages.MainPage));
        Routing.RegisterRoute("LoginPage", typeof(KeroKero.Pages.LoginPage));
        Routing.RegisterRoute("SignUpPage", typeof(KeroKero.Pages.SignUpPage));
        Routing.RegisterRoute("MapPage", typeof(KeroKero.Pages.MapPage));
        Routing.RegisterRoute("InfoPage", typeof(KeroKero.Pages.InfoPage));
    }
        protected override void OnStart()
        {
        // Set the initial route to login
        Shell.Current.GoToAsync("//LoginPage");
        }

    protected override void OnSleep() { }

    protected override void OnResume() { }


}

