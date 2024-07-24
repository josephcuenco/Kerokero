using Firebase.Auth;
using KeroKero.Models;
using KeroKero.Pages;
using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KeroKero.ViewModels
{
    internal class DocumentViewModel : INotifyPropertyChanged
    {
        private bool isHomePressed;
        private bool isInfoPressed;
        private bool isMapPressed;

        //The collection of files
        public ObservableCollection<FileModel> Files { get; set; }
        public Command upload { get; }
        public Command open { get; }

        public Command MapBtn { get; }
        public Command HomeBtn { get; }

        public Command InfoBtn { get; }


        public event PropertyChangedEventHandler? PropertyChanged;
       
        
        public DocumentViewModel()
        {
            Files = new ObservableCollection<FileModel>();
            upload = new Command(uploadFile);
            open = new Command<FileModel>(openFile);
            MapBtn = new Command(MapBtnTappedAsync);
            HomeBtn = new Command(HomeBtnTappedAsync);
            InfoBtn = new Command(InfoBtnTappedAsync);
        }

        public async Task<bool> requestFileEditing()
        {
            var requestFilePermission = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (requestFilePermission != PermissionStatus.Granted)
            {
                requestFilePermission = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }
            return requestFilePermission== await Permissions.RequestAsync<Permissions.StorageWrite>();
        }

        private async void uploadFile()
        {
            //if (await requestFileEditing()) {
                var result = await FilePicker.Default.PickAsync();
                if (result != null)
                {

                    var userFileName = await App.Current.MainPage.DisplayPromptAsync("File Name", "Enter name of file: ");
                    //Debug.WriteLine(FileSystem.Current.AppDataDirectory);
                    if (!string.IsNullOrWhiteSpace(userFileName))
                    {

                        //folder for KeroKero document uploading
                       // Debug.WriteLine(FileSystem.Current.AppDataDirectory);
                       //var kerokeroFolder = Path.Combine(FileSystem.Current.AppDataDirectory, "KeroKero");

                        //creates the folder 
                        //if (!Directory.Exists(kerokeroFolder)) { Directory.CreateDirectory(kerokeroFolder); }

                        //local path to kerokero folder
                        //var fileToKerokero = Path.Combine(kerokeroFolder, userFileName);
                        //File.Copy(result.FullPath, fileToKerokero, overwrite: true);
                        AddFile(userFileName, result.FullPath);
                    }
                //}

            }
            //else
            //{
               // Debug.WriteLine("Permission denied.");
            //}

        }

        private async void openFile(FileModel file)
        {
            if (file == null || string.IsNullOrEmpty(file.filePath))
            {
                await App.Current.MainPage.DisplayAlert("Error", "File path is invalid", "OK");
                return;
            }

            if (File.Exists(file.filePath))
            {
                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(file.filePath)
                });
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "File does not exist", "OK");
            }
        }
        public void AddFile(string filename, string filepath)
        {
            Files.Add(new FileModel { fileName = filename, filePath = filepath });
            OnPropertyChanged(nameof(Files));
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