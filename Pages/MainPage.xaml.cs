using KeroKero.ViewModels;
using System.Text.Json;
using static KeroKero.ViewModels.ChecklistViewModel;
namespace KeroKero.Pages;

public partial class MainPage : ContentPage
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
    public MainPage()
    {
        InitializeComponent();
        BindingContext = MainViewModel.Instance;
        bool s = OS.GetOffline("off");
        if (s == true)
        {
            alert.Source= "alertoff.png";
            doc.Source = "document.png";

        }
    }

}
