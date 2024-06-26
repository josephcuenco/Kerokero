using Firebase.Auth;
using KeroKero.Pages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Map = Microsoft.Maui.Controls.Maps.Map;
using Microsoft.Maui.ApplicationModel;

namespace KeroKero.ViewModels
{
    internal class AppShellViewModel : INotifyPropertyChanged
    {
        //private INavigation _navigation;
        private string userEmail;
        private string userPassword;
        public string webApiKey = "AIzaSyCPz-5MixGymeUJlMKwkyhpZ9ynIGTxIRM";

        public Command SettingsBtn { get; }
        public Command ProfileBtn { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public AppShellViewModel()
        {
            //this._navigation = navigation;
            SettingsBtn = new Command(SettingsTappedAsync);
            ProfileBtn = new Command(ProfileTappedAsync);

        }

        private async void SettingsTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//SettingsPage");
        }

        private async void ProfileTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//ProfilePage");
        }

    }
}
