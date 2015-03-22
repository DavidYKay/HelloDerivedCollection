using System;
using ReactiveUI;
using Splat;
using Xamarin.Forms;
using ReactiveUI.XamForms;
using HelloDerivedCollection.Views;

namespace HelloDerivedCollection.ViewModels
{
  public class AppBootstrapper : ReactiveObject, IScreen
  {
    // The Router holds the ViewModels for the back stack. Because it's
    // in this object, it will be serialized automatically.
    public RoutingState Router { get; protected set; }

    public AppBootstrapper()
    {
      Router = new RoutingState();
      Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));

      // Router
      Locator.CurrentMutable.Register(() => new TweetPage(), typeof(IViewFor<TweetViewModel>));

      // Initial Page
      Router.Navigate.Execute(new TweetViewModel());
    }

    public Page CreateMainPage()
    {
      return new RoutedViewHost();
    }
  }
}

