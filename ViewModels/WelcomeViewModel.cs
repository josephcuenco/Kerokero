using Firebase.Auth;
using Microsoft.Maui.Controls.Maps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace KeroKero.ViewModels
{
    public class OfflineService
    {
        public void SaveOffline(string key, string answer)
        {
            string json = JsonSerializer.Serialize(answer);
            Preferences.Set(key, json);
        }

        public String GetOffline(string key)
        {
            string json = Preferences.Get(key, string.Empty);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            return JsonSerializer.Deserialize<string>(json);
        }
    }
    
    internal class WelcomeViewModel : INotifyPropertyChanged
    {
        private readonly OfflineService OS = new OfflineService();
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
            OS.SaveOffline("offline", "offline");
            await Shell.Current.GoToAsync("//MainPage");

        }

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}

