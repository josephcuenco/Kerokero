using KeroKero.ViewModels;
using System.Formats.Asn1;

namespace KeroKero.Pages;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
		BindingContext = new SignUpViewModel(Navigation);
	}

	/*private async void OnSignUpButtonClicked(object sender, EventArgs e)
	{
		string username = UsernameEntry.Text;
		string password = PasswordEntry.Text;
		string confirmpassword = ConfirmPasswordEntry.Text;

		if (password != confirmpassword)
		{
			SignUpStatusLabel.Text = "Passwords do not match. Try again!";
			return;
		}

		//TODO: Replace with actual sign-up logic
		bool isSignedUp = SignUp(username, password);

		if (isSignedUp)
		{
			await Shell.Current.GoToAsync("///LoginPage");

		}
		else
		{
			SignUpStatusLabel.Text = "Sign-up failed. Try again!";
		}
	}

	private bool SignUp(string username, string password)
	{
		//TODO: Replace with actual sign-up logic
		return true;
	}
	*/
}