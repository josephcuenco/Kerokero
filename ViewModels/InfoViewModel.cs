using Firebase.Auth;
using HtmlAgilityPack;
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
    public class Earthquake
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Magnitude { get; set; }
        public DateTime Date { get; set; }
        public string Id { get; set; }
        public string Distance { get; set; }
    }
    internal class InfoViewModel : INotifyPropertyChanged
    {


        public Command MapBtn { get; }
        public Command HomeBtn { get; }


        private ObservableCollection<Earthquake> _earthquakeList;

        public ObservableCollection<Earthquake> EarthquakeList
        {
            get => _earthquakeList;
            set
            {
                _earthquakeList = value;
                OnPropertyChanged();
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public event PropertyChangedEventHandler? PropertyChanged;


        public InfoViewModel()
        {
            MapBtn = new Command(MapBtnTappedAsync);
            HomeBtn = new Command(HomeBtnTappedAsync);
            EarthquakeList = new ObservableCollection<Earthquake>();
            LoadEarthquakeDataAsync().ConfigureAwait(false);
        }


        public async Task LoadEarthquakeDataAsync()
        {
            var earthquakes = await GetLatestEarthquakesAsync();
            EarthquakeList.Clear();

            foreach (var earthquake in earthquakes)
            {
                EarthquakeList.Add(earthquake);
            }
        }


        private async Task<List<Earthquake>> GetLatestEarthquakesAsync()
        {
            var earthquakes = new List<Earthquake>();
            var httpClient = new HttpClient();

            var response = await httpClient.GetStringAsync("https://earthquakelist.org/japan/kyoto/kyoto/");
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);

            var rows = htmlDoc.DocumentNode.SelectNodes("//tr[@class='tr_odd' or @class='tr_even']"); // Adjust the XPath as per actual HTML structure

            foreach (var row in rows)
            {
                var data = new Earthquake
                {
                    Latitude = Convert.ToDouble(row.GetAttributeValue("data-lat", "0")),
                    Longitude = Convert.ToDouble(row.GetAttributeValue("data-lng", "0")),
                    Magnitude = Convert.ToDouble(row.GetAttributeValue("data-mag", "0")),
                    Date = DateTime.Parse(row.GetAttributeValue("data-date", "")),
                    Id = row.GetAttributeValue("data-id", ""),
                    Distance = row.GetAttributeValue("data-dist", "")
                };

                earthquakes.Add(data);
            }

            return earthquakes;
        }






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
