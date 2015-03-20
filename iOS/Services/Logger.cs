using System;
using HelloDerivedCollection.iOS;

[assembly: Xamarin.Forms.Dependency (typeof (Logger))]
namespace HelloDerivedCollection.iOS
{
  public class Logger : ILogger
  {
    public void Debug(string s) {
      Console.WriteLine(s);
    }
  }
}

