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
    internal class PhonebookViewModel : INotifyPropertyChanged
    {
        private bool isHomePressed;
        private bool isInfoPressed;
        private bool isMapPressed;

        //private INavigation _navigation;
        private string userEmail;
        private string userPassword;
        public string webApiKey = "AIzaSyCPz-5MixGymeUJlMKwkyhpZ9ynIGTxIRM";

        public Command ReturnHomeBtn { get; }
        public Command DialNumberBtn { get; }

        public Command MapBtn { get; }
        public Command HomeBtn { get; }

        public Command InfoBtn { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public PhonebookViewModel()
        {
            //this._navigation = navigation;
            ReturnHomeBtn = new Command(ReturnHomeTappedAsync);
            DialNumberBtn = new Command<string>(async (number) => await DialNumberTappedAsync(number));
            MapBtn = new Command(MapBtnTappedAsync);
            HomeBtn = new Command(HomeBtnTappedAsync);
            InfoBtn = new Command(InfoBtnTappedAsync);
        }

        private async void ReturnHomeTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        private async Task DialNumberTappedAsync(string number)
        {
            if (PhoneDialer.Default.IsSupported)
                PhoneDialer.Default.Open(number);
        }

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



        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}