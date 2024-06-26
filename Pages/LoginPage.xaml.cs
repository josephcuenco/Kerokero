using KeroKero.ViewModels;
using Microsoft.Maui.Controls;
using Realms;
using MongoDB;
using System;

namespace KeroKero.Pages;

public partial class LoginPage : ContentPage
{
	
	public LoginPage()
	{

        
		InitializeComponent();

		BindingContext = new LoginViewModel();

	}



    /* 
     private async void OnSignUpLinkClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
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

	private bool ValidateLogin(string username, string password)
	{
		return username == "user" && password == "password";

	}
	*/
}
