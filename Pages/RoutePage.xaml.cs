using KeroKero.ViewModels;
using Microsoft.Maui.Controls;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace KeroKero.Pages
{
    public partial class RoutePage : ContentPage
    {



        public string Dir { get; set; }

        public class RouteService
        {
            public void SaveRoute(string key, string route)
            {
                string json = JsonSerializer.Serialize(route);
                Preferences.Set(key, json);
            }

            public String GetRoute(string key)
            {
                string json = Preferences.Get(key, string.Empty);
                if (string.IsNullOrEmpty(json))
                {
                    return null;
                }
                return JsonSerializer.Deserialize<String>(json);
            }
        }

        
        private RouteService _routeService = new RouteService();

        private readonly HttpClient _httpClient;
        public RoutePage()
        {
            InitializeComponent();
            BindingContext = new RouteViewModel();



        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Use the Dir property here
            // For example, display it in a Label
            directions.Text = Dir;
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            // Handle button click event
            //DisplayAlert("Button Clicked", "You clicked the button!", "OK");
            //direct.IsVisible = false;
            
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            // Handle button click event
            //DisplayAlert("Button Clicked", "You clicked the button!", "OK");
            _routeService.SaveRoute("r", directions.Text);

        }



    }
}

