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
        private string userName;

        public Command LoginBtn { get; }

        public Command BackBtn { get; }

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

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                RaisePropertyChanged("UserEmail");
            }
        }


        public string webApiKey = "AIzaSyCPz-5MixGymeUJlMKwkyhpZ9ynIGTxIRM";

        public event PropertyChangedEventHandler? PropertyChanged;

        public LoginViewModel()
        {
            //this._navigation = navigation;
            LoginBtn = new Command(LoginBtnTappedAsync);
            BackBtn = new Command(BackBtnTappedAsync);


        }

        

        private async void LoginBtnTappedAsync(object obj)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));

            bool loginCorrect = false;

            while (!loginCorrect)

            {

                try
                {
                    var auth = await authProvider.SignInWithEmailAndPasswordAsync(UserEmail, UserPassword, UserName);
                    var user = await App.RealmApp.LogInAsync(Credentials.EmailPassword(UserEmail, UserPassword));

                    var content = await auth.GetFreshAuthAsync();
                    var serializedContent = JsonConvert.SerializeObject(content);
                    Preferences.Set("FreshFirebaseToken", serializedContent);
                    await Shell.Current.GoToAsync("//MainPage");

                    var mainViewModel = MainViewModel.Instance;
                    mainViewModel.FullName = $"{auth.User.DisplayName}";

                    var profileVM = ProfileViewModel.Instance;
                    profileVM.FullName = $"{auth.User.DisplayName}";

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

        private async void BackBtnTappedAsync(object obj)

        {
            await Shell.Current.GoToAsync("//WelcomePage");
        }

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
