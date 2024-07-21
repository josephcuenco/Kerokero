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
    internal class ChatbotViewModel : INotifyPropertyChanged
    {
        //private INavigation _navigation;
        private string userEmail;
        private string userPassword;
        public string webApiKey = "AIzaSyCPz-5MixGymeUJlMKwkyhpZ9ynIGTxIRM";

        public Command ReturnHomeBtn { get; }
        public Command DialNumberBtn { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ChatbotViewModel()
        {
            //this._navigation = navigation;
            ReturnHomeBtn = new Command(ReturnHomeTappedAsync);
            DialNumberBtn = new Command<string>(async (number) => await DialNumberTappedAsync(number));
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

    }
}