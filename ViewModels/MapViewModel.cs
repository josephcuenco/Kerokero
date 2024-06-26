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
    internal class MapViewModel : INotifyPropertyChanged
    {
        //private INavigation _navigation;
        private string userEmail;
        private string userPassword;
        private HttpClient _httpClient;

        public Command HomeBtn { get; }
        public Command LoginBtn { get; }
       


       

        

        public string webApiKey = "AIzaSyCPz-5MixGymeUJlMKwkyhpZ9ynIGTxIRM";

        public event PropertyChangedEventHandler? PropertyChanged;

        public MapViewModel()
        {
            //this._navigation = navigation;
            _httpClient = new HttpClient();
            var geoR = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(10));
            // var location = await Geolocation.GetLocationAsync(geoR);

            



        }






    }
}
