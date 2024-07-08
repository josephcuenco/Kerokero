﻿using KeroKero.Pages;
using Realms;
namespace KeroKero;

public partial class App : Application
{
    public static Realms.Sync.App RealmApp;
    public const string RealmAppId = "application-0-tdxdsdz";
    public App()
    {
        InitializeComponent();
        RealmApp = Realms.Sync.App.Create(RealmAppId);

        MainPage = new AppShell();

        Routing.RegisterRoute("MainPage", typeof(KeroKero.Pages.MainPage));
        Routing.RegisterRoute("LoginPage", typeof(KeroKero.Pages.LoginPage));
        Routing.RegisterRoute("SignUpPage", typeof(KeroKero.Pages.SignUpPage));
        Routing.RegisterRoute("MapPage", typeof(KeroKero.Pages.MapPage));
        Routing.RegisterRoute("InfoPage", typeof(KeroKero.Pages.InfoPage));
        Routing.RegisterRoute("SettingsPage", typeof(KeroKero.Pages.SettingsPage));
        Routing.RegisterRoute("ProfilePage", typeof(KeroKero.Pages.ProfilePage));
        Routing.RegisterRoute("PhonebookPage", typeof(KeroKero.Pages.PhonebookPage));

    }

    protected override void OnStart()
    {
        // Set the initial route to login
        Shell.Current.GoToAsync("//LoginPage");
    }

    protected override void OnSleep() { }

    protected override void OnResume() { }


}

