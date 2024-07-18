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
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace KeroKero.Pages;

public partial class ChecklistPage : ContentPage
{
   
    public ChecklistPage()
    {
        InitializeComponent();
        BindingContext = new ChecklistViewModel();
    }
}
