using KeroKero.ViewModels;
using Microsoft.Maui.Controls;
using System;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Maui.Storage;
using System.Collections.Generic;
using Map = Microsoft.Maui.Controls.Maps.Map;
using CsvHelper;
using System.Globalization;
using System.Reflection;
//using static Android.Icu.Text.Transliterator;
using System.Text.RegularExpressions;
using Microsoft.Maui.Storage;
using System.Net.NetworkInformation;
//using Android.Locations;

namespace KeroKero.Pages;

public partial class MapPage : ContentPage
{
    private HttpClient _httpClient;

    private Pin Origin = null;
    public MapPage()
	{
		InitializeComponent();
        _httpClient = new HttpClient();
        BindingContext = new MapViewModel();
	}

    public class CustomPin : Pin
    {
        public Color PinColor { get; set; } = Colors.Green;  // Default to standard green
    }



    public class Shelter
    {
        public string Label { get; set; }
        public double Location1 { get; set; }
        public double Location2 { get; set; }
        public string Type { get; set; }

    }

    
    public class LocationPinService
    {
        public void SaveLocationPins(string key, Pin pin)
        {
            string json = JsonSerializer.Serialize(pin);
            Preferences.Set(key, json);
        }

        public Pin GetLocationPins(string key)
        {
            string json = Preferences.Get(key, string.Empty);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            return JsonSerializer.Deserialize<Pin>(json);
        }
    }

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

    private readonly LocationPinService _locationPinService = new LocationPinService();
    private RouteService _routeService = new RouteService();

    string oInput = "";

    string dir = "";



    private async void originClicked(object sender, EventArgs e)
    {
        oInput = originInput.Text;
        if(string.IsNullOrWhiteSpace(oInput))
        {
            await DisplayAlert("Error", "Please enter a valid origin.", "OK");
            return;
        }

        string apiKey = "AIzaSyB7YTUD-ANSh4fDAqNU00QNT7YbrD1KFYw";
        string geocodeUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(oInput)}&key={apiKey}";

        var response = await _httpClient.GetAsync(geocodeUrl);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(content);

        var location = json["results"][0]["geometry"]["location"];
        double lat = (double)location["lat"];
        double lng = (double)location["lng"];

