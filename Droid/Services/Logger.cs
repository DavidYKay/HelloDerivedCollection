using System;
using Xamarin.Forms;
using Android.Util;
using HelloDerivedCollection.Droid;

[assembly: Xamarin.Forms.Dependency (typeof (Logger))]
namespace HelloDerivedCollection.Droid
{
  public class Logger : ILogger
  {
    public void Debug(string s) {
      Android.Util.Log.Debug("android", s);
    }
  }
}

