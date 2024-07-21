using Firebase.Auth;
using KeroKero.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroKero.ViewModels
{
    internal class DocumentViewModel : INotifyPropertyChanged
    {
        //private INavigation _navigation;
        private string userEmail;
        private string userPassword;
        public string webApiKey = "AIzaSyCPz-5MixGymeUJlMKwkyhpZ9ynIGTxIRM";

        private ObservableCollection<string> docs;
        public ObservableCollection<string> Docs
        {
            get => docs; 
            set 
            { 
                docs = value;
                OnPropertyChanged(nameof(Docs));
            }
        }

        public Command ReturnHomeBtn { get; }
        public Command uploadBtn { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public DocumentViewModel()
        {
            //this._navigation = navigation;
            Docs= new ObservableCollection<string>();
            ReturnHomeBtn = new Command(ReturnHomeTappedAsync);
            uploadBtn = new Command(uploadFile);

        }

        private async void ReturnHomeTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        private async void uploadFile()
        {
         
                var result = await FilePicker.Default.PickAsync();
                if (result != null)
                {
                    Docs.Add(result.FileName);
                }
  
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}