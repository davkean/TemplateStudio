namespace Param_RootNamespace.Android
open Android.App
open Android.Content
open Android.Content.PM
open Avalonia
open Avalonia.ReactiveUI
type Application = Android.App.Application

open Avalonia.Android
open Param_RootNamespace

[<Activity(
    Label = "Param_RootNamespace.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = (ConfigChanges.Orientation ||| ConfigChanges.ScreenSize))>]
type MainActivity() =
    inherit AvaloniaMainActivity()


[<Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)>]
type SplashActivity() =
    inherit  AvaloniaSplashActivity<App>()

    override _.CustomizeAppBuilder(builder) =
        base.CustomizeAppBuilder(builder)
            .WithInterFont()
            .UseReactiveUI()

    override x.OnResume() =
        base.OnResume()
        x.StartActivity(new Intent(Application.Context, typeof<MainActivity>))
