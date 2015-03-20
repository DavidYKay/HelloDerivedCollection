using System;
using Xamarin.Forms;

namespace HelloDerivedCollection
{
  public class Log
  {

    private static Log instance = null;
    private static readonly object padlock = new object();

    private static Log Instance {
      get {
        lock (padlock) {
          if (instance == null) {
            instance = new Log();
          }
          return instance;
        }
      }
    }

    ILogger Logger;

    private Log() {
      Logger = DependencyService.Get<ILogger>();
    }

    public static void Debug(string s) {
      Instance.Logger.Debug(s);
    }

  }
}

