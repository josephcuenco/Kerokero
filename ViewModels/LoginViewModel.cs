using Firebase.Auth;
using KeroKero.Pages;
using Microsoft.Maui.Layouts;
using Newtonsoft.Json;
using System;
using Realms.Sync;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.Networking;

namespace KeroKero.ViewModels
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        //private INavigation _navigation;
        private string userEmail;
        private string userPassword;
        

        public Command SignUpBtn { get; }
        public Command LoginBtn { get; }

        public Command MainBtn { get; }

        public string UserEmail { get => userEmail;
            set
            {
                userEmail = value;
                RaisePropertyChanged("UserEmail");
            }
        }


        public string UserPassword { get => userPassword; set
            { 
                userPassword = value;
                RaisePropertyChanged("UserPassword");
            }
        }

        

        public string webApiKey = "AIzaSyCPz-5MixGymeUJlMKwkyhpZ9ynIGTxIRM";

        public event PropertyChangedEventHandler? PropertyChanged;

        public LoginViewModel()
        {
            //this._navigation = navigation;
            SignUpBtn = new Command(SignUpBtnTappedAsync);
            LoginBtn = new Command(LoginBtnTappedAsync);
            MainBtn = new Command(HomeBtnTappedAsync);

        }

        

        private async void LoginBtnTappedAsync(object obj)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));

            bool loginCorrect = false;

            while (!loginCorrect)

            {

                try
                {
                    var auth = await authProvider.SignInWithEmailAndPasswordAsync(UserEmail, UserPassword);
                    var user = await App.RealmApp.LogInAsync(Credentials.EmailPassword(UserEmail, UserPassword));

                    var content = await auth.GetFreshAuthAsync();
                    var serializedContent = JsonConvert.SerializeObject(content);
                    Preferences.Set("FreshFirebaseToken", serializedContent);
                    await Shell.Current.GoToAsync("//MainPage");

                    var mainViewModel = MainViewModel.Instance;
                    mainViewModel.FullName = $"{auth.User.DisplayName}";

                    loginCorrect = true;
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
                    UserEmail = string.Empty;
                    UserPassword = string.Empty;
                    break;
                }
            }
        }

        private async void SignUpBtnTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//SignUpPage");
        }

        private async void HomeBtnTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
