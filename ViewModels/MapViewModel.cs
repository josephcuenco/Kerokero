using Firebase.Auth;
using KeroKero.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroKero.ViewModels
{
    internal class MapViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        private string userEmail;
        private string userPassword;

        public Command HomeBtn { get; }
        public Command LoginBtn { get; }
       
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

        public MapViewModel(INavigation navigation)
        {
            this._navigation = navigation;
            HomeBtn = new Command(HomeBtnTappedAsync);
            LoginBtn = new Command(LoginBtnTappedAsync);

        }

        

        private async void LoginBtnTappedAsync(object obj)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(UserEmail, UserPassword);
                var content = await auth.GetFreshAuthAsync();
                var serializedContent = JsonConvert.SerializeObject(content);
                Preferences.Set("FreshFirebaseToken", serializedContent);
                await this._navigation.PushAsync(new MainPage());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
                throw;
            }
        }

        private async void HomeBtnTappedAsync(object obj)
        {
            await this._navigation.PushAsync(new MainPage());
        }

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
