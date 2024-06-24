using Firebase.Auth;
//using Firebase.Auth.Providers;
using KeroKero.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroKero.ViewModels
{
    internal class SignUpViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        public string webApiKey = "AIzaSyCPz-5MixGymeUJlMKwkyhpZ9ynIGTxIRM";
        private string email;
        private string password;
        public Command SignUpUser { get; }
        public string Email { get => email; set { 
                email = value; 
                RaisePropertyChanged("Email");
            }
        }


        public string Password { get => password; 
            set{
                password = value;
                RaisePropertyChanged("Password");
            }
        }

        

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public SignUpViewModel(INavigation navigation)
        {
            this._navigation = navigation;

            SignUpUser = new Command(SignUpUserTappedAsync);
        }

        private async void SignUpUserTappedAsync(object obj)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            bool signUpValid = false;

            while (!signUpValid)
            {
                try
                {
                    var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Email, Password);
                    string token = auth.FirebaseToken;
                    if (token != null)
                        await App.Current.MainPage.DisplayAlert("Alert", "User Registered successfully", "OK");
                    await this._navigation.PopAsync();
                    signUpValid = true;
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
                    break;
                }
            }
        }
    }
}
