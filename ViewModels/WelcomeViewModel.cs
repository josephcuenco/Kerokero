using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroKero.ViewModels
{
    internal class WelcomeViewModel : INotifyPropertyChanged
    {
        public Command OfflineBtn { get; }
        public Command SignUpBtn { get; }
        public Command LoginBtn { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public WelcomeViewModel()
        {
            //this._navigation = navigation;
            SignUpBtn = new Command(SignUpBtnTappedAsync);
            LoginBtn = new Command(LoginBtnTappedAsync);
            OfflineBtn = new Command(OfflineBtnTappedAsync);


        }

        private async void LoginBtnTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private async void SignUpBtnTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//SignUpPage");
        }

        private async void OfflineBtnTappedAsync(object obj)

        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}