        Origin = new Pin
        {
            Label = "Origin",
            Address = oInput,
            Type = PinType.Place,
            Location = new Location(lat, lng)
        };
        map.Pins.Add(Origin);
        DisplayAlert("Finished setting origin", "", "OK");

    }
    protected override async void OnAppearing()
    {
        //base.OnAppearing();
        var geoR = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(10));
        // var location = await Geolocation.GetLocationAsync(geoR);


        
            Location mapCenter = new Location(35.01163630, 135.76802940);
        map.MoveToRegion(MapSpan.FromCenterAndRadius(mapCenter, Distance.FromMiles(10)));
        MapSpan mapSpan = new MapSpan(mapCenter, 0.1, 0.1);
        map.MoveToRegion(mapSpan);

        Pin KinugasaJunior = new Pin
        {
            Label = "Kinugasa junior high school",
            Address = "Established shelter for flood, cliff collapse, and earthquakes",
            Type = PinType.Place,
            Location = new Location(35.038108, 135.733235)
        };

        /*Pin origin = new Pin
        {
            Label = "Starting Point",
            Address = "Origin location",
            Type = PinType.SavedPin,
            Location = new Location(35.0048, 135.7690)
        };*/

        Pin Kamigamo = new Pin
        {
            Label = "Kamigamo Elementary School",
            Address = "Established shelter for flood, cliff collapse, and earthquakes",
            Type = PinType.Place,
            Location = new Location(35.055878, 135.758261)
        };

        Pin home = _locationPinService.GetLocationPins("HomePin");
        Pin work = _locationPinService.GetLocationPins("WorkPin");
        Pin school = _locationPinService.GetLocationPins("SchoolPin");
        /*DisplayAlert("Geocode Result", $"Got Pin: {retrievedPin.Label}", "OK");
        map.Pins.Add(retrievedPin);*/

        map.Pins.Add(KinugasaJunior);
        if (home != null) { map.Pins.Add(home); }
        if (work != null) { map.Pins.Add(work); }
        if (school != null) { map.Pins.Add(school); }

        
        map.Pins.Add(Kamigamo);
        
        
        Debug.WriteLine("You're here!!");
        //adding shelters from Database
        var assembly = Assembly.GetExecutingAssembly();
        using var pathCSV = await FileSystem.OpenAppPackageFileAsync("Resources/ES.csv");
        
        using var reader = new StreamReader(pathCSV);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var list = csv.GetRecords<Shelter>();
        foreach (var item in list)
        {
            //goes through each and adds to the map
            string name = item.Label;
            Pin temp = new Pin
            {
                Label = item.Label,
                Address = item.Type,
                Location = new Location(item.Location1, item.Location2)
            };

            /*CustomPin temp = new CustomPin
            {
                Label = item.Label,
                Address = item.Type,
                Location = new Location(item.Location1, item.Location2),
                PinColor = Colors.Green  // Set the custom color here
            };

            pn*/


            temp.MarkerClicked += async (s, e) =>
            {
                if (Origin?.Location == null)
                {
                    await DisplayAlert("Error", "Please set the origin first.", "OK");
                    return;
                }
                
                await DisplayRoute(Origin.Location, temp.Location);

            };
            map.Pins.Add(temp);

        }

        bool kam = false;
        bool kin = false;

        

        Kamigamo.MarkerClicked += async (s, e) =>
        {
            /*kam = true;
            kin = false;*/
            if (Origin?.Location == null)
            {
                await DisplayAlert("Error", "Please set the origin first.", "OK");
                return;
            }
            
            await DisplayRoute(Origin.Location, Kamigamo.Location);

        };
        KinugasaJunior.MarkerClicked += async (s, e) =>
        {
            /*kin = true;
            kam = false;*/
            if (Origin?.Location == null)
            {
                await DisplayAlert("Error", "Please set the origin first.", "OK");
                return;
            }
            
            await DisplayRoute(Origin.Location, KinugasaJunior.Location);

        };

        


    }
    private async Task DisplayRoute(Location originLocation, Location destinationLocation)
    {
        originInput.IsVisible = false;
        originBTN.IsVisible = false;

        string originCoords = $"{originLocation.Latitude},{originLocation.Longitude}";
        string destinationCoords = $"{destinationLocation.Latitude},{destinationLocation.Longitude}";
        string apiKey = "AIzaSyBY8pIiPG1tZkFeBU7EVjHTCGjZcaJRyLs";
        string url = $"https://maps.googleapis.com/maps/api/directions/json?origin={originCoords}&destination={destinationCoords}&key={apiKey}";

        try
        {
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                //Debug.WriteLine(responseString);

                var directions = JObject.Parse(responseString);
                var route = directions["routes"].FirstOrDefault();
                if (route != null)
                {
                    var legs = route["legs"].FirstOrDefault();
                    if (legs != null)
                    {
                        var durationText = legs["duration"]?["text"]?.ToString();
                        //Debug.WriteLine($"Duration: {durationText}");

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            durationLabel.Text = $"Duration: {durationText}";
                            durationLabel.IsVisible = true;
                            yourButton.IsVisible = true;
                        });

                        var steps = legs["steps"];

                        var polylineCoordinates = new List<Location>();

                        List<string> s = new List<string>();
                        

                        foreach (var step in steps)
                        {
                            var polyline = step["polyline"]["points"].ToString();
                            var instruct = step["html_instructions"];
                            //Debug.WriteLine($"Instruction: {durationText}");
                            var locations = DecodePolyline(polyline);
                            polylineCoordinates.AddRange(locations);
                            string t = step["html_instructions"] + " for " + step["distance"]["text"] + " (" + step["duration"]["text"] + ") \n";
                            string cleaned = Regex.Replace(t, "<.*?>", String.Empty);
                            dir += cleaned;
                            //Debug.WriteLine($"Step: {cleaned}");
                            //s.Add(cleaned);
                            
                            //s.Add(step["html_instructions"] + " for " + step["distance"]["text"] + " (" + step["duration"]["text"] + ")");

                        }
                        //direct.Text = $"Route: {dir}";



                        var mapPolyline = new Polyline
                        {
                            StrokeColor = Colors.Red,
                            StrokeWidth = 5
                        };

                        foreach (var position in polylineCoordinates)
                        {
                            mapPolyline.Geopath.Add(position);
                        }

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            map.MapElements.Clear();
                            map.MapElements.Add(mapPolyline);
                        });

                        Debug.WriteLine($"Polyline added with {mapPolyline.Geopath.Count} points.");
                    }
                }
            }
            else
            {
                Debug.WriteLine("Failed to get directions");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }

    private void YourButton_Clicked(object sender, EventArgs e)
    {
        // Handle button click event
        //DisplayAlert("Button Clicked", "You clicked the button!", "OK");


        var targetPage = new RoutePage();
        targetPage.Dir = dir; // Pass the string value to the target page
        Navigation.PushAsync(targetPage);
    }

    private void Back_Clicked(object sender, EventArgs e)
    {
        // Handle button click event
        //DisplayAlert("Button Clicked", "You clicked the button!", "OK");
        //direct.IsVisible = false;
        yourButton.Text = "See Route";
        yourButton.Clicked -= Back_Clicked;
        yourButton.Clicked += YourButton_Clicked;
    }

    private void Save_Clicked(object sender, EventArgs e)
    {
        // Handle button click event
        //DisplayAlert("Button Clicked", "You clicked the button!", "OK");
       // _routeService.SaveRoute("r", direct.Text);
        
    }


    public List<Location> DecodePolyline(string encodedPoints)
    {
        if (string.IsNullOrEmpty(encodedPoints))
            return null;

        var poly = new List<Location>();
        char[] polylineChars = encodedPoints.ToCharArray();
        int index = 0;

        int currentLat = 0;
        int currentLng = 0;
        int next5Bits;

        while (index < polylineChars.Length)
        {
            int sum = 0;
            int shifter = 0;
            do
            {
                next5Bits = polylineChars[index++] - 63;
                sum |= (next5Bits & 31) << shifter;
                shifter += 5;
            }
            while (next5Bits >= 32 && index < polylineChars.Length);

            if (index >= polylineChars.Length)
                break;

            currentLat += ((sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1));

            sum = 0;
            shifter = 0;
            do
            {
                next5Bits = polylineChars[index++] - 63;
                sum |= (next5Bits & 31) << shifter;
                shifter += 5;
            }
            while (next5Bits >= 32 && index < polylineChars.Length);

            if (index >= polylineChars.Length && next5Bits >= 32)
                break;

            currentLng += ((sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1));
            Location p = new Location(Convert.ToDouble(currentLat) / 1E5, Convert.ToDouble(currentLng) / 1E5);
            poly.Add(p);
        }

        return poly;
    }




    /* 
     private async void OnSignUpLinkClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }

     private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        // TODO: replace with actual login logic
        bool isValid = ValidateLogin(username, password);

        if (isValid)
        {
            await Shell.Current.GoToAsync("MainPage");
        }
        else
        {
            LoginStatusLabel.Text = "Invalid username or password.";

        }
    }

    private bool ValidateLogin(string username, string password)
    {
        return username == "user" && password == "password";

    }
    */
}
