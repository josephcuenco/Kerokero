using KeroKero.Pages;

namespace KeroKero;

public partial class App : Application
{

    public App()
    {
        InitializeComponent();

        //MainPage = new AppShell();
        MainPage = new NavigationPage(new LoginPage());
        //Shell.Current.GoToAsync("//LoginPage");

    }
}
