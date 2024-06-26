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
    internal class MainViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        private string userEmail;
        private string userPassword;

        public Command MapBtn { get; }
        public Command InfoBtn { get; }
       
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

        public MainViewModel(INavigation navigation)
        {
            this._navigation = navigation;
            MapBtn = new Command(MapBtnTappedAsync);
            InfoBtn = new Command(InfoBtnTappedAsync);
            //LoginBtn = new Command(LoginBtnTappedAsync);

        }

        

        /*private async void LoginBtnTappedAsync(object obj)
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
        }*/

        private async void MapBtnTappedAsync(object obj)
        {
            await this._navigation.PushAsync(new MapPage());
        }

        private async void InfoBtnTappedAsync(object obj)
        {
            await this._navigation.PushAsync(new InfoPage());
        }

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
