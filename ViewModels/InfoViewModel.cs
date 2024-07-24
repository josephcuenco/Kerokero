using Firebase.Auth;
using HtmlAgilityPack;
using KeroKero.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KeroKero.ViewModels
{
    public class Earthquake
    {
        public string Magnitude { get; set; }
        public DateTime Date { get; set; }
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

        private static readonly HttpClient _httpClient = new HttpClient();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
            var currentYear = DateTime.Now.Year;
            var lastYear = DateTime.Now.Year - 1;
            var earthquakes = new List<Earthquake>();

            var response = await _httpClient.GetStringAsync("https://earthquakelist.org/japan/kyoto/kyoto/");
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(response);

            var rows = htmlDoc.DocumentNode.SelectNodes("//tr[@class='tr_odd' or @class='tr_even']"); 

            //processes rows in parallel for increased performance
            Parallel.ForEach(rows, row =>
            {
                var date = DateTime.Parse(row.GetAttributeValue("data-date", ""));
                if (date.Year == currentYear || date.Year == lastYear)
                {
                    var data = new Earthquake
                    {
                        Magnitude = UpdateMagnitude(Convert.ToDouble(row.GetAttributeValue("data-mag", "0"))),
                        Date = date,
                        Distance = row.GetAttributeValue("data-dist", "")
                    };

                    //earthquake list is safely modified by multiple threads
                    lock (earthquakes)
                    {
                        earthquakes.Add(data);
                    }
                }
            });

            earthquakes = earthquakes.OrderByDescending(e => e.Date).ToList();
            return earthquakes;
        }

        public string UpdateMagnitude(double dataMag)
        {
            return dataMag.ToString("0.0");
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
