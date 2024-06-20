using Microsoft.Maui.Controls;
using System;

namespace KeroKero.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private async void OnLoginButtonClicked(object sender, EventArgs e)
	{
		string username = UsernameEntry.Text;
		string password = PasswordEntry.Text;

		// TODO: replace with actual login logic
		bool isValid = ValidateLogin(username, password);

		if (isValid)
		{
			await Shell.Current.GoToAsync("MainPage");
		}
		else
		{
			LoginStatusLabel.Text = "Invalid username or password.";

		}
	}

	private async void OnSignUpButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("SignUpPage");
	}


	private bool ValidateLogin(string username, string password)
	{
		return username == "user" && password == "password";

	}


	private void testfunction()
	{
		
	}

}



//git test oooooooooooogaaaaaaaa boogaaaaaaaa