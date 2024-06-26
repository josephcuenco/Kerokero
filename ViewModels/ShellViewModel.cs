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
    internal class ShellViewModel : INotifyPropertyChanged
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

        public Command DocumentsBtnMapBtn { get; }
        public Command ContactBtn { get; }
        public Command ChecklistBtn { get; }

        public Command SettingsBtn { get; }
        public Command ProfileBtn { get; }



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


        public string UserEmail
        {
            get => userEmail;
            set
            {
                userEmail = value;
                RaisePropertyChanged("UserEmail");
            }
        }


        public string UserPassword
        {
            get => userPassword; set
            {
                userPassword = value;
                RaisePropertyChanged("UserPassword");
            }
        }



        public string webApiKey = "AIzaSyCPz-5MixGymeUJlMKwkyhpZ9ynIGTxIRM";

        public event PropertyChangedEventHandler? PropertyChanged;

        public ShellViewModel()
        {
            //this._navigation = navigation;
            MapBtn = new Command(MapBtnTappedAsync);
            InfoBtn = new Command(InfoBtnTappedAsync);
            //LoginBtn = new Command(LoginBtnTappedAsync);
            DocumentsBtnMapBtn = new Command(DocTappedAsync);
            ChecklistBtn = new Command(CheckTappedAsync);
            ContactBtn = new Command(ContactTappedAsync);
            SettingsBtn = new Command(SettingsTappedAsync);
            ProfileBtn = new Command(ProfileTappedAsync);
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
            await Shell.Current.GoToAsync("//MainPage");
        }

        private async void CheckTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        private async void ContactTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        private async void SettingsTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        private async void ProfileTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}