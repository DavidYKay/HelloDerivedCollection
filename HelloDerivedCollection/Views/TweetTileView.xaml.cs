using System;
using System.Collections.Generic;
using ReactiveUI;

using Xamarin.Forms;
using HelloDerivedCollection.ViewModels;

namespace HelloDerivedCollection.Views
{
  public partial class TweetTileView : ContentView, IViewFor<TweetTileViewModel>
  {
    public TweetTileView ()
    {
      InitializeComponent ();
      
      this.OneWayBind(ViewModel,  vm => vm.Model.Title, v => v.Content.Text);
    }

    public TweetTileViewModel ViewModel {
      get { return (TweetTileViewModel)GetValue(ViewModelProperty); }
      set { SetValue(ViewModelProperty, value); }
    }

    public static readonly BindableProperty ViewModelProperty =
      BindableProperty.Create<TweetTileView, TweetTileViewModel>(x => x.ViewModel, default(TweetTileViewModel), BindingMode.OneWay);

    object IViewFor.ViewModel {
      get { return ViewModel; }
      set { ViewModel = (TweetTileViewModel)value; }
    }
  }
}
