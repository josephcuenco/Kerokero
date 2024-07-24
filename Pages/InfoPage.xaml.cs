using KeroKero.ViewModels;
using Microsoft.Maui.Controls;
using System;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace KeroKero.Pages;

public partial class InfoPage : ContentPage
{
    public class OfflineService
    {
        public void SaveOffline(string key, bool answer)
        {
            string json = JsonSerializer.Serialize(answer);
            Preferences.Set(key, json);
        }

        public bool GetOffline(string key)
        {
            string json = Preferences.Get(key, string.Empty);
            if (string.IsNullOrEmpty(json))
            {
                return false;
            }
            return JsonSerializer.Deserialize<bool>(json);
        }
    }
    private OfflineService OS = new OfflineService();
    public InfoPage()
    {
        InitializeComponent();
        BindingContext = new InfoViewModel();
        bool s = OS.GetOffline("off");
        if (s == true)
        {
            o.IsVisible = true;
            t.IsVisible = false;
            v.IsVisible = false;
            e.IsVisible = false;

        }
        
        Debug.WriteLine(s);
       
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((InfoViewModel)BindingContext).LoadEarthquakeDataAsync();
    }
}
