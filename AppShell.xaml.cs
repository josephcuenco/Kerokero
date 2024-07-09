using KeroKero.Pages;
using KeroKero.ViewModels;
/*using Realms;
using Realms.Sync;*/

namespace KeroKero
{
    public partial class AppShell : Shell
    {
        
        public AppShell()
        {   
            InitializeComponent();
            this.BindingContext = new AppShellViewModel();
            Routing.RegisterRoute("MainPage", typeof(KeroKero.Pages.MainPage));
            Routing.RegisterRoute("LoginPage", typeof(KeroKero.Pages.LoginPage));
            Routing.RegisterRoute("SignUpPage", typeof(KeroKero.Pages.SignUpPage));
            Routing.RegisterRoute("MapPage", typeof(KeroKero.Pages.MapPage));
            Routing.RegisterRoute("InfoPage", typeof(KeroKero.Pages.InfoPage));
            Routing.RegisterRoute("WelcomePage", typeof(KeroKero.Pages.WelcomePage));
            //Routing.RegisterRoute("SettingsPage", typeof(KeroKero.Pages.SettingsPage));
            //Routing.RegisterRoute("ProfilePage", typeof(KeroKero.Pages.ProfilePage));
            //Routing.RegisterRoute("PhonebookPage", typeof(KeroKero.Pages.PhonebookPage));
        }
    }
}

