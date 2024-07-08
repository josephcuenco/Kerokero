using KeroKero.ViewModels;
namespace KeroKero.Pages;

public partial class PhonebookPage : ContentPage
{
	public PhonebookPage()
	{
		InitializeComponent();
		BindingContext = new PhonebookViewModel();
	}
}