using Firebase.Auth;
using KeroKero.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroKero.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        //progress bar
        public double CurrentProgress { get; set; } = 0.0;

        // private INavigation _navigation;
        private string userEmail;
        private string userPassword;

        private static MainViewModel _instance;
        public static MainViewModel Instance => _instance ?? (_instance = new MainViewModel());

        public Command MapBtn { get; }
        public Command InfoBtn { get; }

        public Command DocumentsBtn { get; }
        public Command PhonebookBtn { get; }

        public Command ChecklistBtn { get; }
        public Command SettingsBtn { get; }
        public Command ProfileBtn { get; }

        public Command ChatbotBtn { get; }

        public Command HomeBtn { get; }

        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    RaisePropertyChanged(nameof(FullName));
                }
            }
        }


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

        public MainViewModel()
        {
            //this._navigation = navigation;
            MapBtn = new Command(MapBtnTappedAsync);
            InfoBtn = new Command(InfoBtnTappedAsync);
            //LoginBtn = new Command(LoginBtnTappedAsync);
            DocumentsBtn = new Command(DocTappedAsync);
            ChecklistBtn = new Command(CheckTappedAsync);
            PhonebookBtn = new Command(PhonebookTappedAsync);
            SettingsBtn = new Command(SettingsTappedAsync);
            ProfileBtn = new Command(ProfileTappedAsync);
            ChatbotBtn = new Command(ChatbotBtnTappedAsync);
            HomeBtn= new Command(HomeBtnTappedAsync);   
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

        private async void HomeBtnTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
        private async void ChatbotBtnTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//ChatbotPage");
        }

        private async void MapBtnTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//MapPage");
        }

        private async void InfoBtnTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//InfoPage");
        }

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        private async void DocTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//DocumentPage");
        }

        private async void CheckTappedAsync(object obj)
        {
            
            await Shell.Current.GoToAsync("//ChecklistPage");
        }

        private async void PhonebookTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//PhonebookPage");
        }

        private async void SettingsTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        private async void ProfileTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//ProfilePage");
        }
    }
}
