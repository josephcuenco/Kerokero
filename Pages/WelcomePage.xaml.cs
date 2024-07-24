using KeroKero.ViewModels;
using Microsoft.Maui.Controls.Maps;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace KeroKero.Pages;

public partial class WelcomePage : ContentPage
{

    
    public WelcomePage()
	{
		InitializeComponent();
		BindingContext = new WelcomeViewModel();
	}
}