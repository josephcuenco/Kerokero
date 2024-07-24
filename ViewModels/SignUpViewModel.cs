﻿using Firebase.Auth;
using KeroKero.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroKero.ViewModels
{
    internal class SignUpViewModel : INotifyPropertyChanged
    {
        public string webApiKey = "AIzaSyCPz-5MixGymeUJlMKwkyhpZ9ynIGTxIRM";

        private string email;
        private string password;
        private string firstName;
        private string lastName;
        private string phone;
        private string city;

        public Command SignUpUser { get; }
        public string Email { get => email; set { 
                email = value; 
                RaisePropertyChanged("Email");
            }
        }


        public string Password { get => password; 
            set{
                password = value;
                RaisePropertyChanged("Password");
            }
        }



        public string FirstName { get => firstName;
            set
            {
                firstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        public string LastName { get => lastName;
            set
            {
                lastName = value;
                RaisePropertyChanged("LastName");
            }
        }

        //Get the number
        public string PhoneNumber
        {
            get => phone; set
            {
                phone = value;
                RaisePropertyChanged("PhoneNumber");
            }
        }

        //Get City
        public string PResideCity
        {
            get => city; set
            {
                city = value;
                RaisePropertyChanged("ResideCity");
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public SignUpViewModel()
        {
            SignUpUser = new Command(SignUpUserTappedAsync);
            //Direct to the login Page
            BackBtn = new Command(BackBtnPressedAsync);

        }

        public Command BackBtn { get; set; }

        private async void BackBtnPressedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//WelcomePage");
        }
        private async void SignUpUserTappedAsync(object obj)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            bool signUpValid = false;

            while (!signUpValid)

            {
                try
                {
                    var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Email, Password, $"{FirstName} {LastName}");
                    string token = auth.FirebaseToken;
                    await App.RealmApp.EmailPasswordAuth.RegisterUserAsync(Email, Password);
                    if (token != null)
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "User Registered successfully", "OK");

                        var mainViewModel = MainViewModel.Instance;
                        mainViewModel.FullName = $"{auth.User.DisplayName}";

                        var profileViewModel = ProfileViewModel.Instance;
                        profileViewModel.FullName = $"{auth.User.DisplayName}";


                        await Shell.Current.GoToAsync("//LoginPage");
                    }
                    signUpValid = true;
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
                    break;
                }
            }
        }
    }
}
