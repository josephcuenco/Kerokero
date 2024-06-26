using KeroKero.ViewModels;

namespace KeroKero.Pages;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainViewModel();
    }

}
