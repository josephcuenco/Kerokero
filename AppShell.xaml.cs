using KeroKero.Pages;
/*using Realms;
using Realms.Sync;*/

namespace KeroKero
{
    public partial class AppShell : Shell
    {
        
        public AppShell()
        { 
            
            
            InitializeComponent();
            Routing.RegisterRoute("MainPage", typeof(KeroKero.Pages.MainPage));
            Routing.RegisterRoute("LoginPage", typeof(KeroKero.Pages.LoginPage));
            Routing.RegisterRoute("SignUpPage", typeof(KeroKero.Pages.SignUpPage));
            Routing.RegisterRoute("MapPage", typeof(KeroKero.Pages.MapPage));
            Routing.RegisterRoute("InfoPage", typeof(KeroKero.Pages.InfoPage));
        }
    }
}

