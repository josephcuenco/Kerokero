using Firebase.Auth;
using KeroKero.Pages;
using Firebase.Auth;
using KeroKero.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace KeroKero.ViewModels
{
    internal class ProfileViewModel : INotifyPropertyChanged
    {
        //private INavigation _navigation;
        private string userEmail;
        private string userPassword;
        private bool isHomePressed;
        private bool isInfoPressed;
        private bool isMapPressed;
        public string webApiKey = "AIzaSyCPz-5MixGymeUJlMKwkyhpZ9ynIGTxIRM";

        private static ProfileViewModel _instance;
        public static ProfileViewModel Instance => _instance ?? (_instance = new ProfileViewModel());

        public Command ReturnHomeBtn { get; }

        public Command MapBtn { get; }
        public Command HomeBtn { get; }

        public Command InfoBtn { get; }

        public Command DeleteBtn { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Makes the mimic of the nav bar 
        public bool IsHomeButtonPressed
        {
            get => isHomePressed;
            set
            {
                if (isHomePressed != value)
                {
                    isHomePressed = value;
                    OnPropertyChanged(nameof(IsHomeButtonPressed));
                }
            }
        }

        public bool IsInfoButtonPressed
        {
            get => isInfoPressed;
            set
            {
                if (isInfoPressed != value)
                {
                    isInfoPressed = value;
                    OnPropertyChanged(nameof(IsInfoButtonPressed));
                }
            }
        }

        public bool IsMapButtonPressed
        {
            get => isMapPressed;
            set
            {
                if (isMapPressed != value)
                {
                    isMapPressed = value;
                    OnPropertyChanged(nameof(IsMapButtonPressed));
                }
            }
        }


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

        public ProfileViewModel()
        {
            //this._navigation = navigation;
            ReturnHomeBtn = new Command(ReturnHomeTappedAsync);
            MapBtn = new Command(MapBtnTappedAsync);
            HomeBtn = new Command(HomeBtnTappedAsync);
            InfoBtn = new Command(InfoBtnTappedAsync);
            DeleteBtn = new Command(DeleteBtnTappedAsync);



        }

        private async void ReturnHomeTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        private async void MapBtnTappedAsync(object obj)
        {
            IsMapButtonPressed = true;
            IsHomeButtonPressed = false;
            IsInfoButtonPressed = false;
            await Shell.Current.GoToAsync("//MapPage");
        }

        private async void HomeBtnTappedAsync(object obj)
        {
            IsMapButtonPressed = false;
            IsHomeButtonPressed = true;
            IsInfoButtonPressed = false;
            await Shell.Current.GoToAsync("//MainPage");
        }

        private async void InfoBtnTappedAsync(object obj)
        {
            IsMapButtonPressed = false;
            IsHomeButtonPressed = false;
            IsInfoButtonPressed = true;
            await Shell.Current.GoToAsync("//InfoPage");
        }

        private async void DeleteBtnTappedAsync(object obj)
        {
            IsMapButtonPressed = false;
            IsHomeButtonPressed = false;
            IsInfoButtonPressed = false;
            await Shell.Current.GoToAsync("//WelcomePage");
        }

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

    }
}