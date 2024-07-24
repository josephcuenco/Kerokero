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
using static Realms.ChangeSet;

namespace KeroKero.ViewModels
{
    internal class ChatbotViewModel : INotifyPropertyChanged
    {
        private bool isHomePressed;
        private bool isInfoPressed;
        private bool isMapPressed;

        // Stores all possible bot messages
        private IDictionary<string, string> _BotMessageDict;

        // Store current state
        private string _BotState;

        // Stores a list of messages currently displayed by the chatbot
        private ObservableCollection<ChatbotMessage> _MessagesToDisplay;

        // Stores a list of possible user responses
        private ObservableCollection<string> _PossibleResponses;
        private IDictionary<string, ObservableCollection<string> > _ResponsesDictionary;


        // Stores the user's chosen response
        private string _ChosenResponse;

        // Stores the picker's current index
        private int _ResponseChosenIndex;

        public Command ReturnHomeBtn { get; }
        public Command AddNewMessageBtn { get; }


        public Command MapBtn { get; }
        public Command HomeBtn { get; }

        public Command InfoBtn { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ChatbotViewModel()
        {
            ReturnHomeBtn = new Command(ReturnHomeTappedAsync);
            AddNewMessageBtn = new Command(AddNewMessageTapped);
            MapBtn = new Command(MapBtnTappedAsync);
            HomeBtn = new Command(HomeBtnTappedAsync);
            InfoBtn = new Command(InfoBtnTappedAsync);

            _BotState = "prompt";
            _ChosenResponse = "empty";
            _ResponseChosenIndex = -1;
            _PossibleResponses = new ObservableCollection<string>();
            _BotMessageDict = new Dictionary<string, string>();
            _ResponsesDictionary = new Dictionary<string, ObservableCollection<string> >();

            LoadChatbotResponsesDictionary();
            LoadChatbotMessageDictionary();

            _PossibleResponses = ResponsesDictionary["prompt"];

            _MessagesToDisplay = new ObservableCollection<ChatbotMessage>();

            PrintWelcome();
            PrintNewPrompt();

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
        public IDictionary<string, ObservableCollection<string> > ResponsesDictionary
        {
            get { return _ResponsesDictionary; }
            set
            {
                _ResponsesDictionary = value;
                OnPropertyChanged(nameof(_ResponsesDictionary));
            }
        }
        public IDictionary<string,string> BotMessageDict
        {
            get { return _BotMessageDict; }
            set
            {
                _BotMessageDict = value;
                OnPropertyChanged(nameof(_BotMessageDict));
            }
        }

        public string ChosenResponse
        {
            get { return _ChosenResponse; }
            set
            {
                _ChosenResponse = value;
                OnPropertyChanged(nameof(ChosenResponse));
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
        public string BotState
        {
            get { return _BotState; }
            set
            {
                _BotState = value;
                OnPropertyChanged(nameof(_BotState));
            }
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



        private async void ReturnHomeTappedAsync(object obj)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
        private void AddNewMessageTapped(object obj)
        {
            // Print User Response, if they chose one
            if (ResponseChosenIndex != -1)
                MessagesToDisplay.Add(new ChatbotMessage(ChosenResponse, false));
            
            // User chooses "Learn more about natural disasters"
            if (BotState == "prompt" && ChosenResponse == "Learn more about natural disasters")
            {
                PrintDisasterPrompt(); // Print bot response

                BotState = "disaster"; // Update State
                PossibleResponses = ResponsesDictionary["disaster"]; // Update User Response Options
            }
            // User chooses to learn more about emergency communication networks
            else if (BotState == "prompt" && ChosenResponse == "Emergency communication info")
            {
                PrintCommunicationInfo();
                PrintNewPrompt();

                BotState = "prompt";
                PossibleResponses = ResponsesDictionary["prompt"];
            }
            // User chooses to hear about emergency kit
            else if (BotState == "prompt" && ChosenResponse == "Disaster kit help")
            {
                PrintKitInfo();
                PrintNewPrompt();

                BotState = "prompt";
                PossibleResponses = ResponsesDictionary["prompt"];
            }
            // User chooses to get directions to the nearest shelter
            else if (BotState == "prompt" && ChosenResponse == "Find nearest shelter")
            {
                PrintShelterInfo();
                PrintNewPrompt();

                BotState = "prompt"; // Update State
                PossibleResponses = ResponsesDictionary["prompt"]; // Update User Response Options
            }

            // User chooses to learn about earthquakes
            else if (BotState == "disaster" && ChosenResponse == "Earthquakes")
            {
                PrintEarthquakeInfo();
                PrintNewPrompt();

                BotState = "prompt";
                PossibleResponses = ResponsesDictionary["prompt"];
            }
            // User chooses to learn more about typhoons
            else if (BotState == "disaster" && ChosenResponse == "Typhoons")
            {
                PrintTyphoonInfo();
                PrintNewPrompt();

                BotState = "prompt";    
                PossibleResponses = ResponsesDictionary["prompt"];
            }
            

            // Tell the app to refresh the Picker options
            OnPropertyChanged(nameof(PossibleResponses));

        }

        private void PrintEarthquakeInfo()
        {
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["earthquake_info0"], true));
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["earthquake_info1"], true));
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["earthquake_info2"], true));
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["earthquake_info3"], true));
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["earthquake_info4"], true));
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["earthquake_info5"], true));

        }
        private void PrintTyphoonInfo()
        {
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["typhoon_info0"], true));
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["typhoon_info1"], true));
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["typhoon_info2"], true));
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["typhoon_info3"], true));
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["typhoon_info4"], true));
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["typhoon_info5"], true));
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["typhoon_info6"], true));


        }
        private void PrintNewPrompt()
        {
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["prompt1"], true));
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["prompt2"], true));
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["prompt3"], true));
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["prompt4"], true));

        }
        private void PrintWelcome()
        {
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["welcome0"], true));
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["welcome1"], true));

        }
        private void PrintDisasterPrompt()
        {
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["disaster_info0"], true));
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["disaster_info1"], true));
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["disaster_info2"], true));
        }
        private void PrintShelterInfo()
        {
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["shelter_info0"], true));
        }
        private void PrintCommunicationInfo()
        {
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["communication_info0"], true));
        }
        private void PrintKitInfo()
        {
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["kit_info0"], true));
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["kit_info1"], true));
            MessagesToDisplay.Add(new ChatbotMessage(BotMessageDict["kit_info2"], true));

        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void LoadChatbotMessageDictionary()
        {
            BotMessageDict.Add("welcome0", "Hello! My name is Kero Kero and " +
                                           "I’m here to help with any disaster " +
                                           "preparedness questions and concerns!");
            BotMessageDict.Add("welcome1", "Here are some prompts to help you get " +
                                           "started with disaster preparedness.");
            BotMessageDict.Add("prompt1", "> Learn more ahout a specific natural disaster.");
            BotMessageDict.Add("prompt2", "> Find the nearest emergency shelter.");
            BotMessageDict.Add("prompt3", "> What should I keep in a disaster preparedness kit?");
            BotMessageDict.Add("prompt4", "> How can I communicate with loved ones in the event of " +
                                          "phone network failure?");




            BotMessageDict.Add("disaster_info0", "Which disaster would you like to know more about?.");
            BotMessageDict.Add("disaster_info1", "> Earthquakes");
            BotMessageDict.Add("disaster_info2", "> Typhoons");

            BotMessageDict.Add("typhoon_info0", "Here are some suggestions on how to stay safe during a typhoon:");
            BotMessageDict.Add("typhoon_info1", "1. Stay up-to-date on the weather forecast.");
            BotMessageDict.Add("typhoon_info2", "2. Seal gaps in windows and doors, and close shutters.");
            BotMessageDict.Add("typhoon_info3", "3. Prepare an emergency kit (see the Checklist page).");
            BotMessageDict.Add("typhoon_info4", "4. If your home has gas, turn it off at the main outlet.");
            BotMessageDict.Add("typhoon_info5", "4. Locate the nearest emergency evacuation route to your home.");
            BotMessageDict.Add("typhoon_info6", "For more info see https://www.kcif.or.jp/web/en/livingguide/emergency/.");

            BotMessageDict.Add("earthquake_info0", "Here are some suggestions on how to prepare" +
                " for an earthquake:");
            BotMessageDict.Add("earthquake_info1", "1. Locate a safe place in your home.");
            BotMessageDict.Add("earthquake_info2", "2. Prepare an emergency kit (see the Checklist page).");
            BotMessageDict.Add("earthquake_info3", "3. Secure your furniture, and install anti-shatter " +
                "film on windows and other glass items.");
            BotMessageDict.Add("earthquake_info4", "4. Locate the nearest emergency evacuation route to your home.");
            BotMessageDict.Add("earthquake_info5", "For more info see https://www.kcif.or.jp/web/en/livingguide/emergency/.");





            BotMessageDict.Add("shelter_info0", "See the Map page for shelter info and route finding.");

            BotMessageDict.Add("communication_info0", "In the event of a natural diaster, phone networks may go down. In" +
                " that case, the Disaster Emergency Message Service (171) may be activated. In cases of emergency, it acts as" +
                " a message board where individuals can leave and recieve messages. For more information on how to use the 171 service, " +
                "see https://www.kcif.or.jp/web/en/livingguide/emergency/");

            BotMessageDict.Add("kit_info0", "Here are some items you may want to keep in a disaster kit:");
            BotMessageDict.Add("kit_info1", "Water (3L per person per day), non-perishable food, cash (10 yen coins are useful for payphones)," +
                " copies of important documents (See Documents page), first-aid kit, flashlights and batteries, and some clothes.");

            BotMessageDict.Add("kit_info2", "See the Checklist page for an interactive checklist.");




        }

        public void LoadChatbotResponsesDictionary()
        {
            ObservableCollection<string> prompt_opts = new ObservableCollection<string>()
            {
                "Learn more about natural disasters",
                "Find nearest shelter",
                "Disaster kit help",
                "Emergency communication info"
            };
            ResponsesDictionary.Add("prompt",prompt_opts);

            ObservableCollection<string> disaster_opts = new ObservableCollection<string>()
            {
                "Earthquakes","Typhoons"
            };
            ResponsesDictionary.Add("disaster", disaster_opts);
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