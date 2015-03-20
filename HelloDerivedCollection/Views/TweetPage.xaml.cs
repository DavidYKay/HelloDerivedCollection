using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ReactiveUI;

using HelloDerivedCollection.ViewModels;

namespace HelloDerivedCollection.Views
{
  public partial class TweetPage : ContentPage, IViewFor<TweetViewModel>
  {
    public TweetPage ()
    {
      InitializeComponent ();
      
      this.OneWayBind(ViewModel, vm => vm.VisibleTiles, v => v.TweetList.ItemsSource);
      this.Bind(ViewModel, vm => vm.SearchQuery, v => v.SearchEntry.Text);
    }

    public TweetViewModel ViewModel {
      get { return (TweetViewModel)GetValue(ViewModelProperty); }
      set { SetValue(ViewModelProperty, value); }
    }
    public static readonly BindableProperty ViewModelProperty =
      BindableProperty.Create<TweetPage, TweetViewModel>(x => x.ViewModel, default(TweetViewModel), BindingMode.OneWay);
    
    object IViewFor.ViewModel {
      get { return ViewModel; }
      set { ViewModel = (TweetViewModel)value; }
    }

  }
}

