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
using static KeroKero.Pages.ProfilePage;

namespace KeroKero.Pages;

public partial class ProfilePage : ContentPage
{

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
    public class LocationPin
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Label { get; set; }
        public string Address { get; set; }
    }

    public class LocationPinService
    {
        public void SaveLocationPins(string key, Pin pin)
        {
            string json = JsonSerializer.Serialize(pin);
            Preferences.Set(key, json);
        }
    }

    private readonly HttpClient _httpClient;
    public ProfilePage()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
        BindingContext = new ProfileViewModel();
        string r = _routeService.GetRoute("route");
        if (r != null) {
            DisplayAlert("add routes to", "","OK");
        }
        route.Text = r;


    }
    

    public string UserInput { get; set; }
    private readonly LocationPinService _locationPinService = new LocationPinService();
    private readonly RouteService _routeService = new RouteService();

    private async void OnSaveInputClicked(object sender, EventArgs e)
    {
        UserInput = homeInput.Text;
        home.Text = $"Home: {UserInput}";
        string apiKey = "AIzaSyB7YTUD-ANSh4fDAqNU00QNT7YbrD1KFYw";
        string geocodeUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(UserInput)}&key={apiKey}";

        try
        {
            var response = await _httpClient.GetAsync(geocodeUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);

            if (json["status"].ToString() == "OK")
            {
                var location = json["results"][0]["geometry"]["location"];
                double lat = (double)location["lat"];
                double lng = (double)location["lng"];

                Pin h = new Pin { 
                    Location = new Location(lat, lng), 
                    Label = "Home", 
                    Address = UserInput };
                _locationPinService.SaveLocationPins("HomePin", h);

                // Display the latitude and longitude
                //await DisplayAlert("Geocode Result", $"Latitude: {lat}, Longitude: {lng}", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Geocoding request failed.", "OK");
            }

            // Optionally, you can display an alert to show the saved input
            //DisplayAlert("Input Saved", $"You entered: {UserInput}", "OK");

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void OnSchoolInputClicked(object sender, EventArgs e)
    {
        UserInput = schoolInput.Text;
        school.Text = $"School: {UserInput}";
        string apiKey = "AIzaSyB7YTUD-ANSh4fDAqNU00QNT7YbrD1KFYw";
        string geocodeUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(UserInput)}&key={apiKey}";

        try
        {
            var response = await _httpClient.GetAsync(geocodeUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);

            if (json["status"].ToString() == "OK")
            {
                var location = json["results"][0]["geometry"]["location"];
                double lat = (double)location["lat"];
                double lng = (double)location["lng"];

                Pin s = new Pin
                {
                    Location = new Location(lat, lng),
                    Label = "School",
                    Address = UserInput
                };
                _locationPinService.SaveLocationPins("SchoolPin", s);

                // Display the latitude and longitude
                //await DisplayAlert("Geocode Result", $"Latitude: {lat}, Longitude: {lng}", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Geocoding request failed.", "OK");
            }

            // Optionally, you can display an alert to show the saved input
            //DisplayAlert("Input Saved", $"You entered: {UserInput}", "OK");

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void OnWorkInputClicked(object sender, EventArgs e)
    {
        UserInput = workInput.Text;
        work.Text = $"Work: {UserInput}";
        string apiKey = "AIzaSyB7YTUD-ANSh4fDAqNU00QNT7YbrD1KFYw";
        string geocodeUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(UserInput)}&key={apiKey}";

        try
        {
            var response = await _httpClient.GetAsync(geocodeUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);

            if (json["status"].ToString() == "OK")
            {
                var location = json["results"][0]["geometry"]["location"];
                double lat = (double)location["lat"];
                double lng = (double)location["lng"];

                Pin w = new Pin
                {
                    Location = new Location(lat, lng),
                    Label = "Work",
                    Address = UserInput
                };
                _locationPinService.SaveLocationPins("WorkPin", w);

                // Display the latitude and longitude
                //await DisplayAlert("Geocode Result", $"Latitude: {lat}, Longitude: {lng}", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Geocoding request failed.", "OK");
            }

            // Optionally, you can display an alert to show the saved input
            //DisplayAlert("Input Saved", $"You entered: {UserInput}", "OK");

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}