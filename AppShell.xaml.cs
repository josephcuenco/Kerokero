using KeroKero.Pages;

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
        }
    }
}
