using Foundation;
using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System;
using UIKit;
using Xamarin.Forms;
using HelloDerivedCollection.ViewModels;

namespace HelloDerivedCollection.iOS
{
  [Register ("AppDelegate")]
  public partial class ReactiveAppDelegate : UIApplicationDelegate {

    UIWindow window;
    AutoSuspendHelper suspendHelper;

    public ReactiveAppDelegate () {
      RxApp.SuspensionHost.CreateNewAppState = () => new AppBootstrapper();
    }

    public override bool FinishedLaunching (UIApplication app, NSDictionary options) {
      Forms.Init ();
      RxApp.SuspensionHost.SetupDefaultSuspendResume();

      suspendHelper = new AutoSuspendHelper(this);
      suspendHelper.FinishedLaunching(app, options);

      UserError.RegisterHandler(ue => {
          Console.WriteLine(ue.ErrorMessage);
          return Observable.Return(RecoveryOptionResult.CancelOperation);
          });

      window = new UIWindow (UIScreen.MainScreen.Bounds);
      var bootstrapper = RxApp.SuspensionHost.GetAppState<AppBootstrapper>();

      window.RootViewController = bootstrapper.CreateMainPage().CreateViewController();
      window.MakeKeyAndVisible ();

      return true;
    }

    public override void DidEnterBackground(UIApplication application) {
      suspendHelper.DidEnterBackground(application);
    }

    public override void OnActivated(UIApplication application) {
      suspendHelper.OnActivated(application);
    }
  }
}
