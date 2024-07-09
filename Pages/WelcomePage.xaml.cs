using KeroKero.ViewModels;

namespace KeroKero.Pages;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();
		BindingContext = new WelcomeViewModel();
	}
}