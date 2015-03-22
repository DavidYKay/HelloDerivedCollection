using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ReactiveUI;
using HelloDerivedCollection.ViewModels;

namespace HelloDerivedCollection.Droid
{
  [Activity (Label = "HelloDerivedCollection.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
  {
    protected override void OnCreate (Bundle bundle)
    {
      base.OnCreate (bundle);

      global::Xamarin.Forms.Forms.Init (this, bundle);

      var bootstrapper = new AppBootstrapper();
      this.SetPage(bootstrapper.CreateMainPage());
    }
  }
}

