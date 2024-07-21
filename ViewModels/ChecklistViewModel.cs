using Firebase.Auth;
using KeroKero.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KeroKero.ViewModels
{
    internal class ChecklistViewModel: INotifyPropertyChanged
    {
        //private INavigation _navigation;
        private string userEmail;
        private string userPassword;

        

        public Command MapBtn { get; }
        public Command HomeBtn { get; }
       
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

        public class T
        {
            public string Todo { get; set; }
            public bool IsChecked { get; set; }
        }
        public ObservableCollection<T> Todos { get; set; }

        public ChecklistViewModel()
        {
            //this._navigation = navigation;
            MapBtn = new Command(MapBtnTappedAsync);
            HomeBtn = new Command(HomeBtnTappedAsync);
            Todos = new ObservableCollection<T>
            {
                new T { Todo = "Water", IsChecked = false },
                new T { Todo = "Bob-perishable food (canned, dried)", IsChecked = false },
                new T { Todo = "First aid kit", IsChecked = false },
                new T { Todo = "Batteries", IsChecked = false },
                new T { Todo = "Matches", IsChecked = false },
                new T { Todo = "Blanket", IsChecked = false },
                new T { Todo = "Medication", IsChecked = false },
                new T { Todo = "Emergency Documents", IsChecked = false },
                new T { Todo = "Cash", IsChecked = false },
                new T { Todo = "Chargers/ Portable charger", IsChecked = false },
                new T { Todo = "Sanitary Products", IsChecked = false },
                new T { Todo = "Towel", IsChecked = false },
                new T { Todo = "Clothes", IsChecked = false },
            };

            //LoginBtn = new Command(LoginBtnTappedAsync);

        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            await Shell.Current.GoToAsync("//MapPage");
        }

        private async void HomeBtnTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
