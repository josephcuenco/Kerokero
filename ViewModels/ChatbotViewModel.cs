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
using KeroKero.Models;

namespace KeroKero.ViewModels
{
    internal class ChatbotViewModel : INotifyPropertyChanged
    {
        // Stores all possible bot messages
        private IDictionary<string, string> BotMessageDict = new Dictionary<string, string>();

        // Stores a list of messages currently displayed by the chatbot
        private ObservableCollection<ChatbotMessage> _MessagesToDisplay;

        // Stores a list of possible user responses
        private ObservableCollection<string> _PossibleResponses;
        private ObservableCollection<string> _PossibleResponses1;


        // Stores the user's chosen response
        private string _ResponseChosen;

        // Stores the picker's current index
        private int _ResponseChosenIndex;

        public Command ReturnHomeBtn { get; }
        public Command AddNewMessageBtn { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ChatbotViewModel()
        {
            ReturnHomeBtn = new Command(ReturnHomeTappedAsync);
            AddNewMessageBtn = new Command(AddNewMessageTapped);

            _ResponseChosen = "empty";
            _ResponseChosenIndex = -1;
            _PossibleResponses = new ObservableCollection<string>
            {
                "hi","hello","salutations"

            };
            _PossibleResponses1 = new ObservableCollection<string>
            {
                "peanut","yee"

            };
            _MessagesToDisplay = new ObservableCollection<ChatbotMessage>
            {
                new ChatbotMessage("one",true),
                new ChatbotMessage("two",true),
                new ChatbotMessage("three",false),
                new ChatbotMessage("four",false)
            };

            foreach (var mes in this.MessagesToDisplay)
            {
                Console.WriteLine(mes.MessageContent);
            }

        }

        public ObservableCollection<ChatbotMessage> MessagesToDisplay
        {
            get { return _MessagesToDisplay; }
            set { _MessagesToDisplay = value;
                  OnPropertyChanged(nameof(MessagesToDisplay)); }
        }
        public ObservableCollection<string> PossibleResponses
        {
            get { return _PossibleResponses; }
            set
            {
                _PossibleResponses = value;
                OnPropertyChanged(nameof(_PossibleResponses));
            }
        }

        public ObservableCollection<string> PossibleResponses1
        {
            get { return _PossibleResponses1; }
            set
            {
                _PossibleResponses = value;
                OnPropertyChanged(nameof(_PossibleResponses1));
            }
        }

        public string ResponseChosen
        {
            get { return _ResponseChosen; }
            set
            {
                _ResponseChosen = value;
                OnPropertyChanged(nameof(ResponseChosen));
            }
        }

        public int ResponseChosenIndex
        {
            get { return _ResponseChosenIndex; }
            set
            {
                _ResponseChosenIndex = (int)value;
                OnPropertyChanged(nameof(ResponseChosenIndex));
            }
        }

        private async void ReturnHomeTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
        private void AddNewMessageTapped(object obj)
        {

            MessagesToDisplay.Add(new ChatbotMessage(ResponseChosen, false));
            Console.WriteLine(ResponseChosenIndex.ToString());
            PossibleResponses = PossibleResponses1;
            OnPropertyChanged(nameof(PossibleResponses));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}